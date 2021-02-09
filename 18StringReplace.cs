using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ConsoleApp1
{
    class Mapper
    {
        Dictionary<string, string> dict = new Dictionary<string, string>();
        public Mapper()
        {
            Add("&", "&amp;");
            Add("<", "&lt;");
            Add(">", "&gt;");
            Add("'", "&apos;");
        }

        public void Add(string a, string s)
        {
            dict.Add(a, s);
        }

        public string ReplaceSpecialCharacters(string input)
        {
            string str = input;
            foreach (var item in dict)
            {
                str = str.Replace(item.Key, item.Value);
            }
            return str;
        }
    }

    class Program
    {
        
        static void Main(string[] args)
        {
            Mapper mapper = new Mapper();

            string[] lines = { "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>", 
                "<Log>",
                "This is Log Text <> 'something' &Girish ",
                "</Log>" };

            for(int i=2;i<lines.Length;i++)
            {
                lines[i] = mapper.ReplaceSpecialCharacters(lines[i]);
                break;
            }
            System.IO.File.WriteAllLines(@"D:\One.xml", lines);
        }

    }
}
