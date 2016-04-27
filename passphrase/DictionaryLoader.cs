using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System.IO;
using Android.Content.Res;

namespace passphrase
{
    class DictionaryLoader
    {
        public Dictionary<string, string> fillDictionary(AssetManager assets)
        {
            Dictionary<string, string> dictionary = new Dictionary<string, string>();

            string data;
            StreamReader file;

            using (StreamReader sr = new StreamReader(assets.Open("dictionaries.txt")))
            {
                while (!sr.EndOfStream)
                {
                    data = sr.ReadLine();
                    string[]  fileName = data.Split(' ');

                    if (fileName[0] != null && fileName[1] != null) {
                        file = new StreamReader(assets.Open(fileName[1]));

                        string text = file.ReadToEnd();
                        dictionary.Add(fileName[0], String.Copy(text));
                    }
                    else
                    {
                        return null;
                    }
                }
            }
                            
            return dictionary;
        }


    }
}