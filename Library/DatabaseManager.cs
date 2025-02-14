using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using BCrypt.Net;

namespace Library
{
    public class DatabaseManager
    {
        private readonly string connectionString;

        public DatabaseManager()
        {
            try
            {
                connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"]?.ConnectionString;
                
                if (string.IsNullOrEmpty(connectionString))
                {
                    // Резервная строка подключения
                    connectionString = "Host=localhost;Port=5432;Database=library;Username=postgres;Password=your_password";
                }
            }
            catch (ConfigurationErrorsException)
            {
                // Если возникла ошибка при чтении конфигурации, используем резервную строку
                connectionString = "Host=localhost;Port=5432;Database=library;Username=postgres;Password=your_password";
            }
        }

        public User GetUser(string username, string password)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "SELECT id, username, password_hash, is_admin FROM users WHERE username = @username", conn))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            var user = new User(
                                reader.GetString(1),     // username
                                reader.GetString(2),     // password_hash
                                reader.GetBoolean(3)     // is_admin
                            )
                            {
                                Id = Guid.Parse(reader.GetString(0))
                            };
                            return user.CheckPassword(password) ? user : null;
                        }
                    }
                }
            }
            return null;
        }

        public void AddBook(Book book)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        using (var cmd = new NpgsqlCommand(
                            @"INSERT INTO books (id, title, author, year_published)
                              VALUES (@id, @title, @author, @yearPublished)", conn))
                        {
                            cmd.Parameters.AddWithValue("id", book.Id.ToString());
                            cmd.Parameters.AddWithValue("title", book.Title);
                            cmd.Parameters.AddWithValue("author", book.Author);
                            cmd.Parameters.AddWithValue("yearPublished", book.YearPublished);
                            cmd.ExecuteNonQuery();

                            // Add formats
                            foreach (var format in book.AvailableFormats)
                            {
                                using (var formatCmd = new NpgsqlCommand(
                                    "INSERT INTO book_formats (book_id, format) VALUES (@bookId, @format)", conn))
                                {
                                    formatCmd.Parameters.AddWithValue("bookId", book.Id.ToString());
                                    formatCmd.Parameters.AddWithValue("format", format.ToString());
                                    formatCmd.ExecuteNonQuery();
                                }
                            }

                            transaction.Commit();
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void UpdateBookStatus(string userId, string bookId, string status, int? rating = null, int? currentPage = null)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    @"INSERT INTO user_book_status (user_id, book_id, status, rating, current_page)
                      VALUES (@userId, @bookId, @status, @rating, @currentPage)
                      ON CONFLICT (user_id, book_id) 
                      DO UPDATE SET status = @status, 
                                   rating = COALESCE(@rating, user_book_status.rating),
                                   current_page = COALESCE(@currentPage, user_book_status.current_page)", conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);
                    cmd.Parameters.AddWithValue("bookId", bookId);
                    cmd.Parameters.AddWithValue("status", status);
                    cmd.Parameters.AddWithValue("rating", rating.HasValue ? (object)rating.Value : DBNull.Value);
                    cmd.Parameters.AddWithValue("currentPage", currentPage.HasValue ? (object)currentPage.Value : DBNull.Value);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public Dictionary<string, double> GetBookStatistics(string bookId)
        {
            var stats = new Dictionary<string, double>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    @"SELECT 
                        COALESCE(AVG(NULLIF(rating, 0)), 0) as average_rating,
                        COUNT(CASE WHEN status = 'completed' THEN 1 END) as completed_count,
                        COUNT(CASE WHEN status = 'reading' THEN 1 END) as reading_count,
                        COUNT(CASE WHEN status = 'want_to_read' THEN 1 END) as want_to_read_count
                      FROM user_book_status 
                      WHERE book_id = @bookId", conn))
                {
                    cmd.Parameters.AddWithValue("bookId", bookId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            stats["average_rating"] = reader.GetDouble(0);
                            stats["completed_count"] = reader.GetInt64(1);
                            stats["reading_count"] = reader.GetInt64(2);
                            stats["want_to_read_count"] = reader.GetInt64(3);
                        }
                    }
                }
            }
            return stats;
        }

        public List<Book> GetAllBooks()
        {
            var books = new List<Book>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    @"SELECT b.id, b.title, b.author, b.year_published, f.format 
                      FROM books b 
                      LEFT JOIN book_formats f ON b.id = f.book_id", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        string currentBookId = null;
                        Book currentBook = null;

                        while (reader.Read())
                        {
                            string bookId = reader.GetString(0);

                            if (currentBookId != bookId)
                            {
                                if (currentBook != null)
                                {
                                    books.Add(currentBook);
                                }

                                currentBookId = bookId;
                                BookFormat format = !reader.IsDBNull(4) ? 
                                    (BookFormat)Enum.Parse(typeof(BookFormat), reader.GetString(4)) : 
                                    BookFormat.Hardcover;

                                currentBook = new Book(
                                    reader.GetString(1), // title
                                    reader.GetString(2), // author
                                    reader.GetInt32(3),  // year_published
                                    format
                                );
                                currentBook.Id = Guid.Parse(bookId);
                            }
                            else if (!reader.IsDBNull(4))
                            {
                                currentBook.AvailableFormats.Add(
                                    (BookFormat)Enum.Parse(typeof(BookFormat), reader.GetString(4))
                                );
                            }
                        }

                        if (currentBook != null)
                        {
                            books.Add(currentBook);
                        }
                    }
                }
            }
            return books;
        }

        public bool RemoveBook(Guid bookId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var transaction = conn.BeginTransaction())
                {
                    try
                    {
                        // Сначала удаляем форматы книги
                        using (var cmdFormats = new NpgsqlCommand(
                            "DELETE FROM book_formats WHERE book_id = @bookId", conn))
                        {
                            cmdFormats.Parameters.AddWithValue("bookId", bookId.ToString());
                            cmdFormats.ExecuteNonQuery();
                        }

                        // Затем удаляем саму книгу
                        using (var cmdBook = new NpgsqlCommand(
                            "DELETE FROM books WHERE id = @bookId", conn))
                        {
                            cmdBook.Parameters.AddWithValue("bookId", bookId.ToString());
                            int rowsAffected = cmdBook.ExecuteNonQuery();
                            
                            transaction.Commit();
                            return rowsAffected > 0;
                        }
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

        public void UpdateUserPassword(string username, string newPassword)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "UPDATE users SET password_hash = @passwordHash WHERE username = @username", conn))
                {
                    string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword, BCrypt.Net.BCrypt.GenerateSalt());
                    cmd.Parameters.AddWithValue("passwordHash", hashedPassword);
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public bool IsBookReadByUser(string userId, string bookId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    @"SELECT COUNT(*) FROM user_book_status 
                      WHERE user_id = @userId AND book_id = @bookId 
                      AND status = 'completed'", conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);
                    cmd.Parameters.AddWithValue("bookId", bookId);
                    return (long)cmd.ExecuteScalar() > 0;
                }
            }
        }

        public List<Book> FindBookByTitleAndAuthor(string title, string author)
        {
            var books = new List<Book>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    @"SELECT b.id, b.title, b.author, b.year_published, f.format 
                      FROM books b 
                      LEFT JOIN book_formats f ON b.id = f.book_id
                      WHERE LOWER(b.title) = LOWER(@title) 
                      AND LOWER(b.author) = LOWER(@author)", conn))
                {
                    cmd.Parameters.AddWithValue("title", title.ToLower());
                    cmd.Parameters.AddWithValue("author", author.ToLower());

                    using (var reader = cmd.ExecuteReader())
                    {
                        string currentBookId = null;
                        Book currentBook = null;

                        while (reader.Read())
                        {
                            string bookId = reader.GetString(0);
                            
                            if (currentBookId != bookId)
                            {
                                if (currentBook != null)
                                {
                                    books.Add(currentBook);
                                }

                                currentBookId = bookId;
                                BookFormat format = (BookFormat)Enum.Parse(
                                    typeof(BookFormat), 
                                    reader.GetString(4)
                                );
                                
                                currentBook = new Book(
                                    reader.GetString(1), // title
                                    reader.GetString(2), // author
                                    reader.GetInt32(3),  // year_published
                                    format
                                );
                                currentBook.Id = Guid.Parse(bookId);
                            }
                            else if (!reader.IsDBNull(4))
                            {
                                currentBook.AvailableFormats.Add(
                                    (BookFormat)Enum.Parse(typeof(BookFormat), reader.GetString(4))
                                );
                            }
                        }

                        if (currentBook != null)
                        {
                            books.Add(currentBook);
                        }
                    }
                }
            }
            return books;
        }

        public void AddFeedback(Feedback feedback)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    @"INSERT INTO feedback (id, user_id, message, status, created_at, updated_at)
                      VALUES (@id, @userId, @message, @status, @createdAt, @updatedAt)", conn))
                {
                    cmd.Parameters.AddWithValue("id", feedback.Id.ToString());
                    cmd.Parameters.AddWithValue("userId", feedback.UserId.ToString());
                    cmd.Parameters.AddWithValue("message", feedback.Message);
                    cmd.Parameters.AddWithValue("status", feedback.Status);
                    cmd.Parameters.AddWithValue("createdAt", feedback.CreatedAt);
                    cmd.Parameters.AddWithValue("updatedAt", feedback.UpdatedAt);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public List<Feedback> GetUserFeedback(string userId)
        {
            var feedbacks = new List<Feedback>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    "SELECT id, message, status, admin_response, created_at, updated_at FROM feedback WHERE user_id = @userId", conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var feedback = new Feedback(Guid.Parse(userId), reader.GetString(1))
                            {
                                Id = Guid.Parse(reader.GetString(0)),
                                Status = reader.GetString(2),
                                AdminResponse = reader.IsDBNull(3) ? null : reader.GetString(3),
                                CreatedAt = reader.GetDateTime(4),
                                UpdatedAt = reader.GetDateTime(5)
                            };
                            feedbacks.Add(feedback);
                        }
                    }
                }
            }
            return feedbacks;
        }

        public List<Feedback> GetAllFeedback()
        {
            var feedbacks = new List<Feedback>();
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    @"SELECT f.id, f.user_id, f.message, f.status, f.admin_response, f.created_at, f.updated_at, u.username
                      FROM feedback f
                      JOIN users u ON f.user_id = u.id", conn))
                {
                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var feedback = new Feedback(Guid.Parse(reader.GetString(1)), reader.GetString(2))
                            {
                                Id = Guid.Parse(reader.GetString(0)),
                                Status = reader.GetString(3),
                                AdminResponse = reader.IsDBNull(4) ? null : reader.GetString(4),
                                CreatedAt = reader.GetDateTime(5),
                                UpdatedAt = reader.GetDateTime(6)
                            };
                            feedbacks.Add(feedback);
                        }
                    }
                }
            }
            return feedbacks;
        }

        public void UpdateFeedbackStatus(string feedbackId, string status, string adminResponse = null)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    @"UPDATE feedback 
                      SET status = @status, admin_response = @adminResponse, updated_at = @updatedAt
                      WHERE id = @id", conn))
                {
                    cmd.Parameters.AddWithValue("id", feedbackId);
                    cmd.Parameters.AddWithValue("status", status);
                    cmd.Parameters.AddWithValue("adminResponse", adminResponse ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("updatedAt", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateBookReadingProgress(string userId, string bookId, int currentPage)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    @"INSERT INTO user_book_status (user_id, book_id, status, current_page)
                      VALUES (@userId, @bookId, 'reading', @currentPage)
                      ON CONFLICT (user_id, book_id) 
                      DO UPDATE SET status = 'reading', current_page = @currentPage", conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);
                    cmd.Parameters.AddWithValue("bookId", bookId);
                    cmd.Parameters.AddWithValue("currentPage", currentPage);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public int? GetBookReadingProgress(string userId, string bookId)
        {
            using (var conn = new NpgsqlConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new NpgsqlCommand(
                    @"SELECT current_page 
                      FROM user_book_status 
                      WHERE user_id = @userId 
                      AND book_id = @bookId 
                      AND status = 'reading'", conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);
                    cmd.Parameters.AddWithValue("bookId", bookId);
                    
                    var result = cmd.ExecuteScalar();
                    return result != DBNull.Value ? (int?)result : null;
                }
            }
        }
    }
}