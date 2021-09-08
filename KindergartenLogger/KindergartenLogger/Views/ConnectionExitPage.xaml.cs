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



    public partial class ConnectionExitPage : ContentPage
    {
        public Command<Guardian> GuardianTapped { get; }
        public static ObservableCollection<Guardian> guardians = new ObservableCollection<Guardian>();
        public static ObservableCollection<Connection> connection = new ObservableCollection<Connection>();

        public static ListView RefreshListView = new ListView();
        public ConnectionExitPage()
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
            guardians = new ObservableCollection<Guardian>();
            foreach (Connection con in App.Data.ConnectionList) 
            {
                if (App.Data.ChildPastebin.OIB==con.ChildOIB) 
                {
                    foreach (Guardian gua in App.Data.GuardianList)
                    {
                        if (gua.OIB == con.GuardianOIB)
                        {
                            guardians.Add(gua);
                        }
                    }
                }
            }

            RefreshListView.ItemsSource = guardians;
        }
        
        public void GuardianTappedCommand(Guardian guardian) 
        {
            App.Data.ConnectionList.Add(new Connection { GuardianOIB = guardian.OIB, ChildOIB=App.Data.ChildPastebin.OIB });
            App.Data.RefreshConnection();
            App.Data.PasswordForwardPage = "//ChildSuccessPage";
            App.Data.PasswordBackwardPage = "//ConnectionExitPage";
            App.Data.GuardianPastebin = guardian;
            Shell.Current.GoToAsync("//GuardianPasswordPage");
        }

        private async void CancelClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ChildExitList");
        }
    }
}