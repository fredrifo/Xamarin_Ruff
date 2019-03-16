using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace IAB330_Scruff.Backend_communication
{
    public class ImageUpload
    {
        private class ImagePacket {
            public string imageData;
            public string username;
            public string loginToken;
            public int adId;
            // Subclass for seriallizing a JSON object
            public ImagePacket(string base64String, string usernames, string loginToken)
            {
                this.imageData = base64String;
                this.username = usernames;
                this.loginToken = loginToken;
            }
        }

        /// <summary>
        /// Uploads a profile picture to the server from a MediaFile. You can access the uploaded picture by calling ProfileDownload.DownloadOwnProfileAsync()
        /// <para>Returns int:</para>
        /// <para>Returns int 0 if successful</para> 
        /// <para>Returns int 1 if user not logged in</para>
        /// <para>Returns int 2 if other problem (network,server)</para> 
        /// </summary>
        /// <returns>int</returns>
        /// <param name="image">Image.</param>
        public static async Task<int> uploadProfilePictureAsync(MediaFile image) {

            //converting image to string
            String base64String = ImageConverter.ConvertTo(image);
            //creates a image package for sending to server
            ImagePacket temp = new ImagePacket(base64String, LoginSession.usernames, LoginSession.loginToken);

            HttpClient request = new HttpClient();
            // request URI
            var uri = new Uri("https://www.blocktray.com/scruff/profile_picture_upload.php");
            String jsonString = JsonConvert.SerializeObject(temp);

            try
            {
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                //posts the http request with image in the body
                HttpResponseMessage response = await request.PostAsync(uri, content);
                //parsing result to string
                String result = response.Content.ReadAsStringAsync().Result;
                // gets statuscode wich is used to determine if operaton was successful
                int statusCode = Int16.Parse(result);
                return statusCode;
            }
            catch (Exception e)
            {
                return 3;
            }
        }

        /// <summary>
        /// Uploads a picture for an ad. Provide an ad id you want to add/change picture for and a mediafile. Will override current picture if exists.
        /// <para>Returns 0 if successfull</para>
        /// <para>Returns 1 if user is not logged in</para>
        /// <para>Returns 2 if the user does not own the ad (access denied)</para>
        /// <para>Returns 3 if unable to upload image (try again)</para>
        /// <para>Returns 4 if network/server problem.</para>
        /// </summary>
        /// <returns>int</returns>
        /// <param name="image">Image.</param>
        /// <param name="adId">Ad identifier.</param>
        public static async Task<int> uploadAdPictureAsync(MediaFile image,int adId) {
            String base64String = ImageConverter.ConvertTo(image);
            ImagePacket temp = new ImagePacket(base64String, LoginSession.usernames, LoginSession.loginToken);
            temp.adId = adId;
            HttpClient request = new HttpClient();
            // does the same as the method above, but includes a AD id for server to know which ad you are uploading a image for.
            var uri = new Uri("https://www.blocktray.com/scruff/ad_picture_upload.php");
            String jsonString = JsonConvert.SerializeObject(temp);

            try
            {
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await request.PostAsync(uri, content);

                String result = response.Content.ReadAsStringAsync().Result;

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
