using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace IAB330_Scruff.Backend_communication
{
    public static class RegisterUser
    {
        // class for serializing incomming data
        private class Person
        {
            public String firstName;
            public String lastName;
            public String password;
            public String email;
            public string phoneNumber;
            // Subclass for seriallizing a JSON object
            public Person (String firstName, String lastName, String password, String email, string phoneNumber) {
                this.firstName = firstName;
                this.lastName = lastName;
                this.password = password;
                this.email = email;
                this.phoneNumber = phoneNumber;
            }
        }

        /// <summary>
        /// Makes a new user account with the provided details. All parameters are required.
        /// <para>returns int 0 if successful</para>
        /// <para>returns int 1 if user allready registered (email in use)</para>
        /// <para>returns int 2 if there is invalid data (example: unvalid email)</para>
        /// <para>returns int 3 if server/network problem</para>
        /// </summary>
        /// <returns>The user async.</returns>
        /// <param name="firstName">First name.</param>
        /// <param name="lastName">Last name.</param>
        /// <param name="password">Password.</param>
        /// <param name="email">Email.</param>
        public static async Task<int> RegisterUserAsync(String firstName, String lastName, String password, String email,String phoneNumber)
        {
            // Creates a new http client
            HttpClient request = new HttpClient();
            // creates a new Person object (subclass) for seriallzing the content for the server
            Person tempPerson = new Person(firstName, lastName, password, email,phoneNumber);
            // uri for this kind of request 
            var uri = new Uri("https://www.blocktray.com/scruff/register.php");
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
        }
    }
}
