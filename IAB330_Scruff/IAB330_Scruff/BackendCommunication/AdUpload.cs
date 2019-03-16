using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IAB330_Scruff.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace IAB330_Scruff.Backend_communication
{
    public static class AdUpload
    {
        // class for serializing incomming data
        private class AdPackage
        {
            public AdClass ad;
            public String password;
            public String email;
            // Subclass for seriallizing a JSON object
            public AdPackage(AdClass ad)
            {
                this.ad = ad;
                this.password = LoginSession.loginToken;
                this.email = LoginSession.usernames;
            }
           
        }
        public  class Result
        {
            public int adId;
            public int status;

            public Result(int adId, int status)
            {
                this.adId = adId;
                this.status = status;
            }
        }

        /// <summary>
        /// Uploads a AdClass object to the server. A AdClass object contains a AnimalClass object as a field. First make a AnimalClass object, then pass it to the constructor of AdClass then pass the Adclass to this function.
        /// <para>Returns a Result object containing result code (exmapled under:) and the Ad id of the ad created. This id can be used to upload a picture to the AD via ImageUpload.</para>
        /// <para>returns int 0 if successfully created an ad.</para>
        /// <para>returns int 1 if User has not logged in</para>
        /// <para>returns int 2 if there is invalid data (example: unvalid email)</para>
        /// <para>returns int 3 if server/network problem</para>
        /// </summary>
        /// <returns>The user async.</returns>
        public static async Task<Result> UploadAd(AdClass ad)
        {
            // can add captcha later
            HttpClient request = new HttpClient();
            AdPackage tempPerson = new AdPackage(ad);

            var uri = new Uri("https://www.blocktray.com/scruff/create_ad.php");
            String jsonString = JsonConvert.SerializeObject(tempPerson);

            try
            {
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await request.PostAsync(uri, content);
                String result = response.Content.ReadAsStringAsync().Result;
                var res = JObject.Parse(result);
                return new Result((int)res["adId"], (int)res["status"]);

                /*int statusCode = Int16.Parse(result);
                return statusCode;*/
            }
            catch (Exception e)
            {
                return new Result(0,5);
            };
        }
    }
}
