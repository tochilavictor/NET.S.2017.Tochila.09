using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLogic
{
    //ne uspel normal'no dodelat' i protestirovat' poka
    public class Book:IComparable,IComparable<Book>,IEquatable<Book>
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public int PublishingYear { get; set; }
        public string Style { get; set; }
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
    }
}
