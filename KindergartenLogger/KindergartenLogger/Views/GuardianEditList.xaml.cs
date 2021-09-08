using KindergartenLogger.Models;
using KindergartenLogger.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KindergartenLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GuardianEditList : ContentPage
    {
        public Command<Guardian> GuardianTapped { get; }
        public GuardianEditList()
        {
            InitializeComponent();
            App.Data.ReadFromGuardian();
            RefreshGuardianList();
            listView.ItemsSource = App.Data.GuardianList;
            RefreshListView = listView;
            GuardianTapped = new Command<Guardian>(GuardianTappedCommand);
        }
        public static ListView RefreshListView = new ListView();
        public static void RefreshGuardianList()
        {
            RefreshListView.ItemsSource = App.Data.GuardianList;
        }


        private async void GuardianTappedCommand(Guardian guardian)
        {
            App.Data.GuardianNameText = guardian.Name;
            App.Data.GuardianOIBText = guardian.OIB;


            await Shell.Current.GoToAsync("//GuardianEditPage");
            App.Data.RefreshGuardian();
        }

        private async void AddClicked(object sender, EventArgs e) 
        {
            await Shell.Current.GoToAsync("//GuardianAddPage");
            App.Data.RefreshGuardian();
        }
        private async void BackClicked(object sender, EventArgs e) 
        {
            await Shell.Current.GoToAsync("//ChildEntryList");
        }
    }
}