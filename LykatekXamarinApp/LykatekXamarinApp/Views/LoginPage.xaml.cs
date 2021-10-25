using LykatecMobileApp.Util;
using LykatekXamarinApp.Util;
using System;
using System.Threading.Tasks;
using Uniconta.ClientTools.DataModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LykatekXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public async Task<bool> APILogin()
        {
            if (!Utillity.HasInternetConnection())
            {
                await this.DisplayAlert("Fejl", "Der skete en fejl med internetforbindelsen. Check venligst til forbindelse til internettet.", "OK");
                return false;
            }

            if (!await Utillity.APILogin())
            {
                await this.DisplayAlert("Fejl", "Der skete en fejl med forbindelsen til Uniconta. Prøv venligst igen.", "OK");
                return false;
            }

            return true;
        }

        public async void LoginButton_Clicked(object sender, EventArgs e)
        {
            LoginActivityIndicator.IsRunning = true;
            LoginButton.IsEnabled = false;
            try
            {
                string inputUsername = this.inputUsername.Text;
                string inputPassword = this.inputPassword.Text;

                if (!(await APILogin()))
                {
                    return;
                }

                await Utillity.GetContactPersons();
                ContactClient authenticatedContactPerson = (await Utillity.ContactLogin(inputUsername, inputPassword));
                if (authenticatedContactPerson != null && authenticatedContactPerson is ContactClient)
                {
                    Settings.ContactPersonUsername = inputUsername;
                    Settings.ContactPersonPassword = inputPassword;

                    Settings.ContactPersonId = authenticatedContactPerson.KeyStr;
                    Settings.DebtorId = authenticatedContactPerson.DCAccount;

                    App.SettingsDatabase.SaveSettings();
                   var syncResult = await Utillity.SyncAll();

                    if (!syncResult)
                    {
                        await this.DisplayAlert("Fejl", "API-login fejlede. Prøv venligst at logge ind igen.", "OK");
                        LoginActivityIndicator.IsRunning = false;
                        LoginButton.IsEnabled = true;
                        return;
                    }
                    var testItems = await Utillity.getInvItemWithPictures();
                    Application.Current.MainPage = new NavigationPage(new StartPage());
                    LoginActivityIndicator.IsRunning = false;
                }
                else
                {
                    await this.DisplayAlert("Fejl", "Du har skrevet forkert login eller adgangskode.", "OK");
                    LoginActivityIndicator.IsRunning = false;
                    LoginButton.IsEnabled = true;
                }
            } catch (Exception exception)
            {
                Logger.log("LoginButton_Clicked", exception.Message + "\n" + exception.StackTrace.ToString());
                LoginActivityIndicator.IsRunning = false;
                LoginButton.IsEnabled = true;
            }
        }

        public LoginPage()
        {
            InitializeComponent();
        }
        protected override void OnAppearing() { inputUsername.Focus(); }

        public async Task Init()
        {
            await APILogin();
        }
    }
}