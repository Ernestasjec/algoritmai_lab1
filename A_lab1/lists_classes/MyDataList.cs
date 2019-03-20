using A_lab1.Abstract_classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static A_lab1.Program;

namespace A_lab1
{
    class MyDataList : DataList
    {
        //---------------------------------------------
        class MyLinkedListNode
        {
            public MyLinkedListNode nextNode { get; set; }
            public MyDataStruct data { get; set; }
            public MyLinkedListNode(MyDataStruct data)
            {
                this.data = data;
            }
        }
        //---------------------------------------------
        MyLinkedListNode headNode;
        MyLinkedListNode prevNode;
        MyLinkedListNode currentNode;
        public MyDataList() : base()
        {
            this.headNode = null;
            this.prevNode = null;
            this.currentNode = null;
        }
        public MyDataList(int n, int seed)
        {
            length = n;
            Random rand = new Random(seed);
            headNode = new MyLinkedListNode(new MyDataStruct(rand));
            currentNode = headNode;
            for (int i = 1; i < length; i++)
            {
                prevNode = currentNode;
                currentNode.nextNode = new MyLinkedListNode(new MyDataStruct(rand));
                currentNode = currentNode.nextNode;
            }
            currentNode.nextNode = null;
        }
        public override MyDataStruct First()
        {
            currentNode = headNode;
            prevNode = null;
            return currentNode.data;
        }
        public override MyDataStruct Next()
        {
            prevNode = currentNode;
            currentNode = currentNode.nextNode;
            return currentNode.data;
        }
        public override void Swap(MyDataStruct a, MyDataStruct b)
        {
            prevNode.data = a;
            currentNode.data = b;
        }

        public override void Add(MyDataStruct nauj)
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
    }
}
