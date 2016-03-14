using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassphraseGen.Classes
{
    class Conjunction
    {
        public string value;
        public string type;

        public Conjunction(string value, string type)
        {
            this.value = value;
            this.type = type;
        }
    }
}
