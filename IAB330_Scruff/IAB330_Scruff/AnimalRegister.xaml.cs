using IAB330_Scruff.Models;
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
	public partial class AnimalRegister : ContentPage
	{
        string[] dogBreeds = new string[] 
        {
            "Beagles", "Boxers", "Bulldog", "Dachschunds", "German Shepherd",
            "Golden Retrievers", "Great Dane", "Husky", "Labrador", "Poodles", "Pug",
            "Rottweilers", "Shih Tzu",
        };

		public AnimalRegister ()
		{
			InitializeComponent ();
		}

        //this function is used to test - delete when done
        private void genderPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if (selectedIndex != -1)
            {
                TESTgenderPickedLabel.Text = picker.Items[selectedIndex];
            }
        }

        private void uploadAnimalImage_Clicked(object sender, EventArgs e)
        {

        }

        private void animalTypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if ((string)animalTypePicker.SelectedItem == "Dog")
            {
                breedPicker.ItemsSource = dogBreeds;
                breedPicker.IsEnabled = true;
            }
        }

        private void breedPicker_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void RegisterButton_Clicked(object sender, EventArgs e)
        {
            Animal testAnimal = new Animal(animalNameEntry.Text, DateTime.Now, breedPicker.SelectedItem.ToString(),
                genderPicker.SelectedItem.ToString(), "Dog", "I'm doggo");
            App.AnimalDatabase.SaveAnimal(testAnimal);
        }
    }
}