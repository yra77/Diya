
using Android.Content.Res;
using Android.Graphics;
using Android.OS;

using System.ComponentModel;
using Xamarin.Forms.Platform.Android;


namespace MyApp.Droid.Effects
{
    class Editor_DisableUnderline : PlatformEffect
    {

        protected override void OnAttached()
        {
            Android.Graphics.Color borderColor = Android.Graphics.Color.Transparent;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Control.BackgroundTintList = ColorStateList.ValueOf(borderColor);
            }
            else
            {
                Control.Background.SetColorFilter(borderColor, PorterDuff.Mode.SrcOut);
            }
        }

        protected override void OnDetached()
        {

        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);
        }
    }
}