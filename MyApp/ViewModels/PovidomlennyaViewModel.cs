
using MyApp.Models;
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
    class PovidomlennyaViewModel : BindableBase, INavigatedAware, IActiveAware
    {

        private readonly INavigationService _navigationService;
        private readonly IChangeThemeService _changeThemeService;


        public PovidomlennyaViewModel(INavigationService navigationService,
                                      IChangeThemeService changeThemeService)
        {
            _navigationService = navigationService;
            _changeThemeService = changeThemeService;

            IconTab = "zvonok2.png";

            ListPovidomlennya = new ObservableCollection<Povidomlennya>();
        }

        public event EventHandler IsActiveChanged;


        #region Public property


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


        private ObservableCollection<Povidomlennya> _listPovidomlennya;
        public ObservableCollection<Povidomlennya> ListPovidomlennya
        {
            get => _listPovidomlennya;
            set => SetProperty(ref _listPovidomlennya, value);
        }

        #endregion


        private void PovidomlennyaList()
        {
            ListPovidomlennya.Add(new Povidomlennya { imageHand = "povidomlennyaHand.png", imageStrelka = "strelka.png", text1 = "Зверніть Увагу", text2 = "COVID-сертифікат знайдено" });
            ListPovidomlennya.Add(new Povidomlennya { imageHand = "povidomlennyaHand.png", imageStrelka = "strelka.png", text1 = "Зверніть Увагу", text2 = "COVID-сертифікат знайдено" });
            ListPovidomlennya.Add(new Povidomlennya { imageHand = "povidomlennyaHand.png", imageStrelka = "strelka.png", text1 = "Зверніть Увагу", text2 = "COVID-сертифікат знайдено" });
        }

        private async void IsActiveTabAsync()
        {
            if (IsActive)
            {
                _changeThemeService.SetTheme(Enums.Themes.Povidomlennya);
                IconTab = "zvonok2Black.png";
                await Task.Delay(50);
                ListPovidomlennya.Clear();
                PovidomlennyaList();
            }
            else
            {
                IconTab = "zvonok2.png";
                ListPovidomlennya.Clear();
            }
        }


        public void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
        }

    }
}
