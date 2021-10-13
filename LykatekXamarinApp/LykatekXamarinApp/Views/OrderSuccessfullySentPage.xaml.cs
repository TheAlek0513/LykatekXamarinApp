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
    public partial class OrderSuccessfullySentPage : ContentPage
    {
        public OrderSuccessfullySentPage()
        {
            NavigationPage.SetHasBackButton(this, false);
            InitializeComponent();
        }

        private async void ReturnButton_Clicked(object sender, EventArgs e)
        {
            Application.Current.MainPage = new NavigationPage(new StartPage());
        }
    }
}