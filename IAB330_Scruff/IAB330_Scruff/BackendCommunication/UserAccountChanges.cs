using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IAB330_Scruff.Backend_communication;
using Newtonsoft.Json;

namespace IAB330_Scruff.BackendCommunication
{
    public static class UserAccountChanges
    {
        private class PasswordChange
        {
            public String username;
            public String password;
            public String newPassword;
            public PasswordChange(string newPassword)
            {
                this.username = LoginSession.usernames;
                this.password = LoginSession.loginToken;
                this.newPassword = newPassword;
            }
        }

        public static async Task<int> ChangePasswordAsync(string newPassword) {

            // Creates a new http client
            HttpClient request = new HttpClient();
            // creates a new Person object (subclass) for seriallzing the content for the server
            PasswordChange tempPerson = new PasswordChange(newPassword);
            // uri for this kind of request 
            var uri = new Uri("https://www.blocktray.com/scruff/change_password.php");
            // prepares the json string
            String jsonString = JsonConvert.SerializeObject(tempPerson);

            try
            {
                // prepares and sends message to server
                var content = new StringContent(jsonString, Encoding.UTF8, "application/json");
                HttpResponseMessage response = await request.PostAsync(uri, content);
                // processes the response from server. The statusCode indicates the response from the server (succss/fail)
                String result = response.Content.ReadAsStringAsync().Result;
                int statusCode = Int16.Parse(result);

                return statusCode;
            }
            catch (Exception e)
            {
                return 3;
            };


            return 0;
        }
    }
}
