using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LSGlossary.Abstract;
using System.Xml.Serialization;
using System.IO;
using System.Web.Mvc;

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

        public string GetLogin()
        {
            return User.Login;
        }

        private int GetLastFreeIdOfWords()
        {
            if (User.Words.Count > 0)
                return User.Words.Aggregate((x, y) => (x.Id > y.Id) ? x : y).Id + 1;
            return 0;
        }

        public void AddWord(string name, string pronunciation, string definition, string example)
        {
            int id = GetLastFreeIdOfWords();
            Word word = new Word(id, name, pronunciation, definition, example);
            User.Words.Add(word);
            SaveChanges();
        }

        public Word GetWordById(int id)
        {
            return User.Words.Find(x => x.Id == id);
        }

        public void RemoveWordById(int id)
        {
            User.Words.Remove(GetWordById(id));
            SaveChanges();
        }

        public void EditWord(Word editedWord)
        {
            Word word = GetWordById(editedWord.Id);

            word.Name = editedWord.Name;
            word.Pronunciation = editedWord.Pronunciation;
            word.Definition = editedWord.Definition;
            word.Example = editedWord.Example;

            SaveChanges();
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
                    using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Users/" + userId + ".xml"), FileMode.OpenOrCreate))
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
                using (FileStream fs = new FileStream(HttpContext.Current.Server.MapPath("~/App_Data/Users/" + User.Id + ".xml"), FileMode.Create))
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