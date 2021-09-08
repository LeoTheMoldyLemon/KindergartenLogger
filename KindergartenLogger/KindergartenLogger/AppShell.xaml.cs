
using KindergartenLogger.Views;
using System;
using System.Collections.Generic;
using System.Reflection;
using Xamarin.Forms;

namespace KindergartenLogger
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            try
            {
                InitializeComponent();
            }
            catch(TargetInvocationException e){
                Console.WriteLine(e);
            }
//            Routing.RegisterRoute("ChildAddPage", typeof(ChildAddPage));
//            Routing.RegisterRoute("ChildEditPage", typeof(ChildEditPage));
//            Routing.RegisterRoute("ChildEditList", typeof(ChildEditList));

        }
        private void ChildEditListClicked(object sender, EventArgs e) 
        {
            Console.WriteLine("Password page called");
            App.Data.PasswordForwardPage = "//ChildEditList";
            App.Data.PasswordBackwardPage = "//ChildEntryList";
            Current.GoToAsync("//MainPasswordPage");

        }
        private void GuardianEditListClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Password page called");
            App.Data.PasswordForwardPage = "//GuardianEditList";
            App.Data.PasswordBackwardPage = "//ChildEntryList";
            Current.GoToAsync("//MainPasswordPage");

        }
        private void OptionsEditClicked(object sender, EventArgs e)
        {
            Console.WriteLine("Password page called");
            App.Data.PasswordForwardPage = "//MainOptionsPage";
            App.Data.PasswordBackwardPage = "//ChildEntryList";
            Current.GoToAsync("//MainPasswordPage");

        }



    }
}
