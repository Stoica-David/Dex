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

        public ObservableCollection<Word> Words { get; private set; }
        public ObservableCollection<Admin> Admins { get; private set; }
        public ObservableCollection<string> Categories { get; private set; }

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

            foreach (Word word in words)
            {
                if (categories.FirstOrDefault(category => category == word.Category) == null)
                { 
                    categories.Add(word.Category);
                }
            }

            return categories;
        }
    }
}