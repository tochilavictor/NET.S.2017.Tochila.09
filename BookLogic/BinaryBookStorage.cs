using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLogic
{
    class BinaryBookStorage: BookListStorage, IRepositoryFactory
    {
        private FileStream fread;
        private FileStream fwrite;
        private BinaryReader br;
        private BinaryWriter bw;
        private const int MaxSize = 100;
        public BookListStorage Create()
        {
            fwrite = new FileStream("D:\\1.txt", FileMode.Create);
            bw = new BinaryWriter(fwrite);
            fread = new FileStream("D:\\1.txt", FileMode.Open);
            br = new BinaryReader(fread);
            return this;
        }
        public override Book[] Load()
        {
            Book[] tempRes = new Book[MaxSize];
            int i = 0;
            while (fread.Position < fread.Length && i<MaxSize)
            {
                string author = br.ReadString();
                string title= br.ReadString();
                string style= br.ReadString();
                int publYear = br.ReadInt32();
                Book tempBook= new Book() {Author = author,PublishingYear = publYear,Style = style,Title = title};
                tempRes[i] = tempBook;
                i++;
            }
            Book[] res = new Book[i];
            Array.Copy(tempRes,res,res.Length);
            return res;
        }

        public override void Save(params Book[] bookarray)
        {
            int i = 0;
            while (i < bookarray.Length && bookarray.Length < MaxSize)
            {
                bw.Write(bookarray[i].Author);
                bw.Write(bookarray[i].Title);
                bw.Write(bookarray[i].Style);
                bw.Write(bookarray[i].PublishingYear);
                i++;
            }
        }
    }
}
