using Acr.UserDialogs;
using FixPro.Models;
using FixPro.Services.Data;
using FixPro.ViewModels;
using Newtonsoft.Json;
using OneSignalSDK.Xamarin;
using OneSignalSDK.Xamarin.Core;
using Plugin.Geolocator;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Xamarin.Essentials;
using Xamarin.Forms;
using FixPro.Helpers;
using Microsoft.AspNet.SignalR.Client;
using Syncfusion.SfCalendar.XForms;

namespace FixPro
{
    public partial class MainPage : Controls.CustomsPage
    {
        HomeViewModel ViewModel { get => BindingContext as HomeViewModel; set => BindingContext = value; }

        //private readonly ChatService chatService;
        //public ObservableCollection<string> Messages { get; private set; }

        //private readonly HubConnectionService chatService;
        
        //public ObservableCollection<string> Messages { get; }

        static int Idincerment = 0;

        //private readonly HubConnection hubConnection;

        //private SignalRService _signalRService;

        public MainPage()
        {
            InitializeComponent();

            Animation();
            //HomeViewModel vm = new HomeViewModel();
            //this.BindingContext = vm;

            lblLoginName.Text = Helpers.Settings.UserName;
            lblLoginPhone.Text = Helpers.Settings.Phone;

            //StartGetLocation();

        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        async void Animation()
        {
            await yumImgLogo.TranslateTo(0, 0, 400);
            await yumTimeSheet.ScaleTo(1, 200);
            await yumCustomers.ScaleTo(1, 200);
            await yumSchedules.ScaleTo(1, 200);
            await yumCalls.ScaleTo(1, 200);
            await imgWave.TranslateTo(0, 0, 100);
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();


            AccountImg.Source = Helpers.Settings.UserPricture != null ? Helpers.Settings.UserPricture : "avatar.png";

            //await chatService.Connect();
            //BadgeNotifications.Num = Messages.Count;

            //await chatService.Connect();
            //lblHub.Text = Messages.FirstOrDefault();

            
            //_signalRService = new SignalRService();
            //await Task.Run(Connect);
            //ViewModel.Massages = new ObservableCollection<string>();

            
            //_signalRService.OnMessageReceived += (user, message, userFrom , userTo) =>
            //{
            //   ViewModel.Massages.Add($"{user}: {message} , from: {userFrom} - to: {userTo}");
            //};
            
  
            //await _signalRService.StartAsync();
            //lblHub.Text = ViewModel.Massages.FirstOrDefault();
        }

        protected override async void OnDisappearing()
        {
            base.OnDisappearing();

            //await chatService.Disconnect();
            //BadgeNotifications.Num = Messages.Count;

            //await chatService.Disconnect();
            //lblHub.Text = Messages.FirstOrDefault();
        }


        //async Task Connect()
        //{
        //    try
        //    {
        //        await _signalRService.StartAsync();
        //    }
        //    catch (Exception ex)
        //    {
        //        // Something has gone wrong
        //    }
        //}


        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                var exit = false;
                exit = await this.DisplayAlert("FixPro", "Do you want to exit the program?", "Ok", "I want to stay").ConfigureAwait(false);
                if (exit)
                {
                    System.Diagnostics.Process.GetCurrentProcess().CloseMainWindow();
                }
            });
            return true;
        }

        //string SendSMS(string Phone)
        //{
        //    string accountSid = "AC1e12d1c89d9d238bf945b2f9b8ebc6a2";
        //    string authToken = "d9212c43cc269700de08ef643b0d1dab";

        //    TwilioClient.Init(accountSid, authToken);

        //    var message = MessageResource.Create(
        //        body: "here : Message to customer",
        //        from: new Twilio.Types.PhoneNumber("+12086182061"),
        //        to: new Twilio.Types.PhoneNumber("+1" + Phone)
        //    );

        //    return message.Sid;
        //}


        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            ViewModel.GetPerrmission();

            navigationDrawer.Position = Syncfusion.SfNavigationDrawer.XForms.Position.Left;
            navigationDrawer.ToggleDrawer();
        }

        private void actIndLoading_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (actIndLoading.IsRunning == true)
                this.IsEnabled = false;
            else
                this.IsEnabled = true;
        }

        private async void Button_Clicked_1(object sender, EventArgs e) //Logout
        {
            UserDialogs.Instance.ShowLoading();

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

            UserDialogs.Instance.HideLoading();
        }


        async void StartGetLocation()
        {
            var permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.LocationAlways>();

            if (permission == Xamarin.Essentials.PermissionStatus.Denied)
            {
                // TODO Let the user know they need to accept
                return;
            }

            if (Device.RuntimePlatform == Device.iOS)
            {
                if (CrossGeolocator.Current.IsListening)
                {
                    await CrossGeolocator.Current.StopListeningAsync();
                    CrossGeolocator.Current.PositionChanged -= Current_PositionChanged;
                    return;
                }

                await CrossGeolocator.Current.StartListeningAsync(TimeSpan.FromSeconds(3), 10, false, new Plugin.Geolocator.Abstractions.ListenerSettings
                {
                    ActivityType = Plugin.Geolocator.Abstractions.ActivityType.AutomotiveNavigation,
                    AllowBackgroundUpdates = true,
                    DeferLocationUpdates = false,
                    DeferralDistanceMeters = 10,
                    DeferralTime = TimeSpan.FromSeconds(5),
                    ListenForSignificantChanges = true,
                    PauseLocationUpdatesAutomatically = true
                });

                CrossGeolocator.Current.PositionChanged += Current_PositionChanged;
            }
            //else if (Device.RuntimePlatform == Device.Android)
            //{
            //    if (Preferences.Get("LocationServiceRunning", false) == false)
            //    {
            //        StartService();
            //    }
            //    else
            //    {
            //        StopService();
            //    }
            //}
        }

        private async void Current_PositionChanged(object sender, Plugin.Geolocator.Abstractions.PositionEventArgs e)
        {
            try
            {
                //string uri = "https://projectservices.engprosoft.com/XMLData/61_2023-01-24.xml";

                //UserDialogs.Instance.ShowLoading();
                //XDocument document = XDocument.Load(uri);
                //UserDialogs.Instance.HideLoading();

                //DataSet ds = new DataSet();
                //ds.ReadXml(new XmlTextReader(new StringReader(document.ToString())));

                //DataTable employeesTable = ds.Tables[0];

                //List<DataMapsModel> List = (from DataRow dr in employeesTable.Rows
                //                            where dr["EmployeeId"].ToString() == OneEmployee.Id.ToString()
                //                            select new DataMapsModel()
                //                            {
                //                                Id = int.Parse(dr["Tracking_id"].ToString()),
                //                                EmployeeId = int.Parse(dr["EmployeeId"].ToString()),
                //                                Lat = dr["lat"].ToString(),
                //                                Long = dr["log"].ToString(),
                //                                Time = dr["time"].ToString(),
                //                                CreateDate = dr["date"].ToString(),
                //                                MPosition = new Position(double.Parse(dr["lat"].ToString()), double.Parse(dr["log"].ToString())),
                //                            }).OrderBy(c => c.Id).ToList();

                List<DataMapsModel> Listmap = new List<DataMapsModel>();
                Idincerment += 1;

                Listmap.Add(new DataMapsModel
                {
                    Id = Idincerment,
                    BranchId = int.Parse(Helpers.Settings.BranchId),
                    EmployeeId = int.Parse(Helpers.Settings.UserId),
                    Lat = e.Position.Latitude.ToString(),
                    Long = e.Position.Longitude.ToString(),
                    Time = e.Position.Timestamp.TimeOfDay.ToString(),
                    CreateDate = DateTime.Now.ToShortDateString(),
                    MPosition = new Xamarin.Forms.Maps.Position(e.Position.Latitude, e.Position.Longitude),
                });

                await Helpers.Utility.PostData("api/UploadXML/PostXmlFile", JsonConvert.SerializeObject(Listmap, Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings()
                            {
                                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                            }));


            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Saving position for tracking failed!", "OK");
            }

        }


        //async void StartPermission()
        //{
        //    var permission = await Xamarin.Essentials.Permissions.RequestAsync<Xamarin.Essentials.Permissions.LocationAlways>();

        //    if (permission == Xamarin.Essentials.PermissionStatus.Denied)
        //    {
        //        // TODO Let the user know they need to accept
        //        return;
        //    }

        //    if (Device.RuntimePlatform == Device.Android)
        //    {
        //        if (Preferences.Get("LocationServiceRunning", false) == false)
        //        {
        //            StartService();
        //        }
        //        else
        //        {
        //            StopService();
        //        }
        //    }
        //}



        //private void StartService()
        //{
        //    var startServiceMessage = new StartServiceMessage();
        //    MessagingCenter.Send(startServiceMessage, "ServiceStarted");
        //    Preferences.Set("LocationServiceRunning", true);
        //}

        //private void StopService()
        //{
        //    var stopServiceMessage = new StopServiceMessage();
        //    MessagingCenter.Send(stopServiceMessage, "ServiceStopped");
        //    Preferences.Set("LocationServiceRunning", false);
        //}

    }
}
