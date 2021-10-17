using LykatekXamarinApp.Models;
using LykatekXamarinApp.Models.Uniconta;
using LykatekXamarinApp.Util;
using System;
using System.Collections.Generic;
using Uniconta.API.System;
using Uniconta.ClientTools.DataModel;
using Uniconta.DataModel;

namespace LykatecMobileApp.Util
{
    public static class Settings
    {
        // contains config values
        public static int Id { get; set; }
        public static string APIUsername { get; set; } = "LykApi";
        public static string APIPassword { get; set; } = "lyk123";
        public static Company CurrentCompany { get; set; }
        public static int CurrentCompanyId { get; set; }
        public static string CurrentCompanyName { get; set; }
        public static CrudAPI crudApi { get; set; }
        public static Uniconta.API.Service.Session session { get; set; }
        public static List<ConfigGroup> ConfigGroups { get; set; }
        public static List<ConfigType> ConfigTypes { get; set; }
        public static List<ConfigSeries> ConfigSeries { get; set; }
        public static string ContactPersonUsername { get; set; }
        public static string ContactPersonPassword { get; set; }
        public static List<ContactPerson> contactList { get; set; }
        public static string ContactPersonId { get; set; }
        public static string DebtorId { get; set; }
        public static Uniconta.API.Service.Session Session
        {
            get
            {
                if (session?.LoggedIn != true)
                {
                    session = new Uniconta.API.Service.Session(new Uniconta.API.Service.UnicontaConnection(Uniconta.API.Service.APITarget.Live, true));
                }
                return session;
            }
            set => session = value;
        }
        public static CrudAPI CrudApi
        {
            get
            {
                if (crudApi == null && Session != null && CurrentCompany != null && Session.LoggedIn)
                {
                    try
                    {
                        crudApi = new CrudAPI(Session, CurrentCompany);
                    }
                    catch (Exception exception)
                    {
                        Logger.log("Settings.CrudAPI", exception.Message + "\n" + exception.StackTrace.ToString());

                    }
                }
                return crudApi;
            }
            set => crudApi = null;
        }
    
    }
}
