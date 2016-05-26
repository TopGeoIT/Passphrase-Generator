using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PassphraseGen.Classes
{
    public class BuildObj
    {
        public string word;
        public string type;

        public BuildObj(string word, string type)
        {
            this.word = word;
            this.type = type;
        }
    }
}
