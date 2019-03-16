using IAB330_Scruff.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IAB330_Scruff.Data
{
    public class UserDatabaseController
    {
        static object locker = new object();

        SQLiteConnection database; //Database connnection objet

        public UserDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<User>();
        }

        //Get the first user result
        public User GetUser()
        {
            lock (locker)
            {
                if (database.Table<User>().Count() == 0)
                {
                    return null;
                }
                else
                {
                    return database.Table<User>().First();
                }
            }
        }

        //Add a new user to the DB
        public int SaveUser(User user)
        {
            lock (locker)
            {
                if (user.user_ID != -1)
                {
                    database.Update(user);
                    return user.user_ID;
                }
                else
                {
                    return database.Insert(user);
                }
            }
        }

        //Delete a user from the DB using its ID
        public int DeleteUser(int id)
        {
            lock (locker)
            {
                return database.Delete<User>(id);
            }
        }
    }
}
