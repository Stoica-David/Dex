using Dex;
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
            // TODO: Here
        }

        private void Button_Click_Modify(object sender, RoutedEventArgs e)
        {
            isModifiyEnabled = true;
        }

        private void Button_Click_Remove(object sender, RoutedEventArgs e)
        {
            // TODO: Here
        }
    }
}