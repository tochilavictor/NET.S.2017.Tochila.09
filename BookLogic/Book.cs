using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace BookLogic
{
    [Serializable]
    public class Book:IComparable,IComparable<Book>,IEquatable<Book>//, ISerializable
    {
        private string title;
        private string author;
        private string style;
        private int year;

        public string Title
        {
            get { return title; }
            set
            {
                if(string.IsNullOrEmpty(value)) throw new ArgumentException();
                title = value;
            }
        }
        public string Author
        {
            get { return author; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException();
                author = value;
            }
        }
        public string Style
        {
            get { return style; }
            set
            {
                if (string.IsNullOrEmpty(value)) throw new ArgumentException();
                style = value;
            }
        }
        public int PublishingYear
        {
            get { return year; }
            set
            {
                if (year<0 && year>DateTime.Now.Year) throw new ArgumentException();
                year = value;
            }
        }
        int IComparable.CompareTo(object other)
        {
            if (ReferenceEquals(null, other)) return 1;
            if (this.GetType() != other.GetType()) return 1;
            Book otherbook = (Book) other;
            return CompareTo(otherbook);
        }
        public int CompareTo(Book other)
        {
            if (ReferenceEquals(null, other)) return 1;
            return this.PublishingYear.CompareTo(other.PublishingYear);
        }
        public bool Equals(Book other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (this.Title != other.Title || this.Author != other.Author ||
                this.PublishingYear != other.PublishingYear) return false;
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (this.GetType() != obj.GetType()) return false;
            return Equals((Book) obj);
        }
        public override string ToString()
        {
            return Title.ToString() + "____Author: " + Author.ToString() +
           "____Genre :"+ Style.ToString() + "____Publishing year:" + PublishingYear.ToString();
        }

        public static bool operator ==(Book lhs,Book rhs)
        {
            if (ReferenceEquals(lhs, null)) return false;
            if (ReferenceEquals(lhs, rhs)) return true;
            return lhs.Equals(rhs);
        }
        public static bool operator !=(Book lhs, Book rhs)
        {
            return !object.Equals(lhs,rhs);
        }
    }
}
