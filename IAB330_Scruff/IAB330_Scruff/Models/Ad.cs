using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace IAB330_Scruff.Models
{
    public class Ad
    {
        [PrimaryKey] [AutoIncrement]
        public int ad_ID { get; set; }
        public int price { get; set; }

        public Ad() { }
        public Ad(int price)
        {
            this.price = price;
        }
    }
}
