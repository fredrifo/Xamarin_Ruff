using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IAB330_Scruff.Models
{
    public class User
    {
        public static User currentUser;
        public int user_ID { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string firstName;
        public string lastName;
        public Image profileImage;
        public string base64Image;
        public string email;
        public string phoneNumber;
        public int userId;
        public User() { }
        public User (string username, string password)
        {
            this.username = username;
            this.password = password;
        }

        public User(string firstName, string lastName, string base64Image, string email,string phoneNumber)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.base64Image = base64Image;
            this.email = email;
        }
        public User(string firstName, string lastName, string email, string phoneNumber,int userId)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.phoneNumber = phoneNumber;
            this.email = email;
            this.userId = userId;
        }

        // Waiting until database stuff works for this
        public bool VerifyInformation()
        {
            if (username != null && password != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
