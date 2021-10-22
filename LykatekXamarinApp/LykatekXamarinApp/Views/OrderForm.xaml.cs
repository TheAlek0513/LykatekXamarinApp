using System;
using System.Collections.Generic;
using LykatecMobileApp.Util;
using LykatekXamarinApp.Models.Uniconta;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Reflection;
using LykatekXamarinApp.Models;
using LykatekXamarinApp.Util;
using System.Linq;

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

        private RadioButton M1_krympbarRBJa = null;
        private RadioButton M1_krympbarRBNej = null;
        private CheckBox M1_Mastic = null;

        private RadioButton T1_krympbarRBJa = null;
        private RadioButton T1_krympbarRBNej = null;
        private CheckBox T1_Mastic = null;
        private CheckBox T1_Anboring = null;

        /**
         * Denne liste (allEntries) bliver brugt når vi skal sættes værdierne på OrderTable instansen.
         * Grunden til at listen er nødvendigt er fordi vi ikke direkte kan foreach over EntriesStackLayout, pga. frames som gør det en smule mere besværligt.
         */
        private List<Entry> allEntries = new List<Entry>(); 

        private Frame M1EntryFrames = new Frame();
        private StackLayout M1StackLayout = new StackLayout();
        private Frame T1EntryFrames = new Frame();
        private StackLayout T1StackLayout = new StackLayout();
        private Frame M2EntryFrames = new Frame();
        private StackLayout M2StackLayout = new StackLayout();
        private Frame T2EntryFrames = new Frame();
        private StackLayout T2StackLayout = new StackLayout();



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
            switch (configSerie.KeyName)
            {
                case "T-muffe gr° parallel afgr":
                    ProduktImage.Source = ImageSource.FromFile("Resources/drawable/T_muffe_paralllell_afgrening.png");
                    break;
                case "T-muffe u afgrening":
                    ProduktImage.Source = ImageSource.FromFile("Resources/drawable/T_muffe_afgrening.png");

                    break;
                default:
                    ProduktImage.Source = ImageSource.FromFile("Resources/drawable/NoImageAvailable.jpg");
                    break;
            }
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
            int M1Count = 0;
            int T1Count = 0;
            int M2Count = 0;
            int T2Count = 0;

            foreach (RelevantOrderProperty field in GetRelevantProps())
            {
                switch(field.Name.Substring(0,2))
                {
                    case "M1":
                        M1Count++;
                        break;
                    case "T1":
                        T1Count++;
                        break;
                    case "M2":
                        M2Count++;
                        break;
                    case "T2":
                        T2Count++;
                        break;
                }
            }
            int index = 0;
            foreach (RelevantOrderProperty field in GetRelevantProps())
            {
                int currentTabIndex = LastTabIndex++;
                index++;
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

                allEntries.Add(entry);

                Frame frame = new Frame()
                {
                    BorderColor = Color.FromHex(entryColour),
                    Padding = 12,
                    Content = new StackLayout()
                    {
                        Children =
                        {
                            label, entry
                        }
                    }
                };
                switch (entry.ClassId.Substring(0,2))
                {
                    case "M1":
                        //M1EntryFrames.Add(frame);
                        M1StackLayout.Children.Add(frame);
                        break;
                    case "T1":
                        T1StackLayout.Children.Add(frame);
                        break;
                    case "M2":
                        T2StackLayout.Children.Add(frame);  
                        break;
                    case "T2":
                        M2StackLayout.Children.Add(frame);
                        break;
                }
                //EntriesStacklayout.Children.Add(frame);
                switch (field.Name.Substring(0,2))
                {
                    case "M1":
                        if (index == M1Count)
                        {
                            Label M1_krympbar = new Label
                            {
                                Text = "M1 Krympbar",
                                FontSize = 18,
                                Padding = 2,
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.FromHex(entryColour)
                            };
                            M1_krympbarRBJa = new RadioButton
                            {
                                GroupName = "M1_krymp",
                                Content = "Ja",
                                Value = "1"
                            }; 
                            M1_krympbarRBNej = new RadioButton
                            {
                                GroupName = "M1_krymp",
                                Content = "Nej",
                                Value = "0"
                            };

                            Label M1_MasticLabel = new Label
                            {
                                Text = "M1 ilagt mastic",
                                FontSize = 18,
                                Padding = 2,
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.FromHex(entryColour)
                            };

                            M1_Mastic = new CheckBox
                            {
                                
                            };
                            Frame frame1 = new Frame
                            {
                                BorderColor = Color.FromHex(entryColour),
                                Content = new StackLayout()
                                {
                                    Children =
                                    {
                                        M1_krympbar, M1_krympbarRBJa,M1_krympbarRBNej, M1_MasticLabel, M1_Mastic
                                    }
                                }
                            };
                            M1StackLayout.Children.Add(frame1);
                            //EntriesStacklayout.Children.Add(frame1);
                            index = 0;
                        }
                        break;
                    case "T1":
                        if (index == T1Count)
                        {
                            Label T1_krympbar = new Label
                            {
                                Text = "T1 Krympbar",
                                FontSize = 18,
                                Padding = 2,
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.FromHex(entryColour)
                            };
                            T1_krympbarRBJa = new RadioButton
                            {
                                GroupName = "T1_krymp",
                                Content = "Ja",
                                Value = "1"
                            };
                            T1_krympbarRBNej = new RadioButton
                            {
                                GroupName = "T1_krymp",
                                Content = "Nej",
                                Value = "0"
                            };

                            Label T1_MasticLabel = new Label
                            {
                                Text = "T1 ilagt mastic",
                                FontSize = 18,
                                Padding = 2,
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.FromHex(entryColour)
                            };

                            T1_Mastic = new CheckBox
                            {

                            };

                            Label T1_AnboringLabel = new Label
                            {
                                Text = "T1 anboring",
                                FontSize = 18,
                                Padding = 2,
                                FontAttributes = FontAttributes.Bold,
                                TextColor = Color.FromHex(entryColour)
                            };

                            T1_Anboring = new CheckBox
                            {

                            };

                            Frame frameT = new Frame
                            {
                                BorderColor = Color.FromHex(entryColour),
                                
                                Content = new StackLayout()
                                {
                                    Children =
                                    {
                                        T1_krympbar, T1_krympbarRBJa,T1_krympbarRBNej,T1_MasticLabel,T1_Mastic, T1_AnboringLabel, T1_Anboring
                                    }
                                }
                            };
                            T1StackLayout.Children.Add(frameT);
                            index = 0;
                        }
                        break;
                }
                if (LastTabIndex == 1)
                {
                    firstEntryField = entry;
                }
            }
            if (M1StackLayout.Children.Count > 0)
            {
                M1EntryFrames.Content = M1StackLayout;
                EntriesStacklayout.Children.Add(M1EntryFrames);
            };
            if (T1StackLayout.Children.Count > 0)
            {
                T1EntryFrames.Content = T1StackLayout;
                EntriesStacklayout.Children.Add(T1EntryFrames);

            };
            if (M2StackLayout.Children.Count > 0)
            {
                M2EntryFrames.Content = M2StackLayout;
                EntriesStacklayout.Children.Add(M2EntryFrames);

            };
            if (T2StackLayout.Children.Count > 0)
            {
                T2EntryFrames.Content = T2StackLayout;
                EntriesStacklayout.Children.Add(T2EntryFrames);

            };

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
            OrderTable orderTable = new OrderTable();

            // M1 Krymp:
            if (M1_krympbarRBJa != null && M1_krympbarRBNej != null)
            {
                if (!M1_krympbarRBJa.IsChecked && !M1_krympbarRBNej.IsChecked)
                {
                    await this.DisplayAlert("Valideringsfejl", "Du skal tage stilling til om M1 skal være med krymp eller ej.", "OK");
                    return;
                } else
                {
                    orderTable.M1Shrink = M1_krympbarRBJa.IsChecked;
                }
            }
            // M1 Mastic:
            orderTable.M1LoadedMastic = M1_Mastic != null && M1_Mastic.IsChecked;

            // T1 Krymp:
            if (T1_krympbarRBJa != null && T1_krympbarRBNej != null)
            {
                if (!T1_krympbarRBJa.IsChecked && !T1_krympbarRBNej.IsChecked)
                {
                    await this.DisplayAlert("Valideringsfejl", "Du skal tage stilling til om T1 skal være med krymp eller ej.", "OK");
                    return;
                }
                else
                {
                    orderTable.T1Shrink = T1_krympbarRBJa.IsChecked;
                }
            }
            // T1 Mastic:
            orderTable.T1LoadedMastic = T1_Mastic != null && T1_Mastic.IsChecked;
            // T1 Anboring:
            orderTable.T1Drilling = T1_Anboring != null && T1_Anboring.IsChecked;

            GoFutherButton.IsEnabled = false;
            OrderActivityIndicator.IsRunning = true;
            OrderActivityIndicator.IsVisible = true;

            try
            {
                orderTable.ContactPerson = Settings.ContactPersonId;
                orderTable.ConfigSeries = configSerie.KeyName;
                orderTable.Quantity = Int32.Parse(TotalItemCount.Text).ToString();
                orderTable.Debtor = Settings.DebtorId;

                foreach (var child in allEntries)
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
    }
}