using LykatecMobileApp.Util;
using LykatekXamarinApp.Models.Uniconta;
using LykatekXamarinApp.Util;
using System;
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

        public bool deliveryChecked = false;
        public bool urgentChecked = false;
        public bool isPriorityOrder = false;

        public FinalizeOrderPage(OrderTable orderTable)
        {
            ot = orderTable;
            InitializeComponent();
            DatePickerInput.SetValue(DatePicker.MinimumDateProperty, DateTime.Now);
            SetPriorityOrder();

            DeliveryStreet.Text = !String.IsNullOrEmpty(Settings.LatestDeliveryAddress) ? Settings.LatestDeliveryAddress : "";
            DeliveryCity.Text = !String.IsNullOrEmpty(Settings.LatestDeliveryCity) ? Settings.LatestDeliveryCity : "";
            DeliveryZipCode.Text = !String.IsNullOrEmpty(Settings.LatestDeliveryZipCode) ? Settings.LatestDeliveryZipCode : "";
        }

        public async void SendOrder_Clicked(object sender, EventArgs e)
        {
            SendOrderButton.IsEnabled = false;
            OrderActivityIndicator.IsRunning = true;
            OrderActivityIndicator.IsVisible = true;
            ot.Comment = Description.Text;
            ot.DeliveryAddress = DeliveryStreet.Text;
            ot.ZipCode = DeliveryZipCode.Text;
            ot.City = DeliveryCity.Text;
            ot.YourReference = ContactReference.Text;
            ot.PriorityOrder = isPriorityOrder;
            ot.DeliveryDate = DatePickerInput.Date;
            ot.CreatedDateTime = DateTime.Now;
            ot.SecondaryContact = String.Format("{0} {1}", ContactName.Text, ContactPhone.Text);

            if (DeliveryCheckbox.IsChecked)
            {
                Settings.LatestDeliveryAddress = DeliveryStreet.Text;
                Settings.LatestDeliveryCity = DeliveryCity.Text;
                Settings.LatestDeliveryZipCode = DeliveryZipCode.Text;
            }

            ErrorCodes result = await Utillity.SendOrderTable(ot);

            if (result != ErrorCodes.Succes)
            {
                throw new Exception("Der blev forsøgt at oprette en bestilling. ErrorCode = " + result);
            }

            SendOrderButton.IsEnabled = false;
            OrderActivityIndicator.IsRunning = true;
            OrderActivityIndicator.IsVisible = true;

            await Navigation.PushModalAsync(new OrderSuccessfullySentPage());
        }

        void OnDeliveryCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            DeliveryStreet.IsVisible = DeliveryCheckbox.IsChecked;
            DeliveryStreetLabel.IsVisible = DeliveryCheckbox.IsChecked;

            DeliveryZipCode.IsVisible = DeliveryCheckbox.IsChecked;
            DeliveryZipCodeLabel.IsVisible = DeliveryCheckbox.IsChecked;

            DeliveryCity.IsVisible = DeliveryCheckbox.IsChecked;
            DeliveryCityLabel.IsVisible = DeliveryCheckbox.IsChecked;

            GeoLocatioBT.IsVisible = DeliveryCheckbox.IsChecked;
            DeliveryAddressLabel.IsVisible = DeliveryCheckbox.IsChecked;
        }

        public async void GetLocation_Button(object sender, EventArgs e)
        {
            string[] address = await GeoCode.getLocation();
            if (address != null)
            {
                string[] addressZipAndTown = address.GetValue(1).ToString().Split(' ');
                DeliveryStreet.Text = address.GetValue(0).ToString();
                DeliveryZipCode.Text = addressZipAndTown[1].ToString();
                DeliveryCity.Text = addressZipAndTown[2].ToString();
            }
            else
            {
                await this.DisplayAlert("GPSfejl", "Der Skete en fejl, ved brug af GPS", "OK");
            }
        }

        private void SetPriorityOrder()
        {
            if (DateTime.Compare(DatePickerInput.Date, DateTime.Now.AddDays(2)) <= 0)
            {
                isPriorityOrder = true;
                IsPriorityOrderNoticeLabel.IsVisible = true;
            }
            else
            {
                isPriorityOrder = false;
                IsPriorityOrderNoticeLabel.IsVisible = false;
            }
        }

        private void DatePickerInput_DateSelected(object sender, DateChangedEventArgs e)
        {
            SetPriorityOrder();
        }
    }
}