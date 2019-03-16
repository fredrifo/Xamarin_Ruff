using IAB330_Scruff.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IAB330_Scruff.Data
{
    public class AdDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database; //The database object

        public AdDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Ad>();
        }

        //Get the first ad result
        public Ad GetAd()
        {
            lock (locker) //Prevent concurrent access
            { //If there is no data in the table, return nothing
                if (database.Table<Ad>().Count() == 0)
                {
                    return null;
                }
                else
                {// If there is data in the table, return the first result
                    return database.Table<Ad>().First();
                }
            }
        }

        //Save the ad to the database
        public int SaveAd(Ad ad)
        {
            lock (locker)
            {
                if (ad.ad_ID != 0)
                {
                    database.Update(ad);
                    return ad.ad_ID;
                }
                else
                {
                    return database.Insert(ad);
                }
            }
        }

        //Delete the ad from the database
        public int DeleteAd(int id)
        {
            lock (locker)
            {
                return database.Delete<Ad>(id);
            }
        }
    }
}
