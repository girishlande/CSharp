// How to use Timers 
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp3
{
    public class MyTimer 
    {
        private System.Timers.Timer t1 { get; set; }

        public MyTimer()
        {
            Console.WriteLine("Girish:MyTimer constructor called");
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
            Console.WriteLine("Girish:Timer elapsed:"+ DateTime.Now.ToString("h:mm:ss tt"));
        }

        ~MyTimer()
        {
            Console.WriteLine("Girish:MyTimer destructor called");
        }
    }

    class Program
    {
        private MyTimer m1 { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Program p1 = new Program();
            p1.InitializeEventCallback();
            Console.ReadLine();
            p1.InitializeEventCallback();
            Console.ReadLine();
            p1.InitializeEventCallback();
            Console.ReadLine();
            p1.InitializeEventCallback();
            Console.ReadLine();
        }

        public void InitializeEventCallback()
        {
            m1 = new MyTimer();
			
			// Force Garbage collection 
            System.GC.Collect();
            GC.WaitForPendingFinalizers();
        }
    }
}
