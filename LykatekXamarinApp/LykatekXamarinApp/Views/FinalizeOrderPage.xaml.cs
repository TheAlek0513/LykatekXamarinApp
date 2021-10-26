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
            ContactName.Text = Settings.ContactPersonName;
            ContactPhone.Text = Settings.ContactPersonUsername;
            DeliveryStreet.Text = !String.IsNullOrEmpty(Settings.LatestDeliveryAddress) ? Settings.LatestDeliveryAddress : "";
            DeliveryCity.Text = !String.IsNullOrEmpty(Settings.LatestDeliveryCity) ? Settings.LatestDeliveryCity : "";
            DeliveryZipCode.Text = !String.IsNullOrEmpty(Settings.LatestDeliveryZipCode) ? Settings.LatestDeliveryZipCode : "";
        }

        private void ShowActivityIndicator()
        {
            OrderActivityIndicator.IsVisible = true;
            OrderActivityIndicator.IsRunning = true;
        }

        private void HideActivityIndicator()
        {
            OrderActivityIndicator.IsVisible = false;
            OrderActivityIndicator.IsRunning = false;
        }

        public async void SendOrder_Clicked(object sender, EventArgs e)
        {
            ShowActivityIndicator();
            SendOrderButton.IsEnabled = false;
            try
            {
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
                HideActivityIndicator();

                await Navigation.PushModalAsync(new OrderSuccessfullySentPage());

            }
            catch (Exception exception)
            {
                Logger.log("SendOrder_Clicked", exception.Message + "\n" + exception.StackTrace.ToString());
                SendOrderButton.IsEnabled = true;
                HideActivityIndicator();
                await this.DisplayAlert("Fejl", "Der skete en uventet fejl, prøv at sende bestillingen igen.", "OK");
            }
        }

        void OnDeliveryCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ShowActivityIndicator();

            DeliveryStreet.IsVisible = DeliveryCheckbox.IsChecked;
            DeliveryStreetLabel.IsVisible = DeliveryCheckbox.IsChecked;

            DeliveryZipCode.IsVisible = DeliveryCheckbox.IsChecked;
            DeliveryZipCodeLabel.IsVisible = DeliveryCheckbox.IsChecked;

            DeliveryCity.IsVisible = DeliveryCheckbox.IsChecked;
            DeliveryCityLabel.IsVisible = DeliveryCheckbox.IsChecked;

            GeoLocatioBT.IsVisible = DeliveryCheckbox.IsChecked;
            DeliveryAddressLabel.IsVisible = DeliveryCheckbox.IsChecked;

            HideActivityIndicator();
        }

        public async void GetLocation_Button(object sender, EventArgs e)
        {
            GPSActivityIndicator.IsRunning = true;
            GPSActivityIndicator.IsVisible = true;
            string[] address = await GeoCode.getLocation();
            if (address != null)
            {
                string[] addressZipAndTown = address.GetValue(1).ToString().Split(' ');
                DeliveryStreet.Text = address.GetValue(0).ToString();
                DeliveryZipCode.Text = addressZipAndTown[1].ToString();

                if (addressZipAndTown.Length > 3)
                {
                    DeliveryCity.Text = String.Format("{0} {1}",
                        addressZipAndTown[2].ToString(),
                        addressZipAndTown[3].ToString());
                } else
                {
                    DeliveryCity.Text = addressZipAndTown[2].ToString();
                }

            }
            else
            {
                await this.DisplayAlert("GPS Fejl", "Der skete en fejl med at finde din lokation via. din enheds GPS. Prøv igen.", "OK");
            }

            GPSActivityIndicator.IsRunning = false;
            GPSActivityIndicator.IsVisible = false;
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
            ShowActivityIndicator();
            SetPriorityOrder();
            HideActivityIndicator();
        }
    }
}