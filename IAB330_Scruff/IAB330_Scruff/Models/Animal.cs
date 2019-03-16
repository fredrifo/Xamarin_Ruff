using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAB330_Scruff.Models
{
    public class Animal
    {
        [PrimaryKey] [AutoIncrement]
        public int animal_ID { get; set; }
        public string name { get; set; }
        public DateTime date { get; set; }

        public string breed { get; set; }
        public string gender { get; set; }
        public string type { get; set; }
        public string about { get; set; }

        public Animal() { }
        public Animal(string name, DateTime date, string breed, string gender, string type, string about)
        {
            this.name = name;
            this.date = date;
            this.breed = breed;
            this.gender = gender;
            this.type = type;
            this.about = about;
        }
    }
}
