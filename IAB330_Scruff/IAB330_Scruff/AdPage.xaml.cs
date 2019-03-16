using System;
using System.Collections.Generic;
using IAB330_Scruff.Backend_communication;
using IAB330_Scruff.BackendCommunication;
using IAB330_Scruff.Models;
using Xamarin.Forms;

namespace IAB330_Scruff
{
    public partial class AdPage : ContentPage
    {
        static public int tempAdID;
        public AdPage(int id)
        {
            tempAdID = id; //This is used to identify which ad information to download
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await AdDownload.DownloadAd(tempAdID);

            AdClass t = AdDownload.lastDownload;
            //Set the app bar's title to thename of the animal and the price
            Title = t.animal.name + " $" + t.adPrice;
            //Insert all the animal data into the fields
            dogName1.Text = t.animal.name;
            dogName.Text = t.animal.name;
            dogBreed.Text = t.animal.breed;
            dogPrice.Text = "$ " + t.adPrice.ToString();
            dogGender.Text = t.animal.gender;

            //Insert all the ad details in to the fields
            adText.Text = t.adText;
            adTitle.Text = t.adTitle;
            streetAddress.Text = t.streetAddress;
            advertiserName.Text = t.user.firstName + " " + t.user.lastName;
            phoneNumber.Text = t.user.phoneNumber;
            zipCode.Text = t.zipCode;
            email.Text = t.user.email;
            
            //Set the profile image 
            if (t.user.profileImage != null)
            {
                userImage.Source = t.user.profileImage.Source;
            }
            //Set the ad profile image
            if (t.adImage != null)
            {
                adImage.Source = t.adImage.Source;
            }

            foreach(PostClass n in t.posts)
            {
                n.postImage = ImageConverter.ConvertFrom(n.base64);
            }

            //Set the listview item source to be the animal's posts
            lists.ItemsSource = t.posts;

            //If the user owns the animal, then it will show the "add post" and "change photo" buttons
            if(t.owner)
            {
                upload1.IsVisible = true;
                upload2.IsVisible = true;
            } else {
                upload1.IsVisible = false;
                upload2.IsVisible = false;
            }
        }

        //Change the content displayed depending on which button is clicked
        void Handle_Clicked(object sender, System.EventArgs e)
        {
            Button t = (Button)sender;
            if(t.Text == "Posts") {
                about.IsVisible = false;
                contact.IsVisible = false;
                posts.IsVisible = true;
            } else if (t.Text == "About") {
                about.IsVisible = true;
                contact.IsVisible = false;
                posts.IsVisible = false;
            } else {
                about.IsVisible = false;
                contact.IsVisible = true;
                posts.IsVisible = false;
            }
        }

        //Take to another page depending on which button is clicked
        private async void Handle_Clicked_1(object sender, System.EventArgs e)
        {
            Button t = (Button)sender;
            if (t.Text == "Add Post")
            {
                await Navigation.PushAsync(new PictureUploader("post",tempAdID));
            }
            else
            {
                await Navigation.PushAsync(new PictureUploader("advertisement",tempAdID));
            }
        }
    }
}
