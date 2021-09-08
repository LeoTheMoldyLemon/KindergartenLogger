using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using KindergartenLogger.Models;

namespace KindergartenLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ChildAddPage : ContentPage
    {
        public Regex OIBRegex = new Regex("[0123456789]{11}");
        public ChildAddPage()
        { 
            InitializeComponent();

        }
        private bool OIBIsValid=true; 
        private void ConfirmClicked(object sender, EventArgs e)
        {
            if (ChildNameEntry.Text != "")
            {
                if (OIBRegex.IsMatch(ChildOIBEntry.Text) && ChildOIBEntry.Text.Length == 11)
                {
                    foreach (Child child in App.Data.ChildList)
                    {
                        if (child.OIB == ChildOIBEntry.Text)
                        {
                            OIBIsValid = false;
                            DisplayAlert("Pogreška", "OIB koji ste upisali već je registriran.", "U redu");
                        }
                    }
                    if (OIBIsValid) 
                    {
                        App.Data.ChildList.Add(new Child { Name = ChildNameEntry.Text.Trim(), OIB = ChildOIBEntry.Text, Arrived = false, Departed=false }) ;
                        Shell.Current.GoToAsync("//ChildEditList");
                        App.Data.RefreshChild();
                    }

                }
                else
                {
                    DisplayAlert("Pogreška", "OIB koji ste upisali nije važeći.", "U redu");
                }
            }
            else
            {
                DisplayAlert("Pogreška", "Polje s imenom ne može biti prazno.", "U redu");
            }
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//ChildEditList");
            App.Data.RefreshChild();
        }
    }
}