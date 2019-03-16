using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IAB330_Scruff.Backend_communication
{
    public static class LoginSession
    {
        public static string loginToken; //This is used as a key to access the server
        public static string usernames;
        public static bool success = false; //Has the user logged in
        // Subclass for seriallizing a JSON object
        private class Credentials
        {
            public string username;
            public string password;
            public Credentials(string username, string password)
            {
                this.username = username;
                this.password = password;
            }
        }

        /// <summary>
        /// Logs user in to the server using a provided username and password. AFTER successful login other server methods can be run.
        /// <para>Returns int 0 if successful</para>
        /// <para>Returns int 1 if wrong username/password</para>
        /// <para>Returns int 2 if other problem (network,server)</para>
        /// </summary>
        /// <returns>int</returns>
        /// <param name="username">Username.</param>
        /// <param name="password">Password.</param>
        public static async Task<int> serverLoginAsync(string username, string password) {
            Credentials creds = new Credentials(username, password);
            HttpClient request = new HttpClient(); //Prepare to send a HTTP request to obtain data
            var uri = new Uri("https://www.blocktray.com/scruff/login.php"); //URI to be used to access server DB
            string jsonString = JsonConvert.SerializeObject(creds); //Convert credentials to JSON format

            try
            {
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await request.PostAsync(uri, content); //Send the HTTP request, and receive the response
                string result = response.Content.ReadAsStringAsync().Result; //Read the response string

                //Check if credentials are valid and return a confirmation code
                if (result == "wrong_username_password") {
                    //Login was in unsuccessful
                    success = false;
                    return 1;
                } else if(result == "") {
                    return 2;
                } else {
                    //Login was successful
                    success = true;
                    loginToken = result;
                    usernames = username;
                    return 0;
                }
            }
            catch (Exception e)
            {
            return 2;
            }
        }
    }
}
