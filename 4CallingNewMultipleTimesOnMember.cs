using System;
using System.Timers;

namespace ConsoleApp3
{
    public class MyTimer: Object
    {
        System.Timers.Timer t1;

        public MyTimer()
        {
            Console.WriteLine("MyTimer constructor called");
            CreateTimer();
        }

        void CreateTimer()
        {
            t1 = new System.Timers.Timer();
            t1.Interval = 1000;
            t1.Elapsed += f1;
            t1.Start();
        }

        private void f1(object sender, ElapsedEventArgs e)
        {
            Console.WriteLine("Timer elapsed");
        }

        ~MyTimer()
        {
            Console.WriteLine("MyTimer destructor called");
        }
    }

    class Program
    {
        MyTimer m1;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Program p1 = new Program();
            p1.func();
            Console.ReadLine();
            p1.func();
            Console.ReadLine();
            p1.func();
            Console.ReadLine();
            p1.func();
            Console.ReadLine();
        }

        public void func()
        {
            m1 = new MyTimer();
        }


    }
}
