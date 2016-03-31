using PassphraseGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    public class PassphraseGenController : ApiController
    {

        

        public string Get([FromUri]int bits)
        {
            try {
                PassphraseController passphrasegen = new PassphraseController(AppDomain.CurrentDomain.BaseDirectory + "/dicts/"/*"c:/skola/8.semester/TP/Generator/Dictionaries/"*/);
                string[] veta = passphrasegen.generateSentenceFromEntrophy(bits);
                string celaveta = "";
                for (int i = 0; i < veta.Length; i++)
                {
                    celaveta += veta[i] + " ";
                }
                return celaveta;
            }catch(Exception e)
            {
                return e.Message + " " + e.InnerException;
            }
        }

        public string Post([FromBody]string value)
        {
            try
            {
                PassphraseController passphrasegen = new PassphraseController(AppDomain.CurrentDomain.BaseDirectory  + "/dicts/"/*"c:/skola/8.semester/TP/Generator/Dictionaries/"*/);
                string[] veta = passphrasegen.generateSentenceFromBinary(value);
                string celaveta = "";
                for (int i = 0; i < veta.Length; i++)
                {
                    celaveta += veta[i] + " ";
                }
                return celaveta;
            }
            catch (Exception e)
            {
                return e.Message + " " + e.InnerException;
            }
        }

        public string Put([FromBody]string value)
        {
            try
            {
                PassphraseController passphrasegen = new PassphraseController(AppDomain.CurrentDomain.BaseDirectory  + "/dicts/"/*"c:/skola/8.semester/TP/Generator/Dictionaries/"*/);
                string binary = passphrasegen.generateBinaryFromSentence(value);
                return binary;
            }
            catch (Exception e)
            {
                return e.Message + " " + e.InnerException;
            }
        }

    }
}
