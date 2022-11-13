using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace SysProgDZ03_2 
{
    public  struct ThePair
    {
        public int first { get; set; }
        public int second { get; set; }
    }

    public class Func
    {
        private static object locker = new();

        private List<ThePair> pairs = new();
        private static Random random = new(DateTime.Now.Millisecond);
        private FileInfo fileInfo = new FileInfo("Output.txt");
        private FileStream fs;
        public void Generate()
        {
            lock (locker)
            {
                for (int i = 0; i < random.Next(5, 15); i++)
                {
                    pairs.Add(new ThePair() { first = random.Next(1, 10000), second = random.Next(1, 10000) });
                }
                try
                {
                    using (fs = new(fileInfo.FullName, FileMode.OpenOrCreate, FileAccess.Write))
                    {
                        StreamWriter sw = new(fs) ;
                        foreach (ThePair pair in pairs)
                        {
                            sw.WriteLine(pair.first.ToString() + " " + pair.second.ToString());
                            Console.WriteLine("Generating: " + pair.first + " " + pair.second);
                        }
                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }

            }
        }
        public void Summ()
        {
            lock (locker)
            {
                List<int> intlist = new();
                foreach (ThePair pair in pairs)
                {
                    intlist.Add(pair.first + pair.second);
                }
                try
                {
                    using (fs = new(fileInfo.FullName, FileMode.Append, FileAccess.Write))
                    {
                        StreamWriter sw = new(fs);
                        sw.WriteLine();
                        foreach (int i in intlist)
                        {
                            sw.WriteLine(i.ToString());
                            Console.WriteLine("Generating sum result: " + i);
                        }

                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
        public void Mult()
        {
            lock (locker)
            {
                List<Int64> intlist = new();
                foreach (ThePair pair in pairs)
                {
                    intlist.Add((Int64)(pair.first * pair.second));
                }
                try
                {
                    using (fs = new(fileInfo.FullName, FileMode.Append, FileAccess.Write))
                    {
                        StreamWriter sw = new(fs);
                        sw.WriteLine();
                        foreach (int i in intlist)
                        {
                            sw.WriteLine(i.ToString());
                            Console.WriteLine("Generating mult result: " + i);
                        }

                    }
                }
                catch (Exception ex) { Console.WriteLine(ex.Message); }
            }
        }
    }
    public class Program
    {
        
        public static void Main(string[] args)
        {
            Func func = new();
            Thread thread1 = new(() => func.Generate());
            Thread thread2 = new(() => func.Summ());
            Thread thread3 = new(() => func.Mult());
            thread1.Start();
            thread1.Join();
            thread2.Start();
            thread2.Join();
            thread3.Start();
            thread3.Join();
        }

        

    }
}