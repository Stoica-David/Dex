using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Dex.ViewModels
{
    public class AdministrativeVM : BaseVM
    {
        private string currentRelativePath = "";

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

        private string nameOfTheWord;
        public string NameOfTheWord
        {
            get
            {
                return nameOfTheWord;
            }

            set
            {
                nameOfTheWord = value;
                OnPropertyChanged();
            }
        }

        private string descriptionOfTheWord;
        public string DescriptionOfTheWord
        {
            get
            {
                return descriptionOfTheWord;
            }

            set
            {
                descriptionOfTheWord = value;
                OnPropertyChanged();
            }
        }

        private string categoryOfTheWord;
        public string CategoryOfTheWord
        {
            get
            {
                return categoryOfTheWord;
            }

            set
            {
                categoryOfTheWord = value;
                OnPropertyChanged();
            }
        }

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

        private ICommand buttonAddCommand;
        public ICommand ButtonAddCommand
        {
            get
            {
                if (buttonAddCommand == null)
                {
                    buttonAddCommand = new RelayCommand(o => true, ButtonClickAdd);
                }

                return buttonAddCommand;
            }
        }

        private ICommand buttonModifyCommand;
        public ICommand ButtonModifyCommand
        {
            get
            {
                if (buttonModifyCommand == null)
                {
                    buttonModifyCommand = new RelayCommand(o => true, ButtonClickModify);
                }

                return buttonModifyCommand;
            }
        }

        private ICommand buttonRemoveCommand;
        public ICommand ButtonRemoveCommand
        {
            get
            {
                if (buttonRemoveCommand == null)
                {
                    buttonRemoveCommand = new RelayCommand(o => true, ButtonClickRemove);
                }

                return buttonRemoveCommand;
            }
        }

        private ICommand buttonSelectImageCommand;
        public ICommand ButtonSelectImageCommand
        {
            get
            {
                if (buttonSelectImageCommand == null)
                {
                    buttonSelectImageCommand = new RelayCommand(o => true, ButtonClickSelectImage);
                }

                return buttonSelectImageCommand;
            }
        }

        public delegate void SwitchToMenu();
        public SwitchToMenu OnSwitchToMenu { get; set; }

        public AdministrativeVM()
        {
        }

        private void ButtonClickAdd(object param)
        {
            if (nameOfTheWord != "" && categoryOfTheWord != "" && descriptionOfTheWord != "")
            {
                Word wordToAdd = new Word();

                if (currentRelativePath != "")
                {
                    wordToAdd = new Word(nameOfTheWord.ToLower(), descriptionOfTheWord, currentRelativePath, categoryOfTheWord.ToLower());
                }
                else
                {
                    wordToAdd = new Word(nameOfTheWord.ToLower(), descriptionOfTheWord, categoryOfTheWord.ToLower());
                }

                if (!DexManager.Instance.AddWord(wordToAdd))
                {
                    MessageBox.Show("The word is already in the file!");
                }
                else
                {
                    MessageBox.Show("The word was added to the file!");
                    ClearScene();
                }
            }
            else
            {
                MessageBox.Show("The word, description and category fields must be filled in order to add a word.");
            }
        }

        private void ButtonClickModify(object param)
        {
            if (nameOfTheWord != "" && (descriptionOfTheWord != "" || categoryOfTheWord != "" || selectedImage != null))
            {
                Word wordToModify = new Word();

                if (currentRelativePath != "")
                {
                    wordToModify = new Word(nameOfTheWord.ToLower(), descriptionOfTheWord, currentRelativePath, categoryOfTheWord.ToLower());
                }
                else
                {
                    wordToModify = new Word(nameOfTheWord.ToLower(), descriptionOfTheWord, categoryOfTheWord.ToLower());
                }

                if (!DexManager.Instance.ModifyWord(wordToModify))
                {
                    MessageBox.Show("The word could not be modified!");
                }
                else
                {
                    MessageBox.Show("The word was succesfully modified!");
                    ClearScene();
                }
            }
            else
            {
                MessageBox.Show("There were not enough fields filled in order to modify a word.");
            }
        }

        private void ButtonClickRemove(object param)
        {
            if (nameOfTheWord != "")
            {
                if (!DexManager.Instance.RemoveWord(nameOfTheWord.ToLower()))
                {
                    MessageBox.Show("The given word is already not in the file!");
                }
                else
                {
                    MessageBox.Show("The given word was removed from the file!");
                }
            }
            else
            {
                MessageBox.Show("The word field must be filled in order to remove a word.");
            }

            ClearScene();
        }

        private void ButtonClickSelectImage(object param)
        {
            // Create OpenFileDialog
            OpenFileDialog openFileDialog = new OpenFileDialog();

            // Set filter for file extension and default file extension
            openFileDialog.Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All Files (*.*)|*.*";
            openFileDialog.FilterIndex = 1;
            openFileDialog.Multiselect = false;

            // Show OpenFileDialog
            bool? result = openFileDialog.ShowDialog();

            // Process result
            if (result == true)
            {
                // Get selected file name
                string selectedFilePath = openFileDialog.FileName;

                // Compute relative path
                currentRelativePath = JsonHandler.GetRelativePath(selectedFilePath);

                // Display selected image
                DisplaySelectedImage(currentRelativePath);
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

        private void ClearScene()
        {
            NameOfTheWord = string.Empty;
            CategoryOfTheWord = string.Empty;
            DescriptionOfTheWord = string.Empty;
            currentRelativePath = string.Empty;
            SelectedImage = null;
        }
    }
}
