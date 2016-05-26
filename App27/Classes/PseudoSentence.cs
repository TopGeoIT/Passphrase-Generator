using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassphraseGen.Classes
{
    public class PseudoSentence
    {
        public PseudoSentenceElement[] sentence;
        public int bits;

        public PseudoSentence(PseudoSentenceElement[] sentence, int bits)
        {
            this.sentence = sentence;
            this.bits = bits;
        }
    }
}
