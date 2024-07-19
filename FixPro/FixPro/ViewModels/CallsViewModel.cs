using Acr.UserDialogs;
//using GoogleApi.Entities.Translate.Common.Enums;
using Newtonsoft.Json;
using Plugin.Connectivity;
using FixPro.Models;
using FixPro.Views.SchedulePages;
using Rg.Plugins.Popup.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using GoogleApi.Entities.Maps.AerialView.Common.Enums;
using System.Reflection.Emit;
using Syncfusion.SfCalendar.XForms;

namespace FixPro.ViewModels
{
    public class CallsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        CallModel _OneCall;
        public CallModel OneCall
        {
            get
            {
                return _OneCall;
            }
            set
            {
                _OneCall = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneCall"));
                }
            }
        }

        CustomersModel _CustomerDetails;
        public CustomersModel CustomerDetails
        {
            get
            {
                return _CustomerDetails;
            }
            set
            {
                _CustomerDetails = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CustomerDetails"));
                }
            }
        }

        ReasonModel _OneReason;
        public ReasonModel OneReason
        {
            get
            {
                return _OneReason;
            }
            set
            {
                _OneReason = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneReason"));
                }
            }
        }

        CampaignModel _OneCampaign;
        public CampaignModel OneCampaign
        {
            get
            {
                return _OneCampaign;
            }
            set
            {
                _OneCampaign = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneCampaign"));
                }
            }
        }

        ObservableCollection<CallModel> _LstCalls;
        public ObservableCollection<CallModel> LstCalls
        {
            get
            {
                return _LstCalls;
            }
            set
            {
                _LstCalls = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstCalls"));
                }
            }
        }

        ObservableCollection<ReasonModel> _LstReasons;
        public ObservableCollection<ReasonModel> LstReasons
        {
            get
            {
                return _LstReasons;
            }
            set
            {
                _LstReasons = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstReasons"));
                }
            }
        }

        ObservableCollection<CampaignModel> _LstCampaigns;
        public ObservableCollection<CampaignModel> LstCampaigns
        {
            get
            {
                return _LstCampaigns;
            }
            set
            {
                _LstCampaigns = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstCampaigns"));
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

        int _IsShowBtnSch;
        public int IsShowBtnSch
        {
            get
            {
                return _IsShowBtnSch;
            }
            set
            {
                _IsShowBtnSch = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsShowBtnSch"));
                }
            }
        }

        int _TotalCalls;
        public int TotalCalls
        {
            get
            {
                return _TotalCalls;
            }
            set
            {
                _TotalCalls = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalCalls"));
                }
            }
        }

        int _CreateOrDetailsCall;
        public int CreateOrDetailsCall
        {
            get
            {
                return _CreateOrDetailsCall;
            }
            set
            {
                _CreateOrDetailsCall = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CreateOrDetailsCall"));
                }
            }
        }

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        public ICommand CreateNewCall { get; set; }
        public ICommand OpenFilterCalls { get; set; }
        public ICommand SubmitCall { get; set; }
        public ICommand SelectCallDetails { get; set; }
        public ICommand DeleteCall { get; set; }
        public ICommand SelectGoJob { get; set; }
        public ICommand CreateScheduleFromCall { get; set; }
        public ICommand CreateNewCustomer { get; set; }
        public ICommand ResetCalls { get; set; }


        public CallsViewModel()
        {
            Init();
            IsShowBtnSch = 0;
        }

        public CallsViewModel(CustomersModel model)
        {
            Init();
            IsShowBtnSch = 0;
            OneCall.PhoneNum = model.Phone1;
            OneCall.CustomerId = model.Id;
            OneCall.CustomerName = model.FirstName + " " + model.LastName;
        }

        public CallsViewModel(CallModel model)
        {
            Init();
            OneCall = model;
            IsShowBtnSch = 0;
            if (OneCall.Id != 0)
            {
                IsShowBtnSch = 1;//add sch
                if (OneCall.ScheduleId != 0 && OneCall.ScheduleId != null)
                {
                    IsShowBtnSch = 2;//Show sch
                }
            }

            if (OneCall.CustomerId != 0 && OneCall.CustomerId != null)
            {
                GetOneCustomerDetials(OneCall.CustomerId);
            }


            if (OneCall.ScheduleTitle == null)
                OneCall.ScheduleTitle = "";
        }

        //public CallsViewModel(CallModel model, string StartDate, string EndDate)
        //{
        //    Init();
        //    OpenFilterCalls = new Command(OnOpenFilterCalls);
        //}

        async void Init()
        {
            LstCalls = new ObservableCollection<CallModel>();
            LstReasons = new ObservableCollection<ReasonModel>();
            LstCampaigns = new ObservableCollection<CampaignModel>();
            CustomerDetails = new CustomersModel();
            OneCall = new CallModel();
            OneReason = new ReasonModel();
            OneCampaign = new CampaignModel();

            CreateNewCall = new Command(OnCreateNewCall);
            SubmitCall = new Command<CallModel>(OnSubmitCall);
            SelectCallDetails = new Command<CallModel>(OnSelectCallDetails);
            OpenFilterCalls = new Command(OnOpenFilterCalls);
            DeleteCall = new Command<int>(OnDeleteCall);
            SelectGoJob = new Command<CallModel>(OnSelectGoJob);
            CreateScheduleFromCall = new Command<CustomersModel>(OnCreateScheduleFromCall);
            CreateNewCustomer = new Command(OnCreateNewCustomer);
            ResetCalls = new Command(OnResetCalls);


            await GetCalls();

            if (Controls.StaticMembers.CreateOrDetailsCall == 1)
            {
                UserDialogs.Instance.ShowLoading();
                await GetReasons();
                await GetCampaigns();
                UserDialogs.Instance.HideLoading();
            }
        }

        //Get One Customer Detials
        async void GetOneCustomerDetials(int? CustId)
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();

                string UserToken = await _service.UserToken();

                var json = await ORep.GetAsync<CustomersModel>(string.Format("api/Customers/GetOneCustDetails?" + "CustId=" + CustId), UserToken);

                if (json != null)
                {
                    CustomerDetails = json;
                }

                UserDialogs.Instance.HideLoading();
            }
        }

        //Get Reasons
        async Task GetReasons()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await ORep.GetAsync<ObservableCollection<ReasonModel>>(string.Format("api/Calls/GetReasons?" + "AccountId=" + Helpers.Settings.AccountId), UserToken);

                if (json != null)
                {
                    LstReasons = json;
                    OneReason = LstReasons.Where(x => x.Id == OneCall.ReasonId).FirstOrDefault();
                }
            }
        }

        //Get Campaigns
        async Task GetCampaigns()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await ORep.GetAsync<ObservableCollection<CampaignModel>>(string.Format("api/Calls/GetCampaigns?" + "AccountId=" + Helpers.Settings.AccountId), UserToken);

                if (json != null)
                {
                    LstCampaigns = json;
                    OneCampaign = LstCampaigns.Where(x => x.Id == OneCall.CampaignId).FirstOrDefault();
                }
            }
        }


        //Get Calls
        async Task GetCalls()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();

                string UserToken = await _service.UserToken();

                var json = await ORep.GetAsync<ObservableCollection<CallModel>>(string.Format("api/Calls/GetAllCalls?" + "AccountId=" + Helpers.Settings.AccountId), UserToken);

                if (json != null)
                {
                    LstCalls = json;
                    TotalCalls = LstCalls.Count;
                }

                UserDialogs.Instance.HideLoading();
            }

        }

        async void OnSelectCallDetails(CallModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            Controls.StaticMembers.CreateOrDetailsCall = 1; //For Get Reasons and Campaigns
            var ViewModel = new CallsViewModel(model);
            var page = new Views.CallPages.NewCallPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnCreateNewCall()
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            Controls.StaticMembers.CreateOrDetailsCall = 1; //For Get Reasons and Campaigns
            var ViewModel = new CallsViewModel();
            var page = new Views.CallPages.NewCallPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnCreateNewCustomer()
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            Controls.StaticMembers.WayCreateCust = 1;
            var ViewModel = new CustomersViewModel(true);
            var page = new Views.CustomerPages.CreateNewCustomerPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            await PopupNavigation.Instance.PopAsync();
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnOpenFilterCalls()
        {
            IsBusy = true;

            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    return;
                }
                else
                {
                    string UserToken = await _service.UserToken();

                    var pageView = new FilterCallsViewModel();
                    pageView.CallClose += async (call) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        //string json = await Helpers.Utility.CallWebApi(string.Format("api/Calls/GetFilterCalls?" + "StartDate=" + call.StartDate + "&" + "EndDate=" + call.EndDate + "&" + "PhoneNum=" + call.PhoneNum + "&" + "ReasonId=" + call.ReasonId + "&" + "CampaignId=" + call.CampaignId + "&" + "EmployeeId=" + call.CreateUser + "&" + "SchTitle=" + call.ScheduleTitle));

                        var json = await ORep.GetAsync<ObservableCollection<CallModel>>(string.Format("api/Calls/GetFilterCalls?" + "StartDate=" + call.StartDate + "&" + "EndDate=" + call.EndDate + "&" + "PhoneNum=" + call.PhoneNum + "&" + "ReasonId=" + call.ReasonId + "&" + "CampaignId=" + call.CampaignId + "&" + "EmployeeId=" + call.CreateUser + "&" + "SchTitle=" + call.ScheduleTitle), UserToken);

                        if (json != null)
                        {
                            //List<CallModel> Calls = JsonConvert.DeserializeObject<List<CallModel>>(json);

                            //LstCalls = new ObservableCollection<CallModel>(Calls);
                            LstCalls = json;

                            TotalCalls = LstCalls.Count;
                        }
                        UserDialogs.Instance.HideLoading();
                    };

                    var page = new Views.CallPages.FilterCallPage();
                    page.BindingContext = pageView;
                    await App.Current.MainPage.Navigation.PushAsync(page);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        async void OnSubmitCall(CallModel model)
        {
            IsBusy = true;

            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    return;
                }
                else
                {
                    string UserToken = await _service.UserToken();

                    OneCall = model;

                    OneCall.AccountId = int.Parse(Helpers.Settings.AccountId);
                    OneCall.BrancheId = int.Parse(Helpers.Settings.BranchId);
                    OneCall.Active = true;
                    OneCall.CreateUser = int.Parse(Helpers.Settings.UserId);
                    OneCall.ReasonId = OneReason != null ? OneReason.Id : 0;
                    OneCall.CampaignId = OneCampaign != null ? OneCampaign.Id : 0;

                    if (model.CustomerId == 0 || model.CustomerId == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required: Choose Customer.", "Ok");
                    }
                    else if(string.IsNullOrEmpty(model.PhoneNum))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required: Phone.", "Ok");
                    }
                    else if (OneReason == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required: Choose Reason.", "Ok");
                    }
                    else if (OneCampaign == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Choose Campaign.", "Ok");
                    }
                    else if (string.IsNullOrEmpty(model.Notes))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required: Notes.", "Ok");
                    }
                    else
                    {
                        if (OneCall != null)
                        {
                            var json = "";

                            if (model.Id == 0)
                            {
                                UserDialogs.Instance.ShowLoading();
                                json = await ORep.PostDataAsync("api/Calls/PostCall", OneCall, UserToken);
                                UserDialogs.Instance.HideLoading();
                            }
                            else
                            {
                                UserDialogs.Instance.ShowLoading();
                                json = await ORep.PutDataAsync("api/Calls/PutCall", OneCall, UserToken);
                                UserDialogs.Instance.HideLoading();
                            }

                            if (json != "Bad Request" && json != "api not responding")
                            {
                                await App.Current.MainPage.DisplayAlert("FixPro", "Call saved successfully", "Ok");

                                CallModel Call = JsonConvert.DeserializeObject<CallModel>(json);

                                var ViewModel = new CallsViewModel(Call);
                                var page = new Views.CallPages.NewCallPage();
                                page.BindingContext = ViewModel;
                                await App.Current.MainPage.Navigation.PushAsync(page);
                                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "Failed to add or edit this call", "Ok");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        async void OnDeleteCall(int CallId)
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    return;
                }
                else
                {
                    UserDialogs.Instance.ShowLoading();
                    string UserToken = await _service.UserToken();

                    var json = await ORep.DeleteStrItemAsync(string.Format("api/Calls/DeleteCall/{0}", CallId), UserToken);

                    if (json != null && json != "api not responding")
                    {
                        await App.Current.MainPage.DisplayAlert("FixPro", "Call deleted successfully", "Ok");
                        await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Deleting the call failed!", "Ok");
                    }
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        async void OnSelectGoJob(CallModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            var ViewModel = new SchedulesViewModel(model.ScheduleId.Value, model.ScheduleDateId.Value);
            //var page = new NewSchedulePage();
            var page = new ScheduleDetailsPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnCreateScheduleFromCall(CustomersModel model)
        {
            IsBusy = true;

            UserDialogs.Instance.ShowLoading();

            if (model.Id != 0)
            {
                SchedulesViewModel ViewModel;
                if (Controls.StaticMembers.WayAfterChooseCust == 0)
                {
                    model.CallId = OneCall.Id;
                    ViewModel = new SchedulesViewModel(model);
                    var page = new NewSchedulePage();
                    page.BindingContext = ViewModel;
                    await App.Current.MainPage.Navigation.PushAsync(page);
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Customer details aren’t found in this call!", "Ok");
            }

            UserDialogs.Instance.HideLoading();

            IsBusy = false;
        }

        async void OnResetCalls()
        {
            IsBusy = true;

            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    return;
                }
                else
                {
                    UserDialogs.Instance.ShowLoading();

                    await GetCalls();

                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

    }
}
