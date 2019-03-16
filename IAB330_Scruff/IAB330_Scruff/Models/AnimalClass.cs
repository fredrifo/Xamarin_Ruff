using System;
using IAB330_Scruff.BackendCommunication;

namespace IAB330_Scruff.Models
{
    public class AnimalClass
    {
        public string name { get; set; }
        private DateTime dateBornC { get; set; }
        public string dateBorn;
        public string breed { get; set; }
        public string gender { get; set; }
        public string animalType { get; set; }

        public AnimalClass(string name, DateTime dateBorn, string breed, string gender, string animalType)
        {
            this.name = name;
            this.dateBornC = dateBorn;
            this.dateBorn = DateTimeConverter.ToSql(dateBorn);
            this.breed = breed;
            this.gender = gender;
            this.animalType = animalType;
        }
       
        //Override the standard ToString method with custom
        public override string ToString()
        {
            return "ANIMAL:" + System.Environment.NewLine + name + System.Environment.NewLine + dateBorn + System.Environment.NewLine + breed + System.Environment.NewLine + gender + System.Environment.NewLine + animalType;
        }
    }
}
