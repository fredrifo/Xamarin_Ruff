using IAB330_Scruff.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace IAB330_Scruff.Data
{
    public class AnimalDatabaseController
    {
        static object locker = new object();
        SQLiteConnection database; //Database connection object

        public AnimalDatabaseController()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<Animal>();
        }

        //Return a list of all the animals
        public List<Animal> GetAllAnimals()
        {
            List<Animal> animals = new List<Animal>();
            lock (locker)//Lock access from other users
            {
                for (int i = 0; i < database.Table<Animal>().Count(); i++)
                { //Add all animals to the list
                    animals.Add(database.Table<Animal>().ElementAt(i));
                }             
            }
            //Return the list of everyone animal
            return animals;
        }

        //Save a new entry to the animal Db
        public int SaveAnimal(Animal animal)
        {
            lock (locker)
            {
                if (animal.animal_ID != 0)
                {
                    database.Update(animal);
                    return animal.animal_ID;
                }
                else
                {
                    return database.Insert(animal);
                }
            }
        }

        //Delete an animal from the DB using its ID
        public int DeleteAnimal(int id)
        {
            lock (locker)
            {
                return database.Delete<Animal>(id);
            }
        }
    }
}
