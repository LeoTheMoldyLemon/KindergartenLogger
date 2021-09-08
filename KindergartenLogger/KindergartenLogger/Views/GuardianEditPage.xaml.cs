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
using System.Security.Cryptography;

namespace KindergartenLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GuardianEditPage : ContentPage
    {

        public Regex OIBRegex = new Regex("[0123456789]{11}");
        public SHA256 encrypt = SHA256.Create();
        public GuardianEditPage()
        {
            InitializeComponent();
            GuardianNameEntry.Text = App.Data.GuardianNameText;
            GuardianOIBEntry.Text = App.Data.GuardianOIBText;
        }
        private bool OIBIsValid=true; 
        private void ConfirmClicked(object sender, EventArgs e)
        {
            if (GuardianPasswordEntry.Text.Length < 8 || GuardianPasswordEntry.Text.Length > 16)
            {
                DisplayAlert("Pogreška", "Lozinka mora imati 8-16 simbola", "U redu");
            }
            else
            {
                foreach (Guardian guardian in App.Data.GuardianList) {
                    if (guardian.OIB == App.Data.GuardianOIBText) {
                        guardian.Password = encrypt.ComputeHash(Encoding.UTF8.GetBytes(GuardianPasswordEntry.Text));
                        Shell.Current.GoToAsync("//GuardianEditList");
                        App.Data.RefreshGuardian();
                        break;
                    }
                }
            }
        }

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//GuardianEditList");
            App.Data.RefreshGuardian();
        }

        private async void DeleteClicked(object sender, EventArgs e) 
        {
            if (await DisplayAlert("Potvrda", "Jeste li sigurni da želite izbrisati " + App.Data.GuardianNameText + "?", "Da", "Ne"))
            {
                App.Data.GuardianList = new ObservableCollection<Guardian>(App.Data.GuardianList.Where((c, ind) => c.OIB != App.Data.GuardianOIBText));
                App.Data.RefreshGuardian();
                await Shell.Current.GoToAsync("//GuardianEditList");
            }
        }

    }
}