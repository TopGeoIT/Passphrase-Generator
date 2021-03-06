﻿using PassphraseGen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Web.Controllers
{
    public class PassphraseGenSentenceController : ApiController
    {
        public string Post([FromBody]string value)
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
