using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSGlossary.Models
{
    public class User
    {
        public User()
        {
            Words = new List<Word>();
        }

        public User(string login, int id)
        {
            Login = login;
            Id = id;
            Words = new List<Word>();
        }

        public string Login { get; set; }
        public int Id { get; set; }
        public List<Word> Words { get; set; }
    }
}