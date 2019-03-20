using A_lab1.Abstract_classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_lab1.lists_classes
{
    class MyIntFileArray : DataIntArray
    {
        private string filedDest;
        public MyIntFileArray(int seed)
        {
            Random rand = new Random(seed);
            string file = String.Format(@"IntArray" + rand.Next().ToString() + ".dat");
            while (File.Exists(file))
            {
                file = String.Format(@"IntArray" + rand.Next().ToString() + ".dat");
            }
            filedDest = file;
            using (fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite)) ;
        }
        public MyIntFileArray(string filename, int n, int seed)

        {
            filedDest = filename;
            int data;
            length = n;
            Random rand = new Random(seed);
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename, FileMode.Create)))
                {
                    for (int j = 0; j < length; j++)
                    {
                        data = rand.Next();
                        writer.Write(data);
                        if (longestDigit < data.ToString().Length)
                        {
                            longestDigit = data.ToString().Length;
                        }
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }

        public FileStream fs { get; set; }

        public override void Add(int nauj)
        {
            //if (File.Exists(filedDest) && length == 0) File.Delete(filedDest);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filedDest, FileMode.Append)))
                {
                    writer.Write(nauj);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            length++;
        }
        public override int this[int index]
        {
            get
            {
                using (fs = new FileStream(filedDest, FileMode.Open, FileAccess.ReadWrite))
                {
                    Byte[] data = new Byte[4];
                    fs.Seek(4 * index, SeekOrigin.Begin);
                    fs.Read(data, 0, 4);
                    int result = BitConverter.ToInt32(data, 0);
                    return result;
                }
            }
            set
            {
                using (fs = new FileStream(filedDest, FileMode.Open, FileAccess.ReadWrite))
                {
                    byte[] data = new byte[4];
                    BitConverter.GetBytes(value).CopyTo(data, 0);
                    fs.Seek(4 * index, SeekOrigin.Begin);
                    fs.Write(data, 0, 4);
                }
            }

        }
        public override void Swap(int j, int a, int b)
        {
            throw new NotImplementedException();

        }

        public override int First()
        {
            using (fs = new FileStream(filedDest, FileMode.Open, FileAccess.ReadWrite))
            {
                Byte[] data = new Byte[4];
                fs.Seek(0, SeekOrigin.Begin);
                fs.Read(data, 0, 4);
                return BitConverter.ToInt32(data, 0);
            }
        }

        public override void RemoveFirst()
        {
            for (int i = 0; i < length - 1; i++)
            {
                this[i] = this[i + 1];
            }
            length--;
            if (length == 0)
            {
                File.Delete(filedDest);
            }
        }

        public override void Clear()
        {
            FileStream fileStream = File.Open(filedDest, FileMode.Open);
            fileStream.SetLength(0);
            fileStream.Close();
            length = 0;
        }
    }
}
