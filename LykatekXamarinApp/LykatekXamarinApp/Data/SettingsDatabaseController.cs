using LykatecMobileApp.Data;
using LykatecMobileApp.Models;
using LykatecMobileApp.Util;
using SQLite;
using Xamarin.Forms;

namespace LykatecMobileApp.Data
{
    public class SettingsDatabaseController
    {
        private static readonly object locker = new object();
        private readonly SQLiteConnection database;

        public SettingsDatabaseController()
        {
            try
            {
                database = DependencyService.Get<ISQLite>().GetConnection();
                _ = database.CreateTable<LocalSettings>();
            }
            catch { }
        }

        public int ClearSettings()
        {
            try
            {
                LocalSettings settings = new LocalSettings
                {
                    Id = Settings.Id
                };
                try
                {
                    int i = database.Delete(settings);
                }
                catch
                {
                }
                try
                {
                    return database.Insert(settings);
                }
                catch
                {
                }
            }
            catch { }
            return 0;
        }



        public int DeleteAll()
        {
            try
            {
                lock (locker)
                {
                    return database.DeleteAll<LocalSettings>();
                }
            }
            catch { return 0; }
        }



        public void LoadSettings()
        {
            try
            {
                lock (locker)
                {
                    LocalSettings settings = database.Table<LocalSettings>().FirstOrDefault();


                    if (settings == null)
                    {
                        settings = new LocalSettings();
                    }

                    Settings.APIUsername = settings.APIUsername;
                    Settings.APIPassword = settings.APIPassword;
                    Settings.ContactPersonUsername = settings.ContactPersonUsername;
                    Settings.ContactPersonPassword = settings.ContactPersonPassword;
                }
            }
            catch { }
        }



        public int SaveSettings()
        {
            try
            {
                int i = 0;
                lock (locker)
                {
                    LocalSettings settings = new LocalSettings
                    {
                        Id = Settings.Id,
                        APIUsername = Settings.APIUsername,
                        APIPassword = Settings.APIPassword,
                        ContactPersonUsername = Settings.ContactPersonUsername,
                        ContactPersonPassword = Settings.ContactPersonPassword,
                    };
                    try
                    {
                        i = database.Delete(settings);
                    }
                    catch
                    {
                    }
                    try
                    {
                        i = database.Insert(settings);
                    }
                    catch
                    {
                    }
                }
                return i;
            }
            catch { return 0; }
        }
    }
}