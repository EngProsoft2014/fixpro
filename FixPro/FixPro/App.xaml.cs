
using FixPro.ViewModels;
using FixPro.Views;
using OneSignalSDK.Xamarin;
using OneSignalSDK.Xamarin.Core;

//using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro
{
    public partial class App : Application
    {

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        public static string AppName = "DeepLinking";

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        public App()
        {

            InitializeComponent();

            Plugin.Media.CrossMedia.Current.Initialize();
            //Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            //Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM2MzEwQDMxMzkyZTMzMmUzMExmNTFqY1piMlA5L3VpMVZ6eGtadDZIK2tscjV2MnVMdGpWZzlrOHp0cU09");
            

            OneSignal.Default.Initialize("55990079-84e5-4e65-8452-0220bf277c6d");
            OneSignal.Default.PromptForPushNotificationsWithUserResponse();

            if (Helpers.Settings.UserName != "" && Helpers.Settings.Password != "")
            {
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new Views.LoginPage());
            }

            //MainPage = new NavigationPage(new Views.PopupPages.PaymentMethodsPopup());

        }

        

        protected async override void OnStart()
        {
            base.OnStart();

            //HandleDeepLinking();

            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                if (Helpers.Settings.PlayerId == "0")
                {
                    // do something every 1 seconds
                    Device.BeginInvokeOnMainThread(async () =>
                    {
                        Helpers.Settings.PlayerId = OneSignal.Default.DeviceState.userId != null ? OneSignal.Default.DeviceState.userId.ToString() : null;
                    });
                    return true;
                }
                return false; // runs again, or false to stop
            });

            if (Helpers.Settings.AccountId != "0" && Helpers.Settings.UserId != "0" && Helpers.Settings.PlayerId != "0")
            {
                await RunThread(7);
            }
        }


        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }



        //protected override void OnAppLinkRequestReceived(Uri uri)
        //{
        //    // Handle the deep link when the app is already running
        //    HandleDeepLinking(uri);

        //    base.OnAppLinkRequestReceived(uri);
        //}


        private void HandleDeepLinking(Uri deepLink = null)
        {
            // Parse the deep link URL and extract any relevant information
            // You can use the 'deepLink' parameter or 'AppLinkRequestUri' property
            // to get the deep link URL

            if (deepLink != null)
            {
                // Extract the specific page information from the deep link URL
                string page = ExtractPageFromDeepLink(deepLink);

                // Navigate to the appropriate page based on the deep link information
                MainPage = GetPageForDeepLink(page);
            }
        }

        private string ExtractPageFromDeepLink(Uri deepLink)
        {
            // Extract the specific page information from the deep link URL
            // You may need to parse the deep link URL and extract the necessary information
            // based on the format you used for deep linking

            // Example: Extract the page from the path of the deep link URL
            string page = deepLink.AbsolutePath.TrimStart('/');

            return page;
        }

        private Page GetPageForDeepLink(string page)
        {
            // Return the appropriate Xamarin.Forms page based on the deep link information

            // Example: Create and return the corresponding page based on the extracted 'page' value
            if (page == "page1")
                return new Views.NotificationsPage();
            else
                return new Views.TimeSheetPage();

            //if (page.Contains("page1"))
            //    return new Views.NotificationsPage();
            //else
            //    return new Views.TimeSheetPage();
        }

        public async Task CallMethodEveryXSecondsYTimes()// Method for Check Device every 5 Second
        {

            if (Controls.StartData.IsRunning == true)
            {
                if (Helpers.Settings.AccountId != "0" && Helpers.Settings.UserId != "0" && Helpers.Settings.PlayerId != "0")
                {
                    string UserToken = await _service.UserToken();
                    string PlayerId = await ORep.GetAsync<string>("api/Employee/GetEmployeePlayerId?" + "AccountId=" + Helpers.Settings.AccountId + "&" + "EmpId=" + Helpers.Settings.UserId, UserToken);
                    if (Helpers.Settings.PlayerId != PlayerId)
                    {
                        Helpers.Settings.UserId = "0";
                        Helpers.Settings.UserName = "";
                        Helpers.Settings.UserFristName = "";
                        Helpers.Settings.Email = "";
                        Helpers.Settings.Phone = "";
                        Helpers.Settings.Password = "";
                        Helpers.Settings.CreateDate = "";
                        Helpers.Settings.BranchId = "";
                        Helpers.Settings.BranchName = "";
                        Helpers.Settings.UserRole = "";
                        await App.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
                        Controls.StartData.IsRunning = false;
                        await App.Current.MainPage.DisplayAlert("Alert", "Sorry, for Logout, because the App is open from another device", "Ok");
                    }
                }
            }
        }

        public async Task RunThread(int waitSeconds)
        {
            Device.StartTimer(new TimeSpan(0, 0, waitSeconds), () =>
            {
                // do something every 5 seconds
                Device.BeginInvokeOnMainThread(async () =>
                {
                    await CallMethodEveryXSecondsYTimes();
                });
                return Controls.StartData.IsRunning; // runs again, or false to stop
            });
        }
    }
}
