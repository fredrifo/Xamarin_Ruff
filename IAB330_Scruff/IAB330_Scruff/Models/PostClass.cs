using System;
using Xamarin.Forms;

namespace IAB330_Scruff.Models
{
    public class PostClass
    {
        public string title { get; set; }
        public Image postImage { get; set; }
        public DateTime postTime { get; set; }
        public string base64;
        public string date { get; set; }
    public PostClass(string title, Image postImage, DateTime postTime)
        {
            this.title = title;
            this.postImage = postImage;
            
            this.postTime = postTime;
            date = postTime.ToString();
        }

        //Override the standard ToString method with a custom
        public override string ToString()
        {
            return "POST:" + System.Environment.NewLine + title + System.Environment.NewLine + postTime.ToLongDateString()+ System.Environment.NewLine;
        }
    }
}
