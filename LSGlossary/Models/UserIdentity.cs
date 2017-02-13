using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LSGlossary.Models
{
    public class UserIdentity
    {
        public UserIdentity()
        {

        }

        public UserIdentity(string login, int id)
        {
            Login = login;
            Id = id;
        }
        public string Login { get; set; }
        public int Id { get; set; }
    }
}