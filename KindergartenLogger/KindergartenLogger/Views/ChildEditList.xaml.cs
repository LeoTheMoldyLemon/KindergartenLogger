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
    public partial class ChildEditList : ContentPage
    {
        public Command<Child> ChildTapped { get; }
        public ChildEditList()
        {
            InitializeComponent();
            App.Data.ReadFromChild();
            listView.ItemsSource = App.Data.ChildList;
            RefreshListView = listView;
            RefreshChildList();
            ChildTapped = new Command<Child>(ChildTappedCommand);
        }
        public static ListView RefreshListView = new ListView();
        public static void RefreshChildList()
        {
            RefreshListView.ItemsSource = App.Data.ChildList;
        }


        private async void ChildTappedCommand(Child child)
        {
            App.Data.ChildNameText = child.Name;
            App.Data.ChildOIBText = child.OIB;
            App.Data.ChildPastebin = child;
            App.Data.RefreshConnection();
            await Shell.Current.GoToAsync("//ChildEditPage");
            App.Data.RefreshChild();
        }

        private async void AddClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("//ChildAddPage");
            App.Data.RefreshChild();
        }
        private async void BackClicked(object sender, EventArgs e) 
        {
            await Shell.Current.GoToAsync("//ChildEntryList");
        }
    }
}