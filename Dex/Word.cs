using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex
{
    public class Word
    {
        private const string DefaultImagePath = "C:\\Facultate\\MAP\\Tema 1 - Dex\\Dex\\images\\default.jpg";
 
        // PROPERTIES
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string Category { get; set; }

        // CONSTRUCTORS
        public Word()
        {
            Path = DefaultImagePath;
        }

        public Word(string name, string description, string imagePath, string category)
        {
            Name = name;
            Description = description;
            Path = imagePath;
            Category = category;
        }

        public Word(string name, string description, string category)
        {
            Name = name;
            Description = description;
            Path = DefaultImagePath;
            Category = category;
        }
    }
}