using System;
using System.Collections.Generic;
using IAB330_Scruff.BackendCommunication;
using IAB330_Scruff.Models;
using Xamarin.Forms;

namespace IAB330_Scruff
{
    public partial class MyAds : ContentPage
    {
        public static string gender = "%";
        public static string breed = "%";
        public static List<AdClass> ads = new List<AdClass>();
        public static bool loading = false;

        public MyAds()
        {
            InitializeComponent();
        }

        //Class for the items show in user's individual ads
        public class listItem
        {
            public string adName = "%";
            public string adText = "%";
            public int price;

            public listItem(string adName, string adText, int price)
            {
                this.adName = adName;
                this.adText = adText;
                this.price = price;
            }
        }
        
        protected override async void OnAppearing()
        {
            try
            {
                loading = true;
                sss.ItemsSource = new List<AdClass>(); //Set the listeview source to the lsit of ads owner by the user
                loadbar.IsVisible = true;
                base.OnAppearing();

                int status = await AdSearch.adSearch2(gender, breed); //Find ads which match the gender and breed

                // check for errors. Authentication problem will return 1, server or connection problem will return 2
                if (status == 1)
                {
                    await DisplayAlert("Error", "Make sure you are logged in", "Ok");

                    loadbar.IsVisible = false;
                    return;
                }
                if (status == 2)
                {
                    await DisplayAlert("Error", "Make sure you have a internet connection", "Ok");
                    loadbar.IsVisible = false;
                    return;
                }

                ads = AdSearch.searchResults; //Set the ads variable to the results of the previous search above
                gender = "%";
                breed = "%";
                loadbar.IsVisible = false;
                sss.HeightRequest = 121 * ads.Count; //Set the height of the listview relative to the number of ads returned from the search
                sss.ItemsSource = ads; //Set the listviews source to the updated search results
            }
            catch (Exception e)
            { //If an exception is raised, a popup will tell the user to try again later
                loadbar.IsVisible = false;
                await DisplayAlert("Error", "Try again later", "Ok");
                loading = false;
            }
            loading = false;
        }

        //If an ad is selected, take the user to its individual page
        private async void adSelectedAsync(object sender, EventArgs e)
        {
            if (loading)
            {
                return;
            }

            var lv = (ListView)sender;
            if ((IAB330_Scruff.Models.AdClass)lv.SelectedItem == null)
            {
                return;
            }

            AdClass myItem = (IAB330_Scruff.Models.AdClass)lv.SelectedItem;
            await Navigation.PushAsync(new AdPage(myItem.adId));
        }
    }
}
