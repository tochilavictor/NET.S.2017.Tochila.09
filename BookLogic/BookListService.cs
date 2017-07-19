using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLogic
{
    public interface IBookListStorage
    {
        void Save(IEnumerable<Book> books);
        List<Book> Load();
    }
    public class BookListService
    {
        private List<Book> books;
        public BookListService(params Book[] bookarray)
        {
            //valid
            books = new List<Book>();
            if (bookarray.Length != 0)
            {
                foreach (Book book in bookarray)
                {
                    books.Add(book);
                }
            }
        }

        public void SaveBooks(IBookListStorage storage)
        {
            storage.Save(books);
        }
        public void LoadBooks(IBookListStorage storage)
        {
            books = storage.Load();
        }
        public void AddBook(Book other)
        {
            foreach (Book book in books)
            {
                if (book.Equals(other)) throw new ArgumentException("this book already exists");
            }
            books.Add(other);
        }
        public void RemoveBook(Book other)
        {
            Book tmp = null;
            foreach (Book book in books)
            {
                if (book.Equals(other))
                {
                    tmp = book;
                    break;
                }
            }
            if (tmp == null) throw new ArgumentException("can't find this book");
            books.Remove(tmp);

        }
        public Book FindBookByTag(Predicate<Book> predicate)
        {
            foreach (Book book in books)
            {
                if (predicate(book)) return book;
            }
            return null;
        }
        public void SortBooksByTag(IComparer<Book> comparer)
        {
            books.Sort(comparer);
        }
        public Book[] ToArray()
        {
            List<Book> res = new List<Book>();
            foreach (var book in books)
            {
                if (book != null) res.Add(book);
            }
            return res.ToArray();
        }
    }
}
