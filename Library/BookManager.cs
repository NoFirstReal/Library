using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Library
{
    public class BookManager
    {
        private List<Book> books;

        public BookManager()
        {
            books = new List<Book>();
        }

        public void AddBook(string title, string author, int yearPublished, BookFormat format)
        {
            var book = new Book(title, author, yearPublished, format);
            books.Add(book);
        }

        public void ImportBooksFromCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length >= 3 && int.TryParse(parts[2], out int year))
                {
                    // По умолчанию импортируем как PDF
                    AddBook(parts[0], parts[1], year, BookFormat.PDF);
                }
            }
        }

        public bool RemoveBook(Guid id)
        {
            var book = books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                books.Remove(book);
                return true;
            }
            return false;
        }

        public List<Book> FindBookByName(string title)
        {
            return books.Where(b => b.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public List<Book> FindBookByAuthor(string author)
        {
            return books.Where(b => b.Author.IndexOf(author, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
        }

        public List<Book> GetAllBooks()
        {
            return books;
        }

        public void ExportBooksToCsv(string filePath)
        {
            var lines = books.Select(book =>
                $"{book.Title};{book.Author};{book.YearPublished};{string.Join(",", book.AvailableFormats)}");
            File.WriteAllLines(filePath, lines);
        }

        public void ExportBooksToTxt(string filePath)
        {
            var lines = books.Select(book =>
                $"{book.Title} - {book.Author} ({book.YearPublished}) [Форматы: {string.Join(", ", book.AvailableFormats)}]");
            File.WriteAllLines(filePath, lines);
        }

        public bool ConvertBookFormat(Book book, BookFormat sourceFormat, BookFormat targetFormat)
        {
            if (!book.AvailableFormats.Contains(sourceFormat))
                return false;

            book.AvailableFormats.Add(targetFormat);
            return true;
        }
    }
}
