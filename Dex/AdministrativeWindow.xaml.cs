using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Dex
{
    public partial class AdministrativeWindow : Window
    {
        private string currentRelativePath = "";

        public AdministrativeWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            string nameOfTheWord = wordTextBox.Text.ToLower();
            string descriptionOfTheWord = descriptionTextBox.Text;
            string categoryOfTheWord = categoryTextBox.Text.ToLower();

            if (nameOfTheWord != "" && categoryOfTheWord != "" && descriptionOfTheWord != "")
            {
                Word wordToAdd = new Word();

                if (currentRelativePath != "")
                {
                    wordToAdd = new Word(nameOfTheWord, descriptionOfTheWord, currentRelativePath, categoryOfTheWord);
                }
                else
                {
                    wordToAdd = new Word(nameOfTheWord, descriptionOfTheWord, categoryOfTheWord);
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

        private void Button_Click_Modify(object sender, RoutedEventArgs e)
        {
            string nameOfTheWord = wordTextBox.Text.ToLower();
            string descriptionOfTheWord = descriptionTextBox.Text;
            string categoryOfTheWord = categoryTextBox.Text.ToLower();

            if(nameOfTheWord != "" && (descriptionOfTheWord != "" || categoryOfTheWord != "" || selectedImageControl.Source != null))
            {
                Word wordToModify = new Word();

                if(currentRelativePath != "")
                { 
                    wordToModify = new Word(nameOfTheWord, descriptionOfTheWord, currentRelativePath, categoryOfTheWord);
                }
                else
                {
                    wordToModify = new Word(nameOfTheWord, descriptionOfTheWord, categoryOfTheWord);
                }

                if(!DexManager.Instance.ModifyWord(wordToModify))
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

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            descriptionTextBox.Clear();
            categoryTextBox.Clear();
            selectedImageControl.Source = null;
            currentRelativePath = "";

            string nameOfTheWord = wordTextBox.Text.ToLower();

            if (nameOfTheWord != "")
            {
                if(!DexManager.Instance.RemoveWord(nameOfTheWord))
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
        }

        private void SelectImageButton_Click(object sender, RoutedEventArgs e)
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
                DisplaySelectedImage(selectedFilePath);
            }
        }

        private void DisplaySelectedImage(string imagePath)
        {
            try
            {
                // Create BitmapImage
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(imagePath);
                bitmap.EndInit();

                // Set image source of the Image control
                selectedImageControl.Source = bitmap;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading image: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClearScene()
        {
            wordTextBox.Clear();
            descriptionTextBox.Clear();
            categoryTextBox.Clear();
            selectedImageControl.Source = null;
            currentRelativePath = "";
        }
    }
}