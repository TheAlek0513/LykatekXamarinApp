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
        public DateTime minDate = DateTime.Now;

        public bool deliveryChecked = false;
        public bool urgentChecked = false;

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
            ot.DeliveryAddress = DeliveryStreet.Text;
            ot.ZipCode = DeliveryZipCode.Text;
            ot.City = DeliveryCity.Text;
            ot.YourReference = ContactReference.Text;
            ot.PriorityOrder = PriorityOrder.IsChecked;
            ot.DeliveryDate = DatePickerInput.Date;
            ot.CreatedDateTime = DateTime.Now;
            ot.SecondaryContact = String.Format("{0} {1}", ContactName.Text, ContactPhone.Text);

            ErrorCodes result = await Utillity.SendOrderTable(ot);

            if (result != ErrorCodes.Succes)
            {
                throw new Exception("Der blev forsøgt at oprette en bestilling. ErrorCode = " + result);
            }

            SendOrderButton.IsEnabled = false;
            OrderActivityIndicator.IsRunning = true;

            await Navigation.PushModalAsync(new OrderSuccessfullySentPage());
        }

        void OnDeliveryCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (DeliveryCheckbox.IsChecked)
            {
                DeliveryStreet.IsVisible = true;
                DeliveryStreetLabel.IsVisible = true;

                DeliveryZipCode.IsVisible = true;
                DeliveryZipCodeLabel.IsVisible = true;

                DeliveryCity.IsVisible = true;
                DeliveryCityLabel.IsVisible = true;

            } else
            {
                DeliveryStreet.IsVisible = false;
                DeliveryStreetLabel.IsVisible = false;

                DeliveryZipCode.IsVisible = false;
                DeliveryZipCodeLabel.IsVisible = false;

                DeliveryCity.IsVisible = false;
                DeliveryCityLabel.IsVisible = false;
            }
        }
    }
}