// What is async await in C# . what is asyn
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp4
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Program p1 = new Program();
            for(int i=0;i<5;i++)
            {
            p1.f3();
            }
            Console.WriteLine("Called all functions");
            Console.ReadLine();
        }

        void f3()
        {
            f1();
        }

        async void f1()
        {
            await Task.Run(()=>f2());
        }

        void f2()
        {
            int waitTime = RandomNumber(5, 15);
            Console.WriteLine("f2() called by ThreadId:" + Thread.CurrentThread.ManagedThreadId + " wait:" + waitTime);
            Thread.Sleep(waitTime * 100);
            Console.WriteLine("f2() ended by ThreadId:"+Thread.CurrentThread.ManagedThreadId);
        }

        private readonly Random _random = new Random();

        // Generates a random number within a range.      
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
