using Acr.UserDialogs;
using GoogleApi.Entities.Translate.Common.Enums;
using Newtonsoft.Json;
using FixPro.Models;
using FixPro.Views;
using FixPro.Views.SchedulePages;
using Rg.Plugins.Popup.Services;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Microsoft.IdentityModel.Tokens;

namespace FixPro.ViewModels
{
    public class CustomersViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        ObservableCollection<CustomersModel> _LstCustomers;
        public ObservableCollection<CustomersModel> LstCustomers
        {
            get
            {
                return _LstCustomers;
            }
            set
            {
                _LstCustomers = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstCustomers"));
                }
            }
        }

        ObservableCollection<SchedulesModel> _LstSchedules;
        public ObservableCollection<SchedulesModel> LstSchedules
        {
            get
            {
                return _LstSchedules;
            }
            set
            {
                _LstSchedules = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstSchedules"));
                }
            }
        }

        ObservableCollection<InvoiceModel> _LstInvoices;
        public ObservableCollection<InvoiceModel> LstInvoices
        {
            get
            {
                return _LstInvoices;
            }
            set
            {
                _LstInvoices = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstInvoices"));
                }
            }
        }

        ObservableCollection<EstimateModel> _LstEstimates;
        public ObservableCollection<EstimateModel> LstEstimates
        {
            get
            {
                return _LstEstimates;
            }
            set
            {
                _LstEstimates = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEstimates"));
                }
            }
        }

        ObservableCollection<InvoiceItemServicesModel> _LstItemsInvoice;
        public ObservableCollection<InvoiceItemServicesModel> LstItemsInvoice
        {
            get
            {
                return _LstItemsInvoice;
            }
            set
            {
                _LstItemsInvoice = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstItemsInvoice"));
                }
            }
        }

        ObservableCollection<EstimateItemServicesModel> _LstItemsEstimate;
        public ObservableCollection<EstimateItemServicesModel> LstItemsEstimate
        {
            get
            {
                return _LstItemsEstimate;
            }
            set
            {
                _LstItemsEstimate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstItemsEstimate"));
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

        ObservableCollection<SchaduleDateModel> _LstEstimateDates;
        public ObservableCollection<SchaduleDateModel> LstEstimateDates
        {
            get
            {
                return _LstEstimateDates;
            }
            set
            {
                _LstEstimateDates = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEstimateDates"));
                }
            }
        }

        ObservableCollection<SchaduleDateModel> _LstInvoiceDates;
        public ObservableCollection<SchaduleDateModel> LstInvoiceDates
        {
            get
            {
                return _LstInvoiceDates;
            }
            set
            {
                _LstInvoiceDates = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstInvoiceDates"));
                }
            }
        }

        ObservableCollection<SchaduleDateModel> _LstEstimateSchaduleDates;
        public ObservableCollection<SchaduleDateModel> LstEstimateSchaduleDates
        {
            get
            {
                return _LstEstimateSchaduleDates;
            }
            set
            {
                _LstEstimateSchaduleDates = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEstimateSchaduleDates"));
                }
            }
        }

        ObservableCollection<SchaduleDateModel> _LstEstimateSchaduleDatesActual;
        public ObservableCollection<SchaduleDateModel> LstEstimateSchaduleDatesActual
        {
            get
            {
                return _LstEstimateSchaduleDatesActual;
            }
            set
            {
                _LstEstimateSchaduleDatesActual = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEstimateSchaduleDatesActual"));
                }
            }
        }

        ObservableCollection<SchaduleDateModel> _LstInvoiceSchaduleDates;
        public ObservableCollection<SchaduleDateModel> LstInvoiceSchaduleDates
        {
            get
            {
                return _LstInvoiceSchaduleDates;
            }
            set
            {
                _LstInvoiceSchaduleDates = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstInvoiceSchaduleDates"));
                }
            }
        }

        ObservableCollection<SchaduleDateModel> _LstInvoiceSchaduleDatesActual;
        public ObservableCollection<SchaduleDateModel> LstInvoiceSchaduleDatesActual
        {
            get
            {
                return _LstInvoiceSchaduleDatesActual;
            }
            set
            {
                _LstInvoiceSchaduleDatesActual = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstInvoiceSchaduleDatesActual"));
                }
            }
        }

        ObservableCollection<CustomersCustomFieldModel> _LstCustomField;

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

        SchedulesModel _ScheduleDetails;
        public SchedulesModel ScheduleDetails
        {
            get
            {
                return _ScheduleDetails;
            }
            set
            {
                _ScheduleDetails = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ScheduleDetails"));
                }
            }
        }

        InvoiceModel _OneInvoice;
        public InvoiceModel OneInvoice
        {
            get
            {
                return _OneInvoice;
            }
            set
            {
                _OneInvoice = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneInvoice"));
                }
            }
        }

        EstimateModel _OneEstimate;
        public EstimateModel OneEstimate
        {
            get
            {
                return _OneEstimate;
            }
            set
            {
                _OneEstimate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneEstimate"));
                }
            }
        }

        CustomerfeaturesModel _CustomerFeatures;
        public CustomerfeaturesModel CustomerFeatures
        {
            get
            {
                return _CustomerFeatures;
            }
            set
            {
                _CustomerFeatures = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CustomerFeatures"));
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

        int _LstHeight;
        public int LstHeight
        {
            get
            {
                return _LstHeight;
            }
            set
            {
                _LstHeight = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstHeight"));
                }
            }
        }

        string _BranchName;
        public string BranchName
        {
            get
            {
                return _BranchName;
            }
            set
            {
                _BranchName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("BranchName"));
                }
            }
        }

        string _PhoneView;
        public string PhoneView
        {
            get
            {
                return _PhoneView;
            }
            set
            {
                _PhoneView = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PhoneView"));
                }
            }
        }

        decimal? _Discount;
        public decimal? Discount
        {
            get
            {
                return _Discount;
            }
            set
            {
                _Discount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Discount"));
                }
            }
        }

        decimal? _SubTotal;
        public decimal? SubTotal
        {
            get
            {
                return _SubTotal;
            }
            set
            {
                _SubTotal = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SubTotal"));
                }
            }
        }

        decimal? _Net;
        public decimal? Net
        {
            get
            {
                return _Net;
            }
            set
            {
                _Net = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Net"));
                }
            }
        }

        decimal? _Paid;
        public decimal? Paid
        {
            get
            {
                return _Paid;
            }
            set
            {
                _Paid = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Paid"));
                }
            }
        }

        decimal? _TotalDue;
        public decimal? TotalDue
        {
            get
            {
                return _TotalDue;
            }
            set
            {
                _TotalDue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalDue"));
                }
            }
        }

        bool _Pending;
        public bool Pending
        {
            get
            {
                return _Pending;
            }
            set
            {
                _Pending = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Pending"));
                }
            }
        }

        bool _Accept;
        public bool Accept
        {
            get
            {
                return _Accept;
            }
            set
            {
                _Accept = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Accept"));
                }
            }
        }

        bool _Declind;
        public bool Declind
        {
            get
            {
                return _Declind;
            }
            set
            {
                _Declind = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Declind"));
                }
            }
        }

        string _Address;
        public string Address
        {
            get
            {
                return _Address;
            }
            set
            {
                _Address = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Address"));
                }
            }
        }

        string _HouseValue;
        public string HouseValue
        {
            get
            {
                return _HouseValue;
            }
            set
            {
                _HouseValue = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("HouseValue"));
                }
            }
        }

        string _YearBuilt;
        public string YearBuilt
        {
            get
            {
                return _YearBuilt;
            }
            set
            {
                _YearBuilt = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("YearBuilt"));
                }
            }
        }

        string _SquareFootage;
        public string SquareFootage
        {
            get
            {
                return _SquareFootage;
            }
            set
            {
                _SquareFootage = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SquareFootage"));
                }
            }
        }

        bool _ShowArrowsBar;
        public bool ShowArrowsBar
        {
            get
            {
                return _ShowArrowsBar;
            }
            set
            {
                _ShowArrowsBar = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowArrowsBar"));
                }
            }
        }

        bool _ShowArrowsBarFeatures;
        public bool ShowArrowsBarFeatures
        {
            get
            {
                return _ShowArrowsBarFeatures;
            }
            set
            {
                _ShowArrowsBarFeatures = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowArrowsBarFeatures"));
                }
            }
        }

        string _City;
        public string City
        {
            get
            {
                return _City;
            }
            set
            {
                _City = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("City"));
                }
            }
        }

        string _State;
        public string State
        {
            get
            {
                return _State;
            }
            set
            {
                _State = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("State"));
                }
            }
        }

        string _ZipCode;
        public string ZipCode
        {
            get
            {
                return _ZipCode;
            }
            set
            {
                _ZipCode = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ZipCode"));
                }
            }
        }

        bool _ShowScheduleName;
        public bool ShowScheduleName
        {
            get
            {
                return _ShowScheduleName;
            }
            set
            {
                _ShowScheduleName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowScheduleName"));
                }
            }
        }

        bool _ShowEstimateConvertToInvoice;
        public bool ShowEstimateConvertToInvoice
        {
            get
            {
                return _ShowEstimateConvertToInvoice;
            }
            set
            {
                _ShowEstimateConvertToInvoice = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowEstimateConvertToInvoice"));
                }
            }
        }

        public int BranchIdVM;

        bool _WithContract;
        public bool WithContract
        {
            get
            {
                return _WithContract;
            }
            set
            {
                _WithContract = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("WithContract"));
                }
            }
        }

        bool _AmountOrPersent;
        public bool AmountOrPersent
        {
            get
            {
                return _AmountOrPersent;
            }
            set
            {
                _AmountOrPersent = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AmountOrPersent"));
                }
            }
        }

        bool _ShowDropdownDatesEstimate;
        public bool ShowDropdownDatesEstimate
        {
            get
            {
                return _ShowDropdownDatesEstimate;
            }
            set
            {
                _ShowDropdownDatesEstimate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowDropdownDatesEstimate"));
                }
            }
        }

        bool _ShowDropdownDatesInvoice;
        public bool ShowDropdownDatesInvoice
        {
            get
            {
                return _ShowDropdownDatesInvoice;
            }
            set
            {
                _ShowDropdownDatesInvoice = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowDropdownDatesInvoice"));
                }
            }
        }

        string _StrEstimateDates;
        public string StrEstimateDates
        {
            get
            {
                return _StrEstimateDates;
            }
            set
            {
                _StrEstimateDates = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("StrEstimateDates"));
                }
            }
        }

        string _StrInvoiceDates;
        public string StrInvoiceDates
        {
            get
            {
                return _StrInvoiceDates;
            }
            set
            {
                _StrInvoiceDates = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("StrInvoiceDates"));
                }
            }
        }

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        public ICommand SelecteCustomerDetails { get; set; }
        public ICommand CreateNewSchedule { get; set; }
        public ICommand EditDiscountForCustomer { get; set; }
        public ICommand EditDiscountForCustomerEstimate { get; set; }
        public ICommand SelecteInvoiceDetails { get; set; }
        public ICommand SelecteNewItems { get; set; }
        public ICommand RemoveItem { get; set; }
        public ICommand SelecteNewItemsEstimate { get; set; }
        public ICommand RemoveItemEstimate { get; set; }
        public ICommand SubmitInvoice { get; set; }
        public ICommand SelecteScheduleDetails { get; set; }
        public ICommand SelecteEstimateDetails { get; set; }
        public ICommand ConvertToInvoice { get; set; }
        public ICommand SubmitEstimate { get; set; }
        public ICommand CreateNewCustomer { get; set; }
        public ICommand ChooseCustomerCategory { get; set; }
        public ICommand ChooseCustomerMemberShip { get; set; }
        public ICommand ChooseCustomerTax { get; set; }
        public ICommand ChooseCustomerCampaign { get; set; }
        public ICommand InsertCustomer { get; set; }
        public ICommand SelecteAddress { get; set; }
        public ICommand SelectCustToCreateEstimatePage { get; set; }
        public ICommand SelectCustToCreateInvoicePage { get; set; }
        public ICommand CreditPayment { get; set; }
        public ICommand CashPayment { get; set; }
        public ICommand DeleteInvoice { get; set; }
        public ICommand GoInvoice { get; set; }
        public ICommand OpenEstimateScheduleDates { get; set; }
        public ICommand OpenInvoiceScheduleDates { get; set; }
        public ICommand RemoveInvoiceDate { get; set; }
        public ICommand RemoveEstimateDate { get; set; }

        public CustomersViewModel()
        {
            BranchName = Helpers.Settings.BranchName;
            BranchIdVM = int.Parse(Helpers.Settings.BranchId);
            LstCustomers = new ObservableCollection<CustomersModel>();
            SelecteCustomerDetails = new Command<CustomersModel>(OnSelecteCustomerDetails);
            CreateNewSchedule = new Command<CustomersModel>(OnCreateNewSchedule);
            CreateNewCustomer = new Command(OnCreateNewCustomer);

            GetAllCustomers();
        }

        //Feature Part To Create New Customer
        public CustomersViewModel(bool FeaturePart)
        {
            GetPerrmission();
            
            BranchIdVM = int.Parse(Helpers.Settings.BranchId);
            CustomerDetails = new CustomersModel();
            CustomerFeatures = new CustomerfeaturesModel();
            LstCampaigns = new ObservableCollection<CampaignModel>();

            GetCampaigns();
            GetCustomerFeatures(int.Parse(Helpers.Settings.AccountId));
            //LstSource.Add(new CustomerSourceModel() { Id = 1, Name = "UnKnown", });
            //LstSource.Add(new CustomerSourceModel() { Id = 2, Name = "Door Hanger", });
            //LstSource.Add(new CustomerSourceModel() { Id = 3, Name = "Facebook", });
            //LstSource.Add(new CustomerSourceModel() { Id = 4, Name = "Twitter", });
            //LstSource.Add(new CustomerSourceModel() { Id = 5, Name = "Youtube", });
            //LstSource.Add(new CustomerSourceModel() { Id = 6, Name = "Yelp", });
            //LstSource.Add(new CustomerSourceModel() { Id = 7, Name = "Google", });
            //LstSource.Add(new CustomerSourceModel() { Id = 8, Name = "Other", });

            ChooseCustomerCategory = new Command<CustomersCategoryModel>(OnChooseCustomerCategory);
            ChooseCustomerMemberShip = new Command<MemberModel>(OnChooseCustomerMemberShip);
            ChooseCustomerTax = new Command<TaxModel>(OnChooseCustomerTax);
            ChooseCustomerCampaign = new Command<CampaignModel>(OnChooseCustomerCampaign);
            InsertCustomer = new Command<CustomersModel>(OnInsertCustomer);
            SelecteAddress = new Command(OnSelecteAddress);
       
        }

        //List Customers
        public CustomersViewModel(CustomersModel model)
        {
            GetPerrmission();

            LstInvoices = new ObservableCollection<InvoiceModel>();
            LstSchedules = new ObservableCollection<SchedulesModel>();
            CustomerDetails = new CustomersModel();
            CustomerDetails.LstSchedules = new List<SchedulesModel>();
            CustomerDetails.LstInvoices = new List<InvoiceModel>();
            LstEstimates = new ObservableCollection<EstimateModel>();
            CustomerDetails.LstEstimates = new List<EstimateModel>();

            CustomerDetails = model;

            //if (CustomerDetails.MemberDTO != null)
            //    Discount = CustomerDetails.MemberDTO.MemberValue;

            if (CustomerDetails.MemeberType == true)
            {
                if (CustomerDetails.MemberDTO != null)
                {
                    Discount = CustomerDetails.MemberDTO.MemberValue;
                }
            }
            else
            {
                Discount = CustomerDetails.Discount;
            }

            if (Discount == null)
            {
                Discount = 0;
            }

            GetListsForCustomer(model.Id);

            SelecteInvoiceDetails = new Command<InvoiceModel>(OnSelecteInvoiceDetails);
            SelecteScheduleDetails = new Command<SchedulesModel>(OnSelecteScheduleDetails);
            SelecteEstimateDetails = new Command<EstimateModel>(OnSelecteEstimateDetails);
            SelectCustToCreateEstimatePage = new Command(OnSelectCustToCreateEstimatePage);
            SelectCustToCreateInvoicePage = new Command(OnSelectCustToCreateInvoicePage);
            ConvertToInvoice = new Command<EstimateModel>(OnConvertToInvoice);
            GoInvoice = new Command<int>(OnGoInvoice);
        }

        //From Invoices Tab
        public CustomersViewModel(InvoiceModel model, CustomersModel Cust)
        {
            GetPerrmission();

            if (model.ScheduleId != null)
                ShowScheduleName = true;

            LstItemsInvoice = new ObservableCollection<InvoiceItemServicesModel>();
            LstInvoiceSchaduleDates = new ObservableCollection<SchaduleDateModel>();
            LstInvoiceSchaduleDatesActual = new ObservableCollection<SchaduleDateModel>();
            LstInvoiceDates = new ObservableCollection<SchaduleDateModel>();
            OneInvoice = new InvoiceModel();

            if (model.LstScdDate != null)
            {
                LstInvoiceSchaduleDatesActual = new ObservableCollection<SchaduleDateModel>(model.LstScdDate);
            }
            
            GetOneInvoiceDetails(model.Id, null);

            //OneInvoice = model;
            CustomerDetails = Cust;
            BranchName = Helpers.Settings.BranchName;

            //if (CustomerDetails.MemberDTO != null)
            //Discount = CustomerDetails.MemberDTO.MemberValue = CustomerDetails.MemberDTO.MemberValue == null ? 0 : CustomerDetails.MemberDTO.MemberValue;

            EditDiscountForCustomer = new Command<CustomersModel>(OnEditDiscountForCustomer);
            SelecteNewItems = new Command<InvoiceModel>(OnSelecteNewItems);
            RemoveItem = new Command<InvoiceItemServicesModel>(OnRemoveItem);
            SelecteNewItemsEstimate = new Command<EstimateModel>(OnSelecteNewItemsEstimate);
            RemoveItemEstimate = new Command<EstimateItemServicesModel>(OnRemoveItemEstimate);
            SubmitInvoice = new Command<InvoiceModel>(OnSubmitInvoice);
            CreditPayment = new Command<InvoiceModel>(OnCreditPayment);
            CashPayment = new Command<InvoiceModel>(OnCashPayment);
            DeleteInvoice = new Command<int>(OnDeleteInvoice);
            OpenInvoiceScheduleDates = new Command(OnOpenInvoiceScheduleDates);
            RemoveInvoiceDate = new Command<SchaduleDateModel>(OnRemoveInvoiceDate);
        }

        //From Estimate Tab
        public CustomersViewModel(EstimateModel model, CustomersModel Cust)
        {
            GetPerrmission();

            if (model.ScheduleId != null)
                ShowScheduleName = true;

            LstItemsEstimate = new ObservableCollection<EstimateItemServicesModel>();
            LstEstimateSchaduleDates = new ObservableCollection<SchaduleDateModel>();
            LstEstimateSchaduleDatesActual = new ObservableCollection<SchaduleDateModel>();
            LstEstimateDates = new ObservableCollection<SchaduleDateModel>();
            OneEstimate = new EstimateModel();

            if(model.LstScdDate != null)
            {
                LstEstimateSchaduleDatesActual = new ObservableCollection<SchaduleDateModel>(model.LstScdDate);
            }
                
            GetOneInvoiceDetails(null, model.Id);

            //OneInvoice = model;
            CustomerDetails = Cust;
            BranchName = Helpers.Settings.BranchName;

            //if (CustomerDetails.MemberDTO != null)
            //    Discount = CustomerDetails.MemberDTO.MemberValue = CustomerDetails.MemberDTO.MemberValue == null ? 0 : CustomerDetails.MemberDTO.MemberValue;

            //AmountOrPersent = OneEstimate.DiscountAmountOrPercent == "%" ? false : true ;
            //Discount = OneEstimate.Discount; 

            EditDiscountForCustomerEstimate = new Command<CustomersModel>(OnEditDiscountForCustomerEstimate);
            SelecteNewItems = new Command<InvoiceModel>(OnSelecteNewItems);
            RemoveItem = new Command<InvoiceItemServicesModel>(OnRemoveItem);
            SubmitEstimate = new Command<EstimateModel>(OnSubmitEstimate);
            SelecteNewItemsEstimate = new Command<EstimateModel>(OnSelecteNewItemsEstimate);
            RemoveItemEstimate = new Command<EstimateItemServicesModel>(OnRemoveItemEstimate);
            //ConvertToInvoice = new Command<EstimateModel>(OnConvertToInvoice);
            GoInvoice = new Command<int>(OnGoInvoice);
            OpenEstimateScheduleDates = new Command(OnOpenEstimateScheduleDates);
            RemoveEstimateDate = new Command<SchaduleDateModel>(OnRemoveEstimateDate);
            
        }

        //From Invoices Tab
        public CustomersViewModel(SchedulesModel model)
        {
            GetPerrmission();

            ScheduleDetails = new SchedulesModel();

            ScheduleDetails = model;

            BranchName = Helpers.Settings.BranchName;
        }

        //Get Perrmission for User
        public async void GetPerrmission()
        {
            EmployeePermission = new EmployeeModel();
            await Controls.StartData.CheckPermissionEmployee();
            EmployeePermission = Controls.StartData.EmployeeDataStatic;
        }


        async Task GetScheduleDates(int ScheduleId, int Type)
        {
            UserDialogs.Instance.ShowLoading();
            string UserToken = await _service.UserToken();
            var json = await ORep.GetAsync<ObservableCollection<SchaduleDateModel>>(string.Format("api/Schedules/GetScheduleDates?" + "ScheduleId=" + ScheduleId + "&" + "Type=" + Type), UserToken);

            if (json != null)
            {
                LstEstimateSchaduleDates = json;
                LstInvoiceSchaduleDates = json;
            }

            UserDialogs.Instance.HideLoading();
        }

        //Get Campaigns
        async void GetCampaigns()
        {
            string UserToken = await _service.UserToken();

            var json = await ORep.GetAsync<ObservableCollection<CampaignModel>>(string.Format("api/Calls/GetCampaigns?" + "AccountId=" + Helpers.Settings.AccountId), UserToken);

            if (json != null)
            {
                LstCampaigns = json;
            }
        }


        //Get One Estimate Or Invoice Details
        async Task GetOneInvoiceDetails(int? InvoiceId, int? EstimateId)
        {
            UserDialogs.Instance.ShowLoading();

            string UserToken = await _service.UserToken();

            var Customer = await ORep.GetAsync<ObjectOfCustomerModel>(string.Format("api/Customers/GetObjectOfCustomer?" + "InvoiceId=" + InvoiceId + "&" + "EstimateId=" + EstimateId), UserToken);

            if (Customer != null)
            {
                if (Customer.ObjInvoice != null)
                {
                    OneInvoice = Customer.ObjInvoice;

                    AmountOrPersent = OneInvoice.DiscountAmountOrPercent == "%" ? false : true;
                    Discount = OneInvoice.Discount;

                    if (OneInvoice.ContractId != null)
                    {
                        WithContract = true;
                    }

                    if (Customer.ObjInvoice.ScheduleId != null && Customer.ObjInvoice.ScheduleId != 0)
                    {
                        ShowDropdownDatesInvoice = true;

                        await GetScheduleDates(Customer.ObjInvoice.ScheduleId.Value, 1); // All Schedule Dates

                        LstInvoiceSchaduleDatesActual = new ObservableCollection<SchaduleDateModel>(Customer.ObjInvoice.LstScdDate);

                        string str = "";
                        foreach (var Date in Customer.ObjInvoice.LstScdDate)
                        {
                            str += (" , " + Date.Date);
                            LstInvoiceDates.Add(new SchaduleDateModel
                            {
                                Id = Date.Id,
                                Date = Date.Date,
                            });
                        }

                        if (!string.IsNullOrEmpty(StrInvoiceDates))
                        {
                            StrInvoiceDates = string.Empty;
                            StrInvoiceDates += str;
                            StrInvoiceDates = str.Remove(0, 2);
                        }
                        else
                        {
                            StrInvoiceDates = str.Remove(0, 2);
                        }

                    }

                    if (OneInvoice.LstInvoiceItemServices != null)
                    {
                        if (OneInvoice.LstInvoiceItemServices.Count > 4)
                        {
                            LstHeight = 1;
                        }

                        TotalInvoice(OneInvoice);

                        LstItemsInvoice = new ObservableCollection<InvoiceItemServicesModel>(OneInvoice.LstInvoiceItemServices);
                    }
                    else
                    {
                        SubTotal = 0;
                        Net = 0;
                        Paid = 0;
                        TotalDue = 0;
                    }
                }

                if (Customer.ObjEstimate != null)
                {
                    //if (Customer.ObjEstimate.InvoiceId != 0 && Customer.ObjEstimate.InvoiceId != null || Customer.ObjEstimate.Status != 1)
                    if ((Customer.ObjEstimate.InvoiceId != 0 && Customer.ObjEstimate.InvoiceId != null) || (Customer.ObjEstimate.Status != 1 && Customer.ObjEstimate.Status != 0))
                    {
                        Customer.ObjEstimate.NotShowConvert = true;//NotShowConvert
                        if (Customer.ObjEstimate.InvoiceId > 0)
                        {
                            Customer.ObjEstimate.GoToInvoice = true;
                        }
                    }

                    if(Customer.ObjEstimate.ScheduleId != null &&  Customer.ObjEstimate.ScheduleId != 0)
                    {
                        ShowDropdownDatesEstimate = true;

                        await GetScheduleDates(Customer.ObjEstimate.ScheduleId.Value, 1); // All Schedule Dates

                        LstEstimateSchaduleDatesActual = new ObservableCollection<SchaduleDateModel>(Customer.ObjEstimate.LstScdDate);

                        string str = "";
                        foreach (var Date in Customer.ObjEstimate.LstScdDate)
                        {
                            str += (" , " + Date.Date);
                            LstEstimateDates.Add(new SchaduleDateModel
                            {
                                Id = Date.Id,
                                Date = Date.Date,
                            });
                        }

                        if (!string.IsNullOrEmpty(StrEstimateDates))
                        {
                            StrEstimateDates = string.Empty;
                            StrEstimateDates += str;
                            StrEstimateDates = str.Remove(0, 2);
                        }
                        else
                        {
                            StrEstimateDates = str.Remove(0, 2);
                        }
                       
                    }

                    OneEstimate = Customer.ObjEstimate;

                    AmountOrPersent = OneEstimate.DiscountAmountOrPercent == "%" ? false : true;
                    Discount = OneEstimate.Discount;

                    Pending = OneEstimate.Status == 0 ? true : false;
                    Accept = OneEstimate.Status == 1 ? true : false;
                    Declind = OneEstimate.Status == 2 ? true : false;

                    if (OneEstimate.LstEstimateItemServices != null)
                    {
                        if (OneEstimate.LstEstimateItemServices.Count > 4)
                        {
                            LstHeight = 1;
                        }

                        TotalEstimate(OneEstimate);

                        LstItemsEstimate = new ObservableCollection<EstimateItemServicesModel>(OneEstimate.LstEstimateItemServices);
                    }
                    else
                    {
                        SubTotal = 0;
                        Net = 0;
                        Paid = 0;
                        TotalDue = 0;
                    }
                }
            }

            UserDialogs.Instance.HideLoading();
        }

        //Get all Customers in Branch
        async void GetAllCustomers()
        {
            UserDialogs.Instance.ShowLoading();

            string UserToken = await _service.UserToken();

            var json = await ORep.GetAsync<ObservableCollection<CustomersModel>>(string.Format("api/Customers/GetAllCustInBranch?" + "AccountId=" + Helpers.Settings.AccountId),UserToken);

            if (json != null)
            {
                LstCustomers = json;
            }

            UserDialogs.Instance.HideLoading();
        }


        //Get Lists For Customer
        public async void GetListsForCustomer(int? CustomerId)
        {
            UserDialogs.Instance.ShowLoading();

            string UserToken = await _service.UserToken();

            var Customer = await ORep.GetAsync<CustomersModel>(string.Format("api/Customers/GetListsOfCustomer?" + "CustomerId=" + CustomerId), UserToken);

            if (Customer != null)
            {
                CustomerDetails = Customer;
                Discount = CustomerDetails.Discount == null ? 0 : CustomerDetails.Discount.Value;
                ShowArrowsBar = CustomerDetails.LstCustomersCustomField.Count > 4 ? true : false;


                foreach (EstimateModel ets in CustomerDetails.LstEstimates)
                {
                    if ((ets.InvoiceId != 0 && ets.InvoiceId != null) || ets.Status != 1 || EmployeePermission.UserRole == 1 || EmployeePermission.ActiveEstimate == false || EmployeePermission.ActiveEditPrice == false)
                    {
                        ets.NotShowConvert = true;//NotShowConvert
                        if (ets.InvoiceId > 0)
                        {
                            ets.GoToInvoice = true;
                        }
                    }
                    //CustomerDetails.LstEstimates.Add(ets);
                }

                LstInvoices = new ObservableCollection<InvoiceModel>(CustomerDetails.LstInvoices);
                LstSchedules = new ObservableCollection<SchedulesModel>(CustomerDetails.LstSchedules);
                LstEstimates = new ObservableCollection<EstimateModel>(CustomerDetails.LstEstimates);
            }

            UserDialogs.Instance.HideLoading();
        }

        // New Customer
        async void GetCustomerFeatures(int? AccountId)
        {
            UserDialogs.Instance.ShowLoading();

            string UserToken = await _service.UserToken();

            var Features = await ORep.GetAsync<CustomerfeaturesModel>(string.Format("api/Customers/GetCustomerFeatures?" + "AccountId=" + AccountId), UserToken);

            if (Features != null)
            {
                CustomerFeatures = Features;

                ShowArrowsBarFeatures = CustomerFeatures.LstCustomersCustomField.Count > 4 ? true : false;
            }

            UserDialogs.Instance.HideLoading();
        }

        public void TotalInvoice(InvoiceModel model)
        {
            decimal? SumCost = model.LstInvoiceItemServices.Where(x => x.Price > 0 && (x.SkipOfTotal == false || x.SkipOfTotal == null)).Sum(s => s.Price * s.Quantity);
            decimal? DiscountVal = 0;
            if (model.DiscountAmountOrPercent == "%")
            {
                DiscountVal = SumCost * Discount / 100;
            }
            else
            {
                DiscountVal = Discount;
            }
            SubTotal = Math.Round(SumCost.Value, 2, MidpointRounding.ToEven);
            Paid = model.Status == 0 ? 0 : model.Paid;
            if (model.Taxval != null)
            {
                Net = model.DiscountAmountOrPercent == "%" ? Math.Round(((SumCost - DiscountVal) + model?.Taxval).Value, 2, MidpointRounding.ToEven) : Math.Round(((SumCost - DiscountVal) + model?.Taxval).Value, 2, MidpointRounding.ToEven);
                TotalDue = model.DiscountAmountOrPercent == "%" ? Math.Round(((SumCost - DiscountVal) + model.Taxval - Paid).Value, 2, MidpointRounding.ToEven) : Math.Round(((SumCost - DiscountVal) + model.Taxval - Paid).Value, 2, MidpointRounding.ToEven);
            }
            else
            {
                decimal? TaxValue = model.DiscountAmountOrPercent == "%" ? ((SumCost - DiscountVal) * model.Tax / 100) : ((SumCost - DiscountVal) * model.Tax / 100);
                Net = model.DiscountAmountOrPercent == "%" ? Math.Round(((SumCost - DiscountVal) + TaxValue).Value, 2, MidpointRounding.ToEven) : Math.Round(((SumCost - DiscountVal) + TaxValue).Value, 2, MidpointRounding.ToEven);
                TotalDue = model.DiscountAmountOrPercent == "%" ? Math.Round(((SumCost - DiscountVal) + TaxValue - Paid).Value, 2, MidpointRounding.ToEven) : Math.Round(((SumCost - DiscountVal) + TaxValue - Paid).Value, 2, MidpointRounding.ToEven);
            }
        }

        public void TotalEstimate(EstimateModel model)
        {
            decimal? SumCost = model.LstEstimateItemServices.Where(x => x.Price > 0).Sum(s => s.Price * s.Quantity);
            decimal? DiscountVal = 0;
            if (model.DiscountAmountOrPercent == "%")
            {
                DiscountVal = SumCost * Discount / 100;
            }
            else
            {
                DiscountVal = Discount;
            }
            SubTotal = Math.Round(SumCost.Value, 2, MidpointRounding.ToEven);
            Paid = 0;

            if (model.Taxval != null)
            {
                Net = model.DiscountAmountOrPercent == "%" ? Math.Round(((SumCost - DiscountVal) + model?.Taxval).Value, 2, MidpointRounding.ToEven) : Math.Round(((SumCost - DiscountVal) + model?.Taxval).Value, 2, MidpointRounding.ToEven);
                TotalDue = model.DiscountAmountOrPercent == "%" ? Math.Round(((SumCost - DiscountVal) + model.Taxval - Paid).Value, 2, MidpointRounding.ToEven) : Math.Round(((SumCost - DiscountVal) + model.Taxval - Paid).Value, 2, MidpointRounding.ToEven);
            }
            else
            {
                decimal? TaxValue = model.DiscountAmountOrPercent == "%" ? ((SumCost - DiscountVal) * model.Tax / 100) : ((SumCost - DiscountVal) * model.Tax / 100);
                Net = model.DiscountAmountOrPercent == "%" ? Math.Round(((SumCost - DiscountVal) + TaxValue).Value, 2, MidpointRounding.ToEven) : Math.Round(((SumCost - DiscountVal) + TaxValue).Value, 2, MidpointRounding.ToEven);
                TotalDue = model.DiscountAmountOrPercent == "%" ? Math.Round(((SumCost - DiscountVal) + TaxValue - Paid).Value, 2, MidpointRounding.ToEven) : Math.Round(((SumCost - DiscountVal) + TaxValue - Paid).Value, 2, MidpointRounding.ToEven);
            }
        }

        async void OnSelecteNewItems(InvoiceModel model)
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                    //return;
                }
                else
                {
                    var popupView = new ScheduleItemsServicesViewModel(true);
                    popupView.ItemClose += async (item) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        InvoiceItemServicesModel InvoiceModel = new InvoiceItemServicesModel
                        {
                            AccountId = model.AccountId,
                            BrancheId = model.BrancheId,
                            ItemsServicesId = item.Id,
                            InvoiceId = model.Id,
                            ItemsServicesName = item.Name,
                            ItemServiceDescription = item.Description,
                            TaxId = item.TaxId,
                            Tax = item.Tax,
                            Price = item.CostperUnit,
                            Total = item.QTYTime != null && item.Tax != null ? (item.CostperUnit * item.QTYTime) + (item.CostperUnit * item.QTYTime * item.Tax / 100) : item.QTYTime == null && item.Tax != null ? item.CostperUnit + (item.CostperUnit * item.QTYTime * item.Tax / 100) : item.QTYTime != null && item.Tax == null ? item.CostperUnit * item.QTYTime : item.CostperUnit,
                            Active = item.Active,
                            CreateUser = model.CreateUser,
                            CreateDate = model.CreateDate,
                            Taxable = item.Taxable,
                            Quantity = (item.QTYTime == null || item.QTYTime == 0) ? 1 : item.QTYTime,
                            Unit = item.Unit,
                            SkipOfTotal = false,
                        };

                        if (LstItemsInvoice.Count > 0)
                        {
                            var itm = LstItemsInvoice.Where(x => x.ItemsServicesId == item.Id).FirstOrDefault();
                            if (itm == null)
                            {
                                LstItemsInvoice.Add(InvoiceModel);
                                OneInvoice.LstInvoiceItemServices.Add(InvoiceModel);
                            }
                        }
                        else
                        {
                            LstItemsInvoice.Add(InvoiceModel);
                            OneInvoice.LstInvoiceItemServices.Add(InvoiceModel);
                        }

                        if (LstItemsInvoice.Count > 4)
                        {
                            LstHeight = 1;
                        }

                        TotalInvoice(model);

                        UserDialogs.Instance.HideLoading();
                    };

                    var page = new Views.SchedulePages.NewItemsServicesSchedulePage();
                    page.BindingContext = popupView;
                    await App.Current.MainPage.Navigation.PushAsync(page);
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                //throw;
            }

            IsBusy = false;
        }

        void OnRemoveItem(InvoiceItemServicesModel item)
        {
            IsBusy = true;

            LstItemsInvoice.Remove(item);
            OneInvoice.LstInvoiceItemServices.Remove(item);
            TotalInvoice(OneInvoice);

            IsBusy = false;
        }

        async void OnSelecteNewItemsEstimate(EstimateModel model)
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                    //return;
                }
                else
                {
                    var popupView = new ScheduleItemsServicesViewModel(true);
                    popupView.ItemClose += async (item) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        EstimateItemServicesModel EstimateModel = new EstimateItemServicesModel
                        {
                            AccountId = model.AccountId,
                            BrancheId = model.BrancheId,
                            ItemsServicesId = item.Id,
                            EstimateId = model.Id,
                            ItemsServicesName = item.Name,
                            TaxId = item.TaxId,
                            Tax = item.Tax,
                            Price = item.CostperUnit,
                            Total = item.QTYTime != null && item.Tax != null ? (item.CostperUnit * item.QTYTime) + (item.CostperUnit * item.QTYTime * item.Tax / 100) : item.QTYTime == null && item.Tax != null ? item.CostperUnit + (item.CostperUnit * item.QTYTime * item.Tax / 100) : item.QTYTime != null && item.Tax == null ? item.CostperUnit * item.QTYTime : item.CostperUnit,
                            Active = item.Active,
                            CreateUser = model.CreateUser,
                            CreateDate = model.CreateDate,
                            Taxable = item.Taxable,
                            Quantity = (item.QTYTime == null || item.QTYTime == 0) ? 1 : item.QTYTime,
                            Unit = item.Unit,
                        };

                        if (LstItemsEstimate.Count > 0)
                        {
                            var itm = LstItemsEstimate.Where(x => x.ItemsServicesId == item.Id).FirstOrDefault();
                            if (itm == null)
                            {
                                LstItemsEstimate.Add(EstimateModel);
                                OneEstimate.LstEstimateItemServices.Add(EstimateModel);
                            }
                        }
                        else
                        {
                            LstItemsEstimate.Add(EstimateModel);
                            OneEstimate.LstEstimateItemServices.Add(EstimateModel);
                        }

                        if (LstItemsEstimate.Count > 4)
                        {
                            LstHeight = 1;
                        }

                        TotalEstimate(model);

                        UserDialogs.Instance.HideLoading();
                    };

                    var page = new Views.SchedulePages.NewItemsServicesSchedulePage();
                    page.BindingContext = popupView;
                    await App.Current.MainPage.Navigation.PushAsync(page);
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                //throw;
            }

            IsBusy = false;
        }

        void OnRemoveItemEstimate(EstimateItemServicesModel item)
        {
            IsBusy = true;

            LstItemsEstimate.Remove(item);
            OneEstimate.LstEstimateItemServices.Remove(item);
            TotalEstimate(OneEstimate);

            IsBusy = false;
        }

        async void OnCreateNewSchedule(CustomersModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();

            SchedulesViewModel ViewModel;
            if (Controls.StaticMembers.WayAfterChooseCust == 0)
            {
                ViewModel = new SchedulesViewModel(model);
                var page = new NewSchedulePage();
                page.BindingContext = ViewModel;
                await App.Current.MainPage.Navigation.PushAsync(page);
            }

            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnSelecteCustomerDetails(CustomersModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            var ViewModel = new CustomersViewModel(model);
            var page = new Views.CustomerPages.CustomersDetailsPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        public async void OnSelecteScheduleDetails(SchedulesModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            var ViewModel = new SchedulesViewModel(model.Id, model.OneScheduleDate.Id);
            var page = new Views.SchedulePages.NewSchedulePage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        void OnEditDiscountForCustomerEstimate(CustomersModel model)
        {
            Discount = CustomerDetails.Discount = model.Discount;

            TotalEstimate(OneEstimate);
        }

        void OnEditDiscountForCustomer(CustomersModel model)
        {
            Discount = CustomerDetails.Discount = model.Discount;

            TotalInvoice(OneInvoice);
        }

        async void OnSelecteInvoiceDetails(InvoiceModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            var ViewModel = new CustomersViewModel(model, CustomerDetails);
            var page = new Views.CustomerPages.InvoiceDetailsPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnSelecteEstimateDetails(EstimateModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            var ViewModel = new CustomersViewModel(model, CustomerDetails);
            var page = new Views.CustomerPages.EstimateDetailsPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnSubmitInvoice(InvoiceModel model)
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                    //return;
                }
                else
                {
                    OneInvoice = model;

                    if (OneInvoice != null)
                    {
                        if (OneInvoice.LstInvoiceItemServices.Count > 0)
                        {
                            string UserToken = await _service.UserToken();

                            var CheckItemoutFalse = OneInvoice.LstInvoiceItemServices.Where(m => m.SkipOfTotal == false).FirstOrDefault();
                            OneInvoice.LstScdDate = LstInvoiceSchaduleDatesActual.ToList();

                            if (CheckItemoutFalse != null)
                            {
                                OneInvoice.Total = SubTotal;
                                OneInvoice.Net = Net;
                                OneInvoice.Discount = Discount;

                                var json = "";
                                if (OneInvoice.Id == 0)
                                {
                                    UserDialogs.Instance.ShowLoading();
                                    //json = await Helpers.Utility.PostMData("api/Invoices/PostInvoice", JsonConvert.SerializeObject(OneInvoice));
                                    json = await ORep.PostMData("api/Invoices/PostInvoice", OneInvoice, UserToken);
                                    UserDialogs.Instance.HideLoading();
                                }
                                else
                                {
                                    UserDialogs.Instance.ShowLoading();
                                    //json = await Helpers.Utility.PutPosData("api/Invoices/PutInvoice", JsonConvert.SerializeObject(OneInvoice));
                                    json = await ORep.PutDataAsync("api/Invoices/PutInvoice", OneInvoice, UserToken);
                                    UserDialogs.Instance.HideLoading();
                                }

                                if (json != "Bad Request" && json != "api not responding" && json.Contains("Not_Enough") != true && json.Contains("This Invoice Already Exist") != true && json.Contains("Already Exist For This Schedule Date#") != true)
                                {
                                    await App.Current.MainPage.DisplayAlert("Project Services", "Succes Save Invoice.", "Ok");
                                    var ViewModel = new SchedulesViewModel(OneInvoice, CustomerDetails);
                                    var page = new Views.PopupPages.PaymentMethodsPopup();
                                    page.BindingContext = ViewModel;
                                    await PopupNavigation.Instance.PushAsync(page);
                                    //await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                                    //App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Alert", json, "Ok");
                                    //await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "Please Don't Check all Item-Service Out for this Invoice.", "Ok");
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Please Choose Item-Service for this Invoice.", "Ok");
                        }
                    }
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                //throw;
            }

            IsBusy = false;
        }

        async void OnSubmitEstimate(EstimateModel model)
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                    //return;
                }
                else
                {
                    OneEstimate = model;

                    if (OneEstimate != null)
                    {
                        string UserToken = await _service.UserToken();

                        OneEstimate.Status = Accept == true ? 1 : Declind == true ? 2 : 0; //0 = Pending
                        OneEstimate.LstScdDate = LstEstimateSchaduleDatesActual.ToList();

                        if (OneEstimate.LstEstimateItemServices.Count > 0)
                        {
                            OneEstimate.Total = SubTotal;
                            OneEstimate.Net = Net;
                            OneEstimate.Discount = Discount;

                            var json = "";
                            if (OneEstimate.Id == 0)
                            {
                                UserDialogs.Instance.ShowLoading();
                                //json = await Helpers.Utility.PostData("api/Estimates/PostEstimate", JsonConvert.SerializeObject(OneEstimate));
                                json = await ORep.PostDataAsync("api/Estimates/PostEstimate", OneEstimate, UserToken);
                                UserDialogs.Instance.HideLoading();
                            }
                            else
                            {
                                UserDialogs.Instance.ShowLoading();
                                //json = await Helpers.Utility.PutPosData("api/Estimates/PutEstimate", JsonConvert.SerializeObject(OneEstimate));
                                json = await ORep.PutDataAsync("api/Estimates/PutEstimate", OneEstimate, UserToken);
                                UserDialogs.Instance.HideLoading();
                            }

                            if (json != "Bad Request" && json != "api not responding" && json.Contains("Already Exist For This Schedule Date#") != true)
                            {
                                await App.Current.MainPage.DisplayAlert("Project Services", "Succes Save Estimate.", "Ok");

                                var ViewModel = new CustomersViewModel(CustomerDetails);
                                var page = new Views.CustomerPages.CustomersDetailsPage();
                                page.BindingContext = ViewModel;
                                await App.Current.MainPage.Navigation.PushAsync(page);
                                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);

                                if (OneEstimate.Status == 1)//Status Accept
                                {
                                    ShowEstimateConvertToInvoice = true;
                                }
                                else
                                {
                                    ShowEstimateConvertToInvoice = false;
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", json, "Ok");
                                //await App.Current.MainPage.DisplayAlert("Alert", "Faild Save Estimate.", "Ok");
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Please Choose Item-Service for this Estimate.", "Ok");
                        }
                    }
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                //throw;
            }

            IsBusy = false;
        }

        async void OnGoInvoice(int InvoiceId)
        {
            IsBusy = true;
            await GetOneInvoiceDetails(InvoiceId, null);
            UserDialogs.Instance.ShowLoading();
            var ViewModel = new CustomersViewModel(OneInvoice, CustomerDetails);
            var page = new Views.CustomerPages.InvoiceDetailsPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnConvertToInvoice(EstimateModel model)
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                    //return;
                }
                else
                {
                    await GetOneInvoiceDetails(null, model.Id);

                    string UserToken = await _service.UserToken();

                    if (OneEstimate != null)
                    {
                        OneInvoice = new InvoiceModel();
                        OneInvoice.LstInvoiceItemServices = new List<InvoiceItemServicesModel>();

                        OneInvoice.AccountId = OneEstimate.AccountId;
                        OneInvoice.BrancheId = OneEstimate.BrancheId;
                        //OneInvoice.ContractId = 0;
                        //OneInvoice.ContractInvoiceId = 0;
                        OneInvoice.ScheduleId = OneEstimate.ScheduleId;
                        OneInvoice.EstimateId = OneEstimate.Id;
                        OneInvoice.InvoiceDate = DateTime.Now;
                        OneInvoice.CustomerId = OneEstimate.CustomerId;
                        OneInvoice.Total = OneEstimate.Total;
                        OneInvoice.TaxId = OneEstimate.TaxId;
                        OneInvoice.Tax = OneEstimate.Tax;
                        OneInvoice.Taxval = OneEstimate.Taxval;
                        OneInvoice.MemberId = CustomerDetails.MemeberId;
                        OneInvoice.Discount = OneEstimate.Discount;
                        OneInvoice.DiscountAmountOrPercent = OneEstimate.DiscountAmountOrPercent;
                        OneInvoice.Paid = 0;
                        OneInvoice.Net = OneEstimate.Net;
                        OneInvoice.Status = 0; //Draft status if(1=partail & 2=paid)
                        OneInvoice.Type = 2; //Installment Payment type
                        OneInvoice.SignaturePrintName = null;
                        OneInvoice.SignatureDraw = null;
                        OneInvoice.Terms = null;
                        OneInvoice.NotesForCustomer = OneEstimate.NotesForCustomer;
                        OneInvoice.Notes = OneEstimate.Notes;
                        OneInvoice.Active = OneEstimate.Active;
                        OneInvoice.CreateUser = int.Parse(Helpers.Settings.UserId);
                        OneInvoice.CreateDate = DateTime.Now;

                        foreach (EstimateItemServicesModel item in OneEstimate.LstEstimateItemServices)
                        {
                            InvoiceItemServicesModel ObjItem = new InvoiceItemServicesModel
                            {
                                Id = item.Id,
                                AccountId = OneEstimate.AccountId,
                                BrancheId = OneEstimate.BrancheId,
                                //ItemServiceDescription = item.ItemServiceDescription,
                                TaxId = item.TaxId,
                                Tax = item.Tax,
                                //Taxable = item.Taxable,
                                Taxable = true,
                                Unit = item.Unit,
                                Price = item.Price,
                                Quantity = item.Quantity,
                                //Discountable = CustomerDetails.MemberDTO.MemberValue != null ? true : false,
                                Discountable = true,
                                ItemsServicesId = item.ItemsServicesId,
                                ItemsServicesName = item.ItemsServicesName,
                                CreateUser = int.Parse(Helpers.Settings.UserId),
                                CreateDate = DateTime.Now,
                                SkipOfTotal = false,
                                Total = item.Quantity != null && item.Tax != null ? (item.Price * item.Quantity) + (item.Price * item.Quantity * item.Tax / 100) : item.Quantity == null && item.Tax != null ? item.Price + (item.Price * item.Quantity * item.Tax / 100) : item.Quantity != null && item.Tax == null ? item.Price * item.Quantity : item.Price,
                                Active = OneEstimate.Active,
                            };
                            OneInvoice.LstInvoiceItemServices.Add(ObjItem);
                        }

                        UserDialogs.Instance.ShowLoading();
                        //var json = await Helpers.Utility.PostMData("api/Invoices/PostInvoice", JsonConvert.SerializeObject(OneInvoice));
                        var json = await ORep.PostMData("api/Invoices/PostInvoice", OneInvoice, UserToken);
                        UserDialogs.Instance.HideLoading();

                        if (json != null && json != "api not responding" && json.Contains("Not_Enough") != true && json.Contains("This Invoice Already Exist") != true)
                        {
                            await App.Current.MainPage.DisplayAlert("Project Services", "Succes Convert To Inovice.", "Ok");
                            OneInvoice.Id = int.Parse(json.Trim('"'));

                            var ViewModel = new CustomersViewModel(OneInvoice, CustomerDetails);
                            var page = new Views.CustomerPages.InvoiceDetailsPage();
                            page.BindingContext = ViewModel;
                            await App.Current.MainPage.Navigation.PushAsync(page);
                            //await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", json, "Ok");
                        }
                    }
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                //throw;
            }
            IsBusy = false;
        }

        async void OnCreateNewCustomer()
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            Controls.StaticMembers.WayCreateCust = 0;
            var ViewModel = new CustomersViewModel(true);
            var page = new Views.CustomerPages.CreateNewCustomerPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        void OnChooseCustomerCategory(CustomersCategoryModel model)
        {
            CustomerDetails.CustomerCategory = model;
        }

        void OnChooseCustomerMemberShip(MemberModel model)
        {
            CustomerDetails.MemberDTO = model;
        }

        void OnChooseCustomerTax(TaxModel model)
        {
            CustomerDetails.TaxDTO = model;
        }

        void OnChooseCustomerCampaign(CampaignModel model)
        {
            CustomerDetails.Source = model.Id;
        }

        async void OnSelecteAddress()
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                    //return;
                }
                else
                {
                    var popupView = new Views.PopupPages.AddressPupop();
                    popupView.DidClose += async (str) =>
                    {

                        CustomerDetails.AddressModel = str;
                        Address = CustomerDetails.Address = str.FullAddress;
                        CustomerDetails.locationlatitude = str.Latitude.ToString();
                        CustomerDetails.locationlongitude = str.Longitude.ToString();
                        State = CustomerDetails.State = str.State;
                        City = CustomerDetails.City = str.City;
                        ZipCode = CustomerDetails.PostalcodeZIP = str.Zip;
                        CustomerDetails.Country = str.Country;

                        CustomersModel oCust = Controls.StartData.GetAddressDetails(CustomerDetails);

                        CustomerDetails.EstimedValue = oCust.EstimedValue;
                        HouseValue = string.Format("${0:#,0.#}", float.Parse(oCust.EstimedValue));
                        YearBuilt = CustomerDetails.YearBuit = oCust.YearBuit;
                        SquareFootage = CustomerDetails.Squirefootage = oCust.Squirefootage;
                        CustomerDetails.YearEstimedValue = oCust.YearEstimedValue;


                        //HouseProperty oHouseProperty = await Controls.StartData.GetPropertyByAddress(Address);
                        //if (oHouseProperty != null)
                        //{
                        //    HouseValue = CustomerDetails.EstimedValue = oHouseProperty.Estimatedvalue;
                        //    YearBuilt = CustomerDetails.YearBuit = oHouseProperty.YearBuilt;
                        //    SquareFootage = CustomerDetails.Squirefootage = oHouseProperty.SquareFootage;
                        //    CustomerDetails.YearEstimedValue = oHouseProperty.YearEstimatedValue;
                        //}
                    };

                    await PopupNavigation.Instance.PushAsync(popupView);
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                //throw;
            }

            IsBusy = false;
        }

        async void OnSelectCustToCreateEstimatePage()
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            Controls.StaticMembers.WayAfterChooseCust = 1; //Create Estimate 
            var ViewModel = new SchedulesViewModel(CustomerDetails);
            var page = new Views.CreateEstimateWithoutSchedulePage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnSelectCustToCreateInvoicePage()
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            Controls.StaticMembers.WayAfterChooseCust = 2; //Create Invoice 
            var ViewModel = new SchedulesViewModel(CustomerDetails);
            var page = new Views.CreateInvoiceWithoutSchedulePage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnInsertCustomer(CustomersModel model)
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                    //return;
                }
                else
                {
                    if(string.IsNullOrEmpty(model.FirstName))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : First Name.", "Ok");
                    }
                    else if(string.IsNullOrEmpty(model.LastName))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Last Name.", "Ok");
                    }
                    else if (model.CustomerCategory == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Customer Category.", "Ok");
                    }
                    else if (model.Source == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Source.", "Ok");
                    }
                    else if (string.IsNullOrEmpty(model.Phone1))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Customer Phone.", "Ok");
                    }
                    else if (string.IsNullOrEmpty(model.Email))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Customer Email.", "Ok");
                    }
                    else if (string.IsNullOrEmpty(model.Address))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Customer Address.", "Ok");
                    }
                    else
                    {
                        string UserToken = await _service.UserToken();

                        model.AccountId = int.Parse(Helpers.Settings.AccountId);
                        model.BrancheId = int.Parse(Helpers.Settings.BranchId);
                        model.CreateUser = int.Parse(Helpers.Settings.UserId);
                        model.CustomerType = 1;
                        model.AllowLogin = false;
                        model.Credit = 0;
                        model.Since = DateTime.Now;
                        model.Active = true;
                        model.CreateDate = DateTime.Now;
                        model.State = CustomerDetails.State != null ? CustomerDetails.State : State;
                        model.City = CustomerDetails.City != null ? CustomerDetails.City : City;
                        model.PostalcodeZIP = CustomerDetails.PostalcodeZIP != null ? CustomerDetails.PostalcodeZIP : ZipCode;

                        model.LstCustomersCustomField = new List<CustomersCustomFieldModel>();
                        foreach (CustomersCustomFieldModel item in CustomerFeatures.LstCustomersCustomField.ToList())
                        {
                            if (item.Required == false || string.IsNullOrEmpty(item.DefaultValue?.Trim()) != true)
                            {
                                if (item.FieldType == 4 && item.DefaultValue == "True")
                                {
                                    item.DefaultValue = "Yes";
                                }
                                else if (item.FieldType == 4 && item.DefaultValue == "False")
                                {
                                    item.DefaultValue = "No";
                                }
                                else if (item.FieldType == 3)
                                {
                                    item.DefaultValue = DateTime.Parse(item.DefaultValue).ToString("yyyy-MM-dd");
                                }

                                model.LstCustomersCustomField.Add(item);
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field : {item.CustomFieldName} Required For Custom Field.", "Ok");
                                return;
                            }

                        }

                        //model.LstCustomersCustomField = CustomerFeatures.LstCustomersCustomField;

                        if (CustomerDetails.MemberDTO != null)
                        {
                            model.MemeberType = CustomerDetails.MemberDTO.MemberType;
                            model.MemeberId = CustomerDetails.MemberDTO.Id;
                        }
                        else
                        {
                            model.MemeberType = false;
                        }

                        if (CustomerDetails.TaxDTO != null)
                            model.TaxId = CustomerDetails.TaxDTO.Id;

                        model.Taxable = CustomerDetails.Taxable == null ? false : CustomerDetails.Taxable;

                        if (CustomerDetails.CustomerCategory != null)
                            model.CategoryId = CustomerDetails.CustomerCategory.Id;

                        UserDialogs.Instance.ShowLoading();
                        //var json = await Helpers.Utility.PostData("api/Customers/PostCustomer", JsonConvert.SerializeObject(model));
                        var json = await ORep.PostDataAsync("api/Customers/PostCustomer", model, UserToken);

                        if (json != null && json != "api not responding" && json != "Multiple Choices")
                        {
                            if (Controls.StaticMembers.WayCreateCust == 1)//From CallPage
                            {
                                CustomersModel Customer = JsonConvert.DeserializeObject<CustomersModel>(json);
                                var ViewModel = new CallsViewModel(Customer);
                                var page = new Views.CallPages.NewCallPage();
                                page.BindingContext = ViewModel;
                                await App.Current.MainPage.Navigation.PushAsync(page);
                                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                                await PopupNavigation.Instance.PopAsync();
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Project Services", "Succes Insert Customer.", "Ok");
                                await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                            }
                        }
                        else if (json == "Multiple Choices")
                        {
                            await App.Current.MainPage.DisplayAlert("Project Services", "This Customer phone or Email already exists.", "Ok");
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Project Services", "Field Insert Customer.", "Ok");
                        }
                        UserDialogs.Instance.HideLoading();
                    } 
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                //throw;
            }

            IsBusy = false;
        }

        async void OnCreditPayment(InvoiceModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            Controls.StaticMembers.PayCashOrCredit = 2;
            var ViewModel = new PaymentsViewModel(model, CustomerDetails);
            var page = new Views.CustomerPages.CashOrCreditPaymentPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnCashPayment(InvoiceModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            Controls.StaticMembers.PayCashOrCredit = 1;
            var ViewModel = new PaymentsViewModel(model, CustomerDetails);
            var page = new Views.CustomerPages.CashOrCreditPaymentPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnDeleteInvoice(int InvoiceId)
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                    //return;
                }
                else
                {
                    UserDialogs.Instance.ShowLoading();
                    string UserToken = await _service.UserToken();

                    var json = await ORep.DeleteStrItemAsync(string.Format("api/Invoices/DeleteInvoice/{0}", InvoiceId, UserToken));

                    if (json != null && json != "api not responding" && json.Contains("Is Not Deleted") != true)
                    {
                        await App.Current.MainPage.DisplayAlert("Project Services", "Succes Delete Inovice.", "Ok");
                        await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", json, "Ok");
                        //await App.Current.MainPage.DisplayAlert("Alert", "Failed Delete Inovice.", "Ok");
                    }
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                //throw;
            }

            IsBusy = false;
        }

        async void OnOpenEstimateScheduleDates()
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                    //return;
                }
                else
                {
                    foreach (SchaduleDateModel dt in LstEstimateSchaduleDates)
                    {
                        foreach (SchaduleDateModel dt2 in LstEstimateSchaduleDatesActual)
                        {
                            if (dt.Id == dt2.Id)
                            {
                                dt.IsChecked = true;
                            }
                        }
                    }

                    var popupView = new Views.PopupPages.ScheduleDatesPopup(LstEstimateSchaduleDates);
                    popupView.DatesClose += (Dates) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        if (Dates.Count != 0)
                        {
                            LstEstimateSchaduleDatesActual = new ObservableCollection<SchaduleDateModel>(Dates);
                            string str = "";
                            LstEstimateDates.Clear();
                            foreach (var Date in Dates)
                            {
                                str += (" , " + Date.Date);    
                                LstEstimateDates.Add(new SchaduleDateModel
                                {
                                    Id = Date.Id,
                                    Date = Date.Date,
                                });
                            }

                            if (!string.IsNullOrEmpty(StrEstimateDates))
                            {
                                StrEstimateDates = string.Empty;
                                StrEstimateDates += str;
                                StrEstimateDates = str.Remove(0, 2);
                            }
                            else
                            {
                                StrEstimateDates = str.Remove(0, 2);
                            }
                        }

                        UserDialogs.Instance.HideLoading();
                    };

                    await PopupNavigation.Instance.PushAsync(popupView);
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                //throw;
            }

            IsBusy = false;
        }

        async void OnOpenInvoiceScheduleDates()
        {
            IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                    //return;
                }
                else
                {
                    foreach (SchaduleDateModel dt in LstInvoiceSchaduleDates)
                    {
                        foreach (SchaduleDateModel dt2 in LstInvoiceSchaduleDatesActual)
                        {
                            if (dt.Id == dt2.Id)
                            {
                                dt.IsChecked = true;
                            }
                        }
                    }

                    var popupView = new Views.PopupPages.ScheduleDatesPopup(LstInvoiceSchaduleDates);
                    popupView.DatesClose += (Dates) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        if (Dates.Count != 0)
                        {
                            LstInvoiceSchaduleDatesActual = new ObservableCollection<SchaduleDateModel>(Dates);

                            string str = "";
                            LstInvoiceDates.Clear();
                            foreach (var Date in Dates)
                            {
                                str += (" , " + Date.Date);      
                                LstInvoiceDates.Add(new SchaduleDateModel
                                {
                                    Id = Date.Id,
                                    Date = Date.Date,
                                });
                            }

                            if (!string.IsNullOrEmpty(StrInvoiceDates))
                            {
                                StrInvoiceDates = string.Empty;
                                StrInvoiceDates += str;
                                StrInvoiceDates = str.Remove(0, 2);
                            }
                            else
                            {
                                StrInvoiceDates = str.Remove(0, 2);
                            }
                        }

                        UserDialogs.Instance.HideLoading();
                    };

                    await PopupNavigation.Instance.PushAsync(popupView);
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                //throw;
            }

            IsBusy = false;
        }

        void OnRemoveEstimateDate(SchaduleDateModel Date)
        {
            LstEstimateDates.Remove(Date);
            SchaduleDateModel DataOfScddt = LstEstimateSchaduleDatesActual.Where(x => x.Id == Date.Id).FirstOrDefault();
            LstEstimateSchaduleDatesActual.Remove(DataOfScddt);

            foreach (SchaduleDateModel dt in LstEstimateSchaduleDates)
            {
                foreach (SchaduleDateModel dt2 in LstEstimateSchaduleDatesActual)
                {
                    if (dt.Id == dt2.Id)
                    {
                        dt.IsChecked = true;
                    }
                }
            }

            //Dates Names 
            int index = StrEstimateDates.IndexOf(Date.Date + " , ");
            StrEstimateDates = (index < 0) ? StrEstimateDates : StrEstimateDates.Remove(index, (Date.Date + " , ").Length);

            int index2 = StrEstimateDates.IndexOf(" , " + Date.Date);
            StrEstimateDates = (index2 < 0) ? StrEstimateDates : StrEstimateDates.Remove(index2, (" , " + Date.Date).Length);

            int index3 = StrEstimateDates.IndexOf(Date.Date);
            StrEstimateDates = (index3 < 0) ? StrEstimateDates : StrEstimateDates.Remove(index3, (Date.Date).Length);
        }

        void OnRemoveInvoiceDate(SchaduleDateModel Date)
        {      
            LstInvoiceDates.Remove(Date);
            SchaduleDateModel DataOfScddt = LstInvoiceSchaduleDatesActual.Where(x => x.Id == Date.Id).FirstOrDefault();
            LstInvoiceSchaduleDatesActual.Remove(DataOfScddt);

            foreach (SchaduleDateModel dt in LstInvoiceSchaduleDates)
            { 
                foreach (SchaduleDateModel dt2 in LstInvoiceSchaduleDatesActual)
                {
                    if (dt.Id == dt2.Id)
                    {
                        dt.IsChecked = true;
                    }
                }
            }

            //Dates Names 
            int index = StrInvoiceDates.IndexOf(Date.Date + " , ");
            StrInvoiceDates = (index < 0) ? StrInvoiceDates : StrInvoiceDates.Remove(index, (Date.Date + " , ").Length);

            int index2 = StrInvoiceDates.IndexOf(" , " + Date.Date);
            StrInvoiceDates = (index2 < 0) ? StrInvoiceDates : StrInvoiceDates.Remove(index2, (" , " + Date.Date).Length);

            int index3 = StrInvoiceDates.IndexOf(Date.Date);
            StrInvoiceDates = (index3 < 0) ? StrInvoiceDates : StrInvoiceDates.Remove(index3, (Date.Date).Length);
        }
    }
}
