using A_lab1.Abstract_classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static A_lab1.Program;

namespace A_lab1
{
    class MyFileList : DataList
    {
        private string fileDest;
        int nextNode;
        int prevNode;
        int currentNode;
        public MyFileList() : base()
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            this.nextNode = -1;
            this.prevNode = -1;
            this.currentNode = -1;
            Random rand = new Random(seed);
            string file = String.Format(@"List" + rand.Next().ToString() + ".dat");
            while (File.Exists(file))
            {
                file = String.Format(@"List" + rand.Next().ToString() + ".dat");
            }
            fileDest = file;
            using (fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite)) ;
        }
        public MyFileList(string filename, int n, int seed)
        {
            fileDest = filename;
            length = n;
            Random rand = new Random(seed);
            MyDataStruct MyData;
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
               FileMode.Create)))
                {
                    writer.Write(4);
                    for (int j = 0; j < length; j++)
                    {
                        MyData = new MyDataStruct(rand);
                        writer.Write(MyData.Int);
                        writer.Write(MyData.Float);
                        writer.Write((j + 1) * 12 + 4);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
        public FileStream fs { get; set; }

        public override MyDataStruct First()
        {
            using (fs = new FileStream(fileDest, FileMode.Open, FileAccess.ReadWrite))
            {
                Byte[] data = new Byte[12];
                fs.Seek(0, SeekOrigin.Begin);
                fs.Read(data, 0, 4);
                currentNode = BitConverter.ToInt32(data, 0);
                prevNode = -1;
                fs.Seek(currentNode, SeekOrigin.Begin);
                fs.Read(data, 0, 12);
                MyDataStruct result = new MyDataStruct(BitConverter.ToInt32(data, 0), BitConverter.ToSingle(data, 4));
                nextNode = BitConverter.ToInt32(data, 8);
                return result;
            }
        }
        public override MyDataStruct Next()
        {
            using (fs = new FileStream(fileDest, FileMode.Open, FileAccess.ReadWrite))
            {
                Byte[] data = new Byte[12];
                fs.Seek(nextNode, SeekOrigin.Begin);
                fs.Read(data, 0, 12);
                prevNode = currentNode;
                currentNode = nextNode;
                MyDataStruct result = new MyDataStruct(BitConverter.ToInt32(data, 0), BitConverter.ToSingle(data, 4));
                nextNode = BitConverter.ToInt32(data, 8);
                return result;
            }
        }

        public override void Add(MyDataStruct nauj)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileDest,
               FileMode.Append)))
            {
                if (length > 0)
                {
                    writer.Write(nauj.Int);
                    writer.Write(nauj.Float);
                    writer.Write((Length + 1) * 12 + 4);
                    length++;
                }
                else
                {
                    writer.Write(4);
                    writer.Write(nauj.Int);
                    writer.Write(nauj.Float);
                    writer.Write((Length + 1) * 12 + 4);
                    length++;
                }
            }

        }

        public override void Swap(MyDataStruct a, MyDataStruct b)
        {
            throw new NotImplementedException();

        }

        public override void RemoveFirst()
        {
            this.First();
            using (fs = new FileStream(fileDest, FileMode.Open, FileAccess.ReadWrite))
            {
                Byte[] data = new Byte[4];
                fs.Seek(currentNode + 8, SeekOrigin.Begin);
                fs.Read(data, 0, 4);
                fs.Seek(0, SeekOrigin.Begin);
                fs.Write(data, 0, 4);
            }
            length--;
        }
    }
}
