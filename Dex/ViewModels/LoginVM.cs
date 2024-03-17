using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.TextFormatting;

namespace Dex.ViewModels
{
    public class LoginVM: BaseVM
    {
        // PROPERTIES

        private string username;
        public string Username 
        { 
            get
            { 
                return username; 
            }

            set
            {
                username = value;
                OnPropertyChanged();
            }
        }

        private string password;
        public string Password
        {
            get
            {
                return password;
            }

            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        // COMMANDS

        private ICommand switchToAdministrativeCommand;
        public ICommand SwitchToAdministrativeCommand
        {
            get
            {
                if(switchToAdministrativeCommand == null)
                {
                    switchToAdministrativeCommand = new RelayCommand(o => true, LoginClick);
                }

                return switchToAdministrativeCommand;
            }
        }

        // DELEGATES

        public delegate void SwitchToAdministrative();

        public SwitchToAdministrative OnSwitchToAdministrative { get; set; }

        // METHODS

        public LoginVM() 
        {

        }

        private void LoginClick(object param)
        {
            List<Admin> admins = DexManager.Instance.Admins.ToList();

            Admin matchingAdmin = admins.FirstOrDefault(admin => admin.Name == Username && admin.Password == Password);

            if (matchingAdmin != null)
            {
                OnSwitchToAdministrative();
            }
            else
            {
                MessageBox.Show("Invalid username or password. Please try again.");
            }
        }
    }
}
