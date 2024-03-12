using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex
{
    public class Word
    {
        // Properties
        public string Name { get; set; }
        public string Description { get; set; }
        public string Path { get; set; }
        public string Category { get; set; }

        // Default image path
        private const string DefaultImagePath = "../../images/default.jpg";

        // Constructor
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