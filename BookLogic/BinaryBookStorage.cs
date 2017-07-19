using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLogic
{
    public class BinaryBookStorage : IBookListStorage
    {
        private string path;
        public BinaryBookStorage(string path)
        {
            this.path = path;
        }
        public List<Book> Load()
        {
            FileStream fread = new FileStream(path, FileMode.Open);
            BinaryReader br = new BinaryReader(fread);
            List<Book> boloboks = new List<Book>();
            while (fread.Position < fread.Length)
            {
                string author = br.ReadString();
                string title = br.ReadString();
                string style = br.ReadString();
                int publYear = br.ReadInt32();
                Book tempBook = new Book() { Author = author, PublishingYear = publYear, Style = style, Title = title };
                boloboks.Add(tempBook);
            }
            br.Close();
            return boloboks;
        }

        public void Save(IEnumerable<Book> bookarray)
        {
            FileStream fwrite = new FileStream(path, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fwrite);
            foreach (Book book in bookarray)
            {
                bw.Write(book.Author);
                bw.Write(book.Title);
                bw.Write(book.Style);
                bw.Write(book.PublishingYear);
            }
            bw.Close();
        }
    }
}
