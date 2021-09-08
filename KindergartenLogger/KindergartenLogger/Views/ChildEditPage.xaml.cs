using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using KindergartenLogger.Models;
using System.Collections.ObjectModel;

namespace KindergartenLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildEditPage : ContentPage
    {
        static public ObservableCollection<Guardian> guardians = new ObservableCollection<Guardian>();
        static public ObservableCollection<Connection> connections = new ObservableCollection<Connection>();

        public Regex OIBRegex = new Regex("[0123456789]{11}");
        public static ListView RefreshListView = new ListView();
        public Command<Guardian> GuardianTapped { get; }
        public ChildEditPage()
        {
            InitializeComponent();
            RefreshListView = listView;
            RefreshConnectionList();
            ChildNameEntry.Text = App.Data.ChildNameText;
            ChildOIBEntry.Text = App.Data.ChildOIBText;
            GuardianTapped = new Command<Guardian>(DeleteConnectionClicked);
        }

        public static void RefreshConnectionList() 
        {
            connections = new ObservableCollection<Connection>(App.Data.ConnectionList.Where((c, i) => c.ChildOIB == App.Data.ChildOIBText));
            guardians = new ObservableCollection<Guardian>(); 
            foreach (Guardian g in App.Data.GuardianList)
            {
                foreach (Connection c in App.Data.ConnectionList)
                {
                    if (g.OIB == c.GuardianOIB)
                    {
                        guardians.Add(g);
                    }
                }
            }
            Console.WriteLine("The guardians getting loaded for display:");
            foreach (Guardian guardian in guardians) 
            {
                Console.WriteLine(guardian.OIB);
            }

            RefreshListView.ItemsSource = guardians;
        }
        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ChildEditList");
            App.Data.RefreshChild();
   
        }

        private async void DeleteClicked(object sender, EventArgs e)
        {
            if (await DisplayAlert("Potvrda", "Jeste li sigurni da želite izbrisati " + App.Data.ChildNameText + "?", "Da", "Ne"))
            {
                App.Data.ChildList = new ObservableCollection<Child>(App.Data.ChildList.Where((c, ind) => c.OIB != App.Data.ChildOIBText));
                App.Data.RefreshChild();


                await Shell.Current.GoToAsync("//ChildEditList");
            }
        }

        private async void DeleteConnectionClicked(Guardian g)
        {
            if (await DisplayAlert("Potvrda", "Jeste li sigurni da želite maknuti " + listView.SelectedItem + " kao skrbnika ovog djeteta?", "Da", "Ne"))
            {
                App.Data.ConnectionList = new ObservableCollection<Connection>(App.Data.ConnectionList.Where( (c,i) => c.ChildOIB != App.Data.ChildPastebin.OIB || c.GuardianOIB != g.OIB) );

                App.Data.RefreshConnection();
                Console.WriteLine("Guardians listed in ConnectionDelete");
                foreach (Connection c in App.Data.ConnectionList) 
                {
                    Console.WriteLine(c.GuardianOIB);
                }
            }
        }

        private async void AddConnectionClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ConnectionAddPage");

        }
    }
}