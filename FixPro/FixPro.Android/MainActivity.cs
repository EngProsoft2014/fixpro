using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Acr.UserDialogs;
using Android.Views;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Xamarin.Essentials;
using Plugin.Permissions;
using Android.Content;
using Xamarin.Forms;
using Plugin.PayCards;
using AndroidX.AppCompat.App;
using Plugin.CurrentActivity;
using Plugin.Connectivity;
using OneSignalSDK.Xamarin;
using AndroidX.Core.OS;
using System.Threading.Tasks;
using FixPro.Views;
using Android.Net;
using System.Web;
using FixPro.ViewModels;
using Android.Text;
using Android.Widget;
using OneSignalSDK.Xamarin.Core;
using Android;

namespace FixPro.Droid
{
    [Activity(Label = "FixPro", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = false, ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize)]

    //[IntentFilter(new[] { Android.Content.Intent.ActionView },
    //              DataScheme = "http",
    //              DataHost = "FixPro.com",
    //              DataPathPrefix = "/deeplink",
    //              AutoVerify = true,
    //              Categories = new[] { Android.Content.Intent.CategoryDefault, Android.Content.Intent.CategoryBrowsable })]

    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        Intent serviceIntent;
        private const int RequestCode = 5469;
        protected async override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //Xamarin.FormsMaps.Init(this, savedInstanceState);

            Xamarin.FormsMaps.Init(this, savedInstanceState);
            //Plugin.CurrentActivity.CrossCurrentActivity.Current.Activity = this;
            //Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);


            Rg.Plugins.Popup.Popup.Init(this);
            UserDialogs.Init(this);

            AppCompatDelegate.DefaultNightMode = AppCompatDelegate.ModeNightNo;

            int requestPermissions = 1;
            string cameraPermission = Android.Manifest.Permission.Camera;
            if (!(ContextCompat.CheckSelfPermission(this, cameraPermission) == (int)Permission.Granted))
            {
                ActivityCompat.RequestPermissions(this, new String[] { cameraPermission, }, requestPermissions);
            }

            int requestCode = 1;
            // Check if the permission is not granted
            if (ContextCompat.CheckSelfPermission(this, Manifest.Permission.ReadExternalStorage) != Permission.Granted)
            {
                // Request the permission
                ActivityCompat.RequestPermissions(this, new[] { Manifest.Permission.ReadExternalStorage }, requestCode);
            }



            //await CrossPermissions.Current.RequestPermissionAsync<CameraPermission>();
            //await CrossPermissions.Current.RequestPermissionAsync<StoragePermission>();

            //string DeviceId = Android.Provider.Settings.Secure.GetString(Android.App.Application.Context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
            //Helpers.Settings.DeviceId = DeviceId;

            //if (Intent.Data != null)
            //{
            //    // Extract the URI from the Intent
            //    var uri = Intent.Data.ToString();

            //    // Handle the URI and navigate to the appropriate page
            //    if (uri.StartsWith("myapp://page"))
            //    {
            //        // Extract any additional parameters from the URI if needed
            //        // Navigate to the desired page
            //        MainPage = new MyPage();
            //    }
            //}


            //string cc = OneSignal.Default.DeviceState.userId.ToString();
            //await Task.Delay(3000);


            Window.AddFlags(WindowManagerFlags.Fullscreen);
            Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);

            PayCardsRecognizerService.Initialize(this);

            FFImageLoading.Forms.Platform.CachedImageRenderer.Init(true);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());

            Plugin.CurrentActivity.CrossCurrentActivity.Current.Init(this, savedInstanceState);
            await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
            //try
            //{
            //    if (Connectivity.NetworkAccess != NetworkAccess.Internet)
            //    {
            //        ////await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
            //        await App.Current.MainPage.Navigation.PushAsync(new NoInternetPage(new MainPage()));
            //        return;
            //    }
            //    else
            //    {
                    

            //        //StartService(new Intent(this, typeof(AndroidLocationService)));
            //        //serviceIntent = new Intent(this, typeof(AndroidLocationService));
            //        //SetServiceMethods();

            //        //bool answer = await App.Current.MainPage.DisplayAlert("Location Track Permission", "Allow to Collcet and track your location in the following cases :\r\n\r\n- always in use \r\n- when the app is not in use\r\n\r\nto assign you to a job next to you?", "Yes", "No");
            //        //if (answer)
            //        //{

            //        //}
            //    }
            //}
            //catch (Exception ex)
            //{
            //    await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            //}

        }


        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public override void OnBackPressed()
        {
            Rg.Plugins.Popup.Popup.SendBackPressed(base.OnBackPressed);
        }

        void SetServiceMethods()
        {
            MessagingCenter.Subscribe<StartServiceMessage>(this, "ServiceStarted", message =>
            {
                if (!IsServiceRunning(typeof(AndroidLocationService)))
                {
                    if (Android.OS.Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
                    {
                        StartForegroundService(serviceIntent);
                    }
                    else
                    {
                        StartService(serviceIntent);
                    }
                }
            });

            MessagingCenter.Subscribe<StopServiceMessage>(this, "ServiceStopped", message =>
            {
                if (IsServiceRunning(typeof(AndroidLocationService)))
                    StopService(serviceIntent);
            });
        }

        private bool IsServiceRunning(System.Type cls)
        {
            ActivityManager manager = (ActivityManager)GetSystemService(Context.ActivityService);
            foreach (var service in manager.GetRunningServices(int.MaxValue))
            {
                if (service.Service.ClassName.Equals(Java.Lang.Class.FromType(cls).CanonicalName))
                {
                    return true;
                }
            }
            return false;
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            if (requestCode == RequestCode)
            {
                if (Android.Provider.Settings.CanDrawOverlays(this))
                {

                }
            }

            base.OnActivityResult(requestCode, resultCode, data);

            PayCardsRecognizerService.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnNewIntent(Intent intent)
        {
            base.OnNewIntent(intent);
        }
    }
}