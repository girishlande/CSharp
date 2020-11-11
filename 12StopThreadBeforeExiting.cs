using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;

namespace ConsoleApp3
{
    public class TaskRunner : IDisposable
    {
        private Task task;
        private CancellationTokenSource cts = new CancellationTokenSource();

        public TaskRunner()
        {
            task = new Task(Run, cts.Token, TaskCreationOptions.LongRunning);
            task.Start();
        }

        public void Dispose()
        {
            cts.Cancel();
        }

        private void Run()
        {
            while (!cts.Token.IsCancellationRequested)
            {
                // Your stuff goes here.
                Console.WriteLine("Hello");
                Thread.Sleep(1000);
            }
            Console.WriteLine("Out of while");
        }
    }

    class Program
    {

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            TaskRunner runner = new TaskRunner();
            Console.ReadLine();
            runner.Dispose();
            Console.ReadLine();
        }


    }
}
