using Acr.UserDialogs;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using FixPro.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Essentials;

namespace FixPro.ViewModels
{
    public class FilterCallsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        CallModel _OneFilter;
        public CallModel OneFilter
        {
            get
            {
                return _OneFilter;
            }
            set
            {
                _OneFilter = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneFilter"));
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

        EmployeeModel _OneEmployee;
        public EmployeeModel OneEmployee
        {
            get
            {
                return _OneEmployee;
            }
            set
            {
                _OneEmployee = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneEmployee"));
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

        ObservableCollection<EmployeeModel> _LstEmployees;
        public ObservableCollection<EmployeeModel> LstEmployees
        {
            get
            {
                return _LstEmployees;
            }
            set
            {
                _LstEmployees = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEmployees"));
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

        DateTime _StartDate;
        public DateTime StartDate
        {
            get
            {
                return _StartDate;
            }
            set
            {
                _StartDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("StartDate"));
                }
            }
        }

        DateTime _EndDate;
        public DateTime EndDate
        {
            get
            {
                return _EndDate;
            }
            set
            {
                _EndDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("EndDate"));
                }
            }
        }

        string _SchTitle;
        public string SchTitle
        {
            get
            {
                return _SchTitle;
            }
            set
            {
                _SchTitle = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SchTitle"));
                }
            }
        }

        bool _WithDate;
        public bool WithDate
        {
            get
            {
                return _WithDate;
            }
            set
            {
                _WithDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("WithDate"));
                }
            }
        }

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        public delegate void CallDelegte(CallModel Call);
        public event CallDelegte CallClose;

        public ICommand ApplyFilterCalls { get; set; }

        public FilterCallsViewModel()
        {
            Init();
            if(Controls.StaticMembers.FilterCallModel != null)
            {
                OneFilter = Controls.StaticMembers.FilterCallModel;
                if (string.IsNullOrEmpty(OneFilter.StartDate) != true)
                {
                    StartDate = DateTime.Parse(OneFilter.StartDate);
                    WithDate = true;
                }
                if (string.IsNullOrEmpty(OneFilter.EndDate) != true)
                {
                    EndDate = DateTime.Parse(OneFilter.EndDate);
                    WithDate = true;
                }
            }   

            ApplyFilterCalls = new Command<CallModel>(OnApplyFilterCalls);
        }

        async void Init()
        {
            LstReasons = new ObservableCollection<ReasonModel>();
            LstCampaigns = new ObservableCollection<CampaignModel>();
            LstEmployees = new ObservableCollection<EmployeeModel>();
            OneFilter = new CallModel();
            OneReason = new ReasonModel();
            OneCampaign = new CampaignModel();
            OneEmployee = new EmployeeModel();

            StartDate = DateTime.Now;
            EndDate = DateTime.Now;

            WithDate = false;

            UserDialogs.Instance.ShowLoading();
            await GetReasons();
            await GetCampaigns();
            await GetEmployeesInCall();
            UserDialogs.Instance.HideLoading();
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

                    OneReason = LstReasons.Where(x => x.Id == OneFilter.ReasonId).FirstOrDefault();
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

                    OneCampaign = LstCampaigns.Where(x => x.Id == OneFilter.CampaignId).FirstOrDefault();
                }
            }      
        }

        // Get Employees 
        async Task GetEmployeesInCall()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();

                var json = await ORep.GetAsync<ObservableCollection<EmployeeModel>>(string.Format("api/Employee/GetEmpInCall/{0}/{1}/{2}", Helpers.Settings.AccountId, Controls.StartData.EmployeeDataStatic.UserRole, Helpers.Settings.UserId), UserToken);

                if (json != null)
                {
                    LstEmployees = json;

                    OneEmployee = LstEmployees.Where(x => x.Id == OneFilter.CreateUser).FirstOrDefault();
                }
            }       
        }

        async void OnApplyFilterCalls(CallModel model)
        {
            IsBusy = true;

            model.PhoneNum = model.PhoneNum != null ? model.PhoneNum : "";
            model.ReasonId = OneReason?.Id;
            model.CampaignId = OneCampaign?.Id;
            model.ScheduleTitle = SchTitle;
            model.CreateUser = OneEmployee?.Id;

            if (WithDate == true)
            {
                model.StartDate = StartDate.ToString("MM-dd-yyyy");
                model.EndDate = EndDate.ToString("MM-dd-yyyy");
            }

            Controls.StaticMembers.FilterCallModel = model;

            CallClose.Invoke(model);
            await App.Current.MainPage.Navigation.PopAsync();

            IsBusy = false;
        }
    }
}
