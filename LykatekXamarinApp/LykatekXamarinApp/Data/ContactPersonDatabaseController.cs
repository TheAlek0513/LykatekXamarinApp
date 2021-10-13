using LykatekXamarinApp.Models;
using SQLite;
using System.Collections.Generic;
using Xamarin.Forms;

namespace LykatecMobileApp.Data
{
    public class ContactPersonDatabaseController
    {
        private static readonly object locker = new object();
        private readonly SQLiteConnection database;

        public ContactPersonDatabaseController()
        {
            try
            {
                database = DependencyService.Get<ISQLite>().GetConnection();
                _ = database.CreateTable<ContactPerson>();
            }
            catch { }
        }

        public List<ContactPerson> GetAll()
        {
            try
            {
                lock (locker)
                {

                    return database.Table<ContactPerson>().ToList();
                }
            }
            catch {
                return new List<ContactPerson>();
            }
        }

        public int DeleteAll()
        {
            try
            {
                lock (locker)
                {
                    return database.DeleteAll<ContactPerson>();
                }
            }
            catch { return 0; }
        }

        public int SaveAll(List<ContactPerson> contactPersons)
        {
            DeleteAll();

            try
            {
                lock(locker)
                {
                    return database.InsertAll(contactPersons);
                }
            } catch
            {
                return 0;
            }
        }
    }
}