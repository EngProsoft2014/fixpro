using Acr.UserDialogs;
using Acr.UserDialogs.Infrastructure;
using Newtonsoft.Json;
using Plugin.Geolocator;
using FixPro.Helpers;
using FixPro.Models;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Reactive;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;


namespace FixPro.ViewModels
{
    public class EmployeesViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        ObservableCollection<EmployeeModel> _LstEmployeesInBranch;
        public ObservableCollection<EmployeeModel> LstEmployeesInBranch
        {
            get
            {
                return _LstEmployeesInBranch;
            }
            set
            {
                _LstEmployeesInBranch = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEmployeesInBranch"));
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

        ObservableCollection<EmployeeModel> _LstEmployeesOnePage;
        public ObservableCollection<EmployeeModel> LstEmployeesOnePage
        {
            get
            {
                return _LstEmployeesOnePage;
            }
            set
            {
                _LstEmployeesOnePage = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEmployeesOnePage"));
                }
            }
        }

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

        ObservableCollection<DataMapsModel> _Listmap;
        public ObservableCollection<DataMapsModel> Listmap
        {
            get
            {
                return _Listmap;
            }
            set
            {
                _Listmap = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Listmap"));
                }
            }
        }

        ObservableCollection<DataMapsModel> _LastListmap;
        public ObservableCollection<DataMapsModel> LastListmap
        {
            get
            {
                return _LastListmap;
            }
            set
            {
                _LastListmap = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LastListmap"));
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

        bool _IsRefresh;
        public bool IsRefresh
        {
            get
            {
                return _IsRefresh;
            }
            set
            {
                _IsRefresh = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsRefresh"));
                }
            }
        }

        public int PageNumber { get; set; }

        public int TotalPage { get; set; }

        public int BranchIdVM { get; set; }
        public string BranchNameVM { get; set; }

        public static DataMapsModel MapsModel { get; set; }

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        public ICommand SelectedEmployeeInMap { get; set; }

        DataTable employeesTable;

        DataMapsModel CurrentTrack { get; set; }
        DataSet ds = new DataSet();
        XDocument document = new XDocument();

        public EmployeesViewModel()
        {
            Init();

            Listmap = new ObservableCollection<DataMapsModel>();
            LastListmap = new ObservableCollection<DataMapsModel>();

            //new Timer((Object stateInfo) => { StartGetLocation(); }, new AutoResetEvent(false), 0, 1000);
        }

        //Tracking Constructor
        public EmployeesViewModel(EmployeeModel employee)
        {
            OneEmployee = employee;
            Listmap = new ObservableCollection<DataMapsModel>();
            LastListmap = new ObservableCollection<DataMapsModel>();
            CurrentTrack = new DataMapsModel();
            GetDataEmployee();
            new Timer((Object stateInfo) => { GetDataEmployee(); }, new AutoResetEvent(false), 0, 3000);
        }

        //Employees Working Today Constructor
        public EmployeesViewModel(string startTracking)
        {
            SelectedEmployeeInMap = new Command<EmployeeModel>(OnSelectedEmployeeInMap);

            InitTraking();
        }

        async void Init()
        {
            PageNumber = 1;
            LstEmployeesOnePage = new ObservableCollection<EmployeeModel>();
            LstEmployees = new ObservableCollection<EmployeeModel>();
            LstEmployeesInBranch = new ObservableCollection<EmployeeModel>();
            BranchIdVM = int.Parse(Helpers.Settings.BranchId);
            BranchNameVM = Helpers.Settings.BranchName;

            GetPerrmission();

            //if (Controls.StaticMembers.EmployeesPages == 1)
            //{
            //    await GetEmployeesInBranch();
            //}
            if (Controls.StaticMembers.EmployeesPages == 2)
            {
                await GetEmployees();
            }

        }

        async void InitTraking()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                LstWorkingEmployees = new ObservableCollection<EmployeeModel>();

                string Date = DateTime.Now.ToString("yyyy-MM-dd");

                await Controls.StartData.GetWorkingEmployees(int.Parse(Helpers.Settings.AccountId), Date);

                LstWorkingEmployees = Controls.StartData.LstWorkingEmployeesStatic;
            }     
        }

        //Get Perrmission for User
        public async void GetPerrmission()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                EmployeePermission = new EmployeeModel();
                await Controls.StartData.CheckPermissionEmployee();
                EmployeePermission = Controls.StartData.EmployeeDataStatic;
            }      
        }


        //Get All Employees
        public async Task GetEmployees()
        {         
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();

                string UserToken = await _service.UserToken();

                var json = await ORep.GetAsync<EmployeesInPageModel>(string.Format("api/Employee/GetEmpInPage/{0}/{1}/{2}/{3}/{4}", PageNumber, Helpers.Settings.AccountId, Helpers.Settings.BranchId, Controls.StartData.EmployeeDataStatic.UserRole, Helpers.Settings.UserId), UserToken);

                if (json != null)
                {
                    EmployeesInPageModel Employee = json;
                    TotalPage = Employee.Pages;
                    PageNumber += 1;

                    LstEmployeesOnePage = new ObservableCollection<EmployeeModel>(Employee.EmployeesInPage);

                    if (LstEmployees.Count == 0)
                    {
                        LstEmployees = new ObservableCollection<EmployeeModel>(LstEmployeesOnePage.OrderBy(x => x.UserName).ToList());
                    }
                    else
                    {
                        if (LstEmployees != LstEmployeesOnePage)
                        {
                            LstEmployees = new ObservableCollection<EmployeeModel>(LstEmployees.Concat(LstEmployeesOnePage).OrderBy(x => x.UserName).ToList());
                        }
                    }
                }

                UserDialogs.Instance.HideLoading();
            }
                
        }

        async void OnSelectedEmployeeInMap(EmployeeModel employee)
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
                    var popupView = new EmployeesViewModel(employee);
                    var page = new Views.MenuPages.TrckingMapPage(MapsModel);
                    page.BindingContext = popupView;
                    await App.Current.MainPage.Navigation.PushAsync(page);
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        private async void GetDataEmployee()
        {
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    return;
                }
                else
                {
                    //URi for file data
                    string uri = Helpers.Utility.PathServerMapTracking + Helpers.Settings.AccountName + "/" + OneEmployee.Id + ".xml";

                    document = XDocument.Load(uri);

                    ds.Clear();
                    ds.ReadXml(new XmlTextReader(new StringReader(document.ToString())));

                    employeesTable = ds.Tables[0];

                    CurrentTrack = (from DataRow dr in employeesTable.Rows
                                    select new DataMapsModel()
                                    {
                                        Id = int.Parse(dr["Tracking_id"].ToString()),
                                        EmployeeId = int.Parse(dr["EmployeeId"].ToString()),
                                        Lat = dr["lat"].ToString(),
                                        Long = dr["log"].ToString(),
                                        Time = dr["time"].ToString(),
                                        CreateDate = dr["date"].ToString(),
                                        MPosition = new Position(double.Parse(dr["lat"].ToString()), double.Parse(dr["log"].ToString())),
                                    }).FirstOrDefault();

                    LastListmap.Clear();
                    LastListmap.Add(CurrentTrack);
                    MapsModel = CurrentTrack;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }
    }
}
