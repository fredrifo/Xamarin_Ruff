using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IAB330_Scruff.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace IAB330_Scruff.Backend_communication
{
    public static class ProfileDownload
    {
        // Subclass for seriallizing a JSON object
        private class Credentials
        {
            public String username;
            public String password;
            public Credentials(String username, String password)
            {
                this.username = username;
                this.password = password;
            }
        }

        /// <summary>
        /// Downloads the current logged in users profile. The users properties including profile picture is stored in User.currentUser
        /// </summary>
        /// <para>Returns int 0 if successful</para> 
        /// <para>Returns int 1 if user not logged in</para>
        /// <para>Returns int 2 if other problem (network,server)</para> 
        /// <returns>int</returns>
        public static async Task<int> DownloadOwnProfileAsync()
        {
            // this method downloads the users profile from the server
            HttpClient request = new HttpClient();
            Credentials temp = new Credentials(LoginSession.usernames, LoginSession.loginToken);
            var uri = new Uri("https://www.blocktray.com/scruff/profile_download.php");

            // serializing the credentials object (subclass)
            String jsonString = JsonConvert.SerializeObject(temp);

            try
            {
                //prepares the message
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                //Posts content to server
                HttpResponseMessage response = await request.PostAsync(uri, content);
                // reads in result from server and returns potential errors
                String result = response.Content.ReadAsStringAsync().Result;
                if(result == "1") {
                    return 1;
                } else if(result =="2") {
                    return 2;
                }
                // if json reponse is recieved deserialzing it to a user object
                var test = JObject.Parse(result);
                User.currentUser = new User((string)test["firstName"], (string)test["lastName"], (string)test["base64Image"], (string)test["email"], (string)test["phoneNumber"]);
                if(User.currentUser.email == null)
                {
                    return 2;
                }
                // converts image from the user to a Image file and stores its
                User.currentUser.profileImage = ImageConverter.ConvertFrom(User.currentUser.base64Image);
                return 0;
            }
            catch (Exception e)
            {
                return 2;
            }
        }

        // Downloads an other users profile with a AD ID
        public static User DownloadOtherProfile(String AdId)
        {
            return null;
        }
    }
}
