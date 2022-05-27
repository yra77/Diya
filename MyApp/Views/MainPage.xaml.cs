
using Prism.Mvvm;
using Prism.Navigation;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using System;
using System.Collections.Generic;
using System.IO;
using Xamarin.Forms;


namespace MyApp.Views
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }

        private void OnTouch(object sender, SKTouchEventArgs e)
        {
            ((SKCanvasView)sender).InvalidateSurface();
        }

        private void OnClearButtonClicked(object sender, EventArgs args)
        {
            canvasView.InvalidateSurface();
        }

    }
}
