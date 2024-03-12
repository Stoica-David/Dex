using Dex;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Dex
{
    public class Admin
    {
        public string Username { get; }
        private readonly string password;
        private readonly JsonHandler jsonHandler;

        public Admin(string username, string password)
        {
            Username = username;
            this.password = password;
            jsonHandler = new JsonHandler();
        }

        public bool Authenticate(string enteredPassword)
        {
            return password == enteredPassword;
        }

        public void AddWord(Word newWord)
        {
            List<Word> words = jsonHandler.ReadAllWords();
            words.Add(newWord);
            jsonHandler.AppendWords(words);
        }

        public void RemoveWord(string wordName)
        {
            List<Word> words = jsonHandler.ReadAllWords();
            Word wordToRemove = words.Find(word => word.Name == wordName);
            if (wordToRemove != null)
            {
                words.Remove(wordToRemove);
                jsonHandler.OverwriteFileWithText(words);
            }
        }

        public void ModifyWord(string wordName, Word modifiedWord)
        {
            List<Word> words = jsonHandler.ReadAllWords();
            Word wordToModify = words.Find(word => word.Name == wordName);
            if (wordToModify != null)
            {
                int index = words.IndexOf(wordToModify);
                words[index] = modifiedWord;
                jsonHandler.OverwriteFileWithText(words);
            }
        }
    }
}
