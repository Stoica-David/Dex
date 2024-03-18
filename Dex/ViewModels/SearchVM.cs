using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
                NameLabel = "Name:";
                DescriptionLabel = "Description:";
                CategoryLabel = "Category:";
                ImageLabel = "Image:";
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

        private string nameLabel;
        public string NameLabel
        {
            get
            {
                return nameLabel;
            }

            set
            {
                nameLabel = value;
                OnPropertyChanged();
            }
        }

        private string descriptionLabel;
        public string DescriptionLabel
        {
            get
            {
                return descriptionLabel;
            }

            set
            {
                descriptionLabel = value;
                OnPropertyChanged();
            }
        }

        private string categoryLabel;
        public string CategoryLabel
        {
            get
            {
                return categoryLabel;
            }

            set
            {
                categoryLabel = value;
                OnPropertyChanged();
            }
        }

        private string imageLabel;
        public string ImageLabel
        {
            get
            {
                return imageLabel;
            }

            set
            {
                imageLabel = value;
                OnPropertyChanged();
            }
        }

        // DELEGATES

        public delegate void SwitchToMenu();
        public SwitchToMenu OnSwitchToMenu { get; set; }


        // SEARCH SPECIFIC

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
            FilteredList = DexManager.Instance.Words.Select(word => word.Name).Where(name => name.StartsWith(SearchedString)).ToList();
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
                FilteredList = DexManager.Instance.Words.Select(word => word.Name).Where(name => name.ToLower().StartsWith(SearchedString.ToLower())).ToList();
            }
            else
            {
                var filteredByCategory = DexManager.Instance.Words.Where(word => word.Category == SelectedCategory);

                FilteredList = filteredByCategory.Select(word => word.Name).Where(name => name.ToLower().StartsWith(SearchedString.ToLower())).ToList();
            }
        }

        public void UpdateWordByName()
        {
            SelectedWord = DexManager.Instance.Words.FirstOrDefault(word => word.Name.ToLower() == SelectedWordName.ToLower());

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
