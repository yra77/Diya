

using MyApp.Services;
using MyApp.Droid.Services;
using MyApp.Droid.Effects;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;

using Prism;
using Prism.Ioc;

using Acr.UserDialogs;
using Xamarin.Forms;


[assembly: ResolutionGroupName("MyApp")]
[assembly: ExportEffect(typeof(Editor_DisableUnderline), "PlainEditorEffect")]
[assembly: ExportEffect(typeof(MyApp.Droid.Effects.TouchEffect), "TouchEffect")]


namespace MyApp.Droid
{
    [Activity(ScreenOrientation = ScreenOrientation.Portrait, Label = "Дія", Icon = "@drawable/diya1", Theme = "@style/MainTheme", 
              ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | 
                 ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

            LoadApplication(new App(new AndroidPlatformInitializer()));
            UserDialogs.Init(this);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class AndroidPlatformInitializer : IPlatformInitializer
    {

        void IPlatformInitializer.RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<ISQLiteAsyncConnectionProvider, SQLiteAsyncConnectionProvider>();
            containerRegistry.Register<IResizeImageService, ResizeImageService>();
            containerRegistry.Register<IChangeThemeService, ChangeThemeService>();
            containerRegistry.Register<IAndroid_Exist_NavPanel, Exist_NavPanel>();
        }

    }

}