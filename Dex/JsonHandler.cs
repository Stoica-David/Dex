using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Drawing;
using System.Drawing.Imaging;
using Newtonsoft.Json;

namespace Dex
{
    public class JsonHandler
    {
        private readonly string adminsFilePath = "..\\..\\resources\\admins.json";
        private readonly string wordsFilePath = "..\\..\\resources\\words.json";

        public static string GetRelativePath(string absolutePath)
        {
            string newImageDirectory = Path.GetDirectoryName(absolutePath);
            string existingImagesDirectory = Path.GetDirectoryName(Path.GetFullPath("..\\..\\images\\"));

            bool isAbsoluteAnymore = true;

            if (!string.Equals(newImageDirectory, existingImagesDirectory))
            {
                string newPath = SaveNewImageToImages(absolutePath);
                if (newPath == "")
                    return "";
                else
                {
                    absolutePath = newPath;
                    isAbsoluteAnymore = false;
                }
            }

            // Get the current directory
            string currentDirectory = Directory.GetCurrentDirectory();

            // Create FileInfo instances
            FileInfo fileInfoAbsolute = new FileInfo(absolutePath);
            FileInfo fileInfoCurrent = new FileInfo(currentDirectory);

            // Compute relative path
            Uri uriAbsolute;
            Uri uriCurrent;
            Uri relativeUri;

            if (isAbsoluteAnymore)
            {
                uriAbsolute = new Uri(absolutePath);
                uriCurrent = new Uri(currentDirectory + Path.DirectorySeparatorChar);
                relativeUri = uriCurrent.MakeRelativeUri(uriAbsolute);
            }
            else
            {
                relativeUri = new Uri(absolutePath, UriKind.Relative);
            }

            // Convert relative Uri to string
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            return relativePath;
        }

        public static string SaveNewImageToImages(string absolutePath)
        {
            return SaveImage(absolutePath, "..\\..\\images");
        }

        static string SaveImage(string sourcePath, string destinationPath)
        {
            if (!Path.GetExtension(sourcePath).Equals(".jpg", StringComparison.OrdinalIgnoreCase))
            {
                return "";
            }

            string fileName = Path.GetFileName(sourcePath);
            // Load the image from the source path
            string finalPath = destinationPath + "\\" + fileName;
            using (Image image = Image.FromFile(sourcePath))
            {
                // Save the image to the destination path
                image.Save(finalPath);
            }

            return finalPath;
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

            if (wordToUpdate == null)
            {
                return false;
            }

            string newDescription = modifiedWord.Description;
            string newCategory = modifiedWord.Category;
            string newImagePath = modifiedWord.Path;

            if (newDescription != "")
            {
                wordToUpdate.Description = newDescription;
            }

            if (newImagePath != "")
            {
                wordToUpdate.Path = newImagePath;
            }

            if (newCategory != "")
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