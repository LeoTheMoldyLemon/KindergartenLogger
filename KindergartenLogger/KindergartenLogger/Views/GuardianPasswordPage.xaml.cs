using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KindergartenLogger.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GuardianPasswordPage : ContentPage
    {
        public GuardianPasswordPage()
        {
            InitializeComponent();

        }

        private void ConfirmClicked(object sender, EventArgs e)
        {
            if (App.Data.GuardianPastebin.Password.SequenceEqual(App.Data.Encrypt.ComputeHash(Encoding.UTF8.GetBytes(PasswordEntry.Text))))
            {
                Shell.Current.GoToAsync(App.Data.PasswordForwardPage);
                PasswordEntry.Text = "";
            }
            else
            {
                DisplayAlert("Pogrešna lozinka","Upsiali ste pogrešnu lozinku, ako se ne sjećate lozinke, molim pitajte ovlaštenu osobu za pomoć.","U redu");
                PasswordEntry.Text = "";
            }
        }
        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(App.Data.PasswordBackwardPage);
            PasswordEntry.Text = "";
        }
    }
}
