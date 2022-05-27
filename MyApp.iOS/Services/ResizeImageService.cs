using Foundation;
using MyApp.ViewModels.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UIKit;

namespace MyApp.iOS.Services
{
    class ResizeImageService : IResizeImageService
    {
        public string ResizeImage(string image, string nameImg)
        {

            UIImage originalImage = UIImage.FromFile(image);


            float height = 1300;
            float width = 1200;
            nfloat newHeight = 0;
            nfloat newWidth = 0;

            var originalHeight = originalImage.Size.Height;
            var originalWidth = originalImage.Size.Width;


            if (originalHeight > originalWidth)
            {
                newHeight = height;
                nfloat ratio = originalHeight / height;
                newWidth = originalWidth / ratio;
            }
            else
            {
                newWidth = width;
                nfloat ratio = originalWidth / width;
                newHeight = originalHeight / ratio;
            }

            width = (float)newWidth;
            height = (float)newHeight;

            UIGraphics.BeginImageContext(new SizeF(width, height));
            originalImage.Draw(new RectangleF(0, 0, width, height));
            var resizedImage = UIGraphics.GetImageFromCurrentImageContext();
            UIGraphics.EndImageContext();


            //resizedImage.Dispose();

            using (var bytesImagen = resizedImage.AsPNG())
            {

                var documents = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                var path = System.IO.Path.Combine(documents, nameImg);
                bytesImagen.Save(path, true);

                return path;
            }

        }

        public string SaveToFile(byte[] bitmapImg, string name) 
        {
            return name;
        }
    }
}