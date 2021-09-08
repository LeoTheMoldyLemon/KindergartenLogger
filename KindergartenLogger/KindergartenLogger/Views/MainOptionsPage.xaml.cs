using KindergartenLogger.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KindergartenLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainOptionsPage : ContentPage
    {
        static CheckBox autobox;
        static TimePicker autopicker;
        public MainOptionsPage()
        {
            InitializeComponent();
            autobox = AutomaticCheckBox;
            autopicker = AutomaticTimePicker;
            RefreshOptions();
        }

        public static void RefreshOptions() 
        {
            autobox.IsChecked = App.Data.options.AutomaticEntryMessage;
            autopicker.IsEnabled = App.Data.options.AutomaticEntryMessage;
            autopicker.Time = App.Data.options.EntryTime;
        }

        private void ResetEntryClicked(object sender, EventArgs e)
        {
            foreach (Child child in App.Data.ChildList)
            {
                child.Arrived = false;
            }
            App.Data.RefreshChild();
        }
        private void ResetExitClicked(object sender, EventArgs e)
        {
            foreach (Child child in App.Data.ChildList)
            {
                child.Departed = false;
            }
            App.Data.RefreshChild();
        }
        private void ManualArrivalClicked(object sender, EventArgs e)
        {
            foreach (Child child in App.Data.ChildList)
            {
                child.Arrived = false;
            }
            App.Data.RefreshChild();
            throw new NotImplementedException();
        }
        private void SaveClicked(object sender, EventArgs e)
        {
            App.Data.options.AutomaticEntryMessage = autobox.IsChecked;
            App.Data.options.EntryTime = autopicker.Time;
            App.Data.RefreshOptions();
        }
        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ChildEntryList");
        }

        private void AutomaticCheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            autopicker.IsEnabled = autobox.IsChecked;
        }
    }
}