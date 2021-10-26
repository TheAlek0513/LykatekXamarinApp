using LykatekXamarinApp.Models.Uniconta;
using LykatecMobileApp.Util;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LykatekXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewSeriesList : ContentPage
    {
        ObservableCollection<ConfigSeries> obSeries;

        private void ShowActivityIndicator()
        {
            ListViewActivityIndicator.IsVisible = true;
            ListViewActivityIndicator.IsRunning = true;
        }

        private void HideActivityIndicator()
        {
            ListViewActivityIndicator.IsVisible = false;
            ListViewActivityIndicator.IsRunning = false;
        }

        public ViewSeriesList()
        {
            InitializeComponent();
            AddSeries();
            SeriesList.ItemsSource = obSeries;
        }

        public void AddSeries()
        {
            ShowActivityIndicator();

            try
            {
                obSeries = new ObservableCollection<ConfigSeries>(Settings.ConfigSeries.Where(cs => cs.AppItem == true));

            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message.ToString(), "OK");
            }

            HideActivityIndicator();
        }

        private async void SeriesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {

            ShowActivityIndicator();
            try
            {
                var selectedSeries = (ConfigSeries)e.Item;

                await Navigation.PushAsync(new OrderForm(selectedSeries));

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
            
            // Flush old selection
            SeriesList.SelectedItem = null;

            HideActivityIndicator();
        }
    }
}
