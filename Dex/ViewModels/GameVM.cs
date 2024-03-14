using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Dex.ViewModels
{
    public class GameVM: BaseVM
    {
        private ICommand switchToMenuCommand;
        public ICommand SwitchToMenuCommand
        {
            get
            {
                if (switchToMenuCommand == null)
                {
                    switchToMenuCommand = new RelayCommand(o => true, o => { OnSwitchToMenu(); });
                }

                return switchToMenuCommand;
            }
        }

        public delegate void SwitchToMenu();
        public SwitchToMenu OnSwitchToMenu { get; set; }

        public GameVM() 
        {

        }
    }
}
