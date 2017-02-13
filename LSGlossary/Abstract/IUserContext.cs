using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSGlossary.Models;

namespace LSGlossary.Abstract
{
    interface IUserContext
    {
        List<Word> GetWords();
        void CreateUser(int userId);
        void Open();
        void SaveChanges();        
    }
}
