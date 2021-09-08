using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Android;
using AndroidX.Core.Content;
using AndroidX.Core.App;
using Android.Content;

namespace KindergartenLogger.Droid
{
    [Activity(Label = "KindergartenLogger", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        private void CheckAppPermissions()
        {
            if ((int)Build.VERSION.SdkInt < 23)
            {
                Console.WriteLine("Permissions have been granted thru manifest");
                return;
            }
            else
            {
                if (PackageManager.CheckPermission(Manifest.Permission.ReadExternalStorage, PackageName) != Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.WriteExternalStorage, PackageName) != Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.SendSms, PackageName) != Permission.Granted)
                {
                    var permissions = new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage, Manifest.Permission.SendSms};
                    RequestPermissions(permissions, 1);
                    Console.WriteLine("Permissions have been granted");

                }
                else
                {
                    Console.WriteLine("Permissions have already been granted");
                }
            }
        }
        protected override void OnCreate(Bundle bundle)
        {

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            CheckAppPermissions();
            LoadApplication(new App());

            App.ParentWindow = this;
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            if ((ContextCompat.CheckSelfPermission(this, Manifest.Permission.WriteExternalStorage) != (int)Permission.Granted)
            || (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != (int)Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new string[] { Manifest.Permission.ReadExternalStorage, Manifest.Permission.WriteExternalStorage }, 0);
            }





            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}