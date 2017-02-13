using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LSGlossary.Abstract;
using System.Xml.Serialization;
using System.IO;

namespace LSGlossary.Models
{
    public class UserContext : IDisposable, IUserContext
    {
        private XmlSerializer formatter;
        private User User { get; set; }

        public UserContext(int userId)
        {
            formatter = new XmlSerializer(typeof(User));
            OpenOrCreate(userId);
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public void CreateUser(int userId)
        {
            using (UsersContext users = new UsersContext())
            {
                string login = users.GetUserLoginById(userId);
                User = new User(login, userId);   
                SaveChanges();
            }
        }

        public List<Word> GetWords()
        {
            return User.Words;
        }

        public void Open()
        {
            //выбросит исключение если файл еще не создан
            try
            {
                using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Users/" + User.Id + ".xml"), FileMode.OpenOrCreate))
                {
                    User = (User)formatter.Deserialize(fs);
                }
            }

            //сработет в случае если файла еще не существует. создаст его
            catch
            {
                new Exception("Неизвестная ошибка, мы уже работаем");
            }
        }

        public void OpenOrCreate(int userId)
        {
            try
            {

                //выбросит исключение если файл еще не создан
                try
                {
                    using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Users/" + userId + ".xml"), FileMode.OpenOrCreate))
                    {
                        User = (User)formatter.Deserialize(fs);
                    }
                }

                //сработет в случае если файла еще не существует. создаст его
                catch
                {
                    CreateUser(userId);
                    using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Users" + userId + ".xml"), FileMode.OpenOrCreate))
                    {
                        formatter.Serialize(fs, User);
                    }
                }
            }
            catch
            {
                new Exception("Неизвестная ошибка, мы уже работаем");
            }
        }

        public void SaveChanges()
        {
            try
            {
                using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Users" + User.Id + ".xml"), FileMode.Create))
                {
                    formatter.Serialize(fs, User);
                }
            }
            catch
            {
                new Exception("Неизвестная ошибка, мы уже работаем");
            }
        }
    }
}