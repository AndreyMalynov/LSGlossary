using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using LSGlossary.Models;
using LSGlossary.Abstract;
using LSGlossary.Classes;

namespace LSGlossary.Controllers
{
    public class ValuesController : ApiController
    {
        private IUserContext user;

        public ValuesController()
        {
            // с куки разобраться 
            user = new UserContext(int.Parse(User.Identity.Name));
        }

        public IEnumerable<Word> GetWords()
        {
            return user.GetWords();
        }

        [HttpPost]
        public void AddWord([FromBody]string nameOfWord)
        {
            Parser parser = new Parser();
            Word temperWord = parser.OxfordDictionarieParser(nameOfWord);
            if (temperWord == null)
                user.AddWord(nameOfWord, "***", "***", "***");
            else
                user.AddWord(nameOfWord, temperWord.Pronunciation, temperWord.Definition, temperWord.Example);
        }

        [HttpPut]
        public void EditWord(int id, [FromBody]Word editedWord)
        {
            user.EditWord(editedWord);
        }

        public void DeleteWord(int id)
        {
            Word word = user.GetWordById(id);
            if (word != null)
                user.RemoveWordById(id);
        }
    }
}
