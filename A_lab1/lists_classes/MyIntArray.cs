using A_lab1.Abstract_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_lab1.lists_classes
{
    class MyIntArray : DataIntArray
    {
        int[] data;
        public MyIntArray(int n, int seed)

        {
            data = new int[n];
            length = n;
            Random rand = new Random(seed);
            for (int i = 0; i < length; i++)
            {
                data[i] = rand.Next();

                if (longestDigit < data[i].ToString().Length)
                {
                    longestDigit = data[i].ToString().Length;
                }
            }


        }

        public MyIntArray(int size)

        {
            data = new int[size];
            length = 0;
        }

        public override void Add(int nauj)
        {
            data[length] = nauj;
            length++;
        }
        public override int this[int index]
        {
            get { return data[index]; }
            set { data[index] = value; }

        }
        public override void Swap(int j, int a, int b)

        {
            data[j - 1] = a;
            data[j] = b;

        }

        public override int First()
        {
            return data[0];
        }

        public override void RemoveFirst()
        {
            data = data.Skip(1).ToArray();
            length--;
        }

        public override void Clear()
        {
            length = 0;
        }
    }
}
