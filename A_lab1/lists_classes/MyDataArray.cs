using A_lab1.Abstract_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static A_lab1.Program;

namespace A_lab1
{
    class MyDataArray : DataArray
    {
        MyDataStruct[] data;
        public MyDataArray(int n, int seed)

        {
            data = new MyDataStruct[n];
            length = n;
            Random rand = new Random(seed);
            for(int i = 0; i < length-1; i++)
            {
                data[i] = new MyDataStruct(rand);

            }
            data[length - 1] = new MyDataStruct(rand);
            data[length - 1].Int = data[length - 2].Int;

        }

        public MyDataArray(int size) : base()

        {
            data = new MyDataStruct[size];
            length = 0;
        }

        public override void Add(MyDataStruct nauj)
        {
            data[length] = nauj;
            length++;
        }
        public override MyDataStruct this [int index]
        {
            get { return data[index]; }
            set { data[index] = value;  }

        }

        public override MyDataStruct First()
        {
            return data[0];
        }

        public override void RemoveFirst()
        {
            data = data.Skip(1).ToArray();
            length--;
        }
        public override void Swap(int j, MyDataStruct a, MyDataStruct b)
        {
            data[j - 1] = a;
            data[j] = b;

        }
    }
}
