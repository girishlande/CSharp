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
            p1.func();

            Console.WriteLine("Returned from async function");
            Console.ReadKey();
        }

        async Task func()
        {
            Console.WriteLine("Async func started");
            await Task.Delay(2000);
            Console.WriteLine("Async func finished");
        }
    }
}
