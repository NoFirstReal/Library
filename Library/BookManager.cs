using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Word = Microsoft.Office.Interop.Word;
using System.Drawing;
using ZXing;
using ZXing.QrCode;

namespace Library
{
    public class BookManager
    {
        private readonly DatabaseManager _dbManager;
        private List<Book> books;

        public BookManager()
        {
            _dbManager = new DatabaseManager();
            books = new List<Book>();
        }

        public bool AddBook(string title, string author, int yearPublished, BookFormat format)
        {
            var newBook = new Book(title, author, yearPublished, format);
            try
            {
                _dbManager.AddBook(newBook);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void ImportBooksFromCsv(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                var parts = line.Split(';');
                if (parts.Length >= 3 && int.TryParse(parts[2], out int year))
                {
                    string title = parts[0].Trim();
                    string author = parts[1].Trim();

                    // Проверяем, существует ли уже такая книга
                    var existingBooks = _dbManager.FindBookByTitleAndAuthor(title, author);
                    if (!existingBooks.Any())
                    {
                        AddBook(title, author, year, BookFormat.PDF);
                    }
                }
            }
        }

        public bool RemoveBook(Guid id)
        {
            try
            {
                return _dbManager.RemoveBook(id);
            }
            catch (Exception)
            {
                return false;
            }
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
            return _dbManager.GetAllBooks();
        }

        public void ExportBooksToCsv(string filePath)
        {
            var books = _dbManager.GetAllBooks();
            var lines = books.Select(b => $"{b.Title};{b.Author};{b.YearPublished}");
            File.WriteAllLines(filePath, lines);
        }

        public void ExportBooksToTxt(string filePath)
        {
            var books = _dbManager.GetAllBooks();
            var lines = books.Select(b => b.ToString());
            File.WriteAllLines(filePath, lines);
        }

        public bool ConvertBookFormat(Book book, BookFormat sourceFormat, BookFormat targetFormat)
        {
            if (!book.AvailableFormats.Contains(sourceFormat))
                return false;

            book.AvailableFormats.Add(targetFormat);
            return true;
        }

        public void ExportToPdf(string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            {
                iTextSharp.text.Document document = new iTextSharp.text.Document();
                PdfWriter writer = PdfWriter.GetInstance(document, fs);
                document.Open();

                // Add title
                document.Add(new iTextSharp.text.Paragraph("Library Book List"));
                document.Add(new iTextSharp.text.Paragraph("-------------------"));

                // Get books from database
                var books = _dbManager.GetAllBooks();

                // Add books
                foreach (var book in books)
                {
                    document.Add(new iTextSharp.text.Paragraph($"{book.Title} by {book.Author} ({book.YearPublished})"));
                    document.Add(new iTextSharp.text.Paragraph($"Formats: {string.Join(", ", book.AvailableFormats)}"));
                    document.Add(new iTextSharp.text.Paragraph("-------------------"));
                }

                document.Close();
            }
        }

        public void ExportToWord(string filePath)
        {
            Word.Application wordApp = new Word.Application();
            Word.Document doc = wordApp.Documents.Add();
            
            try
            {
                // Add title
                Word.Range range = doc.Range();
                range.Text = "Library Book List\n\n";
                range.Font.Bold = 1;
                range.InsertParagraphAfter();

                // Get books from database
                var books = _dbManager.GetAllBooks();

                // Add books
                foreach (var book in books)
                {
                    range = doc.Range(doc.Content.End - 1);
                    range.Text = $"{book.Title} by {book.Author} ({book.YearPublished})\n";
                    range.InsertParagraphAfter();
                    
                    range = doc.Range(doc.Content.End - 1);
                    range.Text = $"Formats: {string.Join(", ", book.AvailableFormats)}\n\n";
                    range.InsertParagraphAfter();
                }

                doc.SaveAs2(filePath);
            }
            finally
            {
                doc.Close();
                wordApp.Quit();
            }
        }

        public Bitmap GenerateQRCode()
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = 300,
                    Width = 300
                }
            };

            return writer.Write("https://github.com/NoFirstReal/Library");
        }

        public Dictionary<string, double> GetBookStatistics(string bookId)
        {
            return _dbManager.GetBookStatistics(bookId);
        }

        public void UpdateBookStatus(string userId, string bookId, string status, int? rating = null, int? currentPage = null)
        {
            _dbManager.UpdateBookStatus(userId, bookId, status, rating, currentPage);
        }

        public bool IsBookReadByUser(string userId, string bookId)
        {
            return _dbManager.IsBookReadByUser(userId, bookId);
        }

        public int? GetBookReadingProgress(string userId, string bookId)
        {
            return _dbManager.GetBookReadingProgress(userId, bookId);
        }
    }
}
