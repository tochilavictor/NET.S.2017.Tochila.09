using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NLog;

namespace BookLogic
{
    public interface IBookListStorage
    {
        void Save(IEnumerable<Book> books);
        List<Book> Load();
    }
    public class BookListService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private List<Book> books;
        public BookListService(params Book[] bookarray)
        {
            try
            {
                    if (bookarray == null)
                    throw new ArgumentNullException();
            }
            catch(ArgumentNullException ex)
            {
                logger.Error(ex.Message);
                logger.Debug(ex.StackTrace);
            }
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
            try
            {
                storage.Save(books);
            }
            catch (System.IO.DriveNotFoundException ex)
            {
                logger.Info(ex.GetType());
                logger.Error(ex.Message);
                logger.Trace(ex.StackTrace);
                throw new ArgumentException("invalid path", ex);
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                logger.Info(ex.GetType());
                logger.Error(ex.Message);
                logger.Trace(ex.StackTrace);
                throw new ArgumentException("invalid path", ex);
            }
        }
        public void LoadBooks(IBookListStorage storage)
        {
            try
            {
                books = storage.Load();
            }
            catch (System.IO.DriveNotFoundException ex)
            {
                logger.Info(ex.GetType());
                logger.Error(ex.Message);
                logger.Trace(ex.StackTrace);
                throw new ArgumentException("invalid path", ex);
            }
            catch (System.IO.DirectoryNotFoundException ex)
            {
                logger.Info(ex.GetType());
                logger.Error(ex.Message);
                logger.Trace(ex.StackTrace);
                throw new ArgumentException("invalid path", ex);
            }
            catch (System.IO.FileNotFoundException ex)
            {
                logger.Info(ex.GetType());
                logger.Error(ex.Message);
                logger.Trace(ex.StackTrace);
                throw new ArgumentException("invalid path", ex);
            }
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
        static void ValidateArray(Book[] array)
        {
            if (array == null)
                throw new ArgumentNullException();
            if (array.Length == 0)
                throw new ArgumentException("Empty array");
        }
    }
}
