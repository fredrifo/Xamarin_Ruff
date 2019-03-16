using System;
using System.Collections.Generic;
using IAB330_Scruff.Backend_communication;
using IAB330_Scruff.Models;
using Xamarin.Forms;
using static IAB330_Scruff.Backend_communication.AdUpload;

namespace IAB330_Scruff
{
    public partial class MakeAd : ContentPage
    {
        private static AnimalClass currentAnimal;

        /// <summary>
        /// Get data from animal fields, then laod the ad fields
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RegisterAnimal_ClickedAsync(object sender, EventArgs e)
        {
            //Check data is valid
            try
            {
                string animalName = nameInput.Text;
                DateTime dateBorn = dateBornInput.Date;

                string animalBreed = breedInput.Items[breedInput.SelectedIndex];
                string animalSex = sex.Items[sex.SelectedIndex];
                string typeAnimal = animalType.Items[animalType.SelectedIndex];

                //If any of the fields are null, throw exception
                if (animalName.Length < 1 || animalBreed.Length < 1 || animalSex.Length < 1 || typeAnimal.Length < 1)
                {
                    throw new System.SystemException();
                }
                currentAnimal = new AnimalClass(animalName, dateBorn, animalBreed, animalSex, typeAnimal);
                createAnimal.IsVisible = false;
                createAd.IsVisible = true;
            }
            //If an exception is raised, display an alert with the error
            catch (Exception err)
            {
                DisplayAlert("Register", "All fields are required", "OK");
                return;
            }
        }

        /// <summary>
        /// Get data from ad fields, then register the ad
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void RegisterAD_ClickedAsync(object sender, EventArgs e)
        {
            AdClass temp;
            //Check data is valid
            try
            {
                string adTitle = adTitleInput.Text;
                string adText = adTextInput.Text;
                string adAddress = streetAddressInput.Text;
                string adzip = zipCodeInput.Text;
                ;
                DateTime availableDate = dateAvailableInput.Date;

                string adType = adTypeInput.Items[adTypeInput.SelectedIndex];

                int price = Int16.Parse(priceInput.Text);
                //If any of the fields are null, throw exception
                if (adTitle.Length < 1 || adText.Length < 1 || adAddress.Length < 1 || adzip.Length < 1 || adType.Length < 1)
                {
                    throw new System.SystemException();
                }
                temp = new AdClass(currentAnimal, availableDate, adText, adTitle, adzip, price, adAddress, adType);
            }
            //Catch the exception by showing a popup
            catch (Exception err)
            {
                DisplayAlert("Register", "All fields are required", "OK");
                return;
            }
            //Upload the ad to the database server
            Result test = await AdUpload.UploadAd(temp);
            ;

            //If the ad upload is successful, display a successful popup, otherwise show an error
            if (test.status == 0)
            {
                DisplayAlert("Register", "Ad uploaded to server", "OK");
                await Navigation.PopAsync();
            }
            else if (test.status == 1)
            {
                DisplayAlert("Register", "Check that you are logged in", "OK");
            }
            else if (test.status == 2)
            {
                DisplayAlert("Register", "Make sure all data is entered", "OK");
            }
            else if (test.status == 3)
            {
                DisplayAlert("Register", "Network/problem", "OK");
            }
        }
        public MakeAd()
        {
            InitializeComponent();
        }
    }
}
