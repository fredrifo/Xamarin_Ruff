using System;
using System.Collections.Generic;
using IAB330_Scruff.BackendCommunication;
using Xamarin.Forms;

namespace IAB330_Scruff.Models
{
    public class AdClass 
    {
        private static List<AdClass> adResult;
        private DateTime adDateC; //Ad date creation
        private DateTime dateAvailableC; //Ad date where the animal is available for sale/adoption
        public AnimalClass animal { get; set; }

        public string adDate { get; set; } //Ad date creation
        public string dateAvailable { get; set; } //Ad date where the animal is available for sale/adoption
        public string adText { get; set; } //Ad description
        public string adTitle { get; set; } //Ad title shown on the search results
        public string zipCode { get; set; } //Postcode for the animal's location
        public string adType { get; set; } //Is the animal for sale or adoption?
        public string streetAddress { get; set; } //Animal's address
        public int adPrice { get; set; } //Animal's price
        public User user;
        public bool owner; //Is the logged in user the owner of the animal?
        public int adId { get; set; } //Unique identifier for the ad
        public Image adImage { get; set; } //The image associated with the ad (it's profile photo/thumbnail)
        public string base64Image;
        public List<PostClass> posts { get; set; }

        public AdClass(AnimalClass animal, DateTime dateAvailable, string adText, string adTitle, string zipCode, int adprice, string streetAddress,string adType)
        {
            this.animal = animal;
            this.dateAvailableC = dateAvailable;
            this.dateAvailable = DateTimeConverter.ToSql(dateAvailable);
            this.adDateC = DateTime.Now;
            this.adDate = DateTimeConverter.ToSql(adDateC);
            this.adType = adType;
            this.adText = adText;
            this.adTitle = adTitle;
            this.zipCode = zipCode;
            adPrice = adprice;
            this.streetAddress = streetAddress;
        }

        //Converting the ad info to strings by overwriting the default toString method
        public override string ToString()
        {
            string test = "AD:" + System.Environment.NewLine;
            test += adTitle + System.Environment.NewLine;
            test += adText + System.Environment.NewLine;
            test += zipCode + System.Environment.NewLine;
            test += streetAddress + System.Environment.NewLine;
            test += adType + System.Environment.NewLine;
            test += adPrice.ToString() + System.Environment.NewLine;
            return test;
        }

        public static List<AdClass> AdResult { get => adResult; set => adResult = value; }
    }
}
