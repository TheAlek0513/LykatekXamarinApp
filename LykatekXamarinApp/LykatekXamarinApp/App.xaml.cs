using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using LykatekXamarinApp.Views;
using LykatecMobileApp.Data;
using LykatecMobileApp.Util;

namespace LykatekXamarinApp
{
    public partial class App : Application
    {
        private static SettingsDatabaseController sdc { get; set; }
        private static ContactPersonDatabaseController cpdb { get; set; }

        public App()
        {
            InitializeComponent();
            App.SettingsDatabase.LoadSettings();
            if (!Utillity.IsValidLoginAsync())
            {
                Application.Current.MainPage = new LoginPage();
                return;
            }
            MainPage = new NavigationPage(new StartPage());
            _ = Utillity.SyncAll();
        }

        public static SettingsDatabaseController SettingsDatabase
        {
            get
            {
                if (sdc == null)
                {
                    sdc = new SettingsDatabaseController();
                }

                return sdc;
            }
        }

        public static ContactPersonDatabaseController ContactDatabase
        {
            get
            {
                if (cpdb == null)
                {
                    cpdb = new ContactPersonDatabaseController();
                }

                return cpdb;
            }
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }
        protected override void OnResume()
        {
        }
    }
}
