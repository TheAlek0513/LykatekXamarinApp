using LykatekXamarinApp.Models;
using LykatekXamarinApp.Models.Uniconta;
using LykatecMobileApp.Util;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace LykatekXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewSeriesList : ContentPage
    {
        ObservableCollection<ConfigSeries> obSeries;
        public ViewSeriesList()
        {
            InitializeComponent();
            AddSeries();
            SeriesList.ItemsSource = obSeries;
        }

        public void AddSeries()
        {
            try
            {
                obSeries = new ObservableCollection<ConfigSeries>(Settings.ConfigSeries.Where(cs => cs.AppItem == true));
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message.ToString(), "OK");
            }
        }

        private async void SeriesList_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var selectedSeries = (ConfigSeries)e.Item;

                await Navigation.PushAsync(new OrderForm(selectedSeries));

            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.Message.ToString(), "Ok");
            }
            SeriesList.SelectedItem = null;
        }
    }
}
