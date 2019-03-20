using A_lab1.Abstract_classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_lab1
{
    class MyFileArray : DataArray
    {
        private string filedDest;
        public MyFileArray(int seed) : base()
        {
            Random rand = new Random(seed);
            string file = String.Format(@"Array" + rand.Next().ToString() + ".dat");
            while (File.Exists(file))
            {
                file = String.Format(@"Array" + rand.Next().ToString() + ".dat" );
            }
            filedDest = file;
            using (fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite)) ;
        }
        public MyFileArray(string filename, int n, int seed)
        {
            filedDest = filename;
            MyDataStruct data;
            length = n;
            Random rand = new Random(seed);
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
               FileMode.Create)))
                {
                    for (int j = 0; j < length; j++)
                    {
                        data = new MyDataStruct(rand);
                        writer.Write(data.Int);
                        writer.Write(data.Float);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }


        }

        public FileStream fs { get; set; }

        public override void Add(MyDataStruct nauj)
        {
            //if (File.Exists(filedDest) && length == 0) File.Delete(filedDest);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filedDest, FileMode.Append)))
                {
                     writer.Write(nauj.Int);
                     writer.Write(nauj.Float);
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
            length++;
        }
        public override MyDataStruct this[int index]
        {
            get
            {
                using (fs = new FileStream(filedDest, FileMode.Open, FileAccess.ReadWrite))
                {
                    Byte[] data = new Byte[8];
                    fs.Seek(8 * index, SeekOrigin.Begin);
                    fs.Read(data, 0, 8);
                    MyDataStruct result = new MyDataStruct(BitConverter.ToInt32(data, 0), BitConverter.ToSingle(data, 4));
                    return result;
                }
            }
            set
            {
                using (fs = new FileStream(filedDest, FileMode.Open, FileAccess.ReadWrite))
                {
                    byte[] data = new byte[8];
                    BitConverter.GetBytes(value.Int).CopyTo(data, 0);
                    BitConverter.GetBytes(value.Float).CopyTo(data, 4);
                    fs.Seek(8 * index, SeekOrigin.Begin);
                    fs.Write(data, 0, 8);
                }
            }

        }
        public override void Swap(int j, MyDataStruct a, MyDataStruct b)

        {
            throw new NotImplementedException();

        }

        public override MyDataStruct First()
        {
            using (fs = new FileStream(filedDest, FileMode.Open, FileAccess.ReadWrite))
            {
                Byte[] data = new Byte[8];
                fs.Seek(0, SeekOrigin.Begin);
                fs.Read(data, 0, 8);
                return new MyDataStruct(BitConverter.ToInt32(data, 0), BitConverter.ToSingle(data, 4));
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
    }
}
