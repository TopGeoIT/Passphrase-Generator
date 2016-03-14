using PassphraseGen.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassphraseGen
{
    public class Generator
    {
        private SentenceMember root;
        private int Generated_bits;
        private Dictionary dictonaries;
        public Generator(Dictionary dictonaries)
        {
            this.dictonaries = dictonaries;
            this.root = new SentenceMember("sentence", "singular", true, new SentenceMember("nouns", "singular", false, null, null, null), 
                new SentenceMember("verbs", "singular", false, null, null, null), 
                new SentenceMember("nouns", "singular", false, null, null, null)
                );

            Generated_bits = ((Dictionary.Element) this.dictonaries.GetType().GetField("nouns").GetValue(dictonaries)).size_bits
                + ((Dictionary.Element)this.dictonaries.GetType().GetField("verbs").GetValue(dictonaries)).size_bits
                + ((Dictionary.Element)this.dictonaries.GetType().GetField("nouns").GetValue(dictonaries)).size_bits;
        }

        public PseudoSentence generate(int bits)
        {
            if(bits-250 > 0)
            {
                SentenceMember tmp = this.root;
                root = new SentenceMember("multiSentence", "singular", true, 
                    tmp,
                    new SentenceMember("conjunctions", "singular", false, null, null, null), 
                    new SentenceMember("sentence", "singular", true, 
                        new SentenceMember("nouns", "singular", false, null, null, null),
                        new SentenceMember("verbs", "singular", false, null, null, null),
                        new SentenceMember("nouns", "singular", false, null, null, null)
                    )
                );
                Generated_bits = ((Dictionary.Element)this.dictonaries.GetType().GetField("nouns").GetValue(dictonaries)).size_bits
                + ((Dictionary.Element)this.dictonaries.GetType().GetField("verbs").GetValue(dictonaries)).size_bits
                + ((Dictionary.Element)this.dictonaries.GetType().GetField("nouns").GetValue(dictonaries)).size_bits
                + ((Dictionary.Element)this.dictonaries.GetType().GetField("conjunctions").GetValue(dictonaries)).size_bits;
            }

            int absBits = work(this.root, bits - Generated_bits);

            PseudoSentenceElement[] pseudoSentenceElements = transform(root);

            return new PseudoSentence(pseudoSentenceElements, bits - absBits);
        }

        private PseudoSentenceElement[] transform(SentenceMember root)
        {
            List<PseudoSentenceElement> pseudoSentence = new List<PseudoSentenceElement>();

            if(root.first != null)
            {
                pseudoSentence.AddRange(transform(root.first));
            }
            if (root.middle != null)
            {
                pseudoSentence.AddRange(transform(root.middle));
            }
            if (root.last != null)
            {
                pseudoSentence.AddRange(transform(root.last));
            }
            if(root.first == null && root.middle == null && root.last == null)
            {
                pseudoSentence.Add(new PseudoSentenceElement(root.type, root.spec));
            }

            return pseudoSentence.ToArray();
        }

        private int work(SentenceMember root, int bits){
            Queue<SentenceMember> fronta = new Queue<SentenceMember>();
            fronta.Enqueue(root.first);
            fronta.Enqueue(root.middle);
            fronta.Enqueue(root.last);
            while (bits > 0)
            {
                SentenceMember element = fronta.Dequeue();
                switch (element.type)
                {
                    case "sentence":
                        fronta.Enqueue(element.first);
                        fronta.Enqueue(element.middle);
                        fronta.Enqueue(element.last);
                        break;
                    case "nouns":
                        if (!element.child)
                        {
                            element.first = new SentenceMember("adjectives", "singular", false, null, null, null);
                            element.middle = new SentenceMember("nouns", "singular", true, null, null, null);

                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("adjectives").GetValue(dictonaries)).size_bits;

                            fronta.Enqueue(element.first);
                            fronta.Enqueue(element.middle);
                        } else
                        {
                            element.first = new SentenceMember("nouns", "singular", true, null, null, null);
                            element.middle = new SentenceMember("conjunctions", "singular", false, null, null, null);
                            element.last = new SentenceMember("nouns", "singular", false, null, null, null);

                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("nouns").GetValue(dictonaries)).size_bits;
                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("conjunctions").GetValue(dictonaries)).size_bits;

                            fronta.Enqueue(element.first);
                            fronta.Enqueue(element.last);
                        }
                        break;
                    case "verbs":
                        if (!element.child)
                        {
                            element.first = new SentenceMember("adverbs", "singular", false, null, null, null);
                            element.middle = new SentenceMember("verbs", "singular", true, null, null, null);

                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("adverbs").GetValue(dictonaries)).size_bits;

                            fronta.Enqueue(element.first);
                            fronta.Enqueue(element.middle);
                        }
                        else
                        {
                            
                            element.middle = new SentenceMember("verbs", "singular", true, null, null, null);
                            element.last = new SentenceMember("adverbs", "singular", false, null, null, null);

                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("adverbs").GetValue(dictonaries)).size_bits;

                            fronta.Enqueue(element.last);
                        }
                        break;
                    case "adverbs":
                        if (!element.child)
                        {
                            element.first = new SentenceMember("adverbs", "singular", true, null, null, null);
                            element.middle = new SentenceMember("conjunctions", "singular", false, null, null, null);
                            element.last = new SentenceMember("adverbs", "singular", false, null, null, null);

                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("adverbs").GetValue(dictonaries)).size_bits;
                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("conjunctions").GetValue(dictonaries)).size_bits;

                            fronta.Enqueue(element.first);
                            //fronta.Enqueue(element.last);
                        }
                        else
                        {

                            element.first = new SentenceMember("adverbs", "singular", true, null, null, null);
                            element.middle = new SentenceMember("conjunctions", "singular", true, null, null, null);
                            element.last = new SentenceMember("adverbs", "singular", false, null, null, null);

                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("adverbs").GetValue(dictonaries)).size_bits;
                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("conjunctions").GetValue(dictonaries)).size_bits;

                            fronta.Enqueue(element.first);
                            //fronta.Enqueue(element.last);
                        }
                        break;
                    case "adjectives":
                        if (!element.child)
                        {
                            element.first = new SentenceMember("adjectives", "singular", true, null, null, null);
                            element.middle = new SentenceMember("conjunctions", "singular", false, null, null, null);
                            element.last = new SentenceMember("adjectives", "singular", false, null, null, null);

                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("adjectives").GetValue(dictonaries)).size_bits;
                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("conjunctions").GetValue(dictonaries)).size_bits;

                            fronta.Enqueue(element.first);
                            //fronta.Enqueue(element.last);
                        }
                        else
                        {

                            element.first = new SentenceMember("adjectives", "singular", true, null, null, null);
                            element.middle = new SentenceMember("conjunctions", "singular", true, null, null, null);
                            element.last = new SentenceMember("adjectives", "singular", false, null, null, null);

                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("adjectives").GetValue(dictonaries)).size_bits;
                            bits -= ((Dictionary.Element)this.dictonaries.GetType().GetField("conjunctions").GetValue(dictonaries)).size_bits;

                            fronta.Enqueue(element.first);
                            fronta.Enqueue(element.last);
                        }
                        break;
                }
            }
            return bits;
        }
    }
}
