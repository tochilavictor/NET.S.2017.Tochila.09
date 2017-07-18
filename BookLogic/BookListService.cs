using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLogic
{
    abstract class BookListStorage
    {
        public abstract void Save(params Book[] bookarray);
        public abstract Book[] Load();
    }
    interface IRepositoryFactory
    {
        BookListStorage Create();
    }
    interface IBookComparer
    {
        int CompareTo(Book lhs, Book rhs);
    }
    interface IBookValider
    {
        bool IsValid(Book arg);
    }
    class BookListService
    {
        private Book[] books;
        private int count;
        private IRepositoryFactory repFactory;
        public BookListService(IRepositoryFactory rp,params Book[] bookarray)
        {
            //valid
            if (bookarray.Length != 0)
            {
                books = new Book[bookarray.Length];
                count = bookarray.Length;
                Array.Copy(bookarray, books, bookarray.Length);
            }
            repFactory = rp;
        }

        public void SaveBooks()
        {
            BookListStorage storage = repFactory.Create();
            storage.Save(books);
        }
        public void LoadBooks()
        {
            BookListStorage storage = repFactory.Create();
            books = storage.Load();
        }
        public void AddBook(Book other)
        {
            foreach (Book book in books)
            {
                if(book.Equals(other)) throw new ArgumentException("this book already exists");
            }
            Add(other);
        }
        public void RemoveBook(Book other)
        {
            for (int i = 0; i < books.Length; i++)
            {
                if (books[i].Equals(other))
                {
                    Remove(i);
                    return;
                }
            }
            throw new ArgumentException("can't find this book");
        }
        public Book FindBookByTag(IBookValider valider)
        {
            for (int i = 0; i < books.Length; i++)
            {
                if (valider.IsValid(books[i])) return books[i];
            }
            return null;
        }
        public void SortBooksByTag(IBookComparer comparer)
        {
            for (int i = 0; i < books.Length - 1; i++)
            {
                for (int j = 0; j < books.Length; j++)
                {
                    if(comparer.CompareTo(books[i],books[j])==1)
                    Swap(ref books[i],ref books[j]);
                }
            }
        }

        private void Add(Book b)
        {
            if (count < books.Length)
            {
                books[count++] = b;
                return;
            }
            Book[] newbooks = new Book[books.Length*2];
            Array.Copy(books,newbooks,books.Length);
            newbooks[count++] = b;
            books = newbooks;
        }
        private void Remove(int index)
        {
            for (int i = index; i < books.Length-1; i++)
            {
                books[i] = books[i + 1];
            }
            count--;
        }
        static void Swap(ref Book a, ref Book b)
        {
            Book tmp = a;
            a = b;
            b = tmp;
        }
    }
}
