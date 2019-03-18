using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IAB330_Scruff.Backend_communication;
using IAB330_Scruff.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAB330_Scruff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Profile : ContentPage
    {
        public Profile()
        {
            InitializeComponent();
            LoadProfile();
        }

        public async void LoadProfile()
        {
            loadbar.IsVisible = true;
            int test = await ProfileDownload.DownloadOwnProfileAsync(); //Try to download the profile information
            if (test == 0) //If the download was successful
            {
                if (User.currentUser.profileImage != null)
                {
                    profilePicture.Source = User.currentUser.profileImage.Source;
                }
                name.Text = User.currentUser.firstName + " " + User.currentUser.lastName;
                phone.Text = User.currentUser.phoneNumber;
                email.Text = User.currentUser.email;
                loadbar.IsVisible = false;
            } //If the download was unsuccessful
            else if (test == 1)
            {
                loadbar.IsVisible = false;
                DisplayAlert("Error", "Check that you have logged in" + test, "OK");
            }
            else
            {
                loadbar.IsVisible = false;
                DisplayAlert("Error", "Server network error", "OK");
            }
        }

        //Upload a new profile photo
        private async void UploadProfileImage_ClickedAsync(object sender, EventArgs e)
        {
            stack0.ForceLayout();

            //Pick a photo from the gallery
            MediaFile file;
            file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                CompressionQuality = 15,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CustomPhotoSize = 50,
                MaxWidthHeight = 1000,

            });


            if (file == null) //If no photo was selected, return nothing
            {
                return;
            }
            //Upload the photo to the database server
            int t = await ImageUpload.uploadProfilePictureAsync(file);
            if (t == 0) //If the upload was successful, then display alert
            {
                DisplayAlert("Success", "Uploaded profile picture", "OK");
                LoadProfile();
            }
            else if (t == 1) //Show error message if no user is logged in, this should've be possible to show technically
            {
                DisplayAlert("Error", "User not logged in", "OK");
            }
            else //Error saying something is wrong with the server
            {
                DisplayAlert("Error", "Upload error try again", "OK");
            }
        }
    }
}