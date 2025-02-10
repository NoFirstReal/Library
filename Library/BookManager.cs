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
        private List<Book> books;

        public BookManager()
        {
            books = new List<Book>();
        }

        public bool AddBook(string title, string author, int yearPublished, BookFormat format)
        {
            var newBook = new Book(title, author, yearPublished, format);
            if (books.Any(b => b.Equals(newBook)))
            {
                return false;
            }
            books.Add(newBook);
            return true;
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

                // Add books
                foreach (var book in books)
                {
                    document.Add(new iTextSharp.text.Paragraph($"{book.Title} by {book.Author} ({book.YearPublished})"));
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

                // Add books
                foreach (var book in books)
                {
                    range = doc.Range(doc.Content.End - 1);
                    range.Text = $"{book.Title} by {book.Author} ({book.YearPublished})\n\n";
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
    }
}
