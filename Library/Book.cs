using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public int YearPublished { get; set; }
        public HashSet<BookFormat> AvailableFormats { get; set; }

        public Book(string title, string author, int yearPublished, BookFormat initialFormat)
        {
            Id = Guid.NewGuid();
            Title = title;
            Author = author;
            YearPublished = yearPublished;
            AvailableFormats = new HashSet<BookFormat> { initialFormat };
        }

        public override string ToString()
        {
            string formats = string.Join(", ", AvailableFormats);
            return $"{Title} - {Author} ({YearPublished}) [{formats}]";
        }
    }
}