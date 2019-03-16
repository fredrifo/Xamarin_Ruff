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
    public static class AdSearch
    {
        public static List<AdClass> searchResults = new List<AdClass>();
        public static List<AdClass> searchResults2 = new List<AdClass>();
        private class Credentials
        {
            public string username;
            public string password;
            public string gender;
            public string breed;
            public string searchText;
            // Subclass for seriallizing a JSON object
            public Credentials(string username, string password, string gender, string breed,string searchText)
            {
                this.username = username;
                this.password = password;
                this.gender = gender;
                this.breed = breed;
                this.searchText = searchText;
            }
            public Credentials(string username, string password, string gender, string breed)
            {
                this.username = username;
                this.password = password;
                this.gender = gender;
                this.breed = breed;
                
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
        /// <para>Returns int 3 No results (network,server)</para> 
        /// <returns>int</returns>
        public static async Task<int> adSearch(string gender, string breed,string searchText)
        {
            // creating a http packet and sends it to the server
            AdClass advertisement;
            HttpClient request = new HttpClient();
            Credentials temp = new Credentials(LoginSession.usernames, LoginSession.loginToken,gender,breed,searchText);
            var uri = new Uri("https://www.blocktray.com/scruff/ad_search.php");
            String jsonString = JsonConvert.SerializeObject(temp);

            try
            {
                // Sends the request and waits
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await request.PostAsync(uri, content);
                searchResults.Clear();
                //reads in result and checks for errors before trying to process JSON
                String result = response.Content.ReadAsStringAsync().Result;

                if (result == "1")
                {
                    return 1;
                }
                if (result == "2")
                {
                    return 2;
                }
                if(result == "3") {
                    return 3;
                }
                // Creates AdClass objects from the recieved information from the server. These results are displayed as search results
                var ad = JArray.Parse(result);
                foreach (JObject o in ad.Children<JObject>())
                {
                    AnimalClass animal = new AnimalClass((string)o["name"], DateFromString((string)o["date_born"]), (string)o["breed"], (string)o["gender"], (string)o["animal_type"]);
                    advertisement = new AdClass(animal, DateFromString((string)o["date_available"]), (string)o["ad_text"], (string)o["ad_title"], (string)o["zip_code"], (int)o["ad_price"], (string)o["street_address"], (string)o["ad_type"]);
                    advertisement.adId = (int)o["ad_id"];
                    advertisement.base64Image = (string)o["ad_picture"];
                    advertisement.adImage = ImageConverter.ConvertFrom(advertisement.base64Image);
                    searchResults.Add(advertisement);
                }
                return 0;
            }
            catch (Exception e)
            {
                return 2;
            }
        }

        public static async Task<int> adSearch2(string gender, string breed)
        {
            // Does the same as the method above but sends the desired breed or gender the user is seraching for to limit result.
            AdClass advertisement;
            HttpClient request = new HttpClient();
            Credentials temp = new Credentials(LoginSession.usernames, LoginSession.loginToken, gender, breed);
            var uri = new Uri("https://www.blocktray.com/scruff/ad_searchs.php");
            String jsonString = JsonConvert.SerializeObject(temp);

            try
            {
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await request.PostAsync(uri, content);
                searchResults.Clear();
                string result = response.Content.ReadAsStringAsync().Result;
                if (result == "1")
                {
                    return 1;
                }
                if (result == "2")
                {
                    return 2;
                }

                var ad = JArray.Parse(result);
                foreach (JObject o in ad.Children<JObject>())
                {
                    AnimalClass animal = new AnimalClass((string)o["name"], DateFromString((string)o["date_born"]), (string)o["breed"], (string)o["gender"], (string)o["animal_type"]);
                    advertisement = new AdClass(animal, DateFromString((string)o["date_available"]), (string)o["ad_text"], (string)o["ad_title"], (string)o["zip_code"], (int)o["ad_price"], (string)o["street_address"], (string)o["ad_type"]);
                    advertisement.adId = (int)o["ad_id"];
                    advertisement.base64Image = (string)o["ad_picture"];
                    advertisement.adImage = ImageConverter.ConvertFrom(advertisement.base64Image);
                    searchResults.Add(advertisement);
                }
                return 0;
            }
            catch (Exception e)
            {
                return 3;
            }
        }

        public static DateTime DateFromString(string date)
        {
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
