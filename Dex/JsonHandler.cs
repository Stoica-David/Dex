using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Xml;
using Newtonsoft.Json;

namespace Dex
{
    public class JsonHandler
    {
        private readonly string adminsFilePath = "..\\..\\admins.json";
        private readonly string wordsFilePath = "..\\..\\words.json";

        public static string GetRelativePath(string absolutePath)
        {
            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Create FileInfo instances
            FileInfo fileInfoAbsolute = new FileInfo(absolutePath);
            FileInfo fileInfoCurrent = new FileInfo(currentDirectory);

            // Compute relative path
            Uri uriAbsolute = new Uri(absolutePath);
            Uri uriCurrent = new Uri(currentDirectory + Path.DirectorySeparatorChar);
            Uri relativeUri = uriCurrent.MakeRelativeUri(uriAbsolute);

            // Convert relative Uri to string
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath;
        }

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

        public bool AppendWord(Word word)
        {
            if (DexManager.Instance.Words.FirstOrDefault(currWord => currWord.Name == word.Name) != null)
            {
                return false;
            }

            if (DexManager.Instance.Categories.FirstOrDefault(category => category == word.Category) == null)
            {
                DexManager.Instance.Categories.Add(word.Category);
            }

            DexManager.Instance.Words.Add(word);

            string json = JsonConvert.SerializeObject(DexManager.Instance.Words, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(wordsFilePath, json);

            return true;
        }

        public bool ModifyWord(Word modifiedWord)
        {
            Word wordToUpdate = DexManager.Instance.Words.FirstOrDefault(word => word.Name == modifiedWord.Name);

            if(wordToUpdate == null)
            {
                return false;
            }

            string newDescription = modifiedWord.Description;
            string newCategory = modifiedWord.Category;
            string newImagePath = modifiedWord.ImagePath;

            if (newDescription != "")
            {
                wordToUpdate.Description = newDescription;
            }

            if (newImagePath != "")
            {
                wordToUpdate.ImagePath = newImagePath;
            }

            if(newCategory != "")
            {
                if (DexManager.Instance.Categories.Count(category => category == wordToUpdate.Category) == 1)
                {
                    DexManager.Instance.Categories.Remove(wordToUpdate.Category);
                }
                else if (DexManager.Instance.Categories.Count(category => category == newCategory) == 0)
                {
                    DexManager.Instance.Categories.Add(newCategory);
                }

                wordToUpdate.Category = newCategory;
            }

            string json = JsonConvert.SerializeObject(DexManager.Instance.Words, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(wordsFilePath, json);

            return true;
        }

        public bool RemoveWord(string wordName)
        {
            Word wordToRemove = DexManager.Instance.Words.FirstOrDefault(word => word.Name == wordName);

            if (wordToRemove == null)
            {
                return false;
            }

            if (DexManager.Instance.Categories.Count(category => category == wordToRemove.Category) == 1)
            {
                DexManager.Instance.Categories.Remove(wordToRemove.Category);
            }

            DexManager.Instance.Words.Remove(wordToRemove);

            string json = JsonConvert.SerializeObject(DexManager.Instance.Words, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(wordsFilePath, json);

            return true;
        }
    }
}