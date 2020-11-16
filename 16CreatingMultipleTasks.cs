using System;
using System.Threading;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Test1
{
    class Program
    {
        static void Main(string[] args)
        {
            Program p1 = new Program();

            _ = p1.Test();

            Action c1 = ()=> { p1.f1(); } ;

           var tasks = new[] {
                Task.Factory.StartNew(()=>p1.Test()),
                Task.Factory.StartNew(()=>p1.Test()),
                Task.Factory.StartNew(()=>p1.Test()),
                Task.Factory.StartNew(()=>p1.Test())
            };
            Console.WriteLine("Returned from async function");
            Console.ReadKey();
        }

        void f1()
        {

        }

        async Task Test()
        {
            int output = await func();
            Console.WriteLine("Async func returned:"+output);
        }

        async Task<int> func()
        {
            Console.WriteLine("Async func started: thread:" + Thread.CurrentThread.ManagedThreadId);
            await Task.Delay(2000);
            Console.WriteLine("Async func finished: thread:" + Thread.CurrentThread.ManagedThreadId);
            return 11;
        }
    }
}


//Async func started
//Returned from async function
//Async func finished
//Async func returned:11


