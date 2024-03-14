using Dex.ViewModels;
using System.Windows;

namespace Dex
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.DataContext = new MainWindowVM();
            InitializeComponent();
        }
    }
}
