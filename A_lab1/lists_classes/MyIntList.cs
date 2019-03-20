using A_lab1.Abstract_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_lab1
{
    class MyIntList : DataIntList
    {
        //---------------------------------------------
        class MyLinkedListNode
        {
            public MyLinkedListNode nextNode { get; set; }
            public int data { get; set; }
            public MyLinkedListNode(int data)
            {
                this.data = data;
            }
        }
        //---------------------------------------------
        MyLinkedListNode headNode;
        MyLinkedListNode prevNode;
        MyLinkedListNode currentNode;
        public MyIntList() : base()
        {
            this.headNode = null;
            this.prevNode = null;
            this.currentNode = null;
        }
        public MyIntList(int n, int seed)
        {
            length = n;
            Random rand = new Random(seed);
            headNode = new MyLinkedListNode(rand.Next());
            currentNode = headNode;
            int MyData;
            for (int i = 1; i < length; i++)
            {
                prevNode = currentNode;
                currentNode.nextNode = new MyLinkedListNode(MyData = rand.Next());
                currentNode = currentNode.nextNode;
                if (longestDigit < MyData.ToString().Length)
                {
                    longestDigit = MyData.ToString().Length;
                }
            }
            currentNode.nextNode = null;
        }
        public override int First()
        {
            currentNode = headNode;
            prevNode = null;
            return currentNode.data;
        }
        public override int Next()
        {
            prevNode = currentNode;
            currentNode = currentNode.nextNode;
            return currentNode.data;
        }
        public override void Swap(int a, int b)
        {
            prevNode.data = a;
            currentNode.data = b;
        }

        public override void Add(int nauj)
        {
            if (length > 0)
            {
                prevNode = currentNode;
                currentNode.nextNode = new MyLinkedListNode(nauj);
                currentNode = currentNode.nextNode;
                length++;
            }
            else
            {
                headNode = new MyLinkedListNode(nauj);
                currentNode = headNode;
                length++;
            }

        }


        public override void RemoveFirst()
        {
            currentNode = headNode;
            headNode = currentNode.nextNode;
            currentNode = null;
            currentNode = headNode;
            length--;
        }

        public override void Clear()
        {
            this.headNode = null;
            this.prevNode = null;
            this.currentNode = null;
            length = 0;
        }
    }
}
