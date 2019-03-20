using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_lab1.Abstract_classes
{
    public abstract class DataList
    {
        protected int length;
        public int Length { get { return length; } }
        public abstract MyDataStruct First();
        public abstract MyDataStruct Next();
        public abstract void Swap(MyDataStruct a, MyDataStruct b);
        public abstract void Add(MyDataStruct nauj);
        public abstract void RemoveFirst();
        public void Print(int n)
        {
            Console.WriteLine(" {0:F5} ", First());
            for (int i = 1; i < n; i++)
                Console.WriteLine(" {0} ", Next());
            Console.WriteLine();
        }
    }

    public abstract class DataIntList
    {
        protected int length;
        public int Length { get { return length; } }
        protected int longestDigit;
        public int LongestDigit { get { return length; } }
        public abstract int First();
        public abstract int Next();
        public abstract void Swap(int a, int b);
        public abstract void Add(int nauj);
        public abstract void RemoveFirst();
        public abstract void Clear();
        public void Print(int n)
        {
            Console.WriteLine(" {0} ", First());
            for (int i = 1; i < n; i++)
                Console.WriteLine(" {0} ", Next());
            Console.WriteLine();
        }
    }
}
