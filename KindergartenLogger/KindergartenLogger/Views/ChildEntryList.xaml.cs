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
    public partial class ChildEntryList : ContentPage
    {
        public static ObservableCollection<Child> ChildListNotArrived = new ObservableCollection<Child>();
        public Command<Child> ChildTapped { get; }
        public ChildEntryList()
        {
            InitializeComponent();
            App.Data.ReadFromChild();
            RefreshChildList();
            listView.ItemsSource = ChildListNotArrived;
            RefreshListView = listView;
            ChildTapped = new Command<Child>(ArrivedTappedCommand);
        }
        public static ListView RefreshListView = new ListView();
        public static void RefreshChildList()
        {
            ChildListNotArrived = new ObservableCollection<Child> (App.Data.ChildList.Where((val) => !val.Arrived));
            foreach (Child c in ChildListNotArrived)
            {
                Console.WriteLine(c.OIB + ": " + c.Arrived);
            }
            RefreshListView.ItemsSource = ChildListNotArrived;
        }

        private async void ArrivedTappedCommand(Child child)
        {
            Console.WriteLine("ArrivedTapped called");
            if (await DisplayAlert("Potvrda", "Donosite dijete zvano " + child.Name + " u vrtić?", "Da", "Ne"))
            {
                child.Arrived = true;
                App.Data.RefreshChild();
                foreach (Child c in App.Data.ChildList)
                {
                    Console.WriteLine(c.OIB+": "+c.Arrived);
                }
            }
            else
            {
            
            }

        }
    }
}