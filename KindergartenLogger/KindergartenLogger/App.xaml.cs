
using Android;
using Android.Content.PM;
using Android.OS;
using KindergartenLogger.Services;
using KindergartenLogger.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace KindergartenLogger
{
    public partial class App : Application
    {
        public static object ParentWindow { get; set; }
        public static DataHandler Data = new DataHandler();
        public static MessageHandler Message = new MessageHandler();

        public App()
        {
            InitializeComponent();
            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
