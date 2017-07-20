using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLogic;
using NLog;

namespace ConsoleUI
{
    class BookNamedComparer : IComparer<Book>
    {
        public int Compare(Book lhs, Book rhs)
        {
            return string.Compare(lhs.Author, rhs.Author, StringComparison.OrdinalIgnoreCase);
        }
    }
    class Program
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            try
            {
                Book[] boeks = new Book[4];
                boeks[0] = new Book()
                {
                    Author = "Jack London",
                    Title = "Martin Eden",
                    PublishingYear = 1909,
                    Style = "Novel"
                };
                boeks[1] = new Book()
                {
                    Author = "Lev Toltoi",
                    Title = "Karenina",
                    PublishingYear = 1899,
                    Style = "Novel"
                };
                boeks[2] = new Book() {Author = "aaa", Title = "aaa", PublishingYear = 111, Style = "aaa"};
                boeks[3] = new Book() {Author = "a123123aa", Title = "223aaa", PublishingYear = 1221, Style = "koala"};
                BookListService serv = new BookListService(boeks);
                Book bbb = new Book()
                {
                    Author = "Bochilka2",
                    Title = "karamba2",
                    PublishingYear = 20127,
                    Style = "Rofel2"
                };
                serv.AddBook(bbb);
                Book bbb2 = new Book()
                {
                    Author = "Eochilka22",
                    Title = "karamba2",
                    PublishingYear = 20127,
                    Style = "Rofel2"
                };
                serv.AddBook(bbb2);
                serv.RemoveBook(bbb);
                serv.SortBooksByTag(new BookNamedComparer());
                Console.WriteLine(serv.FindBookByTag(x => x.PublishingYear == 1909));
                serv.SaveBooks(new BinaryBookStorage("K:\\books.txt"));

                BookListService service2 = new BookListService();
                service2.LoadBooks(new BinaryBookStorage("D:\\books2.txt"));
                foreach (Book book in service2.ToArray())
                {
                    Console.WriteLine(book);
                }
            }
            catch (ArgumentException e)
            {
                logger.Trace(e.TargetSite);
                logger.Trace(e.Message);
            }
            catch (Exception e)
            {
                logger.Info("Unhandled Exception");
                logger.Error(e);
            }
            Console.ReadKey();
        }
    }
}
