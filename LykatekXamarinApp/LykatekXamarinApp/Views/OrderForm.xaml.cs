using System;
using System.Collections.Generic;
using LykatecMobileApp.Util;
using LykatekXamarinApp.Models.Uniconta;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using LykatekXamarinApp.Models;
using LykatekXamarinApp.Util;

namespace LykatekXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderForm : ContentPage
    {
        public ConfigGroup configGroup;
        public ConfigType configType;
        public ConfigSeries configSerie;
        public String[] checkProps = new String[]
        {
            "M1Dim", "M1Length", "M1Degrees", "M1TotalLength",
            "M2Dim", "M2Length", "M2Degrees", "M2TotalLength",
            "T1Dim", "T1Length", "T1Degrees", "T1TotalLength",
            "T2Dim", "T2Length", "T2Degrees", "T2TotalLength"
        };
        public Dictionary<string, string> entryColours = new Dictionary<string, string>
        {
            { "M1", "#245c77" },
            { "T1", "#1f762e" },
            { "M2", "blue" },
            { "T2", "green" },
        };
        private Entry firstEntryField = null;
        public int LastTabIndex = 0;

        public string GetFriendlyName(string val)
        {
            var map = new Dictionary<string, string>();
            map.Add("M1Dim", "M1 Diameter");
            map.Add("M2Dim", "M2 Diameter");
            map.Add("T1Dim", "T1 Diameter");
            map.Add("T2Dim", "T2 Diameter");

            map.Add("M1Length", "M1 Længde");
            map.Add("M2Length", "M2 Længde");
            map.Add("T1Length", "T1 Længde");
            map.Add("T2Length", "T2 Længde");

            map.Add("M1Degrees", "M1 Grader");
            map.Add("M2Degrees", "M2 Grader");
            map.Add("T1Degrees", "T1 Grader");
            map.Add("T2Degrees", "T2 Grader");

            map.Add("M1TotalLength", "M1 Total Længde");
            map.Add("M2TotalLength", "M2 Total Længde");
            map.Add("T1TotalLength", "T1 Total Længde");
            map.Add("T2TotalLength", "T2 Total Længde");
            string mapValue;
            if (map.TryGetValue(val, out mapValue))
            {
                return mapValue;
            }

            return String.Empty;
        }

        public OrderForm(ConfigSeries cs)
        {
            configSerie = cs;
            InitializeComponent();
            AddEntries();
        }

        public List<RelevantOrderProperty> GetRelevantProps()
        {
            List<RelevantOrderProperty> configSerieFields = new List<RelevantOrderProperty>();
            foreach (string checkProp in checkProps)
            {
                foreach (PropertyInfo prop in typeof(ConfigSeries).GetProperties())
                {
                    if (prop.Name == checkProp)
                    {
                        int value = Convert.ToInt32(configSerie.GetType().GetProperty(prop.Name).GetValue(configSerie, null));
                        var displayName = GetFriendlyName(prop.Name);
                        if (value > 0)
                        {
                            for (int i = 0; i < value; i++) // måske, måske ikke ?
                            {
                                configSerieFields.Add(new RelevantOrderProperty() { Name = prop.Name, DisplayName = displayName, Value = value });
                            }
                        }
                    }
                }
            }
            return configSerieFields;
        }

        public void AddEntries()
        {
            foreach (RelevantOrderProperty field in GetRelevantProps())
            {
                int currentTabIndex = LastTabIndex++;

                string entryColour = "#000000";
                string attemptColor = entryColour;
                if (entryColours.TryGetValue(field.Name.Substring(0, 2), out attemptColor))
                {
                    entryColour = attemptColor;
                }

                var label = new Label()
                {
                    Text = field.DisplayName,
                    FontSize = 18,
                    Padding = 2,
                    FontAttributes = FontAttributes.Bold,
                    TextColor = Color.FromHex(entryColour)
                };
                var entry = new Entry()
                {
                    Placeholder = field.DisplayName,
                    Keyboard = Keyboard.Numeric,
                    ClassId = field.Name,
                    TabIndex = currentTabIndex,
                    ReturnType = ReturnType.Next,
                    TextColor = Color.FromHex(entryColour)
                };

                Frame frame = new Frame()
                {
                    BorderColor = Color.FromHex(entryColour),
                    Content = new StackLayout()
                    {
                        Children =
                        {
                            label, entry
                        }
                    }
                };

                EntriesStacklayout.Children.Add(frame);

                if (LastTabIndex == 1)
                {
                    firstEntryField = entry;
                }
            }
        }

        protected override void OnAppearing()
        {
            if (firstEntryField != null)
            {
                firstEntryField.Focus();
            }
        }

        public async void GoFuther_Clicked(object sender, EventArgs e)
        {
            GoFutherButton.IsEnabled = false;
            OrderActivityIndicator.IsRunning = true;
            OrderActivityIndicator.IsVisible = true;

            try
            {
                OrderTable orderTable = new OrderTable();
                orderTable.ContactPerson = Settings.ContactPersonId;
                orderTable.ConfigSeries = configSerie.KeyName;
                orderTable.Quantity = Int32.Parse(TotalItemCount.Text).ToString();
                orderTable.Debtor = Settings.DebtorId;

                foreach (var child in EntriesStacklayout.Children)
                {
                    if (child is Entry)
                    {
                        Entry currentEntry = child as Entry;

                        switch (currentEntry.ClassId)
                        {
                            case "M1Dim":
                                if (string.IsNullOrEmpty(orderTable.M1Nominel))
                                {
                                    orderTable.M1Nominel = currentEntry.Text;
                                }
                                else
                                {
                                    orderTable.M1Nominel += ";" + currentEntry.Text;
                                }
                                break;
                            case "M1Length":
                                if (string.IsNullOrEmpty(orderTable.M1Length))
                                {
                                    orderTable.M1Length = currentEntry.Text;
                                }
                                else
                                {
                                    orderTable.M1Length += ";" + currentEntry.Text;
                                }
                                break;
                            case "M1Degrees":
                                if (string.IsNullOrEmpty(orderTable.M1Degrees))
                                {
                                    orderTable.M1Degrees = currentEntry.Text;
                                }
                                else
                                {
                                    orderTable.M1Degrees += ";" + currentEntry.Text;
                                }
                                break;
                            case "M1TotalLength":
                                if (string.IsNullOrEmpty(orderTable.M1LengthTotal))
                                {
                                    orderTable.M1LengthTotal = currentEntry.Text;
                                }
                                else
                                {
                                    orderTable.M1LengthTotal += ";" + currentEntry.Text;
                                }
                                break;
                            case "M2Dim":
                                if (string.IsNullOrEmpty(orderTable.M2Nominel))
                                {
                                    orderTable.M2Nominel = currentEntry.Text;
                                }
                                else
                                {
                                    orderTable.M2Nominel += ";" + currentEntry.Text;
                                }
                                break;
                            case "M2Length":
                                if (string.IsNullOrEmpty(orderTable.M2Length))
                                {
                                    orderTable.M2Length = currentEntry.Text;
                                }
                                else
                                {
                                    orderTable.M2Length += ";" + currentEntry.Text;
                                }
                                break;
                            case "M2Degrees":
                                if (string.IsNullOrEmpty(orderTable.M2Degrees))
                                {
                                    orderTable.M2Degrees = currentEntry.Text;
                                }
                                else
                                {
                                    orderTable.M2Degrees += ";" + currentEntry.Text;
                                }
                                break;
                            case "M2TotalLength":
                                if (string.IsNullOrEmpty(orderTable.M2LengthTotal))
                                {
                                    orderTable.M2LengthTotal = currentEntry.Text;
                                }
                                else
                                {
                                    orderTable.M2LengthTotal += ";" + currentEntry.Text;
                                }
                                break;
                            case "T1Dim":
                                if (string.IsNullOrEmpty(orderTable.T1Nominel))
                                {
                                    orderTable.T1Nominel = currentEntry.Text;
                                }
                                else
                                {
                                    orderTable.T1Nominel += ";" + currentEntry.Text;
                                }
                                break;
                            case "T1Length":
                                if (string.IsNullOrEmpty(orderTable.T1Length))
                                {
                                    orderTable.T1Length = currentEntry.Text;
                                }
                                else
                                {
                                    orderTable.T1Length += ";" + currentEntry.Text;
                                }
                                break;
                            case "T1Degrees":
                                if (string.IsNullOrEmpty(orderTable.T1Degrees))
                                {
                                    orderTable.T1Degrees = currentEntry.Text;
                                }
                                else
                                {
                                    orderTable.T1Degrees += ";" + currentEntry.Text;
                                }
                                break;
                            case "T1TotalLength":
                                if (string.IsNullOrEmpty(orderTable.T1LengthTotal))
                                {
                                    orderTable.T1LengthTotal = currentEntry.Text;
                                }
                                else
                                {
                                    orderTable.T1LengthTotal += ";" + currentEntry.Text;
                                }
                                break;
                        }
                    }
                }

                await Navigation.PushAsync(new FinalizeOrderPage(orderTable));

                GoFutherButton.IsEnabled = true;
                OrderActivityIndicator.IsRunning = false;
                OrderActivityIndicator.IsVisible = false;
            }
            catch (Exception exception)
            {   
                Logger.log("GoFuther_Clicked", exception.Message + "\n" + exception.StackTrace.ToString());
                Console.WriteLine("-----");
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
                Console.WriteLine("-----");
                GoFutherButton.IsEnabled = true;
                OrderActivityIndicator.IsRunning = false;
                OrderActivityIndicator.IsVisible = false;
                await this.DisplayAlert("Fejl", "Der skete en uventet fejl, prøv at sende bestillingen igen", "OK");
            }
        }

        public async void GoFuther_ClickedOld(object sender, EventArgs e)
        {
            GoFutherButton.IsEnabled = false;
            OrderActivityIndicator.IsRunning = true;
            OrderActivityIndicator.IsVisible = true;
            try
            {
                OrderTable orderTable = new OrderTable();
                orderTable.ContactPerson = Settings.ContactPersonId;
                orderTable.ConfigSeries = configSerie.KeyName;
                orderTable.Quantity = Int32.Parse(TotalItemCount.Text).ToString();
                orderTable.Debtor = Settings.DebtorId;

                var M1 = new Dictionary<string, string>();
                var M2 = new Dictionary<string, string>();
                var T1 = new Dictionary<string, string>();
                var T2 = new Dictionary<string, string>();

                foreach (RelevantOrderProperty field in GetRelevantProps())
                {
                    var orderTableProp = orderTable.GetType().GetProperty(field.Name);

                    if (orderTableProp != null)
                    {
                        foreach (var child in EntriesStacklayout.Children)
                        {
                            if (child is Entry)
                            {
                                Entry currentEntry = child as Entry;
                                if (currentEntry.ClassId == field.Name)
                                {
                                    orderTableProp.SetValue(orderTable, currentEntry.Text);


                                    Console.WriteLine("#############################");
                                    Console.WriteLine(field.Name.Substring(0, 2));
                                    Console.WriteLine("#############################");

                                    switch (field.Name.Substring(0, 2))
                                    {
                                        case "M1":
                                            M1.Add(field.Name, currentEntry.Text);
                                            break;
                                        case "M2":
                                            M2.Add(field.Name, currentEntry.Text);
                                            break;
                                        case "T1":
                                            T1.Add(field.Name, currentEntry.Text);
                                            break;
                                        case "T2":
                                            T2.Add(field.Name, currentEntry.Text);
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }

                var _M1 = M1;
                var _M2 = M2;
                var _T1 = T1;
                var _T2 = T2;

                await Navigation.PushAsync(new FinalizeOrderPage(orderTable));

                GoFutherButton.IsEnabled = true;
                OrderActivityIndicator.IsRunning = false;
                OrderActivityIndicator.IsVisible = false;
            }
            catch (Exception exception)
            {
                Logger.log("SendOrder_Clicked", exception.Message + "\n" + exception.StackTrace.ToString());
                Console.WriteLine("-----");
                Console.WriteLine(exception.Message);
                Console.WriteLine(exception.StackTrace);
                Console.WriteLine("-----");
                GoFutherButton.IsEnabled = true;
                OrderActivityIndicator.IsRunning = false;
                OrderActivityIndicator.IsVisible = false;
                await this.DisplayAlert("Fejl", "Der skete en uventet fejl, prøv at sende bestillingen igen", "OK");
            }
        }
    }
}