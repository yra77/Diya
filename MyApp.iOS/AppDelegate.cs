
using Contact.iOS.Services;
using Foundation;
using MyApp.iOS.Effects;
using MyApp.iOS.Services;
using MyApp.Models.Services;
using MyApp.ViewModels.Services;
using Prism;
using Prism.Ioc;
using UIKit;
using Xamarin.Forms;

[assembly: ResolutionGroupName("MyApp")]
[assembly: ExportEffect(typeof(Editor_DisableUnderLine), "PlainEditorEffect")]

namespace MyApp.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();
            LoadApplication(new App(new IOSPlatformInitializer()));

            return base.FinishedLaunching(app, options);
        }
    }

    public class IOSPlatformInitializer : IPlatformInitializer
    {

        void IPlatformInitializer.RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ISQLiteAsyncConnectionProvider, SQLiteAsyncConnectionProvider>();
            containerRegistry.Register<IResizeImageService, ResizeImageService>();
            containerRegistry.Register<IChangeThemeService, ChangeThemeService>();
        }
    }

}
