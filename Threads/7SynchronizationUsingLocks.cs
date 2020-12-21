using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    
    class Program
    {
        private readonly Random _random = new Random();
        public int MyProperty { get; set; }
        public int temp { get; set; }

        object _lock = new object();

        Program(int x)
        {
            MyProperty = x;
        }

        void Updater(int m)
        {

            for(int i=0;i<10;i++)
            {
                lock (_lock)
                {
                    temp = MyProperty + m;
                    int num = _random.Next(10);
                    Thread.Sleep(num);
                    MyProperty = temp;
                }
                Console.WriteLine("Thread:" + Thread.CurrentThread.ManagedThreadId + " value:" + MyProperty);
            }
        }

        static void Main(string[] args)
        {
            Program p1 = new Program(10);
            Console.WriteLine("BEfore execution:"+p1.MyProperty);
            new Thread(() =>
            {
                p1.Updater(1);
            }).Start();

            new Thread(() =>
            {
                p1.Updater(-1);
            }).Start();


            Console.ReadKey();
            Console.WriteLine("After execution:" + p1.MyProperty);
            Console.ReadKey();
        }
    }
}
