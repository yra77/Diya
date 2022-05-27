
using System.ComponentModel;
using UIKit;
using Xamarin.Forms.Platform.iOS;

namespace MyApp.iOS.Effects
{
    class Editor_DisableUnderLine : PlatformEffect
    {

        protected override void OnAttached()
        {
            UITextField textField = (UITextField)this.Control;
            textField.BorderStyle = UITextBorderStyle.None;
            //  this.Control.Layer.BorderColor = UIColor.Transparent.CGColor;
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