using PassphraseGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    class Program
    {
        static int Main(string[] args)
        {
            Worker passphrasegen = new Worker("c:/skola/8.semester/TP/Generator/Dictionaries/");
            string[] veta = passphrasegen.generate(100);
            string celaveta = "";
            for (int i = 0; i<veta.Length;i++)
            {
                celaveta += veta[i] + " ";
            }
            Console.WriteLine(celaveta);
            return 0;
        }
    }
}
