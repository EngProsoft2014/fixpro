using Acr.UserDialogs;
using Acr.UserDialogs.Infrastructure;
using GoogleApi.Entities.Translate.Common.Enums;
using Microsoft.AspNet.SignalR.Client;
using Newtonsoft.Json;
using FixPro.Models;
using FixPro.Views;
using FixPro.Views.MenuPages;
using FixPro.Views.SchedulePages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Twilio.TwiML.Messaging;
using Xamarin.Essentials;
using Xamarin.Forms;
using FixPro.Controls;
using System.Reactive.Concurrency;
using Syncfusion.SfCalendar.XForms;
using GoogleApi.Entities.Search.Video.Common;
using OneSignalSDK.Xamarin.Core;
using Xamarin.Forms.Internals;
using FixPro.Services.Data;

namespace FixPro.ViewModels
{
    public class HomeViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        ObservableCollection<EmployeeModel> _LstWorkingEmployees;
        public ObservableCollection<EmployeeModel> LstWorkingEmployees
        {
            get
            {
                return _LstWorkingEmployees;
            }
            set
            {
                _LstWorkingEmployees = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstWorkingEmployees"));
                }
            }
        }

        ObservableCollection<EmployeeModel> _LstEmpInAccountId;
        public ObservableCollection<EmployeeModel> LstEmpInAccountId
        {
            get
            {
                return _LstEmpInAccountId;
            }
            set
            {
                _LstEmpInAccountId = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEmpInAccountId"));
                }
            }
        }

        ObservableCollection<NotificationsModel> _Messages;
        public ObservableCollection<NotificationsModel> Messages
        {
            get
            {
                return _Messages;
            }
            set
            {
                _Messages = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Messages"));
                }
            }
        }

        bool _IsBusy;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsBusy"));
                }
            }
        }

        int _NumNotify;
        public int NumNotify
        {
            get
            {
                return _NumNotify;
            }
            set
            {
                _NumNotify = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("NumNotify"));
                }
            }
        }

        string _UserRole;
        public string UserRole
        {
            get
            {
                return _UserRole;
            }
            set
            {
                _UserRole = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("UserRole"));
                }
            }
        }

        ImageSource _AccountPhoto;
        public ImageSource AccountPhoto
        {
            get
            {
                return _AccountPhoto;
            }
            set
            {
                _AccountPhoto = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AccountPhoto"));
                }
            }
        }

        string _HeaderNotify;
        public string HeaderNotify
        {
            get
            {
                return _HeaderNotify;
            }
            set
            {
                _HeaderNotify = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("HeaderNotify"));
                }
            }
        }


        string _ContentNotify;
        public string ContentNotify
        {
            get
            {
                return _ContentNotify;
            }
            set
            {
                _ContentNotify = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ContentNotify"));
                }
            }
        }

        EmployeeModel _Login;
        public EmployeeModel Login
        {
            get
            {
                return _Login;
            }
            set
            {
                _Login = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Login"));
                }
            }
        }

        EmployeeModel _EmployeePermission;
        public EmployeeModel EmployeePermission
        {
            get
            {
                return _EmployeePermission;
            }
            set
            {
                _EmployeePermission = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("EmployeePermission"));
                }
            }
        }

        public ICommand SelectedCustomersPage { get; set; }
        public ICommand SelectedSchedulePage { get; set; }
        public ICommand SelectedAllEmployeesPage { get; set; }
        public ICommand SelectedTimeSheetPage { get; set; }
        public ICommand SelectedLocationsPage { get; set; }
        public ICommand SelectedReturnSalesPopup { get; set; }
        public ICommand SelectedEmployeesWorkingPage { get; set; }
        public ICommand SelectedAccountPage { get; set; }
        public ICommand SelectedCallsPage { get; set; }
        public ICommand DeactiveNotify { get; set; }
        public ICommand SelectedNotificationDetails { get; set; }
        public ICommand SelectedNotificationsPage { get; set; }
        public ICommand SelectedCreateNotificationsPage { get; set; }
        public ICommand SelectedSendNotifications { get; set; }


        //private readonly Microsoft.AspNet.SignalR.Client.HubConnection connection;
        //private readonly IHubProxy hubProxy;
        //public event Action<string, string> MessageReceived;

        //ObservableCollection<string> _Massages;
        //public ObservableCollection<string> Massages
        //{
        //    get
        //    {
        //        return _Massages;
        //    }
        //    set
        //    {
        //        _Massages = value;
        //        if (PropertyChanged != null)
        //        {
        //            PropertyChanged(this, new PropertyChangedEventArgs("Massages"));
        //        }
        //    }
        //}

        //public class ChatService
        //{
        //    private readonly Microsoft.AspNet.SignalR.Client.HubConnection connection;
        //    private readonly IHubProxy hubProxy;

        //    public event Action<string, string> MessageReceived;

        //    public ChatService()
        //    {
        //        ///"https://localhost:44322/"
        //        //////connection = new Microsoft.AspNet.SignalR.Client.HubConnection("https://localhost:44322/");
        //        connection = new Microsoft.AspNet.SignalR.Client.HubConnection("https://projectservicesapi.engprosoft.com/");
        //        hubProxy = connection.CreateHubProxy("ChatHub");

        //        hubProxy.On<string, string, string, string>("addMessage", (name, message, fromUserId, toUserId) =>
        //        {
        //            MessageReceived?.Invoke(name, message);
        //        });
        //    }

        //    public async Task Connect()
        //    {
        //        await connection.Start();
        //    }

        //    public async Task Disconnect()
        //    {
        //        connection.Stop();
        //    }
        //}

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        //private SignalRService _signalRService;
        public HomeViewModel()
        {


            //_signalRService = new SignalRService();

            //_signalRService.OnMessageReceived += _signalRService_OnMessageReceived;
            //_signalRService.OnMessageReceived += (user, message, userFrom, userTo) =>
            //{  
            //    Massages.Add($"{user}: {message} , from: {userFrom} - to: {userTo}");
            //};

            //Massages = new ObservableCollection<string>();
            //_signalRService.StartAsync();

            //Massages = new ObservableCollection<string>();
            UserRole = Helpers.Settings.UserRole;

            Init();
            GetPerrmission();
            GetEmployeesInAccountId();
        }


        //Get Perrmission for User
        public async void GetPerrmission()
        {
            EmployeePermission = new EmployeeModel();
            await Controls.StartData.CheckPermissionEmployee();
            EmployeePermission = Controls.StartData.EmployeeDataStatic;
        }

        public async void GetEmployeesInAccountId()
        {
           await GetEmployeesInAccountId(int.Parse(Helpers.Settings.AccountId));
        }

        async void Init()
        {

            SelectedTimeSheetPage = new Command(OnSelectedTimeSheetPage);
            SelectedCustomersPage = new Command(OnSelectedCustomersPage);
            SelectedSchedulePage = new Command(OnSelectedSchedulePage);
            SelectedAccountPage = new Command(OnSelectedAccountPage);
            SelectedAllEmployeesPage = new Command(OnSelectedAllEmployeesPage);
            SelectedEmployeesWorkingPage = new Command<string>(OnSelectedEmployeesWorkingPage);
            SelectedCallsPage = new Command(OnSelectedCallsPage);
            DeactiveNotify = new Command<NotificationsModel>(OnDeactiveNotify);
            SelectedNotificationDetails = new Command<NotificationsModel>(OnSelectedNotificationDetails);
            SelectedNotificationsPage = new Command(OnSelectedNotificationsPage);
            SelectedCreateNotificationsPage = new Command(OnSelectedCreateNotificationsPage);
            SelectedSendNotifications = new Command(OnSelectedSendNotifications);

            Login = new EmployeeModel();
            LstEmpInAccountId = new ObservableCollection<EmployeeModel>();

            await Controls.StartData.GetAccountKeysAsync();

            NumNotify = 0;

            await GetNotifications();

            Login.UserName = Helpers.Settings.UserName;
            Login.Phone1 = Helpers.Settings.Phone;

            if (Helpers.Settings.UserPricture != "" && Helpers.Settings.UserPricture != null && !Helpers.Settings.UserPricture.Contains("https://app.fixprous.com/EmployeePic_"))
            {
                AccountPhoto = Login.Picture = Helpers.Settings.UserPricture;
            }
            else
            {
                AccountPhoto = Login.Picture = "avatar.png";
            }
        }

        public async Task GetNotifications()
        {
            
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();
                Messages = await ORep.GetAsync<ObservableCollection<NotificationsModel>>("api/Notifications/GetNotifications?" + "EmployeeId=" + Helpers.Settings.UserId, UserToken);
                NumNotify = Messages.Count;
            }      
        }

        // Get Employees in Account Id
        public async Task GetEmployeesInAccountId(int AccountId)
        {
            IsBusy = true;
            //UserDialogs.Instance.ShowLoading();
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await ORep.GetAsync<ObservableCollection<EmployeeModel>>("api/Employee/GetEmployeesInAccountId?" + "AccountId=" + AccountId, UserToken);

                if (json != null)
                {
                    LstEmpInAccountId = json;
                }

            }

            //UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnSelectedSendNotifications()
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "No Internet connection!", "OK");
                return;
            }
            else
            {
                string strEmployees = string.Empty;

                List<int> oEmployees = new List<int>();
                oEmployees = LstEmpInAccountId.Where(x => x.IsChecked == true).Select(m => m.Id).ToList();
                if (oEmployees.Count > 0)
                {
                    oEmployees.ForEach(f => strEmployees += $",{f}");
                    strEmployees = strEmployees.Remove(0, 1);
                }

                if (string.IsNullOrEmpty(strEmployees))
                {
                    await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Choose Employees.", "Ok");
                }
                else if (string.IsNullOrEmpty(HeaderNotify))
                {
                    await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Header of Notify.", "Ok");
                }
                else if (string.IsNullOrEmpty(ContentNotify))
                {
                    await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Content of Notify.", "Ok");
                }
                else
                {
                    string UserToken = await _service.UserToken();

                    NotificationsSpecificModel model = new NotificationsSpecificModel()
                    {
                        AccountId = int.Parse(Helpers.Settings.AccountId),

                        app_id = Helpers.Settings.OneSignalAppId,

                        Header = HeaderNotify,

                        Content = ContentNotify,

                        include_player_ids = LstEmpInAccountId.Where(v => v.IsChecked == true && !string.IsNullOrEmpty(v.OneSignalPlayerId)).Select(m => m.OneSignalPlayerId).ToArray(),

                        Employees = strEmployees,

                        CreateUser = int.Parse(Helpers.Settings.UserId),
                    };


                    UserDialogs.Instance.ShowLoading();
                    string json = await ORep.PostDataAsync("api/Notifications/PostNotificationSpecific", model, UserToken);
                    UserDialogs.Instance.HideLoading();

                    if (!string.IsNullOrEmpty(json))
                    {
                        await App.Current.MainPage.DisplayAlert("FixPro", "Notifications sent successfully", "Ok");
                        await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Failed to send notifications!", "Ok");
                    }
                }
            }
                
            IsBusy = false;
        }

        async void OnSelectedNotificationsPage()
        {
            IsBusy = true;
            if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetPage(new MainPage()));
                return;
            }
            else
            {
                UserDialogs.Instance.ShowLoading();

                string UserToken = await _service.UserToken();

                Messages = await ORep.GetAsync<ObservableCollection<NotificationsModel>>("api/Notifications/GetNotifications?" + "EmployeeId=" + Helpers.Settings.UserId, UserToken);
                NumNotify = Messages.Count;
                await App.Current.MainPage.Navigation.PushAsync(new Views.NotificationsPage());
                UserDialogs.Instance.HideLoading();
            }

            IsBusy = false;
        }

        async void OnSelectedCreateNotificationsPage()
        {
            IsBusy = true;

            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new NoInternetPage(new MainPage()));
                    //return;
                }
                else
                {
                    UserDialogs.Instance.ShowLoading();

                    await App.Current.MainPage.Navigation.PushAsync(new CreateNotificationsPage());

                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                //throw;
            }

            IsBusy = false;
        }

        async void OnDeactiveNotify(NotificationsModel model)
        {

            if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
            {
                await App.Current.MainPage.DisplayAlert("Alert", "No Internet connection!", "OK");
                return;
            }
            else
            {
                model.UpdateDate = DateTime.Now;
                model.UpdateUser = int.Parse(Helpers.Settings.UserId);

                var exit = false;
                exit = await App.Current.MainPage.DisplayAlert("FixPro", "Do you want to Deactive the notify?", "Yes", "No").ConfigureAwait(false);
                if (exit)
                {
                    IsBusy = true;
                    string UserToken = await _service.UserToken();
                    UserDialogs.Instance.ShowLoading();
                    var json = await ORep.PutAsync("api/Notifications/PutDeactiveNotify", model, UserToken);
                    UserDialogs.Instance.HideLoading();
                    IsBusy = false;

                    if (json.Active == false)
                    {
                        Messages.Remove(model);
                    }
                }
            }
               
        }

        async void OnSelectedNotificationDetails(NotificationsModel model)
        {
            IsBusy = true;
            if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
            {
                await App.Current.MainPage.Navigation.PushAsync(new NoInternetPage(new NotificationsPage()));
                return;
            }
            else
            {
                UserDialogs.Instance.ShowLoading();
                if (model.ScheduleId != null && model.ScheduleDateId != null && !string.IsNullOrEmpty(model.ScheduleDate))
                {
                    var VM = new SchedulesViewModel(model.ScheduleId.Value, model.ScheduleDateId.Value);
                    //var page = new NewSchedulePage();
                    var page = new ScheduleDetailsPage();
                    page.BindingContext = VM;
                    await App.Current.MainPage.Navigation.PushAsync(page);
                }
                UserDialogs.Instance.HideLoading();
            }

            IsBusy = false;
        }

        async void OnSelectedEmployeesWorkingPage(string startTracking)
        {
            IsBusy = true;

            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new NoInternetPage(new MainPage()));
                    return;
                }
                else
                {
                    UserDialogs.Instance.ShowLoading();

                    var popupView = new EmployeesViewModel(startTracking);
                    var page = new Views.MenuPages.EmployeesWorkingPage();
                    page.BindingContext = popupView;
                    await App.Current.MainPage.Navigation.PushAsync(page);

                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                //throw;
            }

            //UserDialogs.Instance.ShowLoading();

            //var popupView = new EmployeesViewModel(startTracking);
            //var page = new Views.MenuPages.EmployeesWorkingPage();
            //page.BindingContext = popupView;
            //await App.Current.MainPage.Navigation.PushAsync(page);

            //UserDialogs.Instance.HideLoading();

            IsBusy = false;
        }

        async void OnSelectedCustomersPage()
        {
            IsBusy = true;

            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new NoInternetPage(new MainPage()));
                    return;
                }
                else
                {
                    UserDialogs.Instance.ShowLoading();

                    await App.Current.MainPage.Navigation.PushAsync(new Views.CustomerPages.CustomersPage());

                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                //throw;
            }

            //UserDialogs.Instance.ShowLoading();

            //await App.Current.MainPage.Navigation.PushAsync(new Views.CustomerPages.CustomersPage());

            //UserDialogs.Instance.HideLoading();

            IsBusy = false;
        }

        async void OnSelectedCallsPage()
        {
            IsBusy = true;

            try
            {
                UserDialogs.Instance.ShowLoading();

                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new NoInternetPage(new MainPage()));
                }
                else
                {
                    Controls.StaticMembers.CreateOrDetailsCall = 0; // Get Calls Only
                    await App.Current.MainPage.Navigation.PushAsync(new Views.CallPages.CallsPage());
                }

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                //throw;
            }


            //try
            //{
            //    if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
            //    {
            //        await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
            //        //return;
            //    }
            //    else
            //    {
            //        UserDialogs.Instance.ShowLoading();

            //        Controls.StaticMembers.CreateOrDetailsCall = 0; // Get Calls Only
            //        await App.Current.MainPage.Navigation.PushAsync(new Views.CallPages.CallsPage());

            //        UserDialogs.Instance.HideLoading();
            //    }
            //}
            //catch (Exception)
            //{
            //    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
            //    //throw;
            //}

            //UserDialogs.Instance.ShowLoading();

            //Controls.StaticMembers.CreateOrDetailsCall = 0; // Get Calls Only
            //await App.Current.MainPage.Navigation.PushAsync(new Views.CallPages.CallsPage());

            //UserDialogs.Instance.HideLoading();

            IsBusy = false;
        }

        async void OnSelectedSchedulePage()
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new NoInternetPage(new MainPage()));
                    //return;
                }
                else
                {
                    UserDialogs.Instance.ShowLoading();
                    //var ViewModel = new SchedulesViewModel(1);
                    //var page = new Views.SchedulePages.SchedulePage(1);
                    //page.BindingContext = ViewModel;
                    //await App.Current.MainPage.Navigation.PushAsync(page);
                    await App.Current.MainPage.Navigation.PushAsync(new Views.SchedulePages.SchedulePage());

                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                //throw;
            }

            //UserDialogs.Instance.ShowLoading();

            //await App.Current.MainPage.Navigation.PushAsync(new Views.SchedulePages.SchedulePage());

            //UserDialogs.Instance.HideLoading();

            IsBusy = false;
        }

        async void OnSelectedAllEmployeesPage()
        {
            IsBusy = true;

            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new NoInternetPage(new MainPage()));
                    //return;
                }
                else
                {
                    UserDialogs.Instance.ShowLoading();

                    Controls.StaticMembers.EmployeesPages = 2;
                    await App.Current.MainPage.Navigation.PushAsync(new Views.MenuPages.AllEmployeesPage());

                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                //throw;
            }

            //UserDialogs.Instance.ShowLoading();

            //Controls.StaticMembers.EmployeesPages = 2;
            //await App.Current.MainPage.Navigation.PushAsync(new Views.MenuPages.AllEmployeesPage());

            //UserDialogs.Instance.HideLoading();

            IsBusy = false;
        }

        async void OnSelectedTimeSheetPage()
        {
            IsBusy = true;

            try
            {
                UserDialogs.Instance.ShowLoading();

                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.Navigation.PushAsync(new NoInternetPage(new MainPage()));
                }
                else
                {
                    await App.Current.MainPage.Navigation.PushAsync(new TimeSheetPage());
                }

                UserDialogs.Instance.HideLoading();
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                throw;
            }

            //try
            //{
            //    if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
            //    {
            //        await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
            //        //return;
            //    }
            //    else
            //    {
            //        UserDialogs.Instance.ShowLoading();

            //        await App.Current.MainPage.Navigation.PushAsync(new TimeSheetPage());

            //        UserDialogs.Instance.HideLoading();
            //    }
            //}
            //catch (Exception)
            //{
            //    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
            //    throw;
            //}

            //UserDialogs.Instance.ShowLoading();

            //await App.Current.MainPage.Navigation.PushAsync(new TimeSheetPage());

            //UserDialogs.Instance.HideLoading();

            IsBusy = false;
        }

        async void OnSelectedLocationsPage()
        {
            IsBusy = true;
            //UserDialogs.Instance.ShowLoading();
            //await App.Current.MainPage.Navigation.PushAsync(new Views.LocationPage());
            //UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnSelectedReturnSalesPopup()
        {
            IsBusy = true;
            //UserDialogs.Instance.ShowLoading();
            //await PopupNavigation.Instance.PushAsync(new Views.ReturnSalesPopup());
            //UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnSelectedProductsPage()
        {
            IsBusy = true;
            //UserDialogs.Instance.ShowLoading();
            //await App.Current.MainPage.Navigation.PushAsync(new Views.ProductsPage());
            //UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnSelectedAccountPage()
        {
            IsBusy = true;

            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    //return;
                }
                else
                {
                    UserDialogs.Instance.ShowLoading();

                    await App.Current.MainPage.Navigation.PushAsync(new Views.MenuPages.AccountPage());

                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                //throw;
            }

            IsBusy = false;
        }



    }
}
