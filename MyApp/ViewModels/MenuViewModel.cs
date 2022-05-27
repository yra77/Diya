using MyApp.Services;
using Prism;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;

namespace MyApp.ViewModels
{
    class MenuViewModel : BindableBase, INavigatedAware, IActiveAware
    {

        private readonly INavigationService _navigationService;
        private readonly IChangeThemeService _changeThemeService;


        public MenuViewModel(INavigationService navigationService,
                                IChangeThemeService changeThemeService)
        {
            _navigationService = navigationService;
            _changeThemeService = changeThemeService;

            IconTab = "menu.png";

            MenuColection = new ObservableCollection<Models.Menu>();
        }


        public event EventHandler IsActiveChanged;


        #region Public property


        private Models.Menu _selectOfMenu;
        public Models.Menu SelectOfMenu
        {
            get => _selectOfMenu;
            set
            {
                SetProperty(ref _selectOfMenu, value);
                Select_Of_Menu();
            }
        }


        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, IsActiveTabAsync);
        }


        private string _iconTab;
        public string IconTab
        {
            get => _iconTab;
            set => SetProperty(ref _iconTab, value);
        }


        private ObservableCollection<Models.Menu> _menuColection;
        public ObservableCollection<Models.Menu> MenuColection
        {
            get => _menuColection;
            set => SetProperty(ref _menuColection, value);
        }

        #endregion


        private void Select_Of_Menu()
        {
            Console.WriteLine("Select of menu " + SelectOfMenu.name);
        }
        private void MenuList()
        {
            MenuColection.Add(new Models.Menu { name = "Підключені пристрої", image = "zamochek.png" });
            MenuColection.Add(new Models.Menu { name = "Дія.Підпис", image = "key.png" });
            MenuColection.Add(new Models.Menu { name = "Функції та підказки", image = "lampa.png" });
            MenuColection.Add(new Models.Menu { name = "Питання та відповіді", image = "quastions.png" });
            MenuColection.Add(new Models.Menu { name = "Служба підтримки", image = "podskazka.png" });
            MenuColection.Add(new Models.Menu { name = "Копіювати номер пристрою", image = "copy.png" });
            MenuColection.Add(new Models.Menu { name = "Налаштування", image = "settings.png" });
        }

        private async void IsActiveTabAsync()
        {
            if (IsActive)
            {
                _changeThemeService.SetTheme(Enums.Themes.Poslugi);
                IconTab = "menu.png";
                await Task.Delay(50);
                MenuColection.Clear();
                MenuList();
            }
            else 
            {
                IconTab = "menu.png";
                MenuColection.Clear();
            }
        }

        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            _changeThemeService.SetTheme(Enums.Themes.Poslugi);
        }

    }
}
