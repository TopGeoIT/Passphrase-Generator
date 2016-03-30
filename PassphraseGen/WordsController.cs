using PassphraseGen.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PassphraseGen
{
    public class WordsController
    {
        private Noun[] nouns;
        private Verb[] verbs;
        private Adjective[] adjectives;
        private Adverb[] adverbs;
        private Conjunction[] conjunctions;

        public WordsController(string route)
        {
            loadDictionaries(route);
        }

        public BuildObj returner(string value, string sing, string plur, string spec)
        {
            if (sing == null && plur == null && value != null)
                return new BuildObj(value, "");
            else if ((spec == "singular" && sing != null) || (spec == "plural" && plur == null || sing != null))
                return new BuildObj(sing, "sg");
            else if ((spec == "plural" && plur != null) || (spec == "singular" && sing == null || plur != null))
                return new BuildObj(plur, "pl");
            else
                throw new KeyNotFoundException("No word for specified index");
        }

        public BuildObj buildWord(string type, string spec, int index)
        {
            switch (type)
            {
                case "nouns":
                    return returner(this.nouns[index].value, this.nouns[index].singular, this.nouns[index].plural, spec);
                    break;
                case "verbs":
                    return returner(null, this.verbs[index].presentSingular, this.verbs[index].presentPlural, spec);
                    break;
                case "adjectives":
                    return returner(this.adjectives[index].value, null, null, spec);
                    break;
                case "adverbs":
                    return returner(this.adverbs[index].value, null, null, spec);
                    break;
                case "conjunctions":
                    return returner(this.conjunctions[index].value, null, null, spec);
                    break;
                default:
                    throw new ArgumentException("Bad type");
            }
        }

        public int getIndex(string type, string word)
        {
            switch (type)
            {
                case "nouns":
                    var indexNoun = Array.FindIndex(this.nouns, row => (row.singular != null && row.singular == word) || (row.plural != null && row.plural == word) || (row.value != null && row.value == word));
                    return indexNoun;
                    break;
                case "verbs":
                    var indexVerb = Array.FindIndex(this.verbs, row => (row.presentPlural != null && row.presentPlural == word) || (row.presentSingular != null && row.presentSingular == word));
                    return indexVerb;
                    break;
                case "adjectives":
                    var indexAdj = Array.FindIndex(this.adjectives, row => (row.value != null && row.value == word));
                    return indexAdj;
                    break;
                case "adverbs":
                    var indexAdv = Array.FindIndex(this.adverbs, row => (row.value != null && row.value == word));
                    return indexAdv;
                    break;
                case "conjunctions":
                    return 0;
                    break;
                default:
                    throw new ArgumentException("Bad type");
            }
        }

        public void loadDictionaries(string route)
        {
            loadNouns(route);
            loadVerbs(route);
            loadAdjectives(route);
            loadAdverbs(route);
            loadConjunctions(route);
        }

        private void loadConjunctions(string route)
        {
            List<Conjunction> conjunctionsTmp = new List<Conjunction>();
            conjunctionsTmp.Add(new Conjunction("and", "and"));
            conjunctionsTmp.Add(new Conjunction(",", ","));
            this.conjunctions = conjunctionsTmp.ToArray();
        }
        private void loadNouns(string route)
        {
            List<Noun> nounsTmp = new List<Noun>();
            XElement nounsFromFile = XElement.Load(route + "Nouns.xml");
            XElement[] nouns = nounsFromFile.Elements().ToArray();
            for (int i = 0; i < nouns.Length; i++)
            {
                string value = null, sing = null, plur = null;
                if (nouns[i].Attribute("value") != null)
                {
                    value = nouns[i].Attribute("value").Value;
                }
                if (nouns[i].Attribute("singular") != null)
                {
                    sing = nouns[i].Attribute("singular").Value;
                }
                if (nouns[i].Attribute("plural") != null)
                {
                    plur = nouns[i].Attribute("plural").Value;
                }
                nounsTmp.Add(new Noun(value, sing, plur));
            }
            this.nouns = nounsTmp.ToArray();

        }

        private void loadVerbs(string route)
        {
            List<Verb> verbsTmp = new List<Verb>();
            XElement verbsFromFile = XElement.Load(route + "Verbs.xml");
            XElement[] verbs = verbsFromFile.Elements().ToArray();
            for (int i = 0; i < verbs.Length; i++)
            {
                string sing = null, plur = null;
                if (verbs[i].Attribute("presentSingular") != null)
                {
                    sing = verbs[i].Attribute("presentSingular").Value;
                }
                if (verbs[i].Attribute("presentPlural") != null)
                {
                    plur = verbs[i].Attribute("presentPlural").Value;
                }
                verbsTmp.Add(new Verb(sing, plur));
            }
            this.verbs = verbsTmp.ToArray();

        }
        private void loadAdjectives(string route)
        {
            List<Adjective> adjectivesTmp = new List<Adjective>();
            XElement adjectivesFromFile = XElement.Load(route + "Adjectives.xml");
            XElement[] adjectives = adjectivesFromFile.Elements().ToArray();
            for (int i = 0; i < adjectives.Length; i++)
            {
                string value = null;
                if (adjectives[i].Attribute("value") != null)
                {
                    value = adjectives[i].Attribute("value").Value;
                }
                adjectivesTmp.Add(new Adjective(value));
            }
            this.adjectives = adjectivesTmp.ToArray();

        }
        private void loadAdverbs(string route)
        {
            List<Adverb> adverbsTmp = new List<Adverb>();
            XElement adverbsFromFile = XElement.Load(route + "Adverbs.xml");
            XElement[] adverbs = adverbsFromFile.Elements().ToArray();
            for (int i = 0; i < adverbs.Length; i++)
            {
                string value = null;
                if (adverbs[i].Attribute("value") != null)
                {
                    value = adverbs[i].Attribute("value").Value;
                }
                adverbsTmp.Add(new Adverb(value));
            }
            this.adverbs = adverbsTmp.ToArray();

        }
    }
}
