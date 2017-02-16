using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSGlossary.Models
{
    public class UserForView
    {
        public UserForView(string login, List<Word> words)
        {
            Login = login;
            Words = words;
        }

        public string Login { get; set; }
        public List<Word> Words { get; set; }
    }
}