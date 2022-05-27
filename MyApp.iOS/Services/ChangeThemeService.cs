
using MyApp.ViewModels.Services;
using MyApp.ViewModels.Enums;
using MyApp.Views.Themes;
using MyApp;

namespace Contact.iOS.Services
{
    class ChangeThemeService : IChangeThemeService
    {
        //public void SetAppTheme()
        //{
        //    if (Resources.Configuration.UiMode.HasFlag(UiMode.NightYes))
        //        SetTheme(Themes.Dark);
        //    else
        //        SetTheme(Themes.Light);
        //}

        public void SetTheme(Themes mode)
        {
            if (mode == Themes.Dark)
            {
                if (App.AppTheme == Themes.Dark)
                    return;

                App.Current.Resources = new DarkTheme();
            }
            else if (mode == Themes.Light)
            {
                if (App.AppTheme == Themes.Light)
                    return;

                App.Current.Resources = new LightTheme();
            }
            else if (mode == Themes.Poslugi)
            {
                App.Current.Resources = new Poslugi();
            }
            else if (mode == Themes.Povidomlennya)
            {
                App.Current.Resources = new Povidomlennya();
            }
            else if (mode == Themes.IdPassport)
            {
                App.Current.Resources = new IdPassport();
            }

            App.AppTheme = mode;
        }

    }
}