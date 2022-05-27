
using Prism.Mvvm;
using Prism.Navigation;


namespace MyApp.ViewModels
{
    class TabbedPagesViewModel : BindableBase, INavigatedAware
    {

        private INavigationService navigationService;

        public TabbedPagesViewModel(INavigationService _navigationService)
        {
            navigationService = _navigationService;
        }


        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public void OnNavigatedTo(INavigationParameters parameters)
        {
        }
    }
}
