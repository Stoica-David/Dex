using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dex.ViewModels
{
    public class MenuVM: BaseVM
    {
        // COMMAND

        private ICommand switchToLoginCommand;
        public ICommand SwitchToLoginCommand
        {
            get
            {
                if (switchToLoginCommand == null)
                {
                    switchToLoginCommand = new RelayCommand(o => true, o => { OnSwitchToLogin(); });
                }

                return switchToLoginCommand;
            }
        }

        private ICommand switchToSearchCommand;
        public ICommand SwitchToSearchCommand
        {
            get
            {
                if (switchToSearchCommand == null)
                {
                    switchToSearchCommand = new RelayCommand(o => true, o => { OnSwitchToSearch(); });
                }

                return switchToSearchCommand;
            }
        }

        private ICommand switchToGameCommand;
        public ICommand SwitchToGameCommand
        {
            get
            {
                if (switchToGameCommand == null)
                {
                    switchToGameCommand = new RelayCommand(o => true, o => { OnSwitchToGame(); });
                }

                return switchToGameCommand;
            }
        }

        // DELEGATES

        public delegate void SwitchToLogin();
        public SwitchToLogin OnSwitchToLogin{ get; set; }

        public delegate void SwitchToSearch();
        public SwitchToSearch OnSwitchToSearch{ get; set; }

        public delegate void SwitchToGame();
        public SwitchToSearch OnSwitchToGame { get; set; }

        public MenuVM()
        {

        }
    }
}
