using PassphraseGen.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassphraseGen
{
    public class PseudoSentenceController
    {
        private SentenceMember root;
        private int Generated_bits;
        private int Generated_words;
        private DictionaryController dictonaries;

        public PseudoSentenceController(DictionaryController dictonaries)
        {
            this.dictonaries = dictonaries;
            this.root = new SentenceMember("sentence", "singular", true, new SentenceMember("nouns", "singular", false, null, null, null), 
                new SentenceMember("verbs", "singular", false, null, null, null), 
                new SentenceMember("nouns", "singular", false, null, null, null)
                );

            Generated_bits = ((DictionaryController.Element) this.dictonaries.GetType().GetField("nouns").GetValue(dictonaries)).size_bits
                + ((DictionaryController.Element)this.dictonaries.GetType().GetField("verbs").GetValue(dictonaries)).size_bits
                + ((DictionaryController.Element)this.dictonaries.GetType().GetField("nouns").GetValue(dictonaries)).size_bits;
            Generated_words = 3;
        }

        public PseudoSentence generate(int? bits = null, int? count = null, bool? sentences = null)
        {
            this.root = new SentenceMember("sentence", "singular", true, new SentenceMember("nouns", "singular", false, null, null, null),
                new SentenceMember("verbs", "singular", false, null, null, null),
                new SentenceMember("nouns", "singular", false, null, null, null)
                );

            Generated_bits = ((DictionaryController.Element)this.dictonaries.GetType().GetField("nouns").GetValue(dictonaries)).size_bits
                + ((DictionaryController.Element)this.dictonaries.GetType().GetField("verbs").GetValue(dictonaries)).size_bits
                + ((DictionaryController.Element)this.dictonaries.GetType().GetField("nouns").GetValue(dictonaries)).size_bits;
            Generated_words = 3;

            if (bits != null && bits.Value-250 > 0 || (sentences != null && sentences.Value == true))
            {
                SentenceMember tmp = this.root;
                root = new SentenceMember("multiSentence", "singular", true, 
                    tmp,
                    new SentenceMember("conjunctions", "multisentence", false, null, null, null), 
                    new SentenceMember("sentence", "singular", true, 
                        new SentenceMember("nouns", "singular", false, null, null, null),
                        new SentenceMember("verbs", "singular", false, null, null, null),
                        new SentenceMember("nouns", "singular", false, null, null, null)
                    )
                );
                Generated_bits += ((DictionaryController.Element)this.dictonaries.GetType().GetField("nouns").GetValue(dictonaries)).size_bits
                + ((DictionaryController.Element)this.dictonaries.GetType().GetField("verbs").GetValue(dictonaries)).size_bits
                + ((DictionaryController.Element)this.dictonaries.GetType().GetField("nouns").GetValue(dictonaries)).size_bits
                + ((DictionaryController.Element)this.dictonaries.GetType().GetField("conjunctions").GetValue(dictonaries)).size_bits;
                Generated_words += 4;

            }

            int absBits;
            if (bits != null)
            {
                absBits = work(this.root, bits.Value - Generated_bits, null);
            }else if(count != null)
            {
                absBits = work(this.root, null, count.Value - Generated_words);
            }
            else
            {
                throw new ArgumentException("Bad arguments");
            }

            PseudoSentenceElement[] pseudoSentenceElements = transform(root);

            return new PseudoSentence(pseudoSentenceElements, (bits != null? bits.Value : 0) - absBits);
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

        private int work(SentenceMember root, int? bits, int? count){
            Queue<SentenceMember> fronta = new Queue<SentenceMember>();
            fronta.Enqueue(root.first);
            fronta.Enqueue(root.middle);
            fronta.Enqueue(root.last);

            int genBits = (bits != null ? bits.Value : 0);
            while ( (bits != null && genBits > 0) || (count != null && count > 0))
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

                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("adjectives").GetValue(dictonaries)).size_bits;
                            count--;

                            fronta.Enqueue(element.first);
                            fronta.Enqueue(element.middle);
                        } else
                        {
                            element.first = new SentenceMember("nouns", "singular", true, null, null, null);
                            element.middle = new SentenceMember("conjunctions", "singular", false, null, null, null);
                            element.last = new SentenceMember("nouns", "singular", false, null, null, null);

                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("nouns").GetValue(dictonaries)).size_bits;
                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("conjunctions").GetValue(dictonaries)).size_bits;
                            count--;
                            count--;

                            fronta.Enqueue(element.first);
                            fronta.Enqueue(element.last);
                        }
                        break;
                    case "verbs":
                        if (!element.child)
                        {
                            element.first = new SentenceMember("adverbs", "singular", false, null, null, null);
                            element.middle = new SentenceMember("verbs", "singular", true, null, null, null);

                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("adverbs").GetValue(dictonaries)).size_bits;
                            count--;

                            fronta.Enqueue(element.first);
                            fronta.Enqueue(element.middle);
                        }
                        else
                        {
                            
                            element.middle = new SentenceMember("verbs", "singular", true, null, null, null);
                            element.last = new SentenceMember("adverbs", "singular", false, null, null, null);

                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("adverbs").GetValue(dictonaries)).size_bits;
                            count--;

                            fronta.Enqueue(element.last);
                        }
                        break;
                    case "adverbs":
                        if (!element.child)
                        {
                            element.first = new SentenceMember("adverbs", "singular", true, null, null, null);
                            element.middle = new SentenceMember("conjunctions", "singular", false, null, null, null);
                            element.last = new SentenceMember("adverbs", "singular", false, null, null, null);

                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("adverbs").GetValue(dictonaries)).size_bits;
                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("conjunctions").GetValue(dictonaries)).size_bits;
                            count--;
                            count--;

                            fronta.Enqueue(element.first);
                            //fronta.Enqueue(element.last);
                        }
                        else
                        {

                            element.first = new SentenceMember("adverbs", "singular", true, null, null, null);
                            element.middle = new SentenceMember("conjunctions", "singular", true, null, null, null);
                            element.last = new SentenceMember("adverbs", "singular", false, null, null, null);

                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("adverbs").GetValue(dictonaries)).size_bits;
                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("conjunctions").GetValue(dictonaries)).size_bits;
                            count--;
                            count--;
                            
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

                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("adjectives").GetValue(dictonaries)).size_bits;
                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("conjunctions").GetValue(dictonaries)).size_bits;
                            count--;
                            count--;

                            fronta.Enqueue(element.first);
                            //fronta.Enqueue(element.last);
                        }
                        else
                        {

                            element.first = new SentenceMember("adjectives", "singular", true, null, null, null);
                            element.middle = new SentenceMember("conjunctions", "singular", true, null, null, null);
                            element.last = new SentenceMember("adjectives", "singular", false, null, null, null);

                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("adjectives").GetValue(dictonaries)).size_bits;
                            genBits -= ((DictionaryController.Element)this.dictonaries.GetType().GetField("conjunctions").GetValue(dictonaries)).size_bits;
                            count--;
                            count--;

                            fronta.Enqueue(element.first);
                            fronta.Enqueue(element.last);
                        }
                        break;
                }
            }
            return genBits;
        }
    }
}
