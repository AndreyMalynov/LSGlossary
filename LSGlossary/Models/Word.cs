using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSGlossary.Models
{
    public class Word
    {
        public Word()
        {

        }

        public Word(string name, string pronunciation, string definition, string example)
        {
            Name = name;
            Pronunciation = pronunciation;
            Definition = definition;
            Example = example;
        }

        public string Name { get; set; }
        public string Pronunciation { get; set; }
        public string Definition { get; set; }
        public string Example { get; set; }
    }
}