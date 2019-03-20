using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_lab1.Abstract_classes
{
    public abstract class DataArray
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract MyDataStruct this[int index] { get; set; }
        public abstract void Swap(int j, MyDataStruct a, MyDataStruct b);
        public abstract void Add(MyDataStruct nauj);
        public abstract MyDataStruct First();
        public abstract void RemoveFirst();
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
                Console.WriteLine(" {0:F5} ", this[i]);
            Console.WriteLine();
        }
    }

    public abstract class DataIntArray
    {
        protected int length;
        public int Length { get { return length; } }
        protected int longestDigit;
        public int LongestDigit { get { return longestDigit; } }
        public abstract int this[int index] { get; set; }
        public abstract void Swap(int j, int a, int b);
        public abstract void Add(int nauj);
        public abstract int First();
        public abstract void RemoveFirst();
        public abstract void Clear();
        public void Print(int n)
        {
            for (int i = 0; i < n; i++)
                Console.WriteLine(" {0} ", this[i]);
            Console.WriteLine();
        }
    }
}
