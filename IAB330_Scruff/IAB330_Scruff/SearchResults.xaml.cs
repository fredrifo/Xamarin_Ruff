using IAB330_Scruff.BackendCommunication;
using IAB330_Scruff.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAB330_Scruff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchResults : ContentPage
    {
        public static string gender ="%";
        public static string breed = "%";
        public static string searchString = "%";
        public static List<AdClass> ads = new List<AdClass>(); //Obtain all the ads
        public static bool loading = false;

        public SearchResults()
        {
            InitializeComponent();
        }

        // Class for individual ad items
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

        /// <summary>
        /// What happens when the page loads
        /// </summary>
        protected override async void OnAppearing()
        {
         
            loadbar.IsVisible = true;

            try
            {
                loading = true;
                sss.ItemsSource = new List<AdClass>(); //Set the listviews item source to the list of ads
                base.OnAppearing();

                int status = await AdSearch.adSearch(gender, breed,searchString);
                gender = "%";
                breed = "%";
                searchString = "%";
                if (status == 1) {
                    await DisplayAlert("Error", "Make sure you are logged in", "Ok");
                    loadbar.IsVisible = false;
                    return;
                }
                if(status == 2) {
                    await DisplayAlert("Error", "Make sure you have a internet connection", "Ok");
                    loadbar.IsVisible = false;
                    return;
                }
                if(status == 3) {
                    await DisplayAlert("Search", "No results", "Ok");
                    loadbar.IsVisible = false;
                    return;
                }
                

                ads = AdSearch.searchResults;
                gender = "%";
                breed = "%";
                searchString = "%";
                sss.HeightRequest = 121 * ads.Count;
                sss.ItemsSource = ads;
                loadbar.IsVisible = false;
            }
            catch (Exception e)
            {
                loadbar.IsVisible = false;
                await DisplayAlert("Error", "Try again to open later", "Ok");
                loading = false;
            }
            loading = false;
        }

        /// <summary>
        /// What happens when an item is selected
        /// </summary>
        /// <param name="sender">BLAH BLAH</param>
        /// <param name="e"></param>
        private async void adSelectedAsync(object sender, EventArgs e)
        {
            if(loading)
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

        //Go to adding ad page
        private async void AddToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MakeAd());
        }

        //Going to search page
        private async void SearchToolbarItem_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SearchPage());
        }
    }
}