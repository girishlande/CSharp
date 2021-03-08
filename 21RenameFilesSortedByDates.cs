using System;
using System.IO;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        
        static void Main(string[] args)
        {
            var sortedFiles = new DirectoryInfo(@"D:\girish\Books\HowToLiveHappy").GetFiles()
                                                   .OrderBy(f => f.LastWriteTime)
                                                   .ToList();
            int count = 1;
            var basePath = @"D:\girish\Books\HowToLiveHappy\";
            foreach (var item in sortedFiles)
            {
                var newPath = basePath + count + ".png";
                count++;
               
                System.IO.File.Move(item.ToString(),newPath);
                Console.WriteLine("File : " + item.ToString() + " =>" + newPath);
            }
        }
    }
}
