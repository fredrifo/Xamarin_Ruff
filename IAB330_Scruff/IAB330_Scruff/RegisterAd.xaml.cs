using System;
using System.Collections.Generic;
using IAB330_Scruff.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAB330_Scruff
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegisterAd : ContentPage
    {
        private static AnimalClass currentAnimal; 

        private async void RegisterAnimal_ClickedAsync(object sender, EventArgs e)
        {
            // check data
            try
            {
                string animalName = nameInput.Text;
                DateTime dateBorn = dateBornInput.Date;

                string animalBreed = breedInput.Items[breedInput.SelectedIndex];
                string animalSex = sex.Items[sex.SelectedIndex];
                string typeAnimal = animalType.Items[animalType.SelectedIndex];

                if (animalName.Length < 1 || animalBreed.Length < 1 || animalSex.Length < 1 || typeAnimal.Length < 1)
                {
                    throw new System.SystemException();
                }
                currentAnimal = new AnimalClass(animalName, dateBorn, animalBreed, animalSex, typeAnimal);
                createAnimal.IsVisible = false;
                createAd.IsVisible = true;




            } catch (Exception err) {
                return;
            }
        }
        private async void RegisterAD_ClickedAsync(object sender, EventArgs e)
        {

        }

        public RegisterAd()
        {
            InitializeComponent();
        }
    }
}
