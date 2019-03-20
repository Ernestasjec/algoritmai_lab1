using A_lab1.Abstract_classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_lab1
{
    class HashNodeArray
    {
        int length;
        public int Length { get { return length; } }
        private string filedDest;
        public HashNodeArray(int seed)
        {
            Random rand = new Random(seed);
            string file = String.Format(@"Array" + rand.Next().ToString() + ".dat");
            //kol failas egzistuoja to ji keicia kitu
            while (File.Exists(file))
            {
                file = String.Format(@"Array" + rand.Next().ToString() + ".dat");
            }
            filedDest = file;
            using (fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite)) ;
        }
        public HashNodeArray(string fileName, int n)
        {
            filedDest = fileName;
            length = n;
            if (File.Exists(fileName)) File.Delete(fileName);

            using (fs = new FileStream(fileName, FileMode.Create, FileAccess.ReadWrite)) ;

        }

        public FileStream fs { get; set; }

        public HashNode<string, string> this[int index]
        {
            get
            {
                using (fs = new FileStream(filedDest, FileMode.Open, FileAccess.ReadWrite))
                {
                    Byte[] data = new Byte[20];
                    fs.Seek(20 * index, SeekOrigin.Begin);
                    fs.Read(data, 0, 20);

                    HashNode<string, string> result = new HashNode<string, string>(Encoding.ASCII.GetString(data, 0, 10), Encoding.ASCII.GetString(data, 10, 10));
                    return result;
                }
            }
            set
            {
                using (fs = new FileStream(filedDest, FileMode.Open, FileAccess.ReadWrite))
                {
                    byte[] data = new byte[20];
                    Encoding.ASCII.GetBytes(value.key).CopyTo(data, 0);
                    Encoding.ASCII.GetBytes(value.value).CopyTo(data, 10);
                    fs.Seek(20 * index, SeekOrigin.Begin);
                    fs.Write(data, 0, 20);
                }
            }

        }
    }
}
