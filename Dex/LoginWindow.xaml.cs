using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Dex
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            string username = usernameTextBox.Text;
            string password = passwordBox.Password;

            List<Admin> admins = DexManager.Instance.Admins.ToList();

            Admin matchingAdmin = admins.FirstOrDefault(admin => admin.Name == username && admin.Password == password);

            if (matchingAdmin != null)
            {
                AdministrativeWindow adminWindow = new AdministrativeWindow();
                adminWindow.Show();
                Close();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && textBox.Text == "Username")
            {
                textBox.Text = string.Empty;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = sender as TextBox;
            if (textBox != null && string.IsNullOrWhiteSpace(textBox.Text))
            {
                textBox.Text = "Username";
            }
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            // Update the password when it changes
            // Note: In this approach, we don't need to store the password separately
            // as we handle the authentication directly
        }

        private void usernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Your event handling logic here
        }
    }
}
