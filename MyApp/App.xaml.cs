
using MyApp.Services;
using MyApp.Repository;
using MyApp.ViewModels;
using MyApp.Helpers;
using MyApp.Views;

using Prism;
using Prism.Ioc;
using Prism.Unity;

using Xamarin.Forms;


namespace MyApp
{
    public partial class App : PrismApplication
    {
        public static Enums.Themes AppTheme { get; set; }

        public App(IPlatformInitializer platformInitializer = null) : base(platformInitializer) { }

        protected async override void OnInitialized()
        {
            InitializeComponent();
            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<TabbedPages, TabbedPagesViewModel>();
            containerRegistry.RegisterForNavigation<PoslugiPage, PoslugiViewModel>();
            containerRegistry.RegisterForNavigation<DocumentPage, DocumentViewModel>();
            containerRegistry.RegisterForNavigation<PovidomlennyaPage, PovidomlennyaViewModel>();
            containerRegistry.RegisterForNavigation<MenuPage, MenuViewModel>();
            containerRegistry.RegisterForNavigation<PasswordPage, PasswordViewModel>();

            //services
            containerRegistry.RegisterSingleton<IInputVerify, InputVerify>();

            containerRegistry.RegisterSingleton<IMediaService, MediaService>();
            containerRegistry.RegisterSingleton<IRepository, Repository.Repository>();
            containerRegistry.RegisterSingleton<IToRepositoryService, ToRepositoryService>();
        }


        protected override void OnStart() { }
        protected override void OnSleep() { }
        protected override void OnResume(){ }

    }
}
