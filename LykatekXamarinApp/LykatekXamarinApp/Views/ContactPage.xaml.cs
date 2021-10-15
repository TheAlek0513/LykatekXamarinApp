using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LykatekXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ContactPage : ContentPage
    {

        //public string lykaPhone = "+4560654709";
        public string lykaEmail = "lykatec@lykatec.dk";
        public string _lykaPhone = "+4523623700";

        public ContactPage()
        {
            InitializeComponent();
            BindingContext = this;
        }

        [Obsolete]
        void EmailLykatec_Clicked(object sender, EventArgs e)
        {
            try
            {
                Device.OpenUri(new Uri($"mailto:{lykaEmail}"));
            } catch
            {
                this.DisplayAlert("Fejl", "Der skete en uventet fejl, prøv at sende en e-mail til " + lykaEmail, "OK");
            }
        }

        void CallLykatec_Clicked(object sender, EventArgs e)
        {
            try
            {
                PhoneDialer.Open(_lykaPhone);
            }
            catch
            {
                this.DisplayAlert("Fejl", "Der skete en uventet fejl, prøv at ringe til " + _lykaPhone, "OK");
            }
        }
    }
}