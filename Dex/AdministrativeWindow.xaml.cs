using Dex;
using DexOnline.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Dex
{
    public partial class AdministrativeWindow : Window
    {
        private List<Word> words { get; set; }
        private List<String> texBoxWords { get; set; }

        private bool isModifiyEnabled { get; set; } = false;

        private String currentWord { get; set; }
        private String currentCategory { get; set; }

        public AdministrativeWindow()
        {
            InitializeComponent();

            var controller = Controller.GetInstance();

            words = controller.GetWords();
            var categories = controller.GetCategories();

            foreach (Word word in words)
            {
                wordTextBox.Items.Add(word.Name);
            }

            categoryTextBox.Items.Add("All");
            foreach (String category in categories)
            {
                categoryTextBox.Items.Add(category);
            }

            categoryTextBox.SelectedIndex = 0;
            currentCategory = "All";
            texBoxWords = [];
        }

        private void CategorySelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            currentCategory = categoryTextBox.Text;
        }
        private void Button_Click_Back(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void searchBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (wordTextBox.SelectedItem == null)
            {
                return;
            }

            var selectedText = wordTextBox.SelectedItem.ToString();
            if (selectedText == null)
                return;

            var word = GetWord(wordTextBox.SelectedItem.ToString().ToLower());

            if (word.Name != "")
            {
                defArea.Text = word.Description;
            }
        }

        private void OnSearchBoxTextChanged(object sender, KeyEventArgs e)
        {
            if (wordTextBox.Text == null)
                return;

            if (e.Key == Key.Enter && wordTextBox.Text != "")
            {
                if (isModifiyEnabled)
                {
                    Controller.GetInstance().ModifyWord(currentWord.ToLower(), new Word(wordTextBox.Text.Trim(), defArea.Text, "", categoryTextBox.Text));
                    MessageBox.Show("Word modified successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                var word = GetWord(wordTextBox.Text.ToString().ToLower());

                if (word.Name != "")
                {
                    defArea.Text = word.Description;
                }

                return;
            }

            wordTextBox.IsDropDownOpen = true;
            if (wordTextBox.Text == "")
            {
                foreach (Word word in words)
                {
                    wordTextBox.Items.Add(word.Name);
                }

                texBoxWords = [];
            }

            texBoxWords = [];
            string currText = wordTextBox.Text.ToString();
            foreach (var wordObj in words)
            {
                var word = wordObj.Name.ToLower();
                if (word.StartsWith(currText.ToLower()))
                    texBoxWords.Add(wordObj.Name);
            }

            wordTextBox.Items.Clear();
            foreach (var word in texBoxWords)
            {
                wordTextBox.Items.Add(word);
            }

            wordTextBox.Text = currText;
        }

        private Word GetWord(string word)
        {
            foreach (Word wordObj in words)
            {
                if (wordObj.Name.ToLower().Equals(word))
                    return wordObj;
            }

            return new Word("", "", "", "");
        }

        private void Button_Click_Add(object sender, RoutedEventArgs e)
        {
            if (wordTextBox.Text == "" || defArea.Text == "")
            {
                MessageBox.Show("A word must have a name and a definition!", "Error!", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            Controller.GetInstance().AddWord(new Word(wordTextBox.Text, defArea.Text, "", categoryTextBox.Text));

            words.Add(new Word(wordTextBox.Text, defArea.Text, "", categoryTextBox.Text));
            MessageBox.Show("Word added successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Button_Click_Modify(object sender, RoutedEventArgs e)
        {
            isModifiyEnabled = true;
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            var word = GetWord(wordTextBox.Text.ToLower());
            if (word.GetWord() != "")
            {
                Controller.GetInstance().RemoveWord(word);
                MessageBox.Show("Word removed successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
    }
}