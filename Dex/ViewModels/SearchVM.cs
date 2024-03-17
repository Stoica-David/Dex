using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Dex.ViewModels
{
    public class SearchVM : BaseVM
    {
        // PROPERTIES

        private string searchedString;
        public string SearchedString
        {
            get
            {
                return searchedString;
            }
            set
            {
                searchedString = value;
                ChangeFilteredList();
                OnPropertyChanged();
            }
        }

        private string selectedWordName;
        public string SelectedWordName
        {
            get
            {
                return selectedWordName;
            }

            set
            {
                selectedWordName = value;
                UpdateWordByName();
                OnPropertyChanged();
            }
        }

        private Word selectedWord;
        public Word SelectedWord
        {
            get
            {
                return selectedWord;
            }

            set
            {
                selectedWord = value;
                OnPropertyChanged();
            }
        }

        private BitmapImage selectedImage;
        public BitmapImage SelectedImage
        {
            get { return selectedImage; }

            set
            {
                selectedImage = value;
                OnPropertyChanged();
            }
        }

        private string selectedCategory;
        public string SelectedCategory
        {
            get { return selectedCategory; }

            set
            {
                selectedCategory = value;
                ChangeFilteredList();
                OnPropertyChanged();
            }
        }

        public delegate void SwitchToMenu();
        public SwitchToMenu OnSwitchToMenu { get; set; }

        private List<string> filteredList;
        public List<string> FilteredList
        {
            get
            {
                return filteredList;
            }

            set
            {
                filteredList = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<string> categories;
        public ObservableCollection<string> Categories
        {
            get
            {
                return categories;
            }

            set
            {
                categories = value;
                OnPropertyChanged();
            }
        }

        // COMMANDS

        private ICommand switchToMenuCommand;
        public ICommand SwitchToMenuCommand
        {
            get
            {
                if (switchToMenuCommand == null)
                {
                    switchToMenuCommand = new RelayCommand(o => true, o => { OnSwitchToMenu(); });
                }

                return switchToMenuCommand;
            }
        }

        // METHODS

        public SearchVM()
        {
            SearchedString = string.Empty;
            FilteredList = DexManager.Instance.Words.Select(word => word.Name).Where(name => name.StartsWith(searchedString)).ToList();
            Categories = DexManager.Instance.Categories;
            SelectedCategory = Categories[0];
        }

        public void ChangeFilteredList()
        {
            if(SearchedString == "")
            {
                FilteredList = new List<string>();
                return;
            }

            if (SelectedCategory == "All")
            {
                FilteredList = DexManager.Instance.Words.Select(word => word.Name).Where(name => name.StartsWith(SearchedString)).ToList();
            }
            else
            {
                var filteredByCategory = DexManager.Instance.Words.Where(word => word.Category == SelectedCategory);

                FilteredList = filteredByCategory.Select(word => word.Name).Where(name => name.StartsWith(SearchedString)).ToList();
            }
        }

        public void UpdateWordByName()
        {
            SelectedWord = DexManager.Instance.Words.FirstOrDefault(word => word.Name == selectedWordName);

            if (SelectedWord != null)
            {
                DisplaySelectedImage(SelectedWord.Path);
            }
            else
            {
                SelectedImage = null;
            }
        }

        private void DisplaySelectedImage(string imagePath)
        {
            try
            {
                // Create BitmapImage
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath, UriKind.Relative);
                bitmap.EndInit();

                // Set image source of the Image control
                SelectedImage = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
