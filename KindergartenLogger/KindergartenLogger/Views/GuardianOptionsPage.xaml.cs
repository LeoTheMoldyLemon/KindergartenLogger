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
    public partial class GuardianOptionsPage : ContentPage
    {

        public Regex OIBRegex = new Regex("[0123456789]{11}");
        public GuardianOptionsPage()
        {
            InitializeComponent();
            GuardianNameEntry.Text = App.Data.GuardianPastebin.Name;
            GuardianOIBEntry.Text = App.Data.GuardianPastebin.OIB;
            ExitSwitch.IsToggled = App.Data.GuardianPastebin.ReceiveDepartureText;
            EntrySwitch.IsToggled = App.Data.GuardianPastebin.ReceiveArrivalText;

        }

        private void ConfirmClicked(object sender, EventArgs e) 
        {
            App.Data.GuardianPastebin.ReceiveDepartureText = ExitSwitch.IsToggled;
            App.Data.GuardianPastebin.ReceiveArrivalText = EntrySwitch.IsToggled;
            Shell.Current.GoToAsync("//ChildEntryPage");
            App.Data.RefreshGuardian();
        }



        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//GuardianOptionsList");
            App.Data.RefreshGuardian();
        }


    }
}