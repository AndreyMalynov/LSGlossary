using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using LSGlossary.Models;
using LSGlossary.Abstract;

namespace LSGlossary.Controllers
{
    public class ValuesController : ApiController
    {
        private IUserContext user;

        public ValuesController()
        {
            user = new UserContext(int.Parse(User.Identity.Name));
        }

        public IEnumerable<Word> GetWords()
        {
            return user.GetWords();
        }
    }
}
