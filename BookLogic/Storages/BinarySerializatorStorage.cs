using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace BookLogic.Storages
{
    public class BinarySerializatorStorage: IBookListStorage
    {
        private string path;
        public BinarySerializatorStorage(string path)
        {
            this.path = path;
        }

        public void Save(IEnumerable<Book> books)
        {
            IFormatter formatter = new BinaryFormatter();
            using(FileStream fs = File.Open(path,FileMode.Open))
            {
                formatter.Serialize(fs,books);
            }
        }

        public List<Book> Load()
        {
            IFormatter formatter = new BinaryFormatter();
            List<Book> res;
            using (FileStream fs = File.Open(path, FileMode.Open))
            {
                IEnumerable<Book> books = (IEnumerable<Book>) formatter.Deserialize(fs);
                res = new List<Book>(books);
            }
            return res;
        }
    }
}
