using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dex.ViewModels
{
    public class MainWindowVM: BaseVM
    {
        private BaseVM selectedVM;
        public BaseVM SelectedVM 
        { 
            get 
            { 
                return selectedVM; 
            } 
            set 
            { 
                selectedVM = value;
                OnPropertyChanged();
            } 
        }
        
        public MenuVM MenuViewModel { get; set; }

        public LoginVM LoginViewModel { get; set; }

        public AdministrativeVM AdministrativeViewModel { get; set; }

        public SearchVM SearchViewModel { get; set; }

        public GameVM GameViewModel { get; set; }

        public MainWindowVM() 
        {
            switchToMenu();
        }

        public void switchToLogin()
        {
            LoginViewModel = new LoginVM();
            LoginViewModel.OnSwitchToAdministrative = switchToAdministrative;  
            SelectedVM = LoginViewModel;
        }

        public void switchToAdministrative()
        {
            AdministrativeViewModel = new AdministrativeVM();
            AdministrativeViewModel.OnSwitchToMenu = switchToMenu;
            SelectedVM = AdministrativeViewModel;
        }

        public void switchToSearch()
        {
            SearchViewModel = new SearchVM();
            SearchViewModel.OnSwitchToMenu = switchToMenu;
            SelectedVM = SearchViewModel;
        }

        public void switchToGame()
        {
            GameViewModel = new GameVM();
            GameViewModel.OnSwitchToMenu = switchToMenu;
            SelectedVM = GameViewModel;
        }

        public void switchToMenu()
        {
            MenuViewModel = new MenuVM();
            MenuViewModel.OnSwitchToLogin = switchToLogin;
            MenuViewModel.OnSwitchToSearch = switchToSearch;
            MenuViewModel.OnSwitchToGame = switchToGame;
            SelectedVM = MenuViewModel;
        }
    }
}
