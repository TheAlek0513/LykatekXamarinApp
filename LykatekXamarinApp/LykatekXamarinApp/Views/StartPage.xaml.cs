using LykatecMobileApp.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LykatekXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StartPage : ContentPage
    {

        public StartPage()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = OSAppTheme.Light;
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);

        }

        void ViewGroupList_Clicked(object sender, EventArgs e)
        {

            // check login
            //Application.Current.MainPage = new LoginPage();
            //return;
            //Utillity.SyncData();

            //Navigation.PushAsync(new OrderViewGroupList())

            Navigation.PushAsync(new ViewSeriesList());
        }

        async void SignOut_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Log ud", "Hvis du logger ud, vil du ikke automatisk logge på næste gang.\nEr du sikker?", "Ja", "Nej");
            if (answer)
            {
                _ = Utillity.ContactLogout(); // skal fixes senere
                Application.Current.MainPage = new LoginPage();
            }
        }
    }
}