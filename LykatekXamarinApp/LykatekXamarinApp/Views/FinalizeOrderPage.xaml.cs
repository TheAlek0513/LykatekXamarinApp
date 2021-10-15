using LykatecMobileApp.Util;
using LykatekXamarinApp.Models.Uniconta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Uniconta.Common;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LykatekXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FinalizeOrderPage : ContentPage
    {
        public OrderTable ot;
        public FinalizeOrderPage(OrderTable orderTable)
        {
            ot = orderTable;
            InitializeComponent();
        }

        public async void SendOrder_Clicked(object sender, EventArgs e)
        {
            SendOrderButton.IsEnabled = false;
            OrderActivityIndicator.IsRunning = true;
            ot.Comment = Description.Text;


            ErrorCodes result = await Utillity.SendOrderTable(ot);

            if (result != ErrorCodes.Succes)
            {
                throw new Exception("Der blev forsøgt at oprette en bestilling. ErrorCode = " + result);
            }

            SendOrderButton.IsEnabled = false;
            OrderActivityIndicator.IsRunning = true;

            await Navigation.PushModalAsync(new OrderSuccessfullySentPage());
        }


        public void DatePicker_DateSelected(object sender, DateChangedEventArgs e)
        {
            //this.DisplayAlert("Fejl", datePicker.Date.ToString(), "OK");
            Console.WriteLine(DatePickerInput.Date.ToString());
        }

    }
}