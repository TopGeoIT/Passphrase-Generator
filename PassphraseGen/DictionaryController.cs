using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PassphraseGen
{
    public class DictionaryController
    {
        public Element nouns;
        public Element adjectives;
        public Element verbs;
        public Element adverbs;
        public Element conjunctions;
        public class Element
        {
            public int size;
            public int size_bits;
            public Element(int size, int size_bits)
            {
                this.size = size;
                this.size_bits = size_bits;
            }
        }

        public DictionaryController(string route)
        {
            loadDictionaries(route);
        }

        public DictionaryController(Dictionary<string, string> dictionaries)
        {
            loadDictionaries(dictionaries);
        }

        public int getSizeBits(string type)
        {
            switch (type)
            {
                case "nouns":
                    return this.nouns.size_bits;
                case "adjectives":
                    return this.adjectives.size_bits;
                case "verbs":
                    return this.verbs.size_bits;
                case "adverbs":
                    return this.adverbs.size_bits;
                case "conjunctions":
                    return this.conjunctions.size_bits;
            }
            throw new InvalidDataException("Bad type");
        }

        public void loadDictionaries(string route)
        {
            XElement nounsFromFile = XElement.Load(route + "Nouns.xml");
            var nounsSize = Convert.ToInt32(nounsFromFile.FirstAttribute.Value );

            this.nouns = new Element(nounsSize, getEntrophy(nounsSize));

            XElement adjectivesFromFile = XElement.Load(route + "Adjectives.xml");
            var adjectivesSize = Convert.ToInt32(adjectivesFromFile.FirstAttribute.Value);
            this.adjectives = new Element(adjectivesSize, getEntrophy(adjectivesSize));

            XElement verbsFromFile = XElement.Load(route + "Verbs.xml");
            var verbsSize = Convert.ToInt32(verbsFromFile.FirstAttribute.Value);
            this.verbs = new Element(verbsSize, getEntrophy(verbsSize));

            XElement adverbsFromFile = XElement.Load(route + "Adverbs.xml");
            var adverbsSize = Convert.ToInt32(adverbsFromFile.FirstAttribute.Value);
            this.adverbs = new Element(adverbsSize, getEntrophy(adverbsSize));

            /*XElement conjunctionsFromFile = XElement.Load(route + "Conjunctions.xml");
            var conjunctionsSize = Convert.ToInt32(conjunctionsFromFile.FirstAttribute.Value);
            this.conjunctions = new Element(conjunctionsSize, getEntrophy(conjunctionsSize));*/
            this.conjunctions = new Element(0, 0);
        }

        public void loadDictionaries(Dictionary<string, string> dictionaries)
        {
            XElement nounsFromFile = XElement.Parse(dictionaries["nouns"]);
            var nounsSize = Convert.ToInt32(nounsFromFile.FirstAttribute.Value);

            this.nouns = new Element(nounsSize, getEntrophy(nounsSize));

            XElement adjectivesFromFile = XElement.Parse(dictionaries["adjectives"]);
            var adjectivesSize = Convert.ToInt32(adjectivesFromFile.FirstAttribute.Value);
            this.adjectives = new Element(adjectivesSize, getEntrophy(adjectivesSize));

            XElement verbsFromFile = XElement.Parse(dictionaries["verbs"]);
            var verbsSize = Convert.ToInt32(verbsFromFile.FirstAttribute.Value);
            this.verbs = new Element(verbsSize, getEntrophy(verbsSize));

            XElement adverbsFromFile = XElement.Parse(dictionaries["adverbs"]);
            var adverbsSize = Convert.ToInt32(adverbsFromFile.FirstAttribute.Value);
            this.adverbs = new Element(adverbsSize, getEntrophy(adverbsSize));

            /*XElement conjunctionsFromFile = XElement.Load(dictionaries["conjunctions"]);
            var conjunctionsSize = Convert.ToInt32(conjunctionsFromFile.FirstAttribute.Value);
            this.conjunctions = new Element(conjunctionsSize, getEntrophy(conjunctionsSize));*/
            this.conjunctions = new Element(0, 0);
        }

        private int getEntrophy(int size)
        {
            int tmp = 1;
            int size2 = 2;
            while (size > size2) 
            {
                size2 = size2 * 2;
                tmp++;
            }

            return tmp;
        }
    }
}

