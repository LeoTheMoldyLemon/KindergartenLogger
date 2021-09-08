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


    public partial class MainPasswordPage : ContentPage
    {
        public MainPasswordPage()
        {
            InitializeComponent();

        }

        private void ConfirmClicked(object sender, EventArgs e)
        {
            if (PasswordEntry.Text != "")
            {
                if (App.Data.MainPassword.SequenceEqual(App.Data.Encrypt.ComputeHash(Encoding.UTF8.GetBytes(PasswordEntry.Text))))
                {
                    Shell.Current.GoToAsync(App.Data.PasswordForwardPage);
                    PasswordEntry.Text = "";
                }
                else
                {
                    DisplayAlert("Pogrešna lozinka", "Upisali ste pogrešnu lozinku, ako niste ovlaštena osoba, molim potražite pomoć od navedenog.", "U redu");

                    PasswordEntry.Text = "";
                }
            }
        }
        

        private void CancelClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync(App.Data.PasswordBackwardPage);
            PasswordEntry.Text = "";
        }
    }
}