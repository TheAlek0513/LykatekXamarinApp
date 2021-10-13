using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using LykatecMobileApp.Data;
using LykatekXamarinApp.Droid.Data;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Android))]
namespace LykatekXamarinApp.Droid.Data
{
    public class SQLite_Android : ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            const string sqlFileName = "LykatecDB.db3";
            string documentPath = FileSystem.AppDataDirectory;
            string path = Path.Combine(documentPath, sqlFileName);

            return new SQLiteConnection(path);
        }
    }
}