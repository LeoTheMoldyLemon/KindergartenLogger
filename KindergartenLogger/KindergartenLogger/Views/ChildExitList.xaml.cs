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
    public partial class ChildExitList : ContentPage
    {
        public static ObservableCollection<Child> ChildListNotDeparted = new ObservableCollection<Child>();
        public Command<Child> ChildTapped { get; }
        public ChildExitList()
        {
            InitializeComponent();
            App.Data.ReadFromChild();
            RefreshChildList();
            listView.ItemsSource = ChildListNotDeparted;
            RefreshListView = listView;
            ChildTapped = new Command<Child>(DepartedTappedCommand);
        }
        public static ListView RefreshListView = new ListView();
        public static void RefreshChildList()
        {
            ChildListNotDeparted = new ObservableCollection<Child> (App.Data.ChildList.Where((val) => !val.Departed));
            foreach (Child c in ChildListNotDeparted)
            {
                Console.WriteLine(c.OIB + ": " + c.Departed);
            }
            RefreshListView.ItemsSource = ChildListNotDeparted;
        }
        private async void DepartedTappedCommand(Child child)
        {
            Console.WriteLine("DepartedTapped called");
            if (await DisplayAlert("Potvrda", "Odnosite dijete zvano " +  child.Name + " iz vrtića?", "Da", "Ne"))
            {
                App.Data.ChildPastebin = child;
                await Shell.Current.GoToAsync("//ConnectionExitPage");
                App.Data.RefreshChild();
            }
            else
            {
            
            }

        }
    }
}