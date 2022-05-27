
using MyApp.Enums;
using MyApp.Events;
using MyApp.Models;
using MyApp.Services;

using Prism;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;


namespace MyApp.ViewModels
{
    class DocumentViewModel : BindableBase, INavigatedAware, IActiveAware
    {

        private readonly INavigationService _navigationService;
        private readonly IChangeThemeService _changeThemeService;
        private readonly IToRepositoryService _repositoryService;
        private readonly IAndroid_Exist_NavPanel _android_Exist_NavPanel;
        private int _flag;


        public DocumentViewModel(IToRepositoryService repositoryService,
                                 INavigationService navigationService,
                                 IChangeThemeService changeThemeService,
                                 IAndroid_Exist_NavPanel android_Exist_NavPanel)
        {
            _navigationService = navigationService;
            _repositoryService = repositoryService;
            _changeThemeService = changeThemeService;
            _android_Exist_NavPanel = android_Exist_NavPanel;

            Carousel = new List<Documents>();

            Document1_IsEnabled_FirstSide = true;
            Document1_IsEnabled_SecondSide = false;
            ModalIsVisible = false;
            Rotate180 = 0;
            Rotate180_Into = 0;
            ScaleRotate = 1;
            _flag = 0;

            if (DeviceInfo.Platform == DevicePlatform.Android)
            {

                if (_android_Exist_NavPanel.NavigationBarHeight > 0)
                {
                    IsNavPanel = new Thickness(0, 0, 0, 50);
                }
                else
                {
                    IsNavPanel = new Thickness(0, 0, 0, 0);
                }

                if (_android_Exist_NavPanel.ScreenHeight <= 2300)
                {
                    ScreenHeight = new Thickness(-10, 0, 0, 60);

                    if (_android_Exist_NavPanel.ScreenHeight < 2050)
                    {
                        IndicatorPadding = new Thickness(0, 0, 0, 8);
                    }
                    else
                    {
                        IndicatorPadding = new Thickness(0, 0, 0, 58);
                    }
                }
                else
                {
                    ScreenHeight = new Thickness(-10, 50, 0, 53);
                    IndicatorPadding = new Thickness(0, 0, 0, 58);
                }

            }
            else//DeviceInfo.Platform == DevicePlatform.Ios
            {
                IsNavPanel = new Thickness(0, 0, 0, 0);
                ScreenHeight = new Thickness(-10, 0, 0, 60);
            }

            IconTab = "documents.png";
        }


        public event EventHandler IsActiveChanged;


        #region Public property


        private bool _isActive;
        public bool IsActive
        {
            get => _isActive;
            set => SetProperty(ref _isActive, value, IsActiveTabAsync);
        }


        private int _indexOfItem;
        public int IndexOfItem
        {
            get => _indexOfItem;
            set
            {
                SetProperty(ref _indexOfItem, value);
                ChangeIndex();
            }
        }


        private double _scaleRotate;
        public double ScaleRotate
        {
            get => _scaleRotate;
            set => SetProperty(ref _scaleRotate, value);
        }


        private int _rotate180_Into;
        public int Rotate180_Into
        {
            get => _rotate180_Into;
            set => SetProperty(ref _rotate180_Into, value);
        }


        private int _rotate180;
        public int Rotate180
        {
            get => _rotate180;
            set => SetProperty(ref _rotate180, value);
        }


        private TouchActionEventArgs _touchEvent;
        public TouchActionEventArgs TouchEvent
        {
            get => _touchEvent;
            set
            {
                SetProperty(ref _touchEvent, value);
                On_TouchEffectAction(_touchEvent);
            }
        }


        private bool _document1_IsEnabled_FirstSide;
        public bool Document1_IsEnabled_FirstSide
        {
            get => _document1_IsEnabled_FirstSide;
            set => SetProperty(ref _document1_IsEnabled_FirstSide, value);
        }


        private bool _document1_IsEnabled_SecondSide;
        public bool Document1_IsEnabled_SecondSide
        {
            get => _document1_IsEnabled_SecondSide;
            set => SetProperty(ref _document1_IsEnabled_SecondSide, value);
        }


        private bool _modalIsVisible;
        public bool ModalIsVisible
        {
            get => _modalIsVisible;
            set => SetProperty(ref _modalIsVisible, value);
        }

