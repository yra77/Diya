
using Acr.UserDialogs;

using MyApp.Models;
using MyApp.Helpers;
using MyApp.Services;

using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;

using SkiaSharp;
using SkiaSharp.Views.Forms;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace MyApp.ViewModels
{
    class MainPageViewModel : BindableBase, INavigatedAware
    {

        private readonly IInputVerify _inputVerify;
        private IMediaService _mediaService;
        private IToRepositoryService _repositoryService;
        private INavigationService _navigationService;
        private Documents _documentList;
        private readonly Dictionary<long, SKPath> _temporaryPaths;
        private readonly List<SKPath> _paths;
        private SKBitmap _saveBitmap;


        public MainPageViewModel(IToRepositoryService repositoryService,
                                         IInputVerify inputVerify,
                                         IMediaService mediaService,
                                 INavigationService navigationService)
        {

            _inputVerify = inputVerify;
            _mediaService = mediaService;
            _repositoryService = repositoryService;
            _navigationService = navigationService;

            Cancel = new DelegateCommand(Cancel_Click);
            Save = new DelegateCommand(Save_Click);
            Foto = new DelegateCommand(Foto_Click);
            Pidpis = new DelegateCommand(Pidpis_Click);
            ClosePaint = new DelegateCommand(Close_Paint);
            PaintSurfaceCommand = new DelegateCommand<SKPaintSurfaceEventArgs>(PaintSurface_Command);
            OnTouch = new DelegateCommand<SKTouchEventArgs>(OnTouch_Command);
            OnClear_Pidpis_Click = new DelegateCommand(OnClearButtonClicked);

            _temporaryPaths = new Dictionary<long, SKPath>();
            _paths = new List<SKPath>();

            IsPaint = false;
            FotoPath = "one.png";

            NameUkr = "";
            FamiliaUkr = "";
            OtchestvoUkr = "";
            NameEng = "";
            FamiliaEng = "";
            DataBirthday = "";

            _documentList = new Documents();

        }


        #region Property


        private bool _isPaint;
        public bool IsPaint { get => _isPaint; set => SetProperty(ref _isPaint, value); }


        private string _fotoPath;
        public string FotoPath { get => _fotoPath; set => SetProperty(ref _fotoPath, value); }


        private string _familiaUkr;
        public string FamiliaUkr
        {
            get => _familiaUkr;
            set
            {
                SetProperty(ref _familiaUkr, value);

                if (_familiaUkr.Length > 0)
                {
                    string temp = value;

                    if (!_inputVerify.EntryVerify_Ukr(ref temp))
                    {
                        UserDialogs.Instance.Alert("На українській мові", "Error", "Ok");
                    }

                    SetProperty(ref _familiaUkr, temp);
                }
            }
        }


        private string _nameUkr;
        public string NameUkr
        {
            get => _nameUkr;
            set
            {
                SetProperty(ref _nameUkr, value);

                if (_nameUkr.Length > 0)
                {
                    string temp = value;

                    if (!_inputVerify.EntryVerify_Ukr(ref temp))//Verify
                    {
                        UserDialogs.Instance.Alert("На українській мові", "Error", "Ok");
                    }

                    SetProperty(ref _nameUkr, temp);
                }
            }
        }


        private string _otchestvoUkr;
        public string OtchestvoUkr
        {
            get => _otchestvoUkr;
            set
            {
                SetProperty(ref _otchestvoUkr, value);

                if (_otchestvoUkr.Length > 0)
                {
                    string temp = value;

                    if (!_inputVerify.EntryVerify_Ukr(ref temp))//Verify
                    {
                        UserDialogs.Instance.Alert("На українській мові", "Error", "Ok");
                    }

                    SetProperty(ref _otchestvoUkr, temp);
                }
            }
        }


        private string _familiaEng;
        public string FamiliaEng
        {
            get => _familiaEng;
            set
            {
                SetProperty(ref _familiaEng, value);

                if (_familiaEng.Length > 0)
                {
                    string temp = value;

                    if (!_inputVerify.EntryVerify_Eng(ref temp))
                    {
                        UserDialogs.Instance.Alert("На англійській мові", "Error", "Ok");
                    }

                    SetProperty(ref _familiaEng, temp);
                }
            }
        }


        private string _nameEng;
        public string NameEng
        {
            get => _nameEng;
            set
            {
                SetProperty(ref _nameEng, value);

                if (_nameEng.Length > 0)
                {
                    string temp = value;
                    if (!_inputVerify.EntryVerify_Eng(ref temp))//Verify
                    {
                        UserDialogs.Instance.Alert("На англійській мові", "Error", "Ok");
                    }

                    SetProperty(ref _nameEng, temp);
                }
            }
        }


        private string _dataBirthday;
        public string DataBirthday
        {
            get => _dataBirthday;
            set
            {
                SetProperty(ref _dataBirthday, value);
                if (_dataBirthday.Length > 0)
                {
                    string temp = value;
                    if (!_inputVerify.InputVerify_Data(ref temp))//Verify
                    {
                        DataBirthday = temp;
                        UserDialogs.Instance.Alert("Дата має бути у такому форматі: 01.12.2021", "Error", "Ok");
                    }
                }
            }
        }


        public DelegateCommand OnClear_Pidpis_Click { get; }
        public DelegateCommand<SKPaintSurfaceEventArgs> PaintSurfaceCommand { get; set; }
        public DelegateCommand<SKTouchEventArgs> OnTouch { get; set; }
        public DelegateCommand ClosePaint { get; }
        public DelegateCommand Foto { get; }
        public DelegateCommand Pidpis { get; }
        public DelegateCommand Cancel { get; }
        public DelegateCommand Save { get; }


        #endregion



        private async void Foto_Click()
        {
            string camera = await _mediaService.OpenCamera();
            FotoPath = camera;
        }

        private void Pidpis_Click()
        {
            IsPaint = true;
        }

        private void Cancel_Click()// для ios нельзя так делать
        {
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private async void Save_Click()
        {

            if (FamiliaUkr.Length > 2 && NameUkr.Length > 2 && OtchestvoUkr.Length > 2 &&
                FamiliaEng.Length > 2 && NameEng.Length > 2 && DataBirthday.Length == 10 && FotoPath != "one.png")
            {
                _documentList.name = ToTitleCase(NameUkr);
                _documentList.familia = ToTitleCase(FamiliaUkr);
                _documentList.otchestvo = ToTitleCase(OtchestvoUkr);
                _documentList.dataBirthday = DataBirthday;
                _documentList.familiaEng = ToTitleCase(FamiliaEng);
                _documentList.nameEng = ToTitleCase(NameEng);
                _documentList.imagePath = FotoPath;

                await _repositoryService.Insert(_documentList);
                await _navigationService.NavigateAsync("/PasswordPage", animated: true);
            }
            else
            {
                UserDialogs.Instance.Alert("Всі поля мають бути заповнені правильно", "Error", "Ok");
            }
        }

        private string ToTitleCase(string str)//first char to upper
        {
            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
        }

        private void Close_Paint()
        {
            using (SKImage image = SKImage.FromBitmap(_saveBitmap))
            {
                string path = System.IO.Path.Combine(FileSystem.AppDataDirectory, "pidpis.png");

                //var image = surface.Snapshot();

                using (var data = image.Encode(SKEncodedImageFormat.Png, 80))
                using (var stream = File.OpenWrite(path))
                {
                    data.SaveTo(stream);
                }
                _documentList.pidpis = path;
            }
            IsPaint = false;
        }


        //Pidpis
        SKPaint touchPathStroke = new SKPaint
        {
            IsAntialias = true,
            Style = SKPaintStyle.Stroke,
            Color = SKColors.Purple,
            StrokeWidth = 7
        };

        private void PaintSurface_Command(SKPaintSurfaceEventArgs args)
        {
            SKImageInfo info = args.Info;
            SKSurface surface = args.Surface;
            SKCanvas canvas = surface.Canvas;

            if (_saveBitmap == null)
            {
                _saveBitmap = new SKBitmap(info.Width, info.Height);
            }
            else if (_saveBitmap.Width < info.Width || _saveBitmap.Height < info.Height)
            {
                SKBitmap newBitmap = new SKBitmap(Math.Max(_saveBitmap.Width, info.Width),
                                                  Math.Max(_saveBitmap.Height, info.Height));

                using (SKCanvas newCanvas = new SKCanvas(newBitmap))
                {
                    newCanvas.Clear();
                    newCanvas.DrawBitmap(_saveBitmap, 0, 0);
                }

                _saveBitmap = newBitmap;
            }

            canvas.Clear();
            canvas.DrawBitmap(_saveBitmap, 0, 0);

        }

        private void OnTouch_Command(SKTouchEventArgs args)
        {
            switch (args.ActionType)
            {
                case SKTouchAction.Pressed:
                    var p = new SKPath();
                    p.MoveTo(args.Location);
                    _temporaryPaths[args.Id] = p;
                    UpdateBitmap();
                    break;
                case SKTouchAction.Moved:
                    if (args.InContact && _temporaryPaths.TryGetValue(args.Id, out var moving))
                        moving.LineTo(args.Location);
                    UpdateBitmap();
                    break;
                case SKTouchAction.Released:
                    // end of a stroke
                    if (_temporaryPaths.TryGetValue(args.Id, out var releasing))
                        _paths.Add(releasing);
                    _temporaryPaths.Remove(args.Id);
                    UpdateBitmap();
                    break;
                case SKTouchAction.Cancelled:
                    _temporaryPaths.Remove(args.Id);
                    UpdateBitmap();
                    break;
            }

            args.Handled = true;
        }

        void UpdateBitmap()
        {
            using (SKCanvas saveBitmapCanvas = new SKCanvas(_saveBitmap))
            {
                saveBitmapCanvas.Clear();

                foreach (SKPath path in _paths)
                {
                    saveBitmapCanvas.DrawPath(path, touchPathStroke);
                }

                foreach (SKPath path in _temporaryPaths.Values)
                {
                    saveBitmapCanvas.DrawPath(path, touchPathStroke);
                }
            }
        }
        private void OnClearButtonClicked()
        {
            _temporaryPaths.Clear();
            _paths.Clear();
            UpdateBitmap();
        }



        public void OnNavigatedFrom(INavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(INavigationParameters parameters)
        {
            await Task.Delay(100);

            if (_repositoryService.IsTableExist("Documents"))
            {
                await _navigationService.NavigateAsync("/PasswordPage", animated: true);
            }

        }
    }
}
