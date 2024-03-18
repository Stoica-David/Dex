using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Dex
{
    public class DexManager
    {
        private static DexManager instance;
        public static DexManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DexManager();
                }
                return instance;
            }
        }

        public ObservableCollection<Word> Words { get; set; }
        public ObservableCollection<Admin> Admins { get; set; }
        public ObservableCollection<string> Categories { get; set; }

        private readonly JsonHandler jsonHandler = new JsonHandler();

        private DexManager()
        {
            Words = new ObservableCollection<Word>(jsonHandler.ReadAllWords());
            Admins = new ObservableCollection<Admin>(jsonHandler.ReadAllAdmins());
            Categories = new ObservableCollection<string>(GetAllCategories(jsonHandler.ReadAllWords()));
        }

        private List<string> GetAllCategories(List<Word> words)
        {
            List<string> categories = new List<string>();
            categories.Add("All");

            foreach (Word word in words)
            {
                if (categories.FirstOrDefault(category => category == word.Category) == null)
                {
                    categories.Add(word.Category);
                }
            }

            return categories;
        }

        public bool AddWord(Word word)
        { 
            return jsonHandler.AppendWord(word);
        }

        public bool ModifyWord(Word word) 
        {
            return jsonHandler.ModifyWord(word);   
        }

        public bool RemoveWord(string wordName)
        {
            return jsonHandler.RemoveWord(wordName);
        }

        public List<Word> Get5RandomWords()
        {
            int wordsNumber = Words.Count();

            if(wordsNumber< 5)
            {
                return new List<Word>();
            }

            Random random = new Random();

            List<int> indexes = new List<int>();
            while (indexes.Count < 5)
            {
                int randomNumber = random.Next(0, wordsNumber);
                if (!indexes.Contains(randomNumber))
                {
                    indexes.Add(randomNumber);
                }
            }

            List<Word> words = new List<Word>();

            foreach(int i in indexes)
            {
                words.Add(Words[i]);
            }

            return words;
        }
    }
}