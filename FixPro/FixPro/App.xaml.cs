
using Akavache;
using FixPro.Models;
using FixPro.Services.Data;
using FixPro.ViewModels;
using FixPro.Views;
using FixPro.Views.CustomerPages;
using FixPro.Views.SchedulePages;
using GoogleApi.Entities.Translate.Common.Enums;
using OneSignalSDK.Xamarin;
using OneSignalSDK.Xamarin.Core;

//using Plugin.Permissions.Abstractions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Reactive;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using System.Web;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro
{
    public partial class App : Application
    {

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        public static string AppName = "DeepLinking";

        public static AccountModel AccountModelObj { get; set; } = new AccountModel();

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        //public static string UserStateKey = "UserStateKey";
        private SignalRService _signalRService;

        public App()
        {          

            InitializeComponent();

            //Plugin.Media.CrossMedia.Current.Initialize();
            ////Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Camera);
            ////Plugin.Permissions.CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Storage);

            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM2MzEwQDMxMzkyZTMzMmUzMExmNTFqY1piMlA5L3VpMVZ6eGtadDZIK2tscjV2MnVMdGpWZzlrOHp0cU09");

            OneSignal.Default.Initialize("55990079-84e5-4e65-8452-0220bf277c6d");
            OneSignal.Default.PromptForPushNotificationsWithUserResponse();


            Helpers.Settings.PlayerId = OneSignal.Default.DeviceState.userId != null ? OneSignal.Default.DeviceState.userId.ToString() : null;


            if (Helpers.Settings.UserName != "" && Helpers.Settings.Password != "")
            {
                
                MainPage = new NavigationPage(new MainPage());
            }
            else
            {
                MainPage = new NavigationPage(new Views.LoginPage());
            }

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
            OneSignal.Default.NotificationOpened += Default_NotificationOpened;
        }

        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                // Connection to internet is Not available
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetPage(new MainPage()));
                return;
            }
        }



        protected async override void OnStart()
        {
            base.OnStart();

            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                await GetPlayerIdFromOneSignal();

                await SignalRservice();

                if (Helpers.Settings.AccountId != "0")
                {
                    await Init(int.Parse(Helpers.Settings.AccountId));
                }

                await Controls.StartData.GetCom_Main();
            }
            else
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetPage(new MainPage()));
                return;
            }

            //HandleDeepLinking();

            //MainThread();
        }


        protected async override void OnSleep()
        {

            await SignalRNotservice();

            // Save the current page state
            var currentPage = Application.Current.MainPage as NavigationPage;
            var state = currentPage.CurrentPage.BindingContext;
            Application.Current.Properties["CurrentPageState"] = state;

            Controls.StartData.IsRunning = false;
            //MainThread();

            _signalRService.OnMessageReceived += _signalRService_OnMessageReceivedInSleep;


            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;

            base.OnSleep();

        }

        private void _signalRService_OnMessageReceivedInSleep(string arg1, string arg2, string arg3, string arg4)
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                if (Helpers.Settings.UserName != "" && Helpers.Settings.Password != "")
                {
                    if (!string.IsNullOrEmpty(arg1) && arg1 != Helpers.Settings.PlayerId && arg2.ToLower() == (Helpers.Settings.UserName).ToLower())
                    {
                        //await SignalRNotservice();
                        Helpers.Settings.AccountId = "0";
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
                        Helpers.Utility.ServerUrl = Helpers.Utility.ServerUrlStatic;

                        Controls.StartData.IsRunning = false;

                    }
                }
            });
        }

        protected async override void OnResume()
        {
            base.OnResume(); 

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

            // Retrieve the saved page state and set the page properties
            if (Application.Current.Properties.ContainsKey("CurrentPageState"))
            {
                var state = Application.Current.Properties["CurrentPageState"];
                var currentPage = Application.Current.MainPage as NavigationPage;
                currentPage.CurrentPage.BindingContext = state;
            }

            Controls.StartData.IsRunning = true;
            //MainThread();

            if (Helpers.Settings.UserName == "" && Helpers.Settings.Password == "")
            {
                await App.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
                await App.Current.MainPage.DisplayAlert("Alert", "You’ve been logged out.\r\n(account is opened on another device)\r\n", "Ok");
            }

            _signalRService.OnMessageReceived -= _signalRService_OnMessageReceivedInSleep;

            await SignalRservice();
        }

        public async Task GetPlayerIdFromOneSignal()
        {
            Device.StartTimer(new TimeSpan(0, 0, 1), () =>
            {
                if (Helpers.Settings.PlayerId == "0")
                {
                    if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
                    {
                        // do something every 1 seconds
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            Helpers.Settings.PlayerId = OneSignal.Default.DeviceState.userId != null ? OneSignal.Default.DeviceState.userId.ToString() : null;
                        });

                        return true;
                    }
                }
                return false; // runs again, or false to stop
            });
        }

        private async void Default_NotificationOpened(NotificationOpenedResult result)
        {
            if (result.notification.additionalData != null && result.notification.additionalData.ContainsKey("deeplink"))
            {
                string deepLink = result.notification.additionalData["deeplink"].ToString();

                if (deepLink.StartsWith("Schedule"))
                {
                    List<string> list = deepLink.Split(',').ToList(); //list[1] = ScheduleId , list[2] = ScheduleDateId

                    int ScheduleId = 0, ScheduleDateId = 0;

                    bool Try1 = int.TryParse(list[1], out ScheduleId);
                    bool Try2 = int.TryParse(list[2], out ScheduleDateId);

                    if (Try1 && Try2)
                    {
                        var VM = new SchedulesViewModel(ScheduleId, ScheduleDateId);
                        //var page = new NewSchedulePage();
                        var page = new ScheduleDetailsPage();
                        page.BindingContext = VM;
                        await App.Current.MainPage.Navigation.PushAsync(page);
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                    }
                }
                else if (deepLink == "Meeting")
                {
                    await App.Current.MainPage.Navigation.PushAsync(new Views.NotificationsPage());
                }
                else
                {
                    await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                }
            }
        }


        public async Task SignalRNotservice()
        {
            _signalRService.OnMessageReceived -= _signalRService_OnMessageReceived;
        }

        public async Task SignalRservice()
        {
            _signalRService = new SignalRService();

            _signalRService.OnMessageReceived += _signalRService_OnMessageReceived;

            await _signalRService.StartAsync();
        }


        private async void _signalRService_OnMessageReceived(string arg1, string arg2, string arg3, string arg4)
        {
            Device.BeginInvokeOnMainThread(async() =>
            {
                if (Helpers.Settings.UserName != "" && Helpers.Settings.Password != "")
                {
                    if (!string.IsNullOrEmpty(arg1) && arg1 != Helpers.Settings.PlayerId && arg2.ToLower() == (Helpers.Settings.UserName).ToLower())
                    {
                        //await SignalRNotservice();
                        Helpers.Settings.AccountId = "0";
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
                        Helpers.Utility.ServerUrl = Helpers.Utility.ServerUrlStatic;
                        await App.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
                        Controls.StartData.IsRunning = false;
                        await App.Current.MainPage.DisplayAlert("Alert", "You’ve been logged out.\r\n(account is opened on another device)\r\n", "Ok");
                    }
                }
            });

        }

        async void MainThread()
        {
            Device.StartTimer(new TimeSpan(0, 0, 5), () =>
            {
                if (Helpers.Settings.PlayerId == "0")
                {
                    if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
                    {
                        // do something every 1 seconds
                        Device.BeginInvokeOnMainThread(async () =>
                        {
                            Helpers.Settings.PlayerId = OneSignal.Default.DeviceState.userId != null ? OneSignal.Default.DeviceState.userId.ToString() : null;
                        });

                        return true;
                    }
                }
                return false; // runs again, or false to stop
            });

            if (Helpers.Settings.AccountId != "0" && Helpers.Settings.UserId != "0" && Helpers.Settings.PlayerId != "0")
            {
                await RunThread(7);
            }
        }


        async Task Init(int AccountId)
        {
            DateTime Dt = DateTime.Now;

            bool convert = DateTime.TryParse(Helpers.Settings.AccountDayExpired, out Dt);

            if (convert)
            {
                if (DateTime.Now.Day > Dt.Day || DateTime.Now.Month > Dt.Month)
                {
                    AccountModelObj = await GetExpiredDate(AccountId);

                    if (AccountModelObj?.ExpireDate != null)
                    {
                        string AccountExpiredFromDataBase = AccountModelObj?.ExpireDate.ToString();
                        if (!string.IsNullOrEmpty(AccountExpiredFromDataBase))
                        {
                            Helpers.Settings.AccountDayExpired = DateTime.Now.ToString();

                            var _ExpireDate = DateTime.Parse(AccountExpiredFromDataBase);
                            var _TodayDate = DateTime.Parse(DateTime.Now.ToString());
                            TimeSpan diff = _TodayDate - _ExpireDate;
                            var days = diff.Days;
                            var Hours = diff.Hours;

                            if (days > 0 || days == 0)
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "Account expired", "Ok");
                                await App.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
                            }
                            else
                            {
                                await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                                Helpers.Settings.AccountDayExpired = DateTime.Now.ToString();
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                            Helpers.Settings.AccountDayExpired = DateTime.Now.ToString();
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                        Helpers.Settings.AccountDayExpired = DateTime.Now.ToString();
                    }
                }
                else
                {
                    await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                    Helpers.Settings.AccountDayExpired = DateTime.Now.ToString();
                }
            }
            else
            {
                Helpers.Settings.AccountDayExpired = DateTime.Now.ToString();
            }
        }

        async Task<AccountModel> GetExpiredDate(int AccountId)
        {
            try
            {
                var ExpDate = await ORep.GetAsync<AccountModel>("api/Login/GetExpiredDate?AccountId=" + AccountId);
                if(ExpDate != null)
                {
                    return ExpDate;
                }
                else
                {
                    return new AccountModel();
                }
            }
            catch (Exception ex)
            {
                return new AccountModel();
            }
            
        }

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
                        Helpers.Utility.ServerUrl = Helpers.Utility.ServerUrlStatic;
                        await App.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
                        Controls.StartData.IsRunning = false;
                        await App.Current.MainPage.DisplayAlert("Alert", "You’ve been logged out.\r\n(account is opened on another device)\r\n", "Ok");
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
                    if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
                    {
                        await CallMethodEveryXSecondsYTimes();
                    }
                });
                return Controls.StartData.IsRunning; // runs again, or false to stop

            });
        }
    }
}
