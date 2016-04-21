using PassphraseGen.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassphraseGen
{
    public class PassphraseController
    {
        private DictionaryController dictionary;
        private PseudoSentenceController generator;
        private WordsController sentenceBuilder;
        private Random rnd;

        public PassphraseController(string dictionaries)
        {
            this.dictionary = new DictionaryController(dictionaries);
            this.generator = new PseudoSentenceController(this.dictionary);
            this.sentenceBuilder = new WordsController(dictionaries);

            this.rnd = new Random();
        }
        public PassphraseController()
        {

        }
        public string[] generateSentenceFromEntrophy(int entrophy)//"c:/skola/8.semester/TP/Generator/Dictionaries/"
        {
            PseudoSentence sentence = this.generator.generate(entrophy);

            //vygenerovat retazec
            string query = "";
            for (int i = 0; i < sentence.bits -1; i++)
            {
                query += rnd.Next(0, 2).ToString();
            }

            //nedoplnane bity
            query += "0";

            return sentenceToString(sentence, query);
        }
        public string[] generateSentenceFromBinary(string binaryQuery)
        {
            //throw new NotImplementedException("method was not implemented");

            int binaryQuerryLength = binaryQuery.Length;

            PseudoSentence sentence = this.generator.generate(binaryQuerryLength +1); 

            if(sentence.bits == binaryQuerryLength +1)
            {
                //rovnaky pocet ako zadany
                binaryQuery += "0";
            }else if (sentence.bits >= binaryQuerryLength + 4 && sentence.bits <= binaryQuerryLength + 11)
            {
                for(int i = 0; i< sentence.bits - binaryQuerryLength - 4; i++)
                {
                    binaryQuery += rnd.Next(0, 2).ToString();
                }
                

                int count = sentence.bits - binaryQuerryLength - 4;

                binaryQuerryLength = binaryQuery.Length;

                string binary = Convert.ToString(count, 2);

                string formatedBinary = binary.ToString();

                for(int i = 0; i < 3-binary.Length; i++)
                {
                    formatedBinary = "0" + formatedBinary;
                }

                binaryQuery += formatedBinary + "1";
                binaryQuerryLength = binaryQuery.Length;
            }
            else if (sentence.bits <= binaryQuerryLength + 11)
            {
                //vyssi pocet bitov
                sentence = this.generator.generate(binaryQuerryLength + 4);
                if(sentence.bits >= binaryQuerryLength + 4 && sentence.bits <= binaryQuerryLength + 11)
                {
                    for (int i = 0; i < sentence.bits - binaryQuerryLength - 4; i++)
                    {
                        binaryQuery += rnd.Next(0, 2).ToString();
                    }
                    binaryQuerryLength = binaryQuery.Length;

                    int count = sentence.bits - binaryQuerryLength - 4;
                    string binary = Convert.ToString(count, 2);

                    string formatedBinary = binary.ToString();

                    for (int i = 0; i < 3 - binary.Length; i++)
                    {
                        formatedBinary = "0" + formatedBinary;
                    }

                    binaryQuery += formatedBinary + "1";
                }
                else{
                    throw new ArgumentException("Can not generate pseudosentence in selected range.");
                }

            }else if(sentence.bits > binaryQuerryLength + 11)
            {
                throw new ArgumentException("Can not generate pseudosentence in selected range.");
            }

            //dorobit dogenerovanie binarystringu. pridat do generovania +1 entropiu. 
            //ak sa vysledn alisi od zadanej, vygenerovat znovu s +4 oproti originalnej. posledny bit prihodit 0 - ak nie je doplnany string a 1 ak doplnany je. 
            //na predchadzajuce 3 znaky umiestnit pocet pridanych bitov (2^3 = 8 (max 12 sa bude pridavat)) zbytok(pocet doplnenych - 4) doplnit random 0/1

            return sentenceToString(sentence, binaryQuery);
        }
        public string generateBinaryFromSentence(string sentenceQuerry)
        {
            sentenceQuerry.Replace(",", " ,");
            string[] sentenceArray = sentenceQuerry.Split(' ');
            List<string> sentenceList = new List<string>();
            string tmp = null;
            for(int i = 0; i< sentenceArray.Count(); i++)
            {
                if(sentenceArray[i] == "the" || sentenceArray[i] == "The")
                {
                    tmp = sentenceArray[i];
                }else 
                if (tmp != null)
                {
                    sentenceList.Add(tmp + " " + sentenceArray[i]);
                    tmp = null;
                }
                else
                {
                    sentenceList.Add(sentenceArray[i]);
                }
            }


            //TODO> rozparsovat vetu na slova, zistit pocet slov, vygenerovat pseudovetu s tolko slovami, rozparsovat na slovne druhy, zistit index v slovnikoch, vratit binaryString
            PseudoSentence sentence = this.generator.generate(null, sentenceList.Count(), false);

            if(sentence.sentence.Length != sentenceList.Count())
            {
                throw new ArgumentException("Cant generate pseudosentence for sentence.");
            }

            string sentenceBinaryQuerry = "";
            for (int i = 0; i < sentence.sentence.Length; i++)
                if(sentence.sentence[i].type != "conjunctions")
                {
                    string binary = Convert.ToString(this.sentenceBuilder.getIndex(sentence.sentence[i].type, sentenceList[i]), 2);

                    string formatedBinary = binary.ToString();

                    int sizeDict = 0;
                    switch (sentence.sentence[i].type)
                    {
                        case "nouns":
                            sizeDict = this.dictionary.nouns.size_bits;
                            break;
                        case "verbs":
                            sizeDict = this.dictionary.verbs.size_bits;
                            break;
                        case "adjectives":
                            sizeDict = this.dictionary.adjectives.size_bits;
                            break;
                        case "adverbs":
                            sizeDict = this.dictionary.adverbs.size_bits;
                            break;
                        case "conjunctions":
                            sizeDict = this.dictionary.conjunctions.size_bits;
                            break;
                        default:
                            throw new ArgumentException("Bad type");
                    }
            
                    for (int j = 0; j < sizeDict - binary.Length; j++)
                    {
                        formatedBinary = "0" + formatedBinary;
                    }

                    sentenceBinaryQuerry += formatedBinary;
                }

            if(sentenceBinaryQuerry.Last() == '0')
            {
                return sentenceBinaryQuerry.Substring(0, sentenceBinaryQuerry.Length - 1);
            }
            else
            {
                string number = sentenceBinaryQuerry.Substring(sentenceBinaryQuerry.Length - 4, 3);
                int count = Convert.ToInt32(number, 2);

                return sentenceBinaryQuerry.Substring(0, sentenceBinaryQuerry.Length - count - 4);
            }

            throw new NotImplementedException("method was not implemented");
        }

        public string[] sentenceToString(PseudoSentence sentence, string binaryQuery)
        {
            int count = 0;
            List<string> tmpSentence = new List<string>();
            for (int i = 0; i < sentence.sentence.Length; i++)
            {
                string numberStr = binaryQuery.Substring(count, dictionary.getSizeBits(sentence.sentence[i].type));
                int number = numberStr == "" ? 0 : Convert.ToInt32(numberStr, 2);
                count += dictionary.getSizeBits(sentence.sentence[i].type);
                BuildObj word = this.sentenceBuilder.buildWord(sentence.sentence[i].type, sentence.sentence[i].spec, number);
                tmpSentence.Add(word.word);
            }

            return tmpSentence.ToArray();
        }
    }
}
