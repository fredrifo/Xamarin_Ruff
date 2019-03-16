using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using IAB330_Scruff.Data;
using IAB330_Scruff.Droid.Data;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace IAB330_Scruff.Droid.Data
{
    public class SQLite_Android : ISQLite
    {
        public SQLite_Android() { }
        public SQLite.SQLiteConnection GetConnection()
        {
            var sqliteFilename = "TestDB.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            var con = new SQLite.SQLiteConnection(path);
            return con;
        }
    }
}