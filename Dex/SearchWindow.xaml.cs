using System.Windows;

namespace Dex
{
    public partial class SearchWindow : Window
    {
        public SearchWindow()
        {
            InitializeComponent();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            string searchQuery = searchTextBox.Text;
            // Implement your search logic here
            MessageBox.Show($"Searching for: {searchQuery}");
        }
    }
}