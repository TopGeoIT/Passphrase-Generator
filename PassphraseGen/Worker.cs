using PassphraseGen.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassphraseGen
{
    public class Worker
    {
        private Dictionary dictionary;
        private Generator generator;
        private Builder sentenceBuilder;

        public Worker(string dictionaries)
        {
            this.dictionary = new Dictionary(dictionaries);
            this.generator = new Generator(this.dictionary);
            this.sentenceBuilder = new Builder(dictionaries);
        }
        public string[] generate(int entrophy)//"c:/skola/8.semester/TP/Generator/Dictionaries/"
        {
            PseudoSentence veta = this.generator.generate(entrophy);

            //vygenerovat retazec
            string query = "";
            Random rnd = new Random();
            for (int i = 0; i < veta.bits; i++)
            {
                query += rnd.Next(0, 2).ToString();
            }

            //prelozit retazec na vetu
            int count = 0;
            List<string> sentence = new List<string>();
            for (int i = 0; i < veta.sentence.Length; i++)
            {
                string numberStr = query.Substring(count, dictionary.getSizeBits(veta.sentence[i].type));
                int number = numberStr == "" ? 0 : Convert.ToInt32(numberStr, 2);
                count += dictionary.getSizeBits(veta.sentence[i].type);
                BuildObj word = this.sentenceBuilder.buildWord(veta.sentence[i].type, veta.sentence[i].spec, number);
                sentence.Add(word.word);
            }

            return sentence.ToArray();
        }
    }
}
