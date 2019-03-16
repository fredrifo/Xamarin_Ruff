using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAB330_Scruff.Backend_communication;
using IAB330_Scruff.BackendCommunication;
using IAB330_Scruff.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static IAB330_Scruff.Backend_communication.AdUpload;

namespace IAB330_Scruff
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Register : ContentPage
	{
		public Register ()
		{
			InitializeComponent ();
		}
       
        private async void RegisterUserButton_ClickedAsync(object sender, EventArgs e)
        {
            //Register user
            int reigster = await RegisterUser.RegisterUserAsync(firstNameEntry.Text, lastNameEntry.Text, passwordRegisterEntry.Text, emailEntry.Text, phoneEntry.Text);
            //Verify the registering result
            if(reigster == 0)
            {//Succesfful! take user back to login
                DisplayAlert("Register", "Log in to continue", "OK");
                Application.Current.MainPage = new Login();
                
            } else if(reigster == 1) { //Account already exists
                DisplayAlert("Register", "Account already registered. Try again", "OK");
            } else if (reigster ==2){ //Not all data is entered
                DisplayAlert("Register", "Make sure all data is entered", "OK");
            } else if (reigster == 3) { //Server/network error
                DisplayAlert("Register", "Network/problem", "OK");
            }
            return;
        }

        //Go back to login page
        private async void Handle_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new Login();
        }
    }
}