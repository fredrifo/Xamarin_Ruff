using System;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace IAB330_Scruff.Backend_communication
{
    public class ImageDownload
    {
        private class ImagePacket
        {
            public string imageData;
            public string username;
            public string loginToken;

            public ImagePacket(string base64String, string usernames, string loginToken)
            {
                this.imageData = base64String;
                this.username = usernames;
                this.loginToken = loginToken;
            }
        }



        public static Image DownloadProfilePicture()
        {

            ImagePacket temp = new ImagePacket(null, LoginSession.usernames, LoginSession.loginToken);
            // can add captcha later
            HttpClient request = new HttpClient();

            var uri = new Uri("https://www.blocktray.com/scruff/profile_picture_download.php");
            String jsonString = JsonConvert.SerializeObject(temp);


            try
            {
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = request.PostAsync(uri, content).Result;
                String result = response.Content.ReadAsStringAsync().Result;
                if (result == "1") {
                    // error could not find image
                    return null;
                } else {
                    return ImageConverter.ConvertFrom(result);
                }



               


            }
            catch (Exception e)
            {
                return null;
            }



        }



    }
}
