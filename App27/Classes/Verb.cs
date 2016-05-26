using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassphraseGen.Classes
{
    class Verb
    {
        public string presentSingular;
        public string presentPlural;

        public Verb(string presentSingular, string presentPlural)
        {
            this.presentSingular = presentSingular;
            this.presentPlural = presentPlural;
        }
    }
}
