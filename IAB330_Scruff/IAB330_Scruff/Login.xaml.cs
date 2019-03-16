using IAB330_Scruff.Backend_communication;
using IAB330_Scruff.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAB330_Scruff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            // Log out of the user account if logged in
            LoginSession.loginToken = null;
            LoginSession.usernames = null;
            LoginSession.success = false;


        }

            //Login button clicked
            private async void loginButton_Clicked(object sender, EventArgs e)
        {
            //Verify the credentials with the server
            loadbar.IsVisible = true;
            int test = await LoginSession.serverLoginAsync(emailEntry.Text, passwordEntry.Text);
            //Success!
            if (test == 0)
            {
                //Download the user's details

                await ProfileDownload.DownloadOwnProfileAsync();
                loadbar.IsVisible = false;
                //Navigate to home page
                Application.Current.MainPage = new MDP();
            }
            //Fail
            else if (test == 1)
            {
                loadbar.IsVisible = false;
                DisplayAlert("Login", "Wrong username/password", "OK");
            }
            //Network issue (usually server issue)
            else
            {
                loadbar.IsVisible = false;
                DisplayAlert("Login", "Network problem. Try again later", "OK");
            }
        }

        //Register button clicked
        void registerButton_Clicked(object sender, System.EventArgs e)
        {
            //Open the register pages
            Application.Current.MainPage = new Register();
        }
    }
}