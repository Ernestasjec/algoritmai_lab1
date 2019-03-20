using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_lab1
{
    class HashMap<K, V> where V : class
    {
        //hash element array 
        HashNode<K, V>[] table;
        int capacity;
        //current size 
        int size;
        //dummy node 
        HashNode<int, V> dummy;

        public HashMap(int cap)
        {
            //Initial capacity of hash array 
            capacity = cap;
            size = 0;
            table = new HashNode<K, V>[capacity];

            //Initialise all elements of array as NULL 
            for (int i = 0; i < capacity; i++)
                table[i] = null;

        }
        // This implements hash function to find index 
        // for a key 
        int HashCode(K key)
        {
            int h = key.GetHashCode();
            return Math.Abs(h % capacity);
        }

        //Function to add key value pair 
        public void InsertNode(K key, V value)
        {
            HashNode<K, V> temp = new HashNode<K, V>(key, value);

            // Apply hash function to find index for given key 
            int hashIndex = FindPosition(key);

            //if new node to be inserted increase the current size 
            if (table[hashIndex] == null)
                size++;
            table[hashIndex] = temp;
        }

        private int FindPosition(K key)
        {
            int index = HashCode(key);
            int indexO = index;
            int i = 0;
            for (int j = 0; j < capacity; j++)
            {
                if (table[index] == null || table[index].key.Equals(key))
                {
                    return index;
                }
                i++;
                index = (indexO + i * hashCode2(key)) % capacity;
            }

            return -1;
        }

        private int hashCode2(K key)
        {
            return 7 - (Math.Abs(key.GetHashCode()) % 7);
        }

        //Function to search the value for a given key

        public V Get(K key)
        {
            // Apply hash function to find index for given key 
            int positionIndex = FindPosition(key);
            //finding the node with given key    
            if (positionIndex >= 0 && table[positionIndex] != null)
            {
                return table[positionIndex].value;
            }

            //If not found return null 
            return null;
        }

        //Return current size  
        public int SizeofMap()
        {
            return size;
        }

        //Return true if size is 0 
        public bool IsEmpty()
        {
            return size == 0;
        }

        //Function to display the stored key value pairs 
        public void Display()
        {
            for (int i = 0; i < capacity; i++)
            {
                if (table[i] != null)
                    Console.WriteLine("hashcode = " + HashCode(table[i].key) + " key = " + table[i].key.ToString() + " value = " + table[i].value);
            }
        }
    }
}
