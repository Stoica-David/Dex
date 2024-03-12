using System.Collections.Generic;
using System.IO;
using System.Xml;
using Newtonsoft.Json;

namespace Dex
{
    public class JsonHandler
    {
        private readonly string adminsFilePath = "admins.json";
        private readonly string wordsFilePath = "words.json";

        public List<Admin> ReadAllAdmins()
        {
            List<Admin> admins = new List<Admin>();
            if (File.Exists(adminsFilePath))
            {
                string json = File.ReadAllText(adminsFilePath);
                admins = JsonConvert.DeserializeObject<List<Admin>>(json);
            }
            return admins;
        }

        public List<Word> ReadAllWords()
        {
            List<Word> words = new List<Word>();
            if (File.Exists(wordsFilePath))
            {
                string json = File.ReadAllText(wordsFilePath);
                words = JsonConvert.DeserializeObject<List<Word>>(json);
            }
            return words;
        }

        public void AppendWord(Word word)
        {
            List<Word> words = ReadAllWords();
            words.Add(word);
            string json = JsonConvert.SerializeObject(words, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(wordsFilePath, json);
        }

        public void AppendWords(List<Word> newWords)
        {
            List<Word> words = ReadAllWords();
            words.AddRange(newWords);
            string json = JsonConvert.SerializeObject(words, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(wordsFilePath, json);
        }

        public void OverwriteFileWithText(List<Word> newWords)
        {
            string jsonObject = JsonConvert.SerializeObject(newWords, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(wordsFilePath, jsonObject);
        }
    }
}