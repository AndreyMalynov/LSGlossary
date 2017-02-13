using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LSGlossary.Models;
namespace LSGlossary.Abstract
{
    interface IUsersContext
    {
        void AddUserIdentity(string login);
        int GetUserIdByLogin(string login);
        void OpenOrCreate();
        void SaveChanges(); 
        List<UserIdentity> Users { get; set; }
    }
}
