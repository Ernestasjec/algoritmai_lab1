using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_lab1
{
    class HashNode<K,V>
    {
        public V value;
        public K key;

        //Constructor of hashnode  
        public HashNode(K key, V value)
        {
            this.value = value;
            this.key = key;
        }

        public override bool Equals(object obj)
        {
            HashNode<string, string> node = obj as HashNode<string, string>;
            string key = this.key as string;
            string val = this.value as string;
            return (node.key == key && node.value == val);
        }
    }
}
