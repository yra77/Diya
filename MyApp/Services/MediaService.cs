
using System.Threading.Tasks;
using Xamarin.Essentials;


namespace MyApp.Services
{

    class MediaService : IMediaService
    {

        private IResizeImageService _resizeImage;
        public MediaService(IResizeImageService resizeImage)
        {
            _resizeImage = resizeImage;
        }
        public async Task<string> OpenCamera()
        {
            var photo = await MediaPicker.CapturePhotoAsync();
            if (photo != null)// do not remove - will be error
            {
                string str = _resizeImage.ResizeImage(photo.FullPath, photo.FileName);
                return str;
            }
            return "one.png";
        }

        public string SaveToAppFolder(byte[] image, string fileName)
        {
            return _resizeImage.SaveToFile(image, fileName);
        }
    }

}
