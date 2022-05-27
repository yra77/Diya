
using MyApp.Services;

using Android.Content;
using Android.Content.Res;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;

using System;

using Xamarin.Essentials;
using Xamarin.Forms;


namespace MyApp.Droid.Services
{
    class Exist_NavPanel: IAndroid_Exist_NavPanel
    {

        int resourceId = -1;
        IWindowManager windowManager = null;
        Display defaultDisplay = null;
        Android.Util.DisplayMetrics displayMatrics = null;
        DisplayMetrics realMatrics = null;
        Resources resources = null;


        public double ScreenHeight { get => DeviceDisplay.MainDisplayInfo.Height; }
        public double NavigationBarHeight
        {
            get
            {
                try
                {
                    windowManager = Forms.Context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
                    defaultDisplay = windowManager.DefaultDisplay;
                    displayMatrics = new DisplayMetrics();

                    if (Build.VERSION.SdkInt >= BuildVersionCodes.JellyBeanMr2)
                    {
                        realMatrics = new DisplayMetrics();
                        defaultDisplay.GetMetrics(displayMatrics);
                        defaultDisplay.GetRealMetrics(realMatrics);

                        if (displayMatrics.HeightPixels != realMatrics.HeightPixels)
                        {
                            resources = Forms.Context.Resources;
                            return GetHeightOfNivigationBar();
                        }
                    }
                    else
                    {
                        resources = Forms.Context.Resources;
                        resourceId = resources.GetIdentifier("config_showNavigationBar", "bool", "android");

                        if (resourceId > 0 && resources.GetBoolean(resourceId))
                            return GetHeightOfNivigationBar();
                    }
                }
                catch (Exception) { }

                return 0;
            }
        }

        private double GetHeightOfNivigationBar()
        {
            resourceId = resources.GetIdentifier("navigation_bar_height", "dimen", "android");
            if (!ViewConfiguration.Get(Forms.Context).HasPermanentMenuKey && resourceId > 0)
            {
                return resources.GetDimensionPixelSize(resourceId) / displayMatrics.Density;
            }
            return 0;
        }
    }
}