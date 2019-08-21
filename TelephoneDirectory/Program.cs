
using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Mapper
    {
        Dictionary<int, string> dict = new Dictionary<int, string>();
        public Mapper()
        {
            Add(2, "abc");
            Add(3, "def");
            Add(4, "ghi");
            Add(5, "jkl");
            Add(6, "mno");
            Add(7, "pqrs");
            Add(8, "tuv");
            Add(9, "wxyz");
        }
        public void Add(int a, string s)
        {
            dict.Add(a, s);
        }
        public string get(int i)
        {
            if (dict.ContainsKey(i))
                return dict[i];
            return "";
        }
    }

    class Blob
    {
        List<string> list = new List<string>();

        public Blob()
        {

        }
        public Blob(string s)
        {
            foreach (var ch in s)
            {
                Add(ch.ToString());
            }
        }

        public Blob(Blob child,string s)
        {
            if (child.list.Count > 0)
            {
                foreach (var ch in s)
                {
                    foreach (var v in child.list)
                    {
                        Add(ch.ToString() + v);
                    }
                }
            }
            else
            {
                foreach (var ch in s)
                {
                    Add(ch.ToString());
                }
            }
        }

        public void Add(string s)
        {
            list.Add(s);
        }

        public void Display()
        {
            foreach (var s in list)
            {
                Console.Write(s + " ");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Enter number:");
            string input = Console.ReadLine();
            int n = Convert.ToInt32(input);
            Mapper m = new Mapper();

            Blob boss = new Blob();
            while (n > 0)
            {
                int r = n % 10;
                n = n / 10;
                string s = m.get(r);
                if (s.Length == 0) continue;
                Blob b = new Blob(boss,s);
                boss = b;
            }
            boss.Display();
        }
    }
}

