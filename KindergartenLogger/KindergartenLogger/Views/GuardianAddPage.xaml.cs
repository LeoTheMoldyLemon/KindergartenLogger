using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using KindergartenLogger.Models;
using System.Security.Cryptography;

namespace KindergartenLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GuardianAddPage : ContentPage
    {
        public Regex OIBRegex = new Regex("[0123456789]{11}");
        public SHA256 encrypt = SHA256.Create();
        public GuardianAddPage()
        { 
            InitializeComponent();

        }
        private bool OIBIsValid=true; 
        private void ConfirmClicked(object sender, EventArgs e)
        {
            if (GuardianNameEntry.Text != "")
            {
                if (OIBRegex.IsMatch(GuardianOIBEntry.Text) && GuardianOIBEntry.Text.Length == 11)
                {
                    foreach (Guardian guardian in App.Data.GuardianList)
                    {
                        if (guardian.OIB == GuardianOIBEntry.Text)
                        {
                            OIBIsValid = false;
                            DisplayAlert("Pogreška", "OIB koji ste upisali već je registriran.", "U redu");
                            break;
                        }
                    }
                    if (OIBIsValid)
                    {
                        if (!(GuardianPasswordEntry.Text.Length < 8 || GuardianPasswordEntry.Text.Length > 16))
                        {
                            App.Data.GuardianList.Add(new Guardian { Name = GuardianNameEntry.Text.Trim(), OIB = GuardianOIBEntry.Text, Password = encrypt.ComputeHash(Encoding.UTF8.GetBytes(GuardianPasswordEntry.Text)), PhoneNumber = GuardianTelephoneEntry.Text, ReceiveArrivalText = EntrySwitch.IsToggled, ReceiveDepartureText = ExitSwitch.IsToggled });
                            Shell.Current.GoToAsync("//GuardianEditList");
                            App.Data.RefreshGuardian();
                        }
                        else {
                            DisplayAlert("Pogreška", "Lozinka treba imati najmanje 8 te najviše 16 znakova.", "U redu");
                            Console.WriteLine(GuardianPasswordEntry.Text + " " + GuardianPasswordEntry.Text.Length+" "+(GuardianPasswordEntry.Text.Length < 8)+" "+(GuardianPasswordEntry.Text.Length > 16));
                        }
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
            Shell.Current.GoToAsync("//GuardianEditList");
            App.Data.RefreshGuardian();
        }
    }
}