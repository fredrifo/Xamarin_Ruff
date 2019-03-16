using System;
using System.Collections.Generic;
using IAB330_Scruff.BackendCommunication;
using Xamarin.Forms;

namespace IAB330_Scruff
{
    public partial class Settings : ContentPage
    {
        public Settings()
        {
            InitializeComponent();
        }

        async void ChangePassword_async(object sender, System.EventArgs e)
        {
            string tempPassword = passwordEntry.Text;
            if(tempPassword.Length < 4) {
                DisplayAlert("Error", "Password not long enough", "OK");
                return;
            }
            int x = await UserAccountChanges.ChangePasswordAsync(tempPassword);
            passwordEntry.Text = "";
            if(x==0) {
                DisplayAlert("Success", "Password has been changed", "OK");
            } else if(x==1) {
                DisplayAlert("Error", "Make sure you have logged in", "OK");
            } else if(x==2) {
                DisplayAlert("Error", "Check your connection", "OK");
            }
            else
            {
                DisplayAlert("Error", "Unknown error", "OK");
            }


        }
    }
}
