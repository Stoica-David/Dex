using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Data;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Dex
{ 
    public partial class SearchWindow : Window
    {
        private ICollectionView collectionView;

        public SearchWindow()
        {
            InitializeComponent();

            // Create a CollectionViewSource
            CollectionViewSource collectionViewSource = new CollectionViewSource();
            collectionViewSource.Source = DexManager.Instance.Words;

            // Create an ICollectionView for filtering
            collectionView = collectionViewSource.View;
            collectionView.Filter = Filter;

            // Set the ItemsSource of the ListBox to the ICollectionView
            resultsListBox.ItemsSource = collectionView;
        }

        private bool Filter(object item)
        {
            // Implement the filtering logic here
            if (string.IsNullOrWhiteSpace(searchTextBox.Text))
                return false; // Show all items if search text is empty

            Word word = item as Word;
            return word != null && word.Name.ToLower().Contains(searchTextBox.Text.ToLower());
        }

        private void SearchTextBox_TextChanged(object sender, RoutedEventArgs e)
        {
            // Refresh the view to apply the filter
            collectionView.Refresh();
        }

        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected word from the search results
            string searchBoxString = searchTextBox.Text;

            Word selectedWord = DexManager.Instance.Words.FirstOrDefault(word => word.Name == searchBoxString);

            // Check if a word is selected
            if (selectedWord != null)
            {
                // Set the selected word as the DataContext of the word details panel
                wordDetailsPanel.DataContext = new { SelectedWord = selectedWord };

                // Set the image source
                if (!string.IsNullOrEmpty(selectedWord.Path))
                {
                    wordImage.Source = new BitmapImage(new Uri(selectedWord.Path, UriKind.Relative));
                }

                // Show the word details panel
                wordDetailsPanel.Visibility = Visibility.Visible;

                searchTextBox.Clear();
                collectionView.Refresh();
            }
        }

        private void ResultsListBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            // Get the selected item from the ListBox
            Word selectedWord = resultsListBox.SelectedItem as Word;

            // Check if a word is selected
            if (selectedWord != null)
            {
                // Set the selected word as the DataContext of the word details panel
                wordDetailsPanel.DataContext = new { SelectedWord = selectedWord };

                // Set the image source
                if (!string.IsNullOrEmpty(selectedWord.Path))
                {
                    wordImage.Source = new BitmapImage(new Uri(selectedWord.Path, UriKind.Relative));
                }

                // Show the word details panel
                wordDetailsPanel.Visibility = Visibility.Visible;

                searchTextBox.Clear();
                collectionView.Refresh();
            }
        }
    }
}