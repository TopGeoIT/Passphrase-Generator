using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassphraseGen.Classes
{
    public class SentenceMember
    {
        public string type;
        public string spec;
        public bool child;
        public SentenceMember first;
        public SentenceMember middle;
        public SentenceMember last;

        public SentenceMember(string type, string spec, bool child, SentenceMember first, SentenceMember middle, SentenceMember last)
        {
            this.type = type;
            this.spec = spec;
            this.child = child;
            this.first = first;
            this.middle = middle;
            this.last = last;
        }
    };
}
