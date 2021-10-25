using LykatecMobileApp.Data;
using LykatekXamarinApp;
using LykatekXamarinApp.Models;
using LykatekXamarinApp.Models.Uniconta;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Uniconta.API.Service;
using Uniconta.API.System;
using Uniconta.ClientTools.DataModel;
using Uniconta.Common;
using Uniconta.DataModel;
using Xamarin.Essentials;

namespace LykatecMobileApp.Util
{
    public static class Utillity
    {
        #region login
        public static async Task<bool> APILogin(string apiUsername, string apiPassword)
        {
            bool res = false;

            if ((await Settings.Session.LoginAsync(apiUsername, apiPassword, Uniconta.Common.User.LoginType.API, new Guid("eec18f90-ce8b-46a0-96c8-2fcfe346c28c"))) == ErrorCodes.Succes)
            {
                await SetCurrentCompany();
                res = true;
            }
            return res;
        }

        internal static async Task<bool> SyncAll()
        {
            if (!await APILogin())
            {
                return false;
            }

            await SyncConfigSeries();
            return true;
        }

        internal static bool IsValidLoginAsync()
        {
            return !string.IsNullOrEmpty(Settings.APIUsername)
                    && !string.IsNullOrEmpty(Settings.APIPassword)
                    && !string.IsNullOrEmpty(Settings.ContactPersonUsername)
                    && !string.IsNullOrEmpty(Settings.ContactPersonPassword);
        }

        public static async Task<bool> APILogin()
        {
            return await APILogin(Settings.APIUsername, Settings.APIPassword);
        }

        public static async Task<ContactClient> ContactLogin(string username, string password)
        {
            ContactClient res = null;

            List<ContactClient> allContacts = (await Settings.CrudApi.Query<ContactClient>()).ToList();

            ContactClient authenticatedContactPerson = allContacts.Find(c => c.Mobile == username
            && c.GetUserField("Password").ToString() == password
            && bool.Parse(c.GetUserField("MobileActive").ToString()) == true);

            if (authenticatedContactPerson != null)
            {
                res = authenticatedContactPerson;
            }

            return res;
        }

        public static bool ContactLogout()
        {
            Settings.ContactPersonUsername = null;
            Settings.ContactPersonPassword = null;

            App.SettingsDatabase.SaveSettings();
            _ = Utillity.SyncAll();

            return true;
        }

        #endregion
        #region Uniconta CRUD

        public static async Task<List<ConfigGroup>> GetConfigGroups()
        {
            // check login
            return (await Settings.CrudApi.Query<ConfigGroup>()).ToList();
        }

        public static async Task getUserDocsClient()
        {
            var userDocsClients = (await Settings.CrudApi.Query<UserDocsClient>()).ToList();
            var userDocsClient = userDocsClients.Find(x => x.KeyName == "Test Billeder");
            var test2 = (Settings.CrudApi.Read(userDocsClient).Result);
            var lol = userDocsClient.DocumentGuid;
            var data = userDocsClient._Data;
            var test = 1;
            //return Userdocs;
        }

        public static async Task<InvItemClient> getInvItemWithPictures()
        {
            var itemClients = (await Settings.CrudApi.Query<InvItemClient>()).ToList();
            var itemClient = itemClients.Find(x => x.Name == "Test Billeder");
            var userPhoto = itemClient.Photo;
            var userImage = itemClient.Image;
            return itemClient;
        }

        public static async Task SyncConfigSeries()
        {
            Settings.ConfigSeries = (await Settings.CrudApi.Query<ConfigSeries>()).ToList();
        }

        public static async Task<List<ContactPerson>> GetContactPersons()
        {
            List<ContactClientUser> allContacts = (await Settings.CrudApi.Query<ContactClientUser>()).ToList();

            App.ContactDatabase.SaveAll(allContacts.Select(a => new ContactPerson(a)).ToList());
            Settings.contactList = App.ContactDatabase.GetAll();

            return Settings.contactList;
        }

        public static async Task<ErrorCodes> SendOrderTable(OrderTable orderTable)
        {
            var res = await Settings.crudApi.Insert(orderTable);
            return res;
        }
        #endregion

        #region CheckInternet

        public static bool HasInternetConnection()
        {
            try
            {
                return Connectivity.NetworkAccess == NetworkAccess.Internet;
            }
            catch
            {
                return true;
            }
        }

        #endregion


        /// <summary>
        /// This method sets the current company
        /// </summary>
        /// <returns>Errorcode message</returns>
        public static async Task SetCurrentCompany()
        {
            Debug.WriteLine("Settings current company");
            Stopwatch sw = Stopwatch.StartNew();
            if (Settings.Session == null)
            {
                //Settings.ErrorMessageHeader = Language.Translate("SessionMissing");
                //Settings.ErrorMessage = Language.Translate("SessionMissing");
                sw.Stop();
                return;
            }

            try
            {
                // Load the last used company from cache
                if (Settings.CurrentCompanyId != 0)
                {
                    Settings.CurrentCompany = await Settings.Session.OpenCompany(Settings.CurrentCompanyId, false);
                }
                else
                {
                    // Get the users default company
                    int defaultCompany = Settings.Session.User._DefaultCompany;
                    if (defaultCompany != 0)
                    {
                        Settings.CurrentCompany = await Settings.Session.OpenCompany(defaultCompany, false);
                    }
                    else
                    {
                        // Get all companies related to the API user
                        Company[] companies = await Settings.Session.GetCompanies();
                        if (companies.Length == 0)
                        {
                            //Settings.ErrorMessageHeader = Language.Translate("NoCompaniesFoundForUser");
                            //Settings.ErrorMessage = Language.Translate("NoCompaniesFoundForUser");
                            sw.Stop();
                            return;
                        }
                        Company com = companies.FirstOrDefault();
                        if (com != null)
                        {
                            Settings.CurrentCompany = await Settings.Session.OpenCompany(com.CompanyId, true);
                        }
                    }
                }



                if (Settings.CurrentCompany != null)
                {
                    Settings.CurrentCompanyId = Settings.CurrentCompany.CompanyId;
                    Settings.CurrentCompanyName = Settings.CurrentCompany.Name;
                }
                else
                {
                    //Settings.ErrorMessageHeader = Language.Translate("OpenCompanyErr");
                    //Settings.ErrorMessage = Language.Translate("OpenCompanyErr");
                    sw.Stop();
                    return;
                }
            }
            catch (Exception e)
            {
                //Settings.ErrorMessageHeader = Language.Translate("Error");
                //Settings.ErrorMessage = Language.Translate("Error") + " " + e;
                sw.Stop();
                return;
            }
            sw.Stop();
            Debug.WriteLine("User login took: " + sw.ElapsedMilliseconds);
            return;
        }
    }
}
