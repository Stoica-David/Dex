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
            // Retrieve the username and password from the TextBox and PasswordBox
            string username = usernameBox.Text;
            string password = passwordBox.Password;

            // Perform login logic here
            if (Authenticate(username, password))
            {
                MessageBox.Show("Login successful!");
                // Proceed with administrative tasks
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }

        private bool Authenticate(string username, string password)
        {
            // Perform authentication logic here
            // For demonstration, let's assume a hardcoded username and password
            return username == "admin" && password == "password";
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
            var passwordBox = sender as PasswordBox;
            if (passwordBox != null)
            {
                // Update the password when it changes
                // Note: In this approach, we don't need to store the password separately
                // as we handle the authentication directly
            }
        }

        private void usernameBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Your event handling logic here
        }
    }
}