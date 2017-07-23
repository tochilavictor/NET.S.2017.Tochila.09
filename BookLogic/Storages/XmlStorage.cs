using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace BookLogic
{
    public class XmlStorage:IBookListStorage
    {
        private string filename;
        public XmlStorage(string filename)
        {
            this.filename = filename;
        }
        public void Save(IEnumerable<Book> books)
        {
            XmlTextWriter xmltextwr= new XmlTextWriter(filename,Encoding.UTF8)
            {
                Formatting = Formatting.Indented,
                Indentation = 2
            };
            xmltextwr.WriteStartDocument();
            xmltextwr.WriteStartElement("Books");
            foreach (Book book in books)
            {
                xmltextwr.WriteStartElement(nameof(Book));
                xmltextwr.WriteStartElement(nameof(book.Author));
                xmltextwr.WriteString((book.Author));
                xmltextwr.WriteEndElement();
                xmltextwr.WriteStartElement(nameof(book.Title));
                xmltextwr.WriteString((book.Title));
                xmltextwr.WriteEndElement();
                xmltextwr.WriteStartElement(nameof(book.Style));
                xmltextwr.WriteString((book.Style));
                xmltextwr.WriteEndElement();
                xmltextwr.WriteStartElement(nameof(book.PublishingYear));
                xmltextwr.WriteString(book.PublishingYear.ToString());
                xmltextwr.WriteEndElement();
                xmltextwr.WriteEndElement();
            }      
            xmltextwr.WriteEndElement();
            xmltextwr.WriteEndDocument();
            xmltextwr.Flush();
            xmltextwr.Close();
        }

        public List<Book> Load()
        {
            List<Book> res = new List<Book>();
            XmlTextReader xmltextrd = new XmlTextReader(filename);
            string[] bookargs = new string[4];
            int i = 0;
            while (xmltextrd.Read())
            {
                if (xmltextrd.NodeType!=XmlNodeType.Text) continue;
                bookargs[i] = xmltextrd.Value;
                if (i < bookargs.Length - 1) i++;
                else
                {
                    Book temp = new Book()
                    {
                        Author = bookargs[0],
                        Title = bookargs[1],
                        Style = bookargs[2],
                        PublishingYear = int.Parse(bookargs[3])
                    };
                    res.Add(temp);
                    i = 0;
                }

            }
            return res;
        }
    }
}
