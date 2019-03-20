using A_lab1.Abstract_classes;
using A_lab1.lists_classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace A_lab1
{
    class Program
    {
        static Random random;
        static void Main(string[] args)
        {
            int seed = (int)DateTime.Now.Ticks & 0x0000FFFF;
            random = new Random(seed);
            List<int> kiekiai = new List<int>{ 1000, 2000, 4000, 8000, 16000, 32000, 64000 };
            int n = 10;
            //Test_OP_Radix_Sort(seed, n);
            Test_File_Radix_Sort(seed, n);
            //Test_Array_List(seed, n);
            //Test_File_Array_List(seed, n);
            //Test_Op_hashMap(n);
            //Test_Op_hashMap_V2(n);
            //Test_File_hashMap_V2(n);
            Console.WriteLine("Maišos lentelės su dviguba hešavimo funkcija vidutinio paieškos laiko palyginimas išorinėje ir vidinėje atmintyse");
            Console.WriteLine("{0, -10} {1, -20}  {2, -20}", "N", "Operatyvine", "Išornė");
            foreach (var nn in kiekiai)
            {
                //Test_Array_List_Comparison(seed, nn);
                //Test_Array_List_Comparison_File(seed, nn);
                //Test_OP_Radix_Sort_Comparison(seed, nn);
                //Test_File_Radix_Sort_Comparison(seed, nn);
                //Test_hashMap_Comparison(nn);
            }
            DeleteFiles();
            Console.Write("Spauskite bet kurį klavišą...");
            Console.ReadKey();
        }

        //-----------------------------------------------------
        //Masyvo rikiavimas
        private static DataArray MergeSort(DataArray unsorted)                      // kaina | kartai
        {
            if (unsorted.Length <= 1)
                return unsorted;                                                   //    c1  |  1
            DataArray left;
            DataArray right;
            if (unsorted.GetType() == typeof(MyDataArray))
            {
                left = new MyDataArray(unsorted.Length / 2);                       //    c2  |  1
                right = new MyDataArray(unsorted.Length / 2 + 1);                  //    c2  |  1
            }
            else
            {
                left = new MyFileArray(unsorted.Length / 2);                       //    c2  |  1
                right = new MyFileArray(unsorted.Length / 2 + 1);                  //    c2  |  1
            }

            int middle = unsorted.Length / 2;                                      //    c3  | 1
            for (int i = 0; i < middle; i++)  //Dividing the unsorted list
            {
                left.Add(unsorted[i]);
            }
            for (int i = middle; i < unsorted.Length; i++)
            {
                right.Add(unsorted[i]);
            }

            left = MergeSort(left);
            right = MergeSort(right);
            return Merge(left, right);
        }

        private static DataArray Merge(DataArray left, DataArray right)
        {
            DataArray result;
            if (right.GetType() == typeof(MyDataArray))
            {
                result = new MyDataArray(left.Length + right.Length);
            }
            else
            {
                result = new MyFileArray(left.Length + right.Length);
            }
            while (left.Length > 0 || right.Length > 0)
            {
                if (left.Length > 0 && right.Length > 0)
                {
                    if (left.First() <= right.First())  //lyginam pirmus elementus
                    {
                        result.Add(left.First()); //idedam pirma
                        left.RemoveFirst();      //pasalinam is kairio pirma
                    }
                    else
                    {
                        result.Add(right.First());
                        right.RemoveFirst();
                    }
                }
                else if (left.Length > 0)
                {
                    result.Add(left.First());
                    left.RemoveFirst();
                }
                else if (right.Length > 0)
                {
                    result.Add(right.First());

                    right.RemoveFirst();
                }
            }
            return result;
        }
        //--------------------------------------------------------
        //-----------------------------------------------------
        //susietojo sąrašo rikiavimas

        private static DataList MergeSort(DataList unsorted)     
        {
            if (unsorted.Length <= 1)
                return unsorted;

            DataList left;
            DataList right;

            if (unsorted.GetType() == typeof(MyDataList))
            {
                left = new MyDataList();
                right = new MyDataList();
            }
            else
            {
                left = new MyFileList();
                right = new MyFileList();
            }
            MyDataStruct data;

            int middle = unsorted.Length / 2;
            data = unsorted.First();
            for (int i = 0; i < middle; i++)  //dalinam sarasa
            {

                left.Add(data);
                data = unsorted.Next();
            }
            for (int i = middle; i < unsorted.Length; i++)
            {
                right.Add(data);
                if (unsorted.Length != left.Length + right.Length) data = unsorted.Next();
            }

            left = MergeSort(left);
            right = MergeSort(right);
            return Merge(left, right);
        }

        private static DataList Merge(DataList left, DataList right)
        {
            DataList result;

            if (left.GetType() == typeof(MyDataList))
            {
                result = new MyDataList();
            }
            else
            {
                result = new MyFileList();
            }

            while (left.Length > 0 || right.Length > 0)
            {
                if (left.Length > 0 && right.Length > 0)
                {
                    if (left.First() <= right.First())  
                    {
                        result.Add(left.First());
                        left.RemoveFirst();      
                    }
                    else
                    {
                        result.Add(right.First());
                        right.RemoveFirst();
                    }
                }
                else if (left.Length > 0)
                {
                    result.Add(left.First());
                    left.RemoveFirst();
                }
                else if (right.Length > 0)
                {
                    result.Add(right.First());

                    right.RemoveFirst();
                }
            }
            return result;
        }
        //--------------------------------------------------------

        public static DataIntArray RadixSort(DataIntArray data)
        {
            DataIntArray[] buckets = new DataIntArray[10];
            if (data.GetType() == typeof(MyIntArray))
            {
                for (int i = 0; i < buckets.Length; i++)
                {
                    buckets[i] = new MyIntArray(data.Length);
                }
            }
            else
            {
                for (int i = 0; i < buckets.Length; i++)
                {
                    buckets[i] = new MyIntFileArray(data.Length);
                }
            }
            for (int i = 0; i < data.LongestDigit; i++)
            {
                for (int j = 0; j < data.Length; j++)
                {
                    int digit = (int)((data[j] % Math.Pow(10, i + 1)) / Math.Pow(10, i));
                    buckets[digit].Add(data[j]);
                }

                int index = 0;
                for (int k = 0; k < 10; k++)
                {
                    DataIntArray bucket = buckets[k];
                    for (int l = 0; l < bucket.Length; l++)
                    {
                        data[index++] = bucket[l];
                    }
                    buckets[k].Clear();
                }
            }
            return data;
        }

        public static DataIntList RadixSort(DataIntList data)
        {
            DataIntList[] buckets = new DataIntList[10];
            if (data.GetType() == typeof(MyIntArray))
            {
                for (int i = 0; i < buckets.Length; i++)
                {
                    buckets[i] = new MyIntList();
                }
            }
            else
            {
                for (int i = 0; i < buckets.Length; i++)
                {
                    buckets[i] = new MyIntFileList();
                }
            }
            for (int i = 0; i < data.LongestDigit; i++)
            {
                int first = data.First();
                int digit = (int)((first % Math.Pow(10, i + 1)) / Math.Pow(10, i));
                buckets[digit].Add(first);
                for (int j = 1; j < data.Length; j++)
                {
                    int currnet = data.Next();
                    digit = (int)((currnet % Math.Pow(10, i + 1)) / Math.Pow(10, i));

                    buckets[digit].Add(currnet);
                }

                data.Clear();
                for (int k = 0; k < 10; k++)
                {
                    DataIntList bucket = buckets[k];
                    for (int l = 0; l < bucket.Length; l++)
                    {
                        if (l == 0)
                        {
                            data.Add(bucket.First());
                        }
                        else
                        {
                            data.Add(bucket.Next());
                        }
                    }
                    buckets[k].Clear();
                }
            }
            return data;
        }

        public static void Test_Array_List_Comparison(int seed, int n)
        {
            Stopwatch watch = new Stopwatch();
            MyDataArray myarray = new MyDataArray(n, seed);
            watch.Start();
            MyDataArray MySortedArray = (MyDataArray)MergeSort(myarray);
            watch.Stop();
            var arrayTime = watch.Elapsed;
            MyDataList mylist = new MyDataList(n, seed);
            watch.Start();
            MyDataList MySortedList = (MyDataList)MergeSort(mylist);
            watch.Stop();
            var listTime = watch.Elapsed;
            Console.WriteLine("{0, -10} {1, -20}  {2, -20}", n, arrayTime, listTime);
        }

        public static void Test_Array_List_Comparison_File (int seed, int n)
        {
            Stopwatch watch = new Stopwatch();
            MyFileArray myarray = new MyFileArray("testasArray.dat", n, seed);
            watch.Start();
            MyFileArray MySortedArray = (MyFileArray)MergeSort(myarray);
            watch.Stop();
            var arrayTime = watch.Elapsed;
            MyFileList mylist = new MyFileList("testasList.dat", n, seed);
            watch.Start();
            MyFileList MySortedList = (MyFileList)MergeSort(mylist);
            watch.Stop();
            var listTime = watch.Elapsed;
            Console.WriteLine("{0, -10} {1, -20}  {2, -20}", n, arrayTime, listTime);
        }

        public static void Test_Array_List(int seed, int n)
        {
            Console.WriteLine("elementų kiekis: " + n);
            Console.WriteLine("\n Operatyviojoje atmintyje suliejimo rykiavimas\n");
            Stopwatch watch = new Stopwatch();
            MyDataArray myarray = new MyDataArray(n, seed);
            MyDataArray MySortedArray;
            MyDataList MySortedList;
            Console.WriteLine("\n masyvas \n");
            myarray.Print(n);
            watch.Start();
            MySortedArray = (MyDataArray) MergeSort(myarray);
            watch.Stop();
            var arrayTime = watch.Elapsed;
            Console.WriteLine("\n isrykiuotas \n");
            MySortedArray.Print(n);
            MyDataList mylist = new MyDataList(n, seed);
            Console.WriteLine("\n sąrašas \n");
            mylist.Print(n);
            watch.Start();
            MySortedList = (MyDataList)MergeSort(mylist);
            watch.Stop();
            var listTime = watch.Elapsed;
            Console.WriteLine("\n isrykiuotas \n");
            MySortedList.Print(n);
            Console.WriteLine("masyvo vykdymo laikas: " + arrayTime + " sąrašo vykdymo laikas: " + listTime);
        }

        public static void Test_File_Array_List(int seed, int n)
        {
            Console.WriteLine("\n Išorinėje atmintyje suliejimo rykiavimas\n");
            string filename;
            Stopwatch watch = new Stopwatch();
            filename = @"mydataarray.dat";
            MyFileArray sortedFileArray;
            MyFileArray myfilearray = new MyFileArray(filename, n, seed);
            Console.WriteLine("\n Masyvas \n");
            myfilearray.Print(n);
            watch.Start();
            sortedFileArray = (MyFileArray)MergeSort(myfilearray);
            watch.Stop();
            var arrayTime = watch.Elapsed;
            Console.WriteLine("\n Surykiuotas masyvas \n");
            sortedFileArray.Print(n);

            filename = @"mydatalist.dat";
            MyFileList sortedFileList;
            MyFileList myfilelist = new MyFileList(filename, n, seed);
            Console.WriteLine("\n Sąrašas \n");
            myfilelist.Print(n);
            watch.Start();
            sortedFileList = (MyFileList)MergeSort(myfilelist);
            watch.Stop();
            var listTime = watch.Elapsed;
            Console.WriteLine("\n išrykiuotas \n");
            sortedFileList.Print(n);
            Console.WriteLine("masyvo vykdymo laikas: " + arrayTime + " sąrašo vykdymo laikas: " + listTime);
        }

        public static void Test_OP_Radix_Sort(int seed, int n)
        {
            Console.WriteLine("\n Išorinėje atmintyje Radix rykiavimas\n");
            Stopwatch watch = new Stopwatch();
            MyIntArray myarray = new MyIntArray(n, seed);
            MyIntArray MySortedArray;
            Console.WriteLine("\n masyvas \n");
            myarray.Print(n);
            watch.Start();
            MySortedArray = (MyIntArray)RadixSort(myarray);
            watch.Stop();
            var arrayTime = watch.Elapsed;
            Console.WriteLine("\n išrykiuotas \n");
            MySortedArray.Print(n);

            MyIntList MySortedList;
            MyIntList mylist = new MyIntList(n, seed);
            //Console.WriteLine("\n sąrašas \n");
            mylist.Print(n);
            watch.Start();
            MySortedList = (MyIntList)RadixSort(mylist);
            watch.Stop();
            var listTime = watch.Elapsed;
            Console.WriteLine("\n isrykiuotas \n");
            MySortedList.Print(n);
            Console.WriteLine("array elapsed time: " + arrayTime + " list elapsed time: " + listTime);
        }

        public static void Test_OP_Radix_Sort_Comparison(int seed, int n)
        {
            Stopwatch watch = new Stopwatch();
            MyIntArray myarray = new MyIntArray(n, seed);
            MyIntArray MySortedArray;
            watch.Start();
            MySortedArray = (MyIntArray)RadixSort(myarray);
            watch.Stop();
            var arrayTime = watch.Elapsed;

            MyIntList MySortedList;
            MyIntList mylist = new MyIntList(n, seed);
            watch.Start();
            MySortedList = (MyIntList)RadixSort(mylist);
            watch.Stop();
            var listTime = watch.Elapsed;
            Console.WriteLine("{0, -10} {1, -20}  {2, -20}", n, arrayTime, listTime);
        }

        public static void Test_File_Radix_Sort(int seed, int n)
        {
            Console.WriteLine("\n Vidinėje atmintyje Radix rykiavimas\n");
            string filename;
            Stopwatch watch = new Stopwatch();
            filename = @"mydataarray.dat";
            MyIntFileArray sortedFileArray;
            MyIntFileArray myFileArray = new MyIntFileArray(filename, n, seed);
            Console.WriteLine("\n FILE ARRAY \n");
            myFileArray.Print(n);
            watch.Start();
            sortedFileArray = (MyIntFileArray)RadixSort(myFileArray);
            watch.Stop();
            var arrayTime = watch.Elapsed;
            Console.WriteLine("\n sorted FILE ARRAY \n");
            sortedFileArray.Print(n);

            filename = @"mydatalist.dat";
            MyIntFileList sortedFileList;
            MyIntFileList myfilelist = new MyIntFileList(filename, n, seed);
            Console.WriteLine("\n FILE LIST \n");
            myfilelist.Print(n);
            watch.Start();
            sortedFileList = (MyIntFileList)RadixSort(myfilelist);
            watch.Stop();
            var listTime = watch.Elapsed;
            Console.WriteLine("\n SORTED FILE LIST \n");
            sortedFileList.Print(n);
            Console.WriteLine("array elepsed time: " + arrayTime + " list elepsed time: " + listTime);
        }

        public static void Test_File_Radix_Sort_Comparison(int seed, int n)
        {
            MyIntFileList MySortedList;
            MyIntFileList mylist = new MyIntFileList(@"testasList.dat", n, seed);
            Stopwatch watch = new Stopwatch();
            Stopwatch watch2 = new Stopwatch();
            MyIntFileArray myarray = new MyIntFileArray(@"testasArray.dat", n, seed);
            MyIntFileArray MySortedArray;
            watch.Start();
            MySortedArray = (MyIntFileArray)RadixSort(myarray);
            watch.Stop();
            var arrayTime = watch.Elapsed;

            watch2.Start();
            MySortedList = (MyIntFileList)RadixSort(mylist);
            watch2.Stop();
            var listTime = watch2.Elapsed;
            Console.WriteLine("{0, -10} {1, -20}  {2, -20}", n, arrayTime, listTime);
        }

        public static void Test_Op_hashMap(int n)
        {
            Stopwatch watch = new Stopwatch();
            Console.WriteLine("\n Vidinėje atmintyje maišos lentelėje dvigubu heshavimu paieška\n");
            HashMap<string, string> hash = new HashMap<string, string>(n * 2);
            List<string> vardai = Generator.GenerateRandomValues();
            for (int i = 0; i < n; i++)
            {
                hash.InsertNode(vardai[i], vardai[i]);
            }
            long sum = 0;
            for (int i = 0; i < n + 1; i++)
            {
                string surastas;
                watch.Start();
                Console.WriteLine();
                surastas = hash.Get(vardai[i]);
                watch.Stop();
                Console.WriteLine(surastas == null ? "nerastas: " + vardai[i].ToString() : "rastas: " + vardai[i].ToString());
                sum += watch.ElapsedMilliseconds;
            }
            Console.WriteLine("vidutinis paieškos laikas : " + sum/n + " ms");
            //hash.Display();

        }

        public static void Test_Op_hashMap_V2(int n)
        {
            Stopwatch watch = new Stopwatch();
            Console.WriteLine("\n Vidinėje atmintyje maišos lentelėje dvigubu heshavimu paieška\n");
            HashMap<string, string> hash = new HashMap<string, string>(n * 2);
            List<string> names = new List<string>(n);
            for (int i = 0; i < n; i++)
            {
                string name = RandomName(10);
                names.Add(name);
                hash.InsertNode(name, name);
            }
            //names.Add(RandomName(10));
            long sum = 0;
            foreach (var name in names)
            {
                string surastas;
                watch.Start();
                surastas = hash.Get(name);
                watch.Stop();
                Console.WriteLine(surastas == null ? "nerastas: " + name : "rastas: " + name);
                sum += watch.ElapsedMilliseconds;
            }
            Console.WriteLine("vidutinis paieškos laikas : " + sum / n + " ms");
            //hash.Display();

        }

        public static void Test_File_hashMap_V2(int n)
        {
            Stopwatch watch = new Stopwatch();
            Console.WriteLine("\n Išorinėje atmintyje maišos lentelėje dvigubu heshavimu paieška\n");
            HashMapInFile<string, string> hash = new HashMapInFile<string, string>(n * 2);
            List<string> names = new List<string>(n);
            for (int i = 0; i < n; i++)
            {
                string name = RandomName(10);
                names.Add(name);
                hash.InsertNode(name, name);
            }
            //names.Add(RandomName(10));
            long sum = 0;
            foreach (var name in names)
            {
                string surastas;
                watch.Start();
                surastas = hash.Get(name);
                watch.Stop();
                Console.WriteLine(surastas == null ? "nerastas: " + name : "rastas: " + name);
                sum += watch.ElapsedMilliseconds;
            }
            Console.WriteLine("vidutinis paieškos laikas : " + sum / n + " ms");
            //hash.Display();

        }

        public static void Test_hashMap_Comparison(int n)
        {
            Stopwatch watch = new Stopwatch();
            Stopwatch watch2 = new Stopwatch();
            HashMap<string, string> hashOp = new HashMap<string, string>(n * 2);
            HashMapInFile<string, string> hashHDD = new HashMapInFile<string, string>(n * 2);
            List<string> names = new List<string>(n);
            for (int i = 0; i < n; i++)
            {
                string name = RandomName(10);
                names.Add(name);
                hashOp.InsertNode(name, name);
                hashHDD.InsertNode(name, name);
            }
            //names.Add(RandomName(10));
            long sumOp = 0;
            long sumHDD = 0;
            foreach (var name in names)
            {
                watch.Start();
                hashOp.Get(name);
                watch.Stop();

                sumOp += watch.ElapsedMilliseconds;
                watch.Start();
                hashHDD.Get(name);
                watch.Stop();

                sumHDD += watch.ElapsedMilliseconds;
            }
            Console.WriteLine("{0, -10} {1, -20}  {2, -20}", n, sumOp / n + " ms", sumHDD / n + " ms" );
        }

        static string RandomName(int size)
        {
            StringBuilder builder = new StringBuilder();
            char ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 *
            random.NextDouble() + 65)));
            builder.Append(ch);
            for (int i = 1; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 *
               random.NextDouble() + 97)));
                builder.Append(ch);
            }
            return builder.ToString();
        }

        public static void DeleteFiles()
        {
            foreach (string path in Directory.GetFiles("../../bin/Debug"))
            {
                if (path.StartsWith("../../bin/Debug\\Arr") || path.StartsWith("../../bin/Debug\\List"))
                    File.Delete(path);
            }
        }
    }
}

