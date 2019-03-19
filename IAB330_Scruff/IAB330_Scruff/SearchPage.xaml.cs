using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IAB330_Scruff.Data;
using IAB330_Scruff.Models;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace IAB330_Scruff
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SearchPage : ContentPage
	{
        private async void Handle_Clicked(object sender, System.EventArgs e)
        {

            try
            {
                SearchResults.breed = breedInput.Items[breedInput.SelectedIndex];
            }
            catch (Exception n) {
                SearchResults.breed = "%";
            }
            try
            {
                SearchResults.gender = sex.Items[sex.SelectedIndex];
            }
            catch (Exception n)
            {
                SearchResults.gender = "%";
            }
            if(searchText.Text == "") {
                SearchResults.searchString = "%";
            } else {
                SearchResults.searchString = searchText.Text;
            }

            await Navigation.PopAsync();
        }
        
        public SearchPage()
        {
            InitializeComponent();
        }
        /*
        ///<summary>
        /// Searches for information in the database requested from the "SearchPage" page, then displays results on the same page
        /// in the form of a label, ordered by a priority system.
        ///</summary>
        private void FindResults(object sender, EventArgs e) {
            bool noResult = true; // Checks to see if there are any results retured. Initially assumes there are no results.

            // List to store results and their respective priorities. The higher a result's priority is, the higher it is on the 
            // results list. A priority of 0 or less means the result is irrelevant and will not be displayed.
            List<KeyValuePair<Animal, int>> searchResult = new List<KeyValuePair<Animal, int>>();  
            int highestPriority = 1; // Stores result with the highest priority. Initialized to 1.

            resultsLabel.Text = "Results: \n";

            // For every animal in the database...
            foreach (Animal animal in App.AnimalDatabase.GetAllAnimals())
            {
                int value = 0; // Variable that will track the animal's priority.

                if (SearchName.Text != null) // If name entry bar isn't empty...
                {
                    // If animal name contains what is in the name entry bar, its priority is incremented.
                    if (animal.name.ToLower().Contains(SearchName.Text.ToLower())) 
                    {
                        value++;
                    }

                    // If animal name IS EXACTLY what is in the name entry bar, its priority is further incremented.
                    if (animal.name.ToLower() == SearchName.Text.ToLower())
                    {
                        value++;
                    }
                }

                // If animal breed is like what is in the breed entry bar, its priority is incremented.
                if (SearchBreed.Text != null && animal.breed.ToLower().Contains(SearchBreed.Text.ToLower())) value++; 

                if (SearchAge.Text != null)  // If age entry bar isn't empty...
                {
                    // Gets today's date.
                    var today = DateTime.Today;
                    // Calculates the age of the animal.
                    var age = today.Year - animal.date.Year;
                    // Go back to the year the animal was born in case of a leap year
                    if (animal.date > today.AddYears(-age)) age--;

                    int searchedAge = int.Parse(SearchAge.Text); // Converts age entered into an integer.

                    // If age entered is within the animals age by a factor of 1, its priority is incremented.
                    if (age - 1 < searchedAge && searchedAge < age + 1) value++;
                    // If age entered is the exact same as the animals age, its priority is further incremented.
                    if (age == searchedAge) value++;
                }

                // If the animal's type is the same as the type selected, its priority is incremented. 
                if (PickType.Items[PickType.SelectedIndex] != null && animal.type == PickType.Items[PickType.SelectedIndex]) value++;
                // If the animal's gender is the same as the gender selected, its priority is incremented.
                if (PickGender.Items[PickGender.SelectedIndex] != null && animal.gender == PickGender.Items[PickGender.SelectedIndex]) value++;

                // Checks to see if this animal's priority is the highest so far.
                if (value > highestPriority) highestPriority = value;

                if (value > 0) // The animal will be included in the results if its priority is at least 1.                    
                {
                    searchResult.Add(new KeyValuePair<Animal, int>(animal, value));
                }                        
            }

            // Prints the results of the search, starting with the highest priority animals (the most relevant animals to the search)
            for (int i = highestPriority; i > 0; i--)
            {
                foreach (KeyValuePair<Animal, int> animal in searchResult) {
                    if (animal.Value == i) {
                        if (animal.Key.name != null && animal.Key.date != null && 
                            animal.Key.breed != null && animal.Key.gender != null &&
                            animal.Key.type != null && animal.Key.about != null)
                        resultsLabel.Text += "\n " + animal.Key.animal_ID + " " + animal.Key.name + " " + animal.Key.date + " " +
                                animal.Key.breed + " " + animal.Key.gender + " " + animal.Key.type + " "+ animal.Key.about;
                        noResult = false; // If at least one result is in the results page, this is set to false.
                    }
                }
            }

            if (noResult) // If no results were retured from the search, the issue is displayed.
            {
                resultsLabel.Text += "\nSorry no results were found :(";
            }
        }

        private void Picker_OnSelectedIndexChanged(object sender, EventArgs e)
        {
            Picker picker = (Picker) sender;
            if (picker != null && picker.SelectedIndex <= picker.Items.Count && picker.SelectedIndex != 0)
            {
                var selecteditem = picker.Items[picker.SelectedIndex];
            }
        }
        */
    }
}