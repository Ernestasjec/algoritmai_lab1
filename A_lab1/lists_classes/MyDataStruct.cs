using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A_lab1
{
    public class MyDataStruct
    {
        public int Int { get; set; }
        public float Float { get; set; }

        public MyDataStruct(int integer, float floating)
        {
            Int = integer;
            Float = floating;
            //Float = NextFloat(random);
        }

        public MyDataStruct( Random random)
        {
            Int = random.Next();
            Float = (float) random.NextDouble();
            //Float = NextFloat(random);
        }

        static float NextFloat(Random random)
        {
            var result = (random.NextDouble()
                          * (Single.MaxValue - (double)Single.MinValue))
                          + Single.MinValue;
            return (float)result;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", Int, Float);
        }

        public static bool operator >=(MyDataStruct lhs, MyDataStruct rhs)
        {
            if (lhs.Int > rhs.Int)
            {
                return true;
            } else if (lhs.Int == rhs.Int && lhs.Int >= rhs.Int)
            {
                return true;
            }
            return false;
        }

        public static bool operator <=(MyDataStruct lhs, MyDataStruct rhs)
        {
            if (lhs.Int < rhs.Int)
            {
                return true;
            }
            else if (lhs.Int == rhs.Int && lhs.Int <= rhs.Int)
            {
                return true;
            }
            return false;
        }
    }
}
