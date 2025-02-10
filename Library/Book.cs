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

        public override bool Equals(object obj)
        {
            if (obj is Book other)
            {
                return Title.Equals(other.Title, StringComparison.OrdinalIgnoreCase) &&
                       Author.Equals(other.Author, StringComparison.OrdinalIgnoreCase) &&
                       YearPublished == other.YearPublished;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return (Title.ToLowerInvariant() + 
                    Author.ToLowerInvariant() + 
                    YearPublished.ToString()).GetHashCode();
        }

        public override string ToString()
        {
            string formats = string.Join(", ", AvailableFormats);
            return $"{Title} - {Author} ({YearPublished}) [{formats}]";
        }
    }
}