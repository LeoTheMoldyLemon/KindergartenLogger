using KindergartenLogger.Models;
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
    public partial class ChildSuccessPage : ContentPage
    {
        public ChildSuccessPage()
        {
            InitializeComponent();
        }
        ObservableCollection<Child> children;
        protected override async void OnAppearing(){
            foreach (Child c in App.Data.ChildList)
            {
                if (c == App.Data.ChildPastebin) 
                {
                    c.Departed = true;
                }
            }
            App.Data.RefreshChild();
            //fucky stvar s porukama
            Console.WriteLine("Success childasd");
            await DisplayAlert("Uspjeh","Uspješno ste prijavili odlazak dijeteta, poruke o odlasku poslane su ostalim skrbnicima djeteta.", "U redu");
            await Shell.Current.GoToAsync("//ChildExitList");

        }
    }
}
