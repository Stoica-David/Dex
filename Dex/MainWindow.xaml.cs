using System.Windows;

namespace Dex
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Administrative_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow adminWindow = new LoginWindow();
            adminWindow.Show();
            this.Close();
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            SearchWindow searchWindow = new SearchWindow();
            searchWindow.Show();
            this.Close();
        }

        private void Game_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
            this.Close();
        }
    }
}
