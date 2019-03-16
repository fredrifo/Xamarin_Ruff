using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IAB330_Scruff.Backend_communication;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;

namespace IAB330_Scruff.BackendCommunication
{
    public static class PostUpload
    {
        private class ImagePacket
        {
            public string imageData;
            public string username;
            public string loginToken;
            public string postText;
            public int adId;
            public string datePosted;
            // Subclass for seriallizing a JSON object
            public ImagePacket(string base64String, string usernames, string loginToken, int adId,string postText)
            {
                this.imageData = base64String;
                this.username = usernames;
                this.loginToken = loginToken;
                this.datePosted = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
        }

        /// <summary>
        /// Uploads a post to an ad with a picture and text.
        /// <para>Returns int:</para>
        /// <para>Returns int 0 if successful</para> 
        /// <para>Returns int 1 if user not logged in</para>
        /// <para>Returns int 2 if Access denied or wrong adid</para>
        /// <para>Returns int 3 if Failed to write picture (try again)</para>
        /// <para>Returns int 4 network/server error</para>
        /// </summary>
        /// <returns>int</returns>
        /// <param name="image">Image.</param>
        public static async Task<int> UploadPostAsync(MediaFile image,int adId,string postText)
        {
            //Converts supplied inmage to base64 string for upload
            String base64String = ImageConverter.ConvertTo(image);
            ImagePacket temp = new ImagePacket(base64String, LoginSession.usernames, LoginSession.loginToken,adId,postText);
            temp.postText = postText;
            temp.adId = adId;
            HttpClient request = new HttpClient();
            // uri for this request
            var uri = new Uri("https://www.blocktray.com/scruff/post_upload.php");
            //serializing the Imagepacket object
            String jsonString = JsonConvert.SerializeObject(temp);

            try
            {
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                //posts the json from the serializing to the server.
                HttpResponseMessage response = await request.PostAsync(uri, content);
                //reads in result
                String result = response.Content.ReadAsStringAsync().Result;
                // returns the potential error/success code from the server
                int statusCode = Int16.Parse(result);
                return statusCode;
            }
            catch (Exception e)
            {
                return 4;
            }
        }
    }
}
