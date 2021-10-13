using LykatecMobileApp.Util;
using LykatekXamarinApp.Models.Uniconta;
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
    public partial class OrderViewGroupList : ContentPage
    {
        public OrderViewGroupList()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, true);
            NavigationPage.SetHasBackButton(this, true);

            GroupPicker.SelectedIndexChanged += GroupPicker_SelectedIndexChanged;
            GroupPicker.ItemsSource = Settings.ConfigGroups;

            TypePicker.SelectedIndexChanged += TypePicker_SelectedIndexChanged;

            SeriePicker.SelectedIndexChanged += SeriePicker_SelectedIndexChanged;
        }

        private void GroupPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (GroupPicker.SelectedItem != null && GroupPicker.SelectedItem is ConfigGroup cg)
            {
                TypePicker.ItemsSource = Settings.ConfigTypes.Where(ct => ct.ConfigGroup == cg.KeyStr).ToList();
                SeriePicker.ItemsSource = null;
            }
        }

        private void TypePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (TypePicker.SelectedItem != null && TypePicker.SelectedItem is ConfigType ct)
            {
                SeriePicker.ItemsSource = Settings.ConfigSeries.Where(cs => cs.ConfigType == ct.KeyStr && cs.AppItem == true).ToList();
            }
        }

        private void SeriePicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (SeriePicker.SelectedItem != null && SeriePicker.SelectedItem is ConfigSeries cs)
            {
                Button.IsEnabled = true;
            }
        }

        void SelectProduct_Clicked(object sender, EventArgs e)
        {
            if (GroupPicker.SelectedItem == null || TypePicker.SelectedItem == null || SeriePicker.SelectedItem == null)
            {
                this.DisplayAlert("Fejl", "Du mangler at vælge en gruppe, type eller serie", "OK");
                return;
            }
            Navigation.PushAsync(new OrderForm((ConfigSeries)SeriePicker.SelectedItem));
        }
    }
}