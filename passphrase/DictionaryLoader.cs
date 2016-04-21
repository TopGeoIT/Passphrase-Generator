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
        public static List<StreamReader> fillDictionary(AssetManager assets)
        {
            List<StreamReader> dictionary = new List<StreamReader>();

            string fileName;
            StreamReader file;

            using (StreamReader sr = new StreamReader(assets.Open("dictionaries.txt")))
            {
                while (!sr.EndOfStream)
                {
                    fileName = sr.ReadLine();
                    file = new StreamReader(assets.Open(fileName));
                    dictionary.Add(file);
                }
            }
                            
            return dictionary;
        }


    }
}