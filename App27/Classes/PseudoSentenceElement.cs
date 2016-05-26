using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassphraseGen.Classes
{
    public class PseudoSentenceElement
    {
        public string type;
        public string spec;

        public PseudoSentenceElement(string type, string spec)
        {
            this.type = type;
            this.spec = spec;
        }
    }
}
