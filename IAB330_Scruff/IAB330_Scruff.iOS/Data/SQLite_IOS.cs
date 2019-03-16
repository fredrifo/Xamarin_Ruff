using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Foundation;
using IAB330_Scruff.Data;
using IAB330_Scruff.iOS.Data;
using UIKit;
using Xamarin.Forms;

[assembly : Dependency(typeof(SQLite_IOS))]
namespace IAB330_Scruff.iOS.Data
{
    public class SQLite_IOS : ISQLite
    {
        public SQLite_IOS()
        {

        }
        public SQLite.SQLiteConnection GetConnection()
        {
            var fileName = "Testdb.db3";
            var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            var libraryPath = Path.Combine(documentPath, "..", "Library");
            var path = Path.Combine(libraryPath, fileName);
            var con = new SQLite.SQLiteConnection(path);
            return con;
        }
    }
}