using System;
using System.Diagnostics.Contracts;
using System.IO;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace IAB330_Scruff.Backend_communication
{
    // Class for convering a Mediafile (object from camera or image selector) to Base64 string (stored on server)
    public static class ImageConverter
    {
        public static String ConvertTo(MediaFile photo) {
            byte[] byteArr;
            Stream stream = photo.GetStream();
            var memoryStream = new MemoryStream();
            stream.CopyTo(memoryStream);
            byteArr = memoryStream.ToArray();
            String base64 = Convert.ToBase64String(byteArr);
            return base64;
        }
        //convering from base64 to Image (for displaying in client).
        public static Image ConvertFrom(String base64)
        {
            if(base64 == "") {
                return null;
            }
            byte[] byteArr = Convert.FromBase64String(base64);
            Stream stream = new MemoryStream(byteArr);
            Image image = new Image();
            image.Source = ImageSource.FromStream(() => stream);

            return image;
        }
    }
}
