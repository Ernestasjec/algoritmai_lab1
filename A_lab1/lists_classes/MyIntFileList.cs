using A_lab1.Abstract_classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_lab1.lists_classes
{
    class MyIntFileList : DataIntList
    {
        private string fileDest;
        int nextNode;
        int prevNode;
        int currentNode;
        public MyIntFileList()
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            this.nextNode = -1;
            this.prevNode = -1;
            this.currentNode = -1;
            Random rand = new Random(seed);
            string file = String.Format(@"IntList" + rand.Next().ToString() + ".dat");
            while (File.Exists(file))
            {
                file = String.Format(@"IntList" + rand.Next().ToString() + ".dat");
            }
            fileDest = file;
            using (fs = new FileStream(file, FileMode.Create, FileAccess.ReadWrite)) ;
        }
        public MyIntFileList(string filename, int n, int seed)
        {
            fileDest = filename;
            length = n;
            Random rand = new Random(seed);
            int MyData;
            if (File.Exists(filename)) File.Delete(filename);
            try
            {
                using (BinaryWriter writer = new BinaryWriter(File.Open(filename,
               FileMode.Create)))
                {
                    writer.Write(4);
                    for (int j = 0; j < length; j++)
                    {
                        MyData = rand.Next();
                        writer.Write(MyData);
                        writer.Write((j + 1) * 8 + 4);
                        if (longestDigit < MyData.ToString().Length)
                        {
                            longestDigit = MyData.ToString().Length;
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

        public override int First()
        {
            using (fs = new FileStream(fileDest, FileMode.Open, FileAccess.ReadWrite))
            {
                Byte[] data = new Byte[8];
                fs.Seek(0, SeekOrigin.Begin);
                fs.Read(data, 0, 4);
                currentNode = BitConverter.ToInt32(data, 0);
                prevNode = -1;
                fs.Seek(currentNode, SeekOrigin.Begin);
                fs.Read(data, 0, 8);
                int result = BitConverter.ToInt32(data, 0);
                nextNode = BitConverter.ToInt32(data, 4);
                return result;
            }
        }
        public override int Next()
        {
            using (fs = new FileStream(fileDest, FileMode.Open, FileAccess.ReadWrite))
            {
                Byte[] data = new Byte[8];
                fs.Seek(nextNode, SeekOrigin.Begin);
                fs.Read(data, 0, 8);
                prevNode = currentNode;
                currentNode = nextNode;
                int result = BitConverter.ToInt32(data, 0);
                nextNode = BitConverter.ToInt32(data, 4);
                return result;
            }
        }
        public override void Swap(int a, int b)
        {
            throw new NotImplementedException();

        }

        public override void Add(int nauj)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(fileDest,
               FileMode.Append)))
            {
                if (length > 0)
                {
                    writer.Write(nauj);
                    writer.Write((Length + 1) * 8 + 4);
                    length++;
                }
                else
                {
                    writer.Write(4);
                    writer.Write(nauj);
                    writer.Write((Length + 1) * 8 + 4);
                    length++;
                }
            }

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

        public override void Clear()
        {
            this.nextNode = -1;
            this.prevNode = -1;
            this.currentNode = -1;
            FileStream fileStream = File.Open(fileDest, FileMode.Open);
            fileStream.SetLength(0);
            fileStream.Close();
            length = 0;
        }
    }
}
