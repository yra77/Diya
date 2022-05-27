

using MyApp.Events;
using Xamarin.Forms;


namespace MyApp.Effects
{
    public class TouchEffect : RoutingEffect
    {

        public event TouchActionEventHandler TouchAction;
        private TouchActionEventArgs type;

        public TouchEffect() : base("MyApp.TouchEffect") { }

        public TouchActionEventArgs TouchType { get => type; set => type = value; }
        public bool Capture { set; get; }

        public void OnTouchAction(Element element, TouchActionEventArgs args)
        {
                TouchAction?.Invoke(element, args);
                TouchType = args;
                // GetCommand(element);
                SetCommand(element, args);
        }

        public static BindableProperty CommandProperty = BindableProperty.Create(nameof(TouchAction), 
                                                                                 typeof(TouchActionEventArgs),
                                                                                 declaringType: typeof(TouchEffect));
        public static TouchActionEventArgs GetCommand(BindableObject view)
        {
            return (TouchActionEventArgs)view.GetValue(CommandProperty);
        }

        public static void SetCommand(BindableObject view, object value)
        {
            view.SetValue(CommandProperty, value);
        }

    }
}