        private Thickness _isNavPanel;
        public Thickness IsNavPanel
        {
            get => _isNavPanel;
            set => SetProperty(ref _isNavPanel, value);
        }


        private Thickness _screenHeight;
        public Thickness ScreenHeight
        {
            get => _screenHeight;
            set => SetProperty(ref _screenHeight, value);
        }


        private bool _indicatorEnabled;
        public bool IndicatorEnabled
        {
            get => _indicatorEnabled;
            set => SetProperty(ref _indicatorEnabled, value);
        }


        private Thickness _indicatorPadding;
        public Thickness IndicatorPadding
        {
            get => _indicatorPadding;
            set => SetProperty(ref _indicatorPadding, value);
        }


        private string _iconTab;
        public string IconTab
        {
            get => _iconTab;
            set => SetProperty(ref _iconTab, value);
        }


        private List<Documents> _carousel;
        public List<Documents> Carousel
        {
            get => _carousel;
            set => SetProperty(ref _carousel, value);
        }

        public DelegateCommand CloseModal => new DelegateCommand(Close_Modal);
        public DelegateCommand DocumentClick => new DelegateCommand(Document_Click);
        public DelegateCommand ButtonSertificate => new DelegateCommand(OpenModal);

        #endregion



        private async void CaruselList()
        {
            Carousel = await _repositoryService.GetData<Documents>("Documents");
        }

        private void ChangeIndex()
        {

            Document1_IsEnabled_FirstSide = true;
            Document1_IsEnabled_SecondSide = false;

            switch (IndexOfItem)
            {
                case 0:
                    _changeThemeService.SetTheme(Enums.Themes.Light);
                    break;
                case 1:
                    _changeThemeService.SetTheme(Enums.Themes.Light);
                    break;
                case 2:
                    _changeThemeService.SetTheme(Enums.Themes.Dark);
                    break;
                case 3:
                    _changeThemeService.SetTheme(Enums.Themes.IdPassport);
                    break;
                case 4:
                    _changeThemeService.SetTheme(Enums.Themes.Poslugi);
                    break;
                default:
                    break;
            }
        }

        //Documents first and second side
        private void Document_Click()
        {

            if (Document1_IsEnabled_FirstSide)
            {
                Document1_IsEnabled_FirstSide = false;
                Document1_IsEnabled_SecondSide = true;
            }
            else
            {
                Document1_IsEnabled_FirstSide = true;
                Document1_IsEnabled_SecondSide = false;
            }

        }

        private void OpenModal()
        {
            ModalIsVisible = true;
        }

        private void Close_Modal()
        {
            ModalIsVisible = false;
        }

        private void On_TouchEffectAction(TouchActionEventArgs args)
        {
            if (args != null)
            {
                switch (args.Type)
                {

                    case TouchActionType.Moved:
                        _flag = 3;
                        break;

                    case TouchActionType.Cancelled:

                        if (_flag == 3)
                        {
                            _flag = 0;
                        }

                        break;

                    case TouchActionType.Released:

                        if (_flag == 1)
                        {
                            RotateDoc();
                            _flag = 0;
                        }
                        if (_flag == 3)
                        {
                            _flag = 0;
                        }

                        break;

                    case TouchActionType.Pressed:

                        if (_flag == 0)
                        {
                            _flag = 1;
                        }

                        break;

                    default:
                        break;
                }
            }
        }

        private async void RotateDoc()
        {
            ScaleRotate = 0.6;
            for (int i = 0; i <= 180; i += 15)
            {
                Rotate180 = i;
                if (i == 90)
                {
                    Document_Click();
                }
                Rotate180_Into = i;
                if (i == 165)
                    ScaleRotate = 1;
                await Task.Delay(1);

            }
            Rotate180 = 0;
        }

        private async void IsActiveTabAsync()
        {
            if (IsActive)
            {
                ChangeIndex();
                IconTab = "documents.png";
            }
            else
            {
                IconTab = "documentsWhite.png";
            }
            // await Task.Delay(150);
        }


        public void OnNavigatedFrom(INavigationParameters parameters)
        {
            IndicatorEnabled = false;
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
            ChangeIndex();
            CaruselList();
            IndicatorEnabled = true;
        }
    }
}
