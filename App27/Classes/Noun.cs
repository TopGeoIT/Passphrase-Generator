using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassphraseGen.Classes
{
    class Noun
    {
        public string value;
        public string singular;
        public string plural;

        public Noun(string value, string sing, string plur)
        {
            this.value = value;
            this.singular = sing;
            this.plural = plur;
        }
    }
}
