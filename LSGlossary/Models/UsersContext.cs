using System;
using System.Collections.Generic;
using System.Web;
using LSGlossary.Abstract;
using LSGlossary.Models;
using System.Xml.Serialization;
using System.IO;
using System.Linq;

namespace LSGlossary.Models
{
    public class UsersContext : IDisposable, IUsersContext
    {

        private XmlSerializer formatter;
        private static object lockObj;

        public UsersContext()
        {
            lockObj = new object();
            formatter = new XmlSerializer(typeof(List<UserIdentity>));
            OpenOrCreate();
        }

        public List<UserIdentity> Users { get; set; }

        public void AddUserIdentity(string login)
        {
            lock (lockObj)
            {
                OpenOrCreate();
                UserIdentity newUserIdentity = new UserIdentity(login, GetLastFreeId());
                Users.Add(newUserIdentity);
                SaveChanges();
            }
        }

        public void Dispose()
        {
        }

        private int GetLastFreeId()
        {
            if (Users.Count > 0)
                return Users.Aggregate((x, y) => (x.Id > y.Id) ? x : y).Id + 1;
            return 0;
        }

        public int GetUserIdByLogin(string login)
        {
            UserIdentity userIdentity = Users.Find(x => x.Login == login);
            return (userIdentity == null) ? -1 : userIdentity.Id;
        }

        public string GetUserLoginById(int id)
        {
            UserIdentity userIdentity = Users.Find(x => x.Id == id);
            return userIdentity.Login;
        }
        
        
        public void OpenOrCreate()
        {
            lock (lockObj)
            {
                try
                {

                    //выбросит исключение если файл еще не создан
                    try
                    {
                        using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Users.xml"), FileMode.OpenOrCreate))
                        {
                            Users = (List<UserIdentity>)formatter.Deserialize(fs);
                        }
                    }

                    //сработет в случае если файла еще не существует. создаст его
                    catch
                    {
                        Users = new List<UserIdentity>();
                        using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Users.xml"), FileMode.OpenOrCreate))
                        {
                            formatter.Serialize(fs, Users);
                        }
                    }
                }
                catch
                {
                    new Exception("Неизвестная ошибка, мы уже работаем");
                }
            }
        }

        public void SaveChanges()
        {
            lock (lockObj)
            {
                try
                {
                    using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Users.xml"), FileMode.Create))
                    {
                        formatter.Serialize(fs, Users);
                    }
                }
                catch
                {
                    new Exception("Неизвестная ошибка, мы уже работаем");
                }
            }
        }
    }
}