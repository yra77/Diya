
using MyApp.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System.Threading.Tasks;


namespace MyApp.ViewModels
{
    class PasswordViewModel : BindableBase, INavigatedAware
    {

        private IToRepositoryService _repositoryService;
        private INavigationService _navigationService;
        private string _password;

        public PasswordViewModel(IToRepositoryService repositoryService,
                                 INavigationService navigationService)
        {
            _repositoryService = repositoryService;
            _navigationService = navigationService;

            ButtonColor = "Black";
            _password = "";
            ImagePassword1 = "EmptyCircle.png";
            ImagePassword2 = "EmptyCircle.png";
            ImagePassword3 = "EmptyCircle.png";
            ImagePassword4 = "EmptyCircle.png";
        }


        #region Public property


        private string _imagePassword1;
        public string ImagePassword1 { get => _imagePassword1; set => SetProperty(ref _imagePassword1, value); }


        private string _imagePassword2;
        public string ImagePassword2 { get => _imagePassword2; set => SetProperty(ref _imagePassword2, value); }

        
        private string _imagePassword3;
        public string ImagePassword3 { get => _imagePassword3; set => SetProperty(ref _imagePassword3, value); }


        private string _imagePassword4;
        public string ImagePassword4 { get => _imagePassword4; set => SetProperty(ref _imagePassword4, value); }


        private string _buttonColor;
        public string ButtonColor { get => _buttonColor; set => SetProperty(ref _buttonColor, value); }
        

        public DelegateCommand RefreshPassword => new DelegateCommand(RefreshPassword_Click);
        public DelegateCommand DigitCancel => new DelegateCommand(RemoveDigit);
        public DelegateCommand<string> ClickPass => new DelegateCommand<string>(PasswordClick);

        #endregion


        private void RefreshPassword_Click()
        {
            ButtonColor = "Red";
            //navigationService.NavigateAsync("Page1");
        }

        private void RemoveDigit()
        {
            if (_password.Length > 0)
            {
                ChangeImage(_password.Length, "EmptyCircle.png");
                _password = _password.Remove(_password.Length - 1);
            }
        }

        private async void PasswordClick(string obj)
        {
            if (_password.Length < 5)
            {
                _password += obj;
                ChangeImage(_password.Length, "FillCircle.png");

                if (_password.Length == 4)
                {
                    await Task.Delay(200);
                    await _navigationService.NavigateAsync("/TabbedPages", animated: true);
                }
            }
        }

        private void ChangeImage(int count, string image)
        {
            switch (count)
            {
                case 1:
                    ImagePassword1 = image;
                    break;
                case 2:
                    ImagePassword2 = image;
                    break;
                case 3:
                    ImagePassword3 = image;
                    break;
                case 4:
                    ImagePassword4 = image;
                    break;
            }
        }


        public void OnNavigatedFrom(INavigationParameters parameters)
        {         
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {         
        }
    }
}
