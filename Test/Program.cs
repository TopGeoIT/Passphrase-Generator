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
            /*Worker passphrasegen = new Worker("c:/skola/8.semester/TP/Generator/Dictionaries/");
            string[] veta = passphrasegen.generateSentenceFromEntrophy(100);
            string celaveta = "";
            for (int i = 0; i<veta.Length;i++)
            {
                celaveta += veta[i] + " ";
            }
            Console.WriteLine(celaveta);*/

            PassphraseController passphrasegen = new PassphraseController("../../../Dictionaries/");
            string[] veta = passphrasegen.generateSentenceFromBinary("010111011001101010011111010100001011001101");
                                                                   //"01011101100110101001111101010000101100110"
            string binary = passphrasegen.generateBinaryFromSentence(string.Join(" ", veta));

            return 0;
        }
    }
}
