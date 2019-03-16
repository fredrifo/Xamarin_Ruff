using IAB330_Scruff.Data;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace IAB330_Scruff
{
	public partial class App : Application
	{
        static AnimalDatabaseController animalDatabase;
        static UserDatabaseController userDatabase;
        static AdDatabaseController adDatabase;

		public App ()
		{
			InitializeComponent();
            MainPage = new Login();
        }

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}

        //Create an user database if none exist
        public static UserDatabaseController UserDatabase 
        {
            get {
                if (userDatabase == null)
                {
                    userDatabase = new UserDatabaseController();
                }
                return userDatabase;
            }
        }

        //Create an animal database if none exist
        public static AnimalDatabaseController AnimalDatabase {
            get {
                if (animalDatabase == null)
                {
                    animalDatabase = new AnimalDatabaseController();
                }
                return animalDatabase;
            }
        }

        //Create an ad database if none exist
        public static AdDatabaseController AdDatabase {
            get {
                if (adDatabase == null)
                {
                    adDatabase = new AdDatabaseController();
                }
                return adDatabase;
            }
        }
    }
}
