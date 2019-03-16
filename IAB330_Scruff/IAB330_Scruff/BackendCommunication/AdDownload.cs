using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IAB330_Scruff.Backend_communication;
using IAB330_Scruff.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IAB330_Scruff.BackendCommunication
{
    public static class AdDownload
    {
        public static AdClass lastDownload;
        private class Credentials
        {
            public string username;
            public string password;
            public int adId;
            // Subclass for seriallizing a JSON object
            public Credentials(string username, string password,int adId)
            {
                this.username = username;
                this.password = password;
                this.adId = adId;
            }
        }

        /// <summary>
        /// Downloads an entire ad with provided adId. This should be called when a user clicks on an ad from results. Includes the following information:
        /// <para>AD information (including picture),Animal information, posts on this ad (including pictures), Information about ad owner</para>
        /// <para>Access information by accessing AdClass.lastDownload</para>
        /// </summary>
        /// <para>Returns int 0 if successful</para> 
        /// <para>Returns int 1 if user not logged in</para>
        /// <para>Returns int 2 if other problem (network,server)</para> 
        /// <returns>int</returns>
        public static async Task<string> DownloadAd(int adId)
        {
            // creates new web client
            HttpClient request = new HttpClient();
            // makes subclass obejct for seriallizing
            Credentials temp = new Credentials(LoginSession.usernames, LoginSession.loginToken,adId);
            // Request URI for this request
            var uri = new Uri("https://www.blocktray.com/scruff/ad_download.php");
            //serializing the object
            string jsonString = JsonConvert.SerializeObject(temp);

            try
            {
                PostClass tester2;
                // Encodes seriallized object into the http body
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await request.PostAsync(uri, content);
                // reading in response string
                string result = response.Content.ReadAsStringAsync().Result;
                if (result == "1")
                {
                    return "";
                }
                // parsing the result. Is a array of objects 
                var ad = JArray.Parse(result);
                // Runing rough every object and creates instances of User,Adclass, AnimalClass from each object in the array. Used for displaying the ad
                foreach (JObject o in ad.Children<JObject>())
                {
                    AnimalClass animal = new AnimalClass((string)o["name"], DateFromString((string) o["date_born"]),(string)o["breed"], (string)o["gender"], (string)o["animal_type"]);
                    AdClass advertisement = new AdClass(animal, DateFromString((string)o["date_available"]),(string)o["ad_text"], (string)o["ad_title"], (string)o["zip_code"],(int)o["ad_price"], (string)o["street_address"], (string)o["ad_type"]);
                    User user = new User((string)o["first_name"],(string)o["last_name"],(string)o["email"],(string)o["phone_number"],(int)o["user_id"]);
                    if((int)o["owner"] == 1) {
                        advertisement.owner = true;
                    } else {
                        advertisement.owner = false;
                    }
                    List<PostClass> posts = new List<PostClass>();
                    foreach (JObject post in o["posts"].Children<JObject>())
                    { 
                        if((string)post["post_picture"] != "null") {
                            tester2  = new PostClass((string)post["post_text"], ImageConverter.ConvertFrom((string)post["post_picture"]), DateTimeConverter.FromSQL((string)post["post_date"]));
                            tester2.base64 = (string)post["post_picture"];
                            posts.Add(tester2);
                            
                        } else {
                            continue;
                        }
                    }
                    // If the user or ad has related pictures it will read in the data and store it as MediaFiles
                    if((string)o["user_picture"] != "") {
                        user.base64Image = (string)o["user_picture"];
                        user.profileImage = ImageConverter.ConvertFrom(user.base64Image);
                    } else {
                        user.profileImage = null;
                    }
                    if ((string)o["ad_picture"] != "")
                    {
                        advertisement.base64Image = (string)o["ad_picture"];
                        advertisement.adImage = ImageConverter.ConvertFrom(advertisement.base64Image);
                    } else {
                        advertisement.adImage = null;
                    }
                        advertisement.posts = posts;
                        advertisement.animal = animal;
                        advertisement.user = user;
                        lastDownload = advertisement;
                    }
                return "";
            }
            catch (Exception e)
            {
                return e.ToString();
            }
        }
        public static DateTime DateFromString(string date) {
            // convert from 
            string formatString = "yyyy-MM-dd HH:mm:ss";
            if (date == "")
            {
                return DateTime.Now;
            } 
            else
            {
                return DateTime.Now;
            }
        }
    }
}
