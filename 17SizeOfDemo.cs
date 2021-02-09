using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StudentTable
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)] public StudentEntry[] data;
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct StudentEntry
    {
        public int Roll;
        public ushort Math;
        public ushort Physica;
        public ushort Chemistry;
    }

    class Program
    {
        static void Main(string[] args)
        {
            int studentEntrySize = Marshal.SizeOf<StudentEntry>();
            Console.WriteLine("size of StudentEntry:" + studentEntrySize);
            int StudentTableSize = Marshal.SizeOf<StudentTable>();
            Console.WriteLine("size of StudentTable:" + StudentTableSize);

        }

    }
}
