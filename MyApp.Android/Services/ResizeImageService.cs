
using MyApp.Services;
using Android.Graphics;
using System.IO;
using Xamarin.Essentials;


namespace MyApp.Droid.Services
{
    public class ResizeImageService : IResizeImageService
    {
        public string ResizeImage(string imagePath, string nameImg)
        {
            float height = 1300;
            float width = 1200;
            float newHeight = 0;
            float newWidth = 0;

            BitmapFactory.Options options = new BitmapFactory.Options();
            options.InSampleSize = 10;

            Bitmap originalImage = BitmapFactory.DecodeFile(imagePath, options);

            int originalHeight = originalImage.Height;
            int originalWidth = originalImage.Width;

            if (originalHeight > originalWidth)
            {
                newHeight = height;
                float ratio = originalHeight / height;
                newWidth = originalWidth / ratio;
            }
            else
            {
                newWidth = width;
                float ratio = originalWidth / width;
                newHeight = originalHeight / ratio;
            }

            Bitmap resizedImg = Bitmap.CreateScaledBitmap(originalImage, (int)newWidth, (int)newHeight, false);

            Bitmap resizedImage = Rotate(resizedImg);

            resizedImg.Recycle();
            originalImage.Recycle();

            using (MemoryStream ms = new MemoryStream())
            {
                resizedImage.Compress(Bitmap.CompressFormat.Png, 10, ms);

                resizedImage.Recycle();

                return SaveToFile(ms.ToArray(), nameImg);
            }
        }

        public string SaveToFile(byte[] bitmapImg, string name)
        {
            // var path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            string path = System.IO.Path.Combine(FileSystem.AppDataDirectory, name);
            FileStream stream = new FileStream(path, FileMode.Create);
            stream.Write(bitmapImg, 0, bitmapImg.Length);
            stream.Flush();
            stream.Close();

            return path;
        }

        Bitmap Rotate(Bitmap bitmap)
        {
            Matrix matrix = new Matrix();

            if (bitmap.Width > bitmap.Height)
            {
                matrix.SetRotate(90);
            }

            return Bitmap.CreateBitmap(bitmap, 0, 0, bitmap.Width, bitmap.Height, matrix, true);
        }


    }
}