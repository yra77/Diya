

namespace MyApp.Services
{
    public interface IResizeImageService
    {
        string ResizeImage(string image, string nameImg);
        string SaveToFile(byte[] bitmapImg, string name);
    }
}
