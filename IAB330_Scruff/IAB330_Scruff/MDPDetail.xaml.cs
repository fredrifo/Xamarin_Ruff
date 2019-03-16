using IAB330_Scruff.Backend_communication;
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
    public partial class MDPDetail : ContentPage
    {
        //All functions take you to another page
        public MDPDetail()
        {
            InitializeComponent();
        }

        private async void gmapsButton_Clicked(object sender, EventArgs e)
        {
            SearchResults.breed = "%";
            SearchResults.gender = "%";
            await Navigation.PushAsync(new SearchResults());
        }

        void Handle_Clicked(object sender, System.EventArgs e)
        {
            LoginSession.loginToken = "";
            LoginSession.success = false;
            LoginSession.usernames = "";
            Application.Current.MainPage = new Login();
        }

        private async void searchButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }

        private async void loginButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Login());
        }

        private async void userRegisterButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Register());
        }

        private async void createAdButton_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MakeAd());
        }

        private async void openAd_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AdPage(9));
        }
    }
}