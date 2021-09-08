using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using KindergartenLogger.Models;
using KindergartenLogger.Views;
using System.Collections.ObjectModel;

namespace KindergartenLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]



    public partial class ConnectionAddPage : ContentPage
    {
        public Command<Guardian> GuardianTapped { get; }
        public static ObservableCollection<Guardian> guardians = new ObservableCollection<Guardian>();
        public static ListView RefreshListView = new ListView();
        public ConnectionAddPage()
        {

            InitializeComponent();
            GuardianTapped = new Command<Guardian>(GuardianTappedCommand);
            Console.WriteLine("Guardians listed in ConnectionAdd");
            foreach (Guardian g in guardians) 
            {
                Console.WriteLine(g.OIB);
            }
            RefreshListView = listView;
            RefreshGuardian();
        }

        public static void RefreshGuardian()
        {
            guardians = new ObservableCollection<Guardian>(App.Data.GuardianList.Where((c, i) => !ChildEditPage.guardians.Contains(c)));
            RefreshListView.ItemsSource = guardians;
            Console.WriteLine("Guardians listed in ConnectionAdd");
            foreach (Guardian g in guardians)
            {
                Console.WriteLine(g.OIB);
            }
        }
        
        public void GuardianTappedCommand(Guardian guardian) 
        {
            App.Data.ConnectionList.Add(new Connection { GuardianOIB = guardian.OIB, ChildOIB=App.Data.ChildPastebin.OIB });
            App.Data.RefreshConnection();
            foreach (Connection c in App.Data.ConnectionList)
            {
                Console.WriteLine(c.GuardianOIB);
            }
            Shell.Current.GoToAsync("//ChildEditPage");
        }

        private async void CancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ChildEditPage");
        }
    }
}