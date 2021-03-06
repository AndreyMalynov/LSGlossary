﻿using System;
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
        void AddWord(string nameOfWord, string v1, string v2, string v3);
        void EditWord(Word editedWord);
        Word GetWordById(int id);
        void RemoveWordById(int id);
    }
}
