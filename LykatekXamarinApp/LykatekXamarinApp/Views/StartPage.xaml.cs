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
        private void ShowActivityIndicator()
        {
            StartPageActivityIndicator.IsVisible = true;
            StartPageActivityIndicator.IsRunning = true;
        }

        private void HideActivityIndicator()
        {
            StartPageActivityIndicator.IsVisible = false;
            StartPageActivityIndicator.IsRunning = false;
        }

        public StartPage()
        {
            InitializeComponent();

            Application.Current.UserAppTheme = OSAppTheme.Light;
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);
        }

        void ViewGroupList_Clicked(object sender, EventArgs e)
        {
            ShowActivityIndicator();
            Navigation.PushAsync(new ViewSeriesList());
            HideActivityIndicator();
        }

        async void SignOut_Clicked(object sender, EventArgs e)
        {
            var answer = await DisplayAlert("Log ud", "Hvis du logger ud, vil du ikke automatisk logge på næste gang.\nEr du sikker?", "Ja", "Nej");
            if (answer)
            {
                ShowActivityIndicator();
                _ = Utillity.ContactLogout(); // skal fixes senere
                Application.Current.MainPage = new LoginPage();
                Utillity.getUserDocsClient();
                var test = Utillity.getInvItemWithPictures();
                HideActivityIndicator();
            }
        }

        async void ViewContactPage_Clicked(object sender, EventArgs e)
        {
            ShowActivityIndicator();
            await Navigation.PushAsync(new ContactPage());
            HideActivityIndicator();
        }

        protected override bool OnBackButtonPressed()
        {
            return true; // true prevent navigation back and false to allow
        }
    }
}