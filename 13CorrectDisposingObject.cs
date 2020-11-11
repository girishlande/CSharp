using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp3
{
    public class MyTimer : IDisposable
    {
        private System.Timers.Timer t1 { get; set; }
        public int mId { get; set; }
        static int counter = 1;
        public MyTimer()
        {
            mId = counter++;
            Console.WriteLine("Girish:MyTimer constructor called ID:" + mId);
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
            Console.WriteLine("Girish:Timer elapsed:" + DateTime.Now.ToString("h:mm:ss tt"));
        }

        public void Dispose()
        {
            t1.Elapsed -= f1;
            t1.Dispose();
        }

        ~MyTimer()
        {
            Console.WriteLine("Girish:MyTimer destructor called ID:" + mId);
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
            p1.Dispose();
            Console.WriteLine("Calling Garbage Collector");
            System.GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.ReadLine();
        }

        public void InitializeEventCallback()
        {
            Dispose();
            m1 = new MyTimer();
            InvokeGarbageCollection();
        }

        public void Dispose()
        {
            if (m1 != null)
                m1.Dispose();
        }

        public void InvokeGarbageCollection()
        {
            System.GC.Collect();
            GC.WaitForPendingFinalizers();
        }

    }
}
