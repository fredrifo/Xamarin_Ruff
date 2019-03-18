using System;
using System.Collections.Generic;
using IAB330_Scruff.Backend_communication;
using IAB330_Scruff.BackendCommunication;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace IAB330_Scruff
{
    public partial class PictureUploader : ContentPage
    {
        public MediaFile currentImage; //Used to store the image selected/taken
        public string uploadMethod;

        public PictureUploader(string uploadMethod,int adId)
        {
            InitializeComponent();
            this.uploadMethod = uploadMethod;
            if (uploadMethod == "advertisement")
            {
                postTextView.Text = "Upload a main picture to your advertisement";
            }
            if (uploadMethod == "post")
            {

                postTextView.Text = "Upload a post to your advertisement, write text under:";
            }
        }

        MediaFile currentFile;

        //Opening camera on button click
        private async void openCamera_Clicked(object sender, EventArgs e)
        {


            //If the phone is not capable of taking a photo or has no gallery, show error popup
            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await DisplayAlert("Error", "Camera not detected/supported", "Ok");
                return;
            }

            //Take a photo
            var tempPicture = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "images",
                SaveToAlbum = true,
                CompressionQuality = 15,
                CustomPhotoSize = 50,
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 1000,
                DefaultCamera = CameraDevice.Rear,
            });
            if (tempPicture == null)
                return;

            //If a picture was taken, set it to the currentImage variable
            
                currentFile = (MediaFile)tempPicture;
                imagePreview.Source = ImageSource.FromStream(() =>
                {
                   var stream = tempPicture.GetStream();
                   //tempPicture.Dispose();
                    return stream;
                });
            
                currentImage = currentFile;
            
            return;
        }

        //Upload picture from phone gallery
        private async void uploadPic_Clicked(object sender, EventArgs e)
        {
            //Select a photo from the gallery
            var tempPicture = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                CompressionQuality = 15,
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,
                CustomPhotoSize = 50,
                MaxWidthHeight = 1000,

            });
            
            //Was a compatible photo selected?
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                //If the photo was not compatible, show an error popup
                await DisplayAlert("Uploading denied", "photo format is not supported", "Okay :(");
                return;
            }

            //If a picture was selected, set the photo to the currentFile variable
            if (tempPicture != null)
            {
                currentFile = (MediaFile)tempPicture;
                currentImage = currentFile;
                imagePreview.Source = ImageSource.FromStream(() => tempPicture.GetStream());
            }
            return;
        }
         
        //Upload the photo to the DB server
        private async void UploadPicture(object sender, EventArgs e)
        {
            int n;
            if(currentFile == null) {
                return;
            }
            loading.IsVisible = true;
            if (uploadMethod == "advertisement")
            {
                n = await ImageUpload.uploadAdPictureAsync(currentImage, AdPage.tempAdID);
                if (n == 0)
                {
                     DisplayAlert("Upload", "Ad image uploaded sucessfully", "Continue");
                    loading.IsVisible = false;
                    await Navigation.PopAsync();
                }
                else if (n == 1)
                {
                    await DisplayAlert("Upload", "Check that you are logged in", "Continue");
                }
                else if (n == 2)
                {
                    await DisplayAlert("Upload", "Error uploading ad image, try again later", "Continue");
                }
                else
                {
                    await DisplayAlert("Upload", "Error uploading ad image, check connection", "Continue");
                }
                loading.IsVisible = false;
            }
            if (uploadMethod == "post")
            {
                n = await PostUpload.UploadPostAsync(currentImage, AdPage.tempAdID, postText.Text);
                if(n == 0) {
                     await DisplayAlert("Upload","Post uploaded sucessfully", "Continue");
                    loading.IsVisible = false;
                    await Navigation.PopAsync();
                } else if(n==1) {
                    await DisplayAlert("Upload", "Check that you are logged in", "Continue");
                } else if (n==2) {
                    await DisplayAlert("Upload", "Error uploading post, try again later", "Continue");
                } else {
                    await DisplayAlert("Upload", "Error uploading post, check connection", "Continue");
                }
                loading.IsVisible = false;
            }



            loading.IsVisible = false;
        }
    }
}
