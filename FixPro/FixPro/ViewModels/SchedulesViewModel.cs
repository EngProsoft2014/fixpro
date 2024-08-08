
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Plugin.Media;
using FixPro.Models;
using FixPro.Views.MenuPages;
using FixPro.Views.SchedulePages;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Reactive;
using System.Reactive.Concurrency;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Acr.UserDialogs.Infrastructure;
using static FixPro.Helpers.ItemHelper;
//using GoogleApi.Entities.Translate.Common.Enums;
using Syncfusion.SfCalendar.XForms;
using Rg.Plugins.Popup.Pages;
using System.Runtime.InteropServices;
//using Org.BouncyCastle.Bcpg;
using Xamarin.Essentials;
using Acr.UserDialogs;
using Stripe;
using GoogleApi.Entities.Translate.Common.Enums;
using System.IO;
using OneSignalSDK.Xamarin.Core;
using GoogleApi.Entities.Search.Video.Common;
using Splat.ModeDetection;
using Xamarin.CommunityToolkit.UI.Views.Options;
using Xamarin.CommunityToolkit.Extensions;
using Twilio.Types;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using FixPro.Views.PopupPages;

//using Plugin.Permissions.Abstractions;
//using Plugin.Permissions;

namespace FixPro.ViewModels
{
    public class SchedulesViewModel : INotifyPropertyChanged
    {
        private MediaFile _mediaFile;

        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

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

        ObservableCollection<SchedulesModel> _CalendarDataToday;
        public ObservableCollection<SchedulesModel> CalendarDataToday
        {
            get
            {
                return _CalendarDataToday;
            }
            set
            {
                _CalendarDataToday = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CalendarDataToday"));
                }
            }
        }

        ObservableCollection<SchedulesModel> _LstSchedulesSearch;
        public ObservableCollection<SchedulesModel> LstSchedulesSearch
        {
            get
            {
                return _LstSchedulesSearch;
            }
            set
            {
                _LstSchedulesSearch = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstSchedulesSearch"));
                }
            }
        }

        ObservableCollection<ScheduleItemsServicesModel> _LstItems;
        public ObservableCollection<ScheduleItemsServicesModel> LstItems
        {
            get
            {
                return _LstItems;
            }
            set
            {
                _LstItems = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstItems"));
                }
            }
        }

        ObservableCollection<ScheduleItemsServicesModel> _LstItemsInvoice;
        public ObservableCollection<ScheduleItemsServicesModel> LstItemsInvoice
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

        ObservableCollection<ScheduleItemsServicesModel> _LstFreeServices;
        public ObservableCollection<ScheduleItemsServicesModel> LstFreeServices
        {
            get
            {
                return _LstFreeServices;
            }
            set
            {
                _LstFreeServices = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstFreeServices"));
                }
            }
        }

        ObservableCollection<ScheduleMaterialReceiptModel> _LstMaterialReceipt;
        public ObservableCollection<ScheduleMaterialReceiptModel> LstMaterialReceipt
        {
            get
            {
                return _LstMaterialReceipt;
            }
            set
            {
                _LstMaterialReceipt = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstMaterialReceipt"));
                }
            }
        }

        ObservableCollection<ScheduleItemsServicesModel> _LstItemsEstimate;
        public ObservableCollection<ScheduleItemsServicesModel> LstItemsEstimate
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

        ObservableCollection<SheetColorModel> _LstColors;
        public ObservableCollection<SheetColorModel> LstColors
        {
            get
            {
                return _LstColors;
            }
            set
            {
                _LstColors = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstColors"));
                }
            }
        }

        ObservableCollection<EmployeesCategoryModel> _LstEmpCategory;
        public ObservableCollection<EmployeesCategoryModel> LstEmpCategory
        {
            get
            {
                return _LstEmpCategory;
            }
            set
            {
                _LstEmpCategory = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEmpCategory"));
                }
            }
        }

        ObservableCollection<EmployeeModel> _LstEmpInOneCategory;
        public ObservableCollection<EmployeeModel> LstEmpInOneCategory
        {
            get
            {
                return _LstEmpInOneCategory;
            }
            set
            {
                _LstEmpInOneCategory = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEmpInOneCategory"));
                }
            }
        }

        SchedulePicturesModel _OnePictureModel;
        public SchedulePicturesModel OnePictureModel
        {
            get
            {
                return _OnePictureModel;
            }
            set
            {
                _OnePictureModel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OnePictureModel"));
                }
            }
        }

        ObservableCollection<SchedulePicturesModel> _LstAllPictures;
        public ObservableCollection<SchedulePicturesModel> LstAllPictures
        {
            get
            {
                return _LstAllPictures;
            }
            set
            {
                _LstAllPictures = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstAllPictures"));
                }
            }
        }

        ObservableCollection<SchedulePicturesModel> _LstATwoPictures;
        public ObservableCollection<SchedulePicturesModel> LstATwoPictures
        {
            get
            {
                return _LstATwoPictures;
            }
            set
            {
                _LstATwoPictures = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstATwoPictures"));
                }
            }
        }

        ObservableCollection<SchedulePicturesModel> _LstNewPictures;
        public ObservableCollection<SchedulePicturesModel> LstNewPictures
        {
            get
            {
                return _LstNewPictures;
            }
            set
            {
                _LstNewPictures = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstNewPictures"));
                }
            }
        }

        ObservableCollection<PriorityModel> _LstPriority;
        public ObservableCollection<PriorityModel> LstPriority
        {
            get
            {
                return _LstPriority;
            }
            set
            {
                _LstPriority = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstPriority"));
                }
            }
        }

        ObservableCollection<ScheduleEmployeesModel> _LstEmps;
        public ObservableCollection<ScheduleEmployeesModel> LstEmps
        {
            get
            {
                return _LstEmps;
            }
            set
            {
                _LstEmps = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEmps"));
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


        ObservableCollection<ItemsServicesModel> _LstServices;
        public ObservableCollection<ItemsServicesModel> LstServices
        {
            get
            {
                return _LstServices;
            }
            set
            {
                _LstServices = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstServices"));
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

        CalendarEventCollection _LstScheduleEvevts;
        public CalendarEventCollection LstScheduleEvevts
        {
            get
            {
                return _LstScheduleEvevts;
            }
            set
            {
                _LstScheduleEvevts = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstScheduleEvevts"));
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

        EmployeesCategoryModel _EmpCategory;
        public EmployeesCategoryModel EmpCategory
        {
            get
            {
                return _EmpCategory;
            }
            set
            {
                _EmpCategory = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("EmpCategory"));
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

        EmployeesCategoryModel _SelectedCateory;
        public EmployeesCategoryModel SelectedCateory
        {
            get
            {
                return _SelectedCateory;
            }
            set
            {
                _SelectedCateory = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedCateory"));
                }
            }
        }

        EmployeeModel _SelectedEmployeeAddDate;
        public EmployeeModel SelectedEmployeeAddDate
        {
            get
            {
                return _SelectedEmployeeAddDate;
            }
            set
            {
                _SelectedEmployeeAddDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedEmployeeAddDate"));
                }
            }
        }

        ItemsServicesModel _SelectedService;
        public ItemsServicesModel SelectedService
        {
            get
            {
                return _SelectedService;
            }
            set
            {
                _SelectedService = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedService"));
                }
            }
        }

        SchaduleDateModel _OneScheduleDate;
        public SchaduleDateModel OneScheduleDate
        {
            get
            {
                return _OneScheduleDate;
            }
            set
            {
                _OneScheduleDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneScheduleDate"));
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

        PriorityModel _OnePriorityModel;
        public PriorityModel OnePriorityModel
        {
            get
            {
                return _OnePriorityModel;
            }
            set
            {
                _OnePriorityModel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OnePriorityModel"));
                }
            }
        }

        Object _SelectedColor;
        public Object SelectedColor
        {
            get
            {
                return _SelectedColor;
            }
            set
            {
                _SelectedColor = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedColor"));
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

        string _StrEmployees;
        public string StrEmployees
        {
            get
            {
                return _StrEmployees;
            }
            set
            {
                _StrEmployees = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("StrEmployees"));
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


        string _StrEmployeesId;
        public string StrEmployeesId
        {
            get
            {
                return _StrEmployeesId;
            }
            set
            {
                _StrEmployeesId = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("StrEmployeesId"));
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

        DateTime _ScheduleDate;
        public DateTime ScheduleDate
        {
            get
            {
                return _ScheduleDate;
            }
            set
            {
                _ScheduleDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ScheduleDate"));
                }
            }
        }

        DateTime _ScheduleAddDate;
        public DateTime ScheduleAddDate
        {
            get
            {
                return _ScheduleAddDate;
            }
            set
            {
                _ScheduleAddDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ScheduleAddDate"));
                }
            }
        }

        DateTime _InvoiceDate;
        public DateTime InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }
            set
            {
                _InvoiceDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("InvoiceDate"));
                }
            }
        }

        TimeSpan _TimeFrom;
        public TimeSpan TimeFrom
        {
            get
            {
                return _TimeFrom;
            }
            set
            {
                _TimeFrom = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TimeFrom"));
                }
            }
        }

        TimeSpan _TimeTo;
        public TimeSpan TimeTo
        {
            get
            {
                return _TimeTo;
            }
            set
            {
                _TimeTo = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TimeTo"));
                }
            }
        }

        TimeSpan _TimeFromAddDate;
        public TimeSpan TimeFromAddDate
        {
            get
            {
                return _TimeFromAddDate;
            }
            set
            {
                _TimeFromAddDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TimeFromAddDate"));
                }
            }
        }

        TimeSpan _TimeToAddDate;
        public TimeSpan TimeToAddDate
        {
            get
            {
                return _TimeToAddDate;
            }
            set
            {
                _TimeToAddDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TimeToAddDate"));
                }
            }
        }

        string _StartTime;
        public string StartTime
        {
            get
            {
                return _StartTime;
            }
            set
            {
                _StartTime = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("StartTime"));
                }
            }
        }

        string _EndTime;
        public string EndTime
        {
            get
            {
                return _EndTime;
            }
            set
            {
                _EndTime = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("EndTime"));
                }
            }
        }

        string _SpentHours;
        public string SpentHours
        {
            get
            {
                return _SpentHours;
            }
            set
            {
                _SpentHours = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SpentHours"));
                }
            }
        }

        string _SpentMin;
        public string SpentMin
        {
            get
            {
                return _SpentMin;
            }
            set
            {
                _SpentMin = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SpentMin"));
                }
            }
        }

        ImageSource _SchedulePhoto;
        public ImageSource SchedulePhoto
        {
            get
            {
                return _SchedulePhoto;
            }
            set
            {
                _SchedulePhoto = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SchedulePhoto"));
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

        decimal? _SubTotalEst;
        public decimal? SubTotalEst
        {
            get
            {
                return _SubTotalEst;
            }
            set
            {
                _SubTotalEst = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SubTotalEst"));
                }
            }
        }

        decimal? _NetEst;
        public decimal? NetEst
        {
            get
            {
                return _NetEst;
            }
            set
            {
                _NetEst = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("NetEst"));
                }
            }
        }

        decimal? _PaidEst;
        public decimal? PaidEst
        {
            get
            {
                return _PaidEst;
            }
            set
            {
                _PaidEst = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PaidEst"));
                }
            }
        }

        decimal? _TotalDueEst;
        public decimal? TotalDueEst
        {
            get
            {
                return _TotalDueEst;
            }
            set
            {
                _TotalDueEst = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TotalDueEst"));
                }
            }
        }

        bool _DoneFlag;
        public bool DoneFlag
        {
            get
            {
                return _DoneFlag;
            }
            set
            {
                _DoneFlag = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("DoneFlag"));
                }
            }
        }

        bool _ShowImages;
        public bool ShowImages
        {
            get
            {
                return _ShowImages;
            }
            set
            {
                _ShowImages = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowImages"));
                }
            }
        }

        int _ShowDispatch;
        public int ShowDispatch
        {
            get
            {
                return _ShowDispatch;
            }
            set
            {
                _ShowDispatch = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowDispatch"));
                }
            }
        }

        bool _ShowEstimateButton;
        public bool ShowEstimateButton
        {
            get
            {
                return _ShowEstimateButton;
            }
            set
            {
                _ShowEstimateButton = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowEstimateButton"));
                }
            }
        }

        bool _ShowQty; //Don't Show Qty in Schedule items but Show Qty in Estimate items and Invoice items
        public bool ShowQty
        {
            get
            {
                return _ShowQty;
            }
            set
            {
                _ShowQty = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ShowQty"));
                }
            }
        }

        bool _IsReOpen;
        public bool IsReOpen
        {
            get
            {
                return _IsReOpen;
            }
            set
            {
                _IsReOpen = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsReOpen"));
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

        bool _IsShowSearchByItem;
        public bool IsShowSearchByItem
        {
            get
            {
                return _IsShowSearchByItem;
            }
            set
            {
                _IsShowSearchByItem = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsShowSearchByItem"));
                }
            }
        }

        int _SchedulePage;
        public int SchedulePage
        {
            get
            {
                return _SchedulePage;
            }
            set
            {
                _SchedulePage = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SchedulePage"));
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

        string _OldResonNotServiced;
        public string OldResonNotServiced
        {
            get
            {
                return _OldResonNotServiced;
            }
            set
            {
                _OldResonNotServiced = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OldResonNotServiced"));
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

        bool _IsShowAllSchedule;
        public bool IsShowAllSchedule
        {
            get
            {
                return _IsShowAllSchedule;
            }
            set
            {
                _IsShowAllSchedule = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsShowAllSchedule"));
                }
            }
        }

        bool _IsShowScheduleDates;
        public bool IsShowScheduleDates
        {
            get
            {
                return _IsShowScheduleDates;
            }
            set
            {
                _IsShowScheduleDates = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsShowScheduleDates"));
                }
            }
        }

        int _PhotosCount;
        public int PhotosCount
        {
            get
            {
                return _PhotosCount;
            }
            set
            {
                _PhotosCount = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("PhotosCount"));
                }
            }
        }

        string _SignatureImageByte64Estimate;
        public string SignatureImageByte64Estimate
        {
            get
            {
                return _SignatureImageByte64Estimate;
            }
            set
            {
                _SignatureImageByte64Estimate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SignatureImageByte64Estimate"));
                }
            }
        }


        public int BranchIdVM;

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        public bool GetPictures { get; set; } = false;
        public ICommand SelecteCamSchedulePhoto { get; set; }
        public ICommand SelectePickSchedulePhoto { get; set; }
        public ICommand SelecteNewItems { get; set; }
        public ICommand SelecteNewFreeService { get; set; }

        public ICommand SelecteNewItemsEstimate { get; set; }
        public ICommand SelectedEmpCategory { get; set; }
        public ICommand SelectedEmpInOneCategory { get; set; }
        public ICommand SelectedSubmitSchedule { get; set; }
        public ICommand RemoveItem { get; set; }
        public ICommand RemoveService { get; set; }
        public ICommand RemoveMaterialReceipt { get; set; }
        public ICommand RemoveItemEstimate { get; set; }

        //public ICommand RemoveItemfromList { get; set; }
        public ICommand OpenEmployeesInOneCategory { get; set; }
        public ICommand OpenEstimateScheduleDates { get; set; }
        public ICommand OpenInvoiceScheduleDates { get; set; }
        public ICommand SelectJobDetails { get; set; }
        public ICommand AddScheduleDate { get; set; }
        public ICommand SaveResponNotServiceScheduleDate { get; set; }
        public ICommand DoneScheduleDate { get; set; }
        public ICommand DeleteSchedulePhoto { get; set; }
        public ICommand OpenImages { get; set; }
        public ICommand OpenAddImagesPopup { get; set; }
        public ICommand DonePictures { get; set; }
        public ICommand CreateScheduleInvoice { get; set; }
        public ICommand CreateScheduleEstimate { get; set; }
        public ICommand EditDiscountForCustomer { get; set; }
        public ICommand EditDiscountForCustomerEstimate { get; set; }
        public ICommand SaveReOpenScheduleDate { get; set; }
        public ICommand SubmitSchInvoiceOrEstimate { get; set; }
        public ICommand SubmitCustInvoiceOrEstimate { get; set; }
        public ICommand OpenCustomerDetails { get; set; }
        //public ICommand ConvertToInvoice { get; set; }
        public ICommand CreditPayment { get; set; }
        public ICommand CashPayment { get; set; }
        public ICommand ScheduleDetailsformList { get; set; }
        public ICommand StartScheduleOutSide { get; set; }
        public ICommand EndScheduleOutSide { get; set; }
        public ICommand SelecteNewMaterialReceipt { get; set; }
        public ICommand ReturnBackFromScheduleImages { get; set; }
        public ICommand RemoveEmployee { get; set; }
        public ICommand RemoveInvoiceDate { get; set; }
        public ICommand RemoveEstimateDate { get; set; }
        public ICommand OpenMaterialDetails { get; set; }
        public ICommand OpenServiceDetails { get; set; }
        public ICommand OpenMaterialReceiptDetails { get; set; }
        public ICommand OutScheduleImage { get; set; }
        public ICommand OpenFullScreenSchImage { get; set; }
        public ICommand OpenFullScreenSchImageBeforeInsert { get; set; }
        public ICommand SelectedDispatch { get; set; }
        public ICommand ChangeTextSearchJobs { get; set; }
        public ICommand FullScreenNote { get; set; }
        public ICommand CallCustomer { get; set; }
        public ICommand MyWay { get; set; }



        public SchedulesViewModel()
        {
            BranchIdVM = Helpers.Settings.UserRole == "4" ? int.Parse(Helpers.Settings.AccountId) : int.Parse(Helpers.Settings.BranchId);

            CustomerDetails = new CustomersModel();
            ScheduleDetails = new SchedulesModel();
            EmpCategory = new EmployeesCategoryModel();
            OneEmployee = new EmployeeModel();
            LstEmpInOneCategory = new ObservableCollection<EmployeeModel>();
            LstSchedules = new ObservableCollection<SchedulesModel>();
            ScheduleDetails.LstScheduleItemsServices = new List<ScheduleItemsServicesModel>();
            ScheduleDetails.LstFreeServices = new List<ScheduleItemsServicesModel>();
            CustomerDetails.LstCustItemsServices = new List<ScheduleItemsServicesModel>();
            ScheduleDetails.LstMaterialReceipt = new List<ScheduleMaterialReceiptModel>();
            OnePictureModel = new SchedulePicturesModel();
            OnePriorityModel = new PriorityModel();
            LstServices = new ObservableCollection<ItemsServicesModel>();
            SelectedService = new ItemsServicesModel();
            LstATwoPictures = new ObservableCollection<SchedulePicturesModel>();
            SelectedEmployeeAddDate = new EmployeeModel();

            LstItems = new ObservableCollection<ScheduleItemsServicesModel>();
            LstItemsEstimate = new ObservableCollection<ScheduleItemsServicesModel>();
            LstItemsInvoice = new ObservableCollection<ScheduleItemsServicesModel>();
            LstMaterialReceipt = new ObservableCollection<ScheduleMaterialReceiptModel>();
            LstColors = new ObservableCollection<SheetColorModel>();
            LstEmpCategory = new ObservableCollection<EmployeesCategoryModel>();
            CalendarDataToday = new ObservableCollection<SchedulesModel>();
            ScheduleDetailsformList = new Command<SchedulesModel>(OnScheduleDetailsformList);
            StartScheduleOutSide = new Command<SchedulesModel>(OnStartScheduleOutSide);
            EndScheduleOutSide = new Command<SchedulesModel>(OnEndScheduleOutSide);
            RemoveEmployee = new Command<ScheduleEmployeesModel>(OnRemoveEmployee);
            ChangeTextSearchJobs = new Command<string>(OnChangeTextSearchJobs);

            GetPerrmission();
            GetAllSchedules();
        }

        public SchedulesViewModel(CustomersModel model)
        {
            ShowQty = false; //New Schedule
            if (Controls.StaticMembers.WayAfterChooseCust == 1 || Controls.StaticMembers.WayAfterChooseCust == 2)
            {
                ShowQty = true; //New Estimate Or Invoice
            }
            SchedulePage = 0; //New Schedule
            ScheduleDetails = new SchedulesModel();

            GetPerrmission();

            Init();

            //StrEmployees = "Choose Employees";
            ScheduleDate = DateTime.Now;

            InvoiceDate = DateTime.Now;
            BranchName = Helpers.Settings.BranchName;

            //Chech the year now because change value for House details
            CheckHouseDataCust(model);

            //if (!string.IsNullOrEmpty(model.EstimedValue) && model.EstimedValue.StartsWith("$") != true)
            //{
            //    model.EstimedValue = string.Format("${0:#,0.#}", float.Parse(model.EstimedValue));
            //}

            CustomerDetails = model;

            AmountOrPersent = CustomerDetails.MemeberType == false ? false : CustomerDetails.MemberDTO == null ? false : CustomerDetails.MemberDTO.MemberType == true ? true : false;

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
            SelecteNewItems = new Command<SchedulesModel>(OnSelecteNewItems);
            SelecteNewMaterialReceipt = new Command<SchedulesModel>(OnSelecteNewMaterialReceipt);
            SelectedEmpCategory = new Command<EmployeesCategoryModel>(OnSelectedEmpCategory);
            SelectedEmpInOneCategory = new Command<ObservableCollection<EmployeeModel>>(OnSelectedEmpInOneCategory);
            SelectedSubmitSchedule = new Command<SchedulesModel>(OnSelectedSubmitSchedule);
            RemoveItem = new Command<ScheduleItemsServicesModel>(OnRemoveItemAsync);
            RemoveEmployee = new Command<ScheduleEmployeesModel>(OnRemoveEmployee);
            //RemoveItemfromList = new Command<ScheduleItemsServicesModel>(OnRemoveItemfromList);
            OpenEmployeesInOneCategory = new Command(OnOpenEmployeesInOneCategory);
            OpenEstimateScheduleDates = new Command(OnOpenEstimateScheduleDates);
            OpenInvoiceScheduleDates = new Command(OnOpenInvoiceScheduleDates);
        }

        //One Schedule Details Or From CallViewModel
        public SchedulesViewModel(int SchedulId, int ScheduleDateId)
        {
            ShowQty = false; //Old Schedule 
            IsShowAllSchedule = true; // Show all schedule details
            Init();

            GetOneScheduleDetails(SchedulId, ScheduleDateId);
        }

        //Schedule Update
        public SchedulesViewModel(SchedulesModel model)
        {
            //&& model.Recurring == false //old Check
            IsReOpen = ((String.IsNullOrEmpty(model.OneScheduleDate.StartTime) == true && model.OneScheduleDate.Status == 0)) ? true : false; //Show ReOpen Button If Don't Start Job after NotServiced only
            OldResonNotServiced = model.OneScheduleDate.Reasonnotserve;
            ShowQty = true; //Invoice Page
            GetPerrmission();

            if (model.LstScheduleItemsServices.Count > 4)
            {
                LstHeight = 1;
            }

            InitData(model);

            IsShowScheduleDates = true; //Show all Schedules Dates
            GetScheduleDates(model.Id, 1); //All Schedule Dates

            AmountOrPersent = CustomerDetails.MemeberType == false ? false : CustomerDetails.MemberDTO == null ? false : CustomerDetails.MemberDTO.MemberType == true ? true : false;

            OpenCustomerDetails = new Command<CustomersModel>(OnOpenCustomerDetails);
            ReturnBackFromScheduleImages = new Command<SchedulesModel>(OnReturnBackFromScheduleImages);
            AddScheduleDate = new Command<SchedulesModel>(OnAddScheduleDate);
            OutScheduleImage = new Command<SchedulePicturesModel>(OnOutScheduleImage);
            OpenFullScreenSchImage = new Command<string>(OnOpenFullScreenSchImage);
            OpenFullScreenSchImageBeforeInsert = new Command<ImageSource>(OnOpenFullScreenSchImageBeforeInsert);
            SelectedDispatch = new Command<SchaduleDateModel>(OnSelectedDispatch);

            ScheduleAddDate = DateTime.Now;
        }

        public SchedulesViewModel(InvoiceModel model, CustomersModel Cust)
        {
            GetPerrmission();

            OneInvoice = model;
            CustomerDetails = Cust;

            CreditPayment = new Command<InvoiceModel>(OnCreditPayment);
            CashPayment = new Command<InvoiceModel>(OnCashPayment);
        }

        async void Init()
        {
            CustomerDetails = new CustomersModel();
            ScheduleDetails = new SchedulesModel();
            EmpCategory = new EmployeesCategoryModel();
            OneEmployee = new EmployeeModel();
            LstItems = new ObservableCollection<ScheduleItemsServicesModel>();
            LstFreeServices = new ObservableCollection<ScheduleItemsServicesModel>();
            LstMaterialReceipt = new ObservableCollection<ScheduleMaterialReceiptModel>();
            LstItemsInvoice = new ObservableCollection<ScheduleItemsServicesModel>();
            LstItemsEstimate = new ObservableCollection<ScheduleItemsServicesModel>();
            ScheduleDetails.LstScheduleItemsServices = new List<ScheduleItemsServicesModel>();
            ScheduleDetails.LstFreeServices = new List<ScheduleItemsServicesModel>();
            ScheduleDetails.LstFirstCreateServices = new List<ScheduleItemsServicesModel>();
            ScheduleDetails.LstScheduleEmployeeDTO = new List<ScheduleEmployeesModel>();
            CustomerDetails.LstCustItemsServices = new List<ScheduleItemsServicesModel>();
            LstColors = new ObservableCollection<SheetColorModel>();
            LstEmpCategory = new ObservableCollection<EmployeesCategoryModel>();
            LstEmpInOneCategory = new ObservableCollection<EmployeeModel>();
            OneInvoice = new InvoiceModel();
            OneInvoice.LstInvoiceItemServices = new List<InvoiceItemServicesModel>();
            OneEstimate = new EstimateModel();
            OneEstimate.LstEstimateItemServices = new List<EstimateItemServicesModel>();
            LstPriority = new ObservableCollection<PriorityModel>();
            LstEmps = new ObservableCollection<ScheduleEmployeesModel>();
            LstServices = new ObservableCollection<ItemsServicesModel>();
            SelectedService = new ItemsServicesModel();
            LstATwoPictures = new ObservableCollection<SchedulePicturesModel>();
            LstEstimateSchaduleDates = new ObservableCollection<SchaduleDateModel>();
            LstEstimateSchaduleDatesActual = new ObservableCollection<SchaduleDateModel>();
            LstInvoiceSchaduleDates = new ObservableCollection<SchaduleDateModel>();
            LstInvoiceSchaduleDatesActual = new ObservableCollection<SchaduleDateModel>();
            LstInvoiceDates = new ObservableCollection<SchaduleDateModel>();
            LstEstimateDates = new ObservableCollection<SchaduleDateModel>();

            //Schdule Date
            ScheduleDetails.OneScheduleDate = new SchaduleDateModel();
            OneScheduleDate = new SchaduleDateModel();

            OnePictureModel = new SchedulePicturesModel();
            BranchIdVM = int.Parse(Helpers.Settings.BranchId);

            SelecteNewItems = new Command<SchedulesModel>(OnSelecteNewItems);
            SelecteNewFreeService = new Command<SchedulesModel>(OnSelecteNewFreeService);
            SelecteNewMaterialReceipt = new Command<SchedulesModel>(OnSelecteNewMaterialReceipt);
            SelecteNewItemsEstimate = new Command<SchedulesModel>(OnSelecteNewItemsEstimate);
            SelectedEmpCategory = new Command<EmployeesCategoryModel>(OnSelectedEmpCategory);
            SelectedEmpInOneCategory = new Command<ObservableCollection<EmployeeModel>>(OnSelectedEmpInOneCategory);
            SelectedSubmitSchedule = new Command<SchedulesModel>(OnSelectedSubmitSchedule);
            RemoveItem = new Command<ScheduleItemsServicesModel>(OnRemoveItemAsync);
            RemoveService = new Command<ScheduleItemsServicesModel>(OnRemoveService);
            RemoveMaterialReceipt = new Command<ScheduleMaterialReceiptModel>(OnRemoveMaterialReceipt);
            RemoveItemEstimate = new Command<ScheduleItemsServicesModel>(OnRemoveItemEstimate);
            //RemoveItemfromList = new Command<ScheduleItemsServicesModel>(OnRemoveItemfromList);
            OpenEmployeesInOneCategory = new Command(OnOpenEmployeesInOneCategory);
            OpenEstimateScheduleDates = new Command(OnOpenEstimateScheduleDates);
            OpenInvoiceScheduleDates = new Command(OnOpenInvoiceScheduleDates);
            SelectJobDetails = new Command<SchedulesModel>(OnSelectJobDetails);
            //AddScheduleDate = new Command<SchedulesModel>(OnAddScheduleDate);
            SaveResponNotServiceScheduleDate = new Command<SchaduleDateModel>(OnSaveResponNotServiceScheduleDate);
            DoneScheduleDate = new Command<SchaduleDateModel>(OnDoneScheduleDate);
            SelecteCamSchedulePhoto = new Command(OnSelecteCamSchedulePhoto);
            SelectePickSchedulePhoto = new Command(OnSelectePickSchedulePhoto);
            DeleteSchedulePhoto = new Command<SchedulePicturesModel>(OnDeleteSchedulePhoto);
            OpenImages = new Command<SchedulesModel>(OnOpenImages);
            OpenAddImagesPopup = new Command<SchedulesModel>(OnOpenAddImagesPopup);
            DonePictures = new Command<SchedulesModel>(OnDonePictures);
            CreateScheduleInvoice = new Command<SchedulesModel>(OnCreateScheduleInvoice);
            CreateScheduleEstimate = new Command<SchedulesModel>(OnCreateScheduleEstimate);
            EditDiscountForCustomer = new Command<CustomersModel>(OnEditDiscountForCustomer);
            EditDiscountForCustomerEstimate = new Command<CustomersModel>(OnEditDiscountForCustomerEstimate);
            SaveReOpenScheduleDate = new Command<SchaduleDateModel>(OnSaveReOpenScheduleDate);
            SubmitSchInvoiceOrEstimate = new Command<SchedulesModel>(OnSubmitSchInvoiceOrEstimate);
            SubmitCustInvoiceOrEstimate = new Command<CustomersModel>(OnSubmitCustInvoiceOrEstimate);
            OpenCustomerDetails = new Command<CustomersModel>(OnOpenCustomerDetails);
            RemoveEmployee = new Command<ScheduleEmployeesModel>(OnRemoveEmployee);
            RemoveEstimateDate = new Command<SchaduleDateModel>(OnRemoveEstimateDate);
            RemoveInvoiceDate = new Command<SchaduleDateModel>(OnRemoveInvoiceDate);
            OpenMaterialDetails = new Command<ScheduleItemsServicesModel>(OnOpenMaterialDetails);
            OpenMaterialReceiptDetails = new Command<ScheduleMaterialReceiptModel>(OnOpenMaterialReceiptDetails);
            OpenServiceDetails = new Command<ScheduleItemsServicesModel>(OnOpenServiceDetails);
            SelectedDispatch = new Command<SchaduleDateModel>(OnSelectedDispatch);
            OpenFullScreenSchImage = new Command<string>(OnOpenFullScreenSchImage);
            FullScreenNote = new Command<string>(OnFullScreenNote);
            CallCustomer = new Command<CustomersModel>(OnCallCustomer);
            MyWay = new Command<CustomersModel>(OnMyWay);

            //ConvertToInvoice = new Command<EstimateModel>(OnConvertToInvoice);

            LstColors.Add(new SheetColorModel() { ColorName = "Red", ColorHex = "#eb4034" });
            LstColors.Add(new SheetColorModel() { ColorName = "Blue", ColorHex = "#2f6fde" });
            LstColors.Add(new SheetColorModel() { ColorName = "Green", ColorHex = "#23b007" });
            LstColors.Add(new SheetColorModel() { ColorName = "Black", ColorHex = "#272927" });
            LstColors.Add(new SheetColorModel() { ColorName = "Gray", ColorHex = "#878787" });
            LstColors.Add(new SheetColorModel() { ColorName = "Brwon", ColorHex = "#7d654c" });

            LstPriority.Add(new PriorityModel() { Id = 1, Name = "Normal" });
            LstPriority.Add(new PriorityModel() { Id = 2, Name = "Urgent" });

            OnePriorityModel = new PriorityModel() { Id = 1, Name = "Normal" };

            StrEstimateDates = "Choose Schedule Dates";
            StrInvoiceDates = "Choose Schedule Dates";

            await GetServices();
            await GetEmpCategories();
        }

        async void InitData(SchedulesModel model)
        {
            ShowImages = true;
            SchedulePage = 1; //Update Schedule 

            Init();

            ScheduleDetails = model;
            CustomerDetails = model.CustomerDTO;

            if (ScheduleDetails.CustomerDTO.MemeberType == true)
            {
                if (ScheduleDetails.CustomerDTO.MemberDTO != null)
                {
                    Discount = ScheduleDetails.CustomerDTO.MemberDTO.MemberValue;
                }
            }
            else
            {
                Discount = ScheduleDetails.CustomerDTO.Discount;
            }

            if (Discount == null)
            {
                Discount = 0;
            }


            if (model.LstScheduleItemsServices.Count > 0)
            {
                LstItems = new ObservableCollection<ScheduleItemsServicesModel>(model.LstScheduleItemsServices);
            }

            //invoice
            SubTotal = 0;
            Net = 0;
            Paid = 0;
            TotalDue = 0;

            //estimate
            SubTotalEst = 0;
            NetEst = 0;
            PaidEst = 0;
            TotalDueEst = 0;

            //if (model.LstFreeServices.Count > 0)
            //{
            //    LstItemsInvoice = new ObservableCollection<ScheduleItemsServicesModel>(model.LstFreeServices.Where(s => s.TypeMaterial_Services == null).ToList());
            //    TotalInvoice(model, CustomerDetails);
            //}
            //else
            //{
            //    SubTotal = 0;
            //    Net = 0;
            //    Paid = 0;
            //    TotalDue = 0;
            //}

            //if (model.LstFreeServices.Count > 0)
            //{
            //    //LstItemsEstimate = new ObservableCollection<ScheduleItemsServicesModel>(model.LstScheduleItemsServices); //old code
            //    LstItemsEstimate = new ObservableCollection<ScheduleItemsServicesModel>(model.LstFreeServices.Where(s => s.TypeMaterial_Services == null).ToList());
            //    TotalEstimate(model, CustomerDetails);
            //}
            //else
            //{
            //    SubTotalEst = 0;
            //    NetEst = 0;
            //    PaidEst = 0;
            //    TotalDueEst = 0;
            //}

            if (model.LstFreeServices.Count > 0)
            {
                LstFreeServices = new ObservableCollection<ScheduleItemsServicesModel>(model.LstFreeServices);
            }

            if (model.LstMaterialReceipt.Count > 0)
            {
                LstMaterialReceipt = new ObservableCollection<ScheduleMaterialReceiptModel>(model.LstMaterialReceipt);
            }

            //if (model.LstScheduleItemsServices != null)
            //{
            //    TotalInvoice(model, CustomerDetails);

            //    LstItems = new ObservableCollection<ScheduleItemsServicesModel>(model.LstScheduleItemsServices);

            //    TotalEstimate(model, CustomerDetails);

            //    LstItemsEstimate = new ObservableCollection<ScheduleItemsServicesModel>(model.LstScheduleItemsServices);
            //}


            InvoiceDate = DateTime.Now;

            //ScheduleDate = DateTime.Parse(model.StartDate);
            //TimeFrom = new TimeSpan(model.TimeHourFrom, model.TimeMinFrom, 0);
            //TimeTo = new TimeSpan(model.TimeHourTo, model.TimeMinTo, 0);

            ScheduleDate = DateTime.Parse(model.OneScheduleDate.Date);
            TimeFrom = new TimeSpan(model.OneScheduleDate.TimeHourFrom, model.OneScheduleDate.TimeMinFrom, 0);
            TimeTo = new TimeSpan(model.OneScheduleDate.TimeHourTo, model.OneScheduleDate.TimeMinTo, 0);
            //LstEmpInOneCategory = new ObservableCollection<EmployeeModel>(model.LstEmployeeDTO);
            OnePriorityModel = LstPriority.Where(x => x.Id == model.PriorityId).FirstOrDefault();

            BranchName = Helpers.Settings.BranchName;

            //if (ScheduleDetails.CustomerDTO != null)
            //    Discount = ScheduleDetails.CustomerDTO.Discount = ScheduleDetails.CustomerDTO.Discount == null ? 0 : ScheduleDetails.CustomerDTO.Discount;



            //Schedule Pictures
            if (model.LstSchedulePictures.Count != 0)
            {
                LstAllPictures = new ObservableCollection<SchedulePicturesModel>(model.LstSchedulePictures);
                LstNewPictures = new ObservableCollection<SchedulePicturesModel>(model.LstSchedulePictures.Where(x => x.Id == 0).ToList());
                //PhotosCount = model.LstSchedulePictures.Count - 2;
                await CalcSchPhotoCount(model.LstSchedulePictures.Count);
            }

            //PhotosCount = model.CountPhotos.Value - 2;
            await CalcSchPhotoCount(model.CountPhotos.Value);

            if (model.GetPictures == true)
            {
                GetPictuers(model.ScheduleDateId);

            }

            StartTime = (ScheduleDetails.OneScheduleDate.StartTime == null) ? "No start yet" : ScheduleDetails.OneScheduleDate.StartTime;
            EndTime = (ScheduleDetails.OneScheduleDate.EndTime == null) ? "No end yet" : ScheduleDetails.OneScheduleDate.EndTime;

            SpentHours = (ScheduleDetails.OneScheduleDate.SpentTimeHour == null) ? "Wait job finish" : ScheduleDetails.OneScheduleDate.SpentTimeHour;
            SpentMin = (ScheduleDetails.OneScheduleDate.SpentTimeMin == null) ? "Wait job finish" : ScheduleDetails.OneScheduleDate.SpentTimeMin;

            //if (String.IsNullOrEmpty(ScheduleDetails.OneScheduleDate.StartTime) != true)
            //    StartTime = TimeSpan.Parse(ScheduleDetails.OneScheduleDate.StartTime);
            //if (String.IsNullOrEmpty(ScheduleDetails.OneScheduleDate.EndTime) != true)
            //    EndTime = TimeSpan.Parse(ScheduleDetails.OneScheduleDate.EndTime);
            //if (String.IsNullOrEmpty(ScheduleDetails.OneScheduleDate.SpentTimeHour) != true)
            //    SpentHours = ScheduleDetails.OneScheduleDate.SpentTimeHour;
            //if (String.IsNullOrEmpty(ScheduleDetails.OneScheduleDate.SpentTimeMin) != true)
            //    SpentMin = ScheduleDetails.OneScheduleDate.SpentTimeMin;

            OneScheduleDate = ScheduleDetails.OneScheduleDate;

            SelectedColor = LstColors.Where(x => x.ColorHex == model.CalendarColor).Select(c => c.IsChecked = true).FirstOrDefault();

        }


        async void CheckHouseDataCust(CustomersModel model)
        {
            if (!string.IsNullOrEmpty(model.YearEstimedValue))
            {
                if (DateTime.Now.Year - int.Parse(model.YearEstimedValue) > 1)
                {
                    model = await Controls.StartData.GetAddressDetails(model);
                }
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

        async void OnScheduleDetailsformList(SchedulesModel model)
        {
            var VM = new SchedulesViewModel(model.Id, model.ScheduleDateId);
            //var page = new NewSchedulePage();
            var page = new ScheduleDetailsPage();
            page.BindingContext = VM;
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

        public void TotalInvoice(SchedulesModel SchModel, CustomersModel CustModel)
        {
            if (SchModel.Id != 0)
            {
                //decimal? SumCost = SchModel.LstScheduleItemsServices.Where(x => x.CostRate > 0 && x.Out == false).Sum(s => s.CostRate * s.Quantity);
                decimal? SumCost = LstItemsInvoice.Where(x => x.CostRate > 0 && x.Out == false).Sum(s => s.CostRate * s.Quantity);

                //decimal? DiscountVal = (SchModel.CustomerDTO != null && SchModel.CustomerDTO.MemeberType == false) ? (SumCost * SchModel.CustomerDTO.Discount / 100) : (SchModel.CustomerDTO != null && SchModel.CustomerDTO.MemeberType == true) ? (SumCost - SchModel.CustomerDTO.Discount) : 0;
                decimal? DiscountVal = (SchModel.CustomerDTO != null && SchModel.CustomerDTO.MemeberType == false) ? Discount != 0 ? (SumCost * Discount / 100) : 0 : (SchModel.CustomerDTO != null && SchModel.CustomerDTO.MemberDTO == null) ? Discount != 0 ? (SumCost * Discount / 100) : 0 : (SchModel.CustomerDTO != null && SchModel.CustomerDTO.MemberDTO.MemberType == false) ? Discount != 0 ? (SumCost * Discount / 100) : 0 : (Discount);

                decimal? TaxValue = SchModel.CustomerDTO.TaxDTO != null ? (SumCost - DiscountVal) * SchModel.CustomerDTO.TaxDTO.Rate / 100 : 0;

                SubTotal = Math.Round(SumCost.Value, 2, MidpointRounding.ToEven);
                Paid = 0;
                Net = Math.Round(((SumCost - DiscountVal) + TaxValue).Value, 2, MidpointRounding.ToEven);
                TotalDue = Math.Round(((SumCost - DiscountVal) + TaxValue - Paid).Value, 2, MidpointRounding.ToEven);
            }

            if (CustModel.Id != 0 && SchModel.Id == 0)
            {
                //decimal? SumCost = CustModel.LstCustItemsServices.Where(x => x.CostRate > 0 && x.Out == false).Sum(s => s.CostRate * s.Quantity);
                decimal? SumCost = LstItemsInvoice.Where(x => x.CostRate > 0 && x.Out == false).Sum(s => s.CostRate * s.Quantity);

                decimal? DiscountVal = (CustModel.MemeberType == false) ? Discount != 0 ? (SumCost * Discount / 100) : 0 : (CustModel.MemberDTO == null) ? Discount != 0 ? (SumCost * Discount / 100) : 0 : (CustModel.MemberDTO.MemberType == false) ? (SumCost * Discount / 100) : (Discount);

                decimal? TaxValue = CustModel.TaxDTO != null ? (SumCost - DiscountVal) * CustModel.TaxDTO.Rate / 100 : 0;

                SubTotal = Math.Round(SumCost.Value, 2, MidpointRounding.ToEven);
                Paid = 0;
                Net = Math.Round(((SumCost - DiscountVal) + TaxValue).Value, 2, MidpointRounding.ToEven);
                TotalDue = Math.Round(((SumCost - DiscountVal) + TaxValue - Paid).Value, 2, MidpointRounding.ToEven);
            }
            //if (CustModel.Id != 0 && SchModel.Id == 0)
            //{
            //    decimal? SumCost = CustModel.LstCustItemsServices.Where(x => x.CostRate > 0 && x.Out == false).Sum(s => s.CostRate * s.Quantity);

            //    decimal? DiscountVal = SchModel.CustomerDTO != null ? SchModel.CustomerDTO.MemberDTO != null ? (SumCost * SchModel.CustomerDTO.MemberDTO.MemberValue / 100) : 0 : 0;
            //    decimal? TaxValue = SchModel.CustomerDTO != null ? SchModel.CustomerDTO.TaxDTO != null ? (SumCost - DiscountVal) * SchModel.CustomerDTO.TaxDTO.Rate / 100 : 0 : 0;
            //    SubTotal = Math.Round(SumCost.Value, 2, MidpointRounding.ToEven);
            //    Paid = 0;
            //    Net = Math.Round(((SumCost - DiscountVal) + TaxValue).Value, 2, MidpointRounding.ToEven);
            //    TotalDue = Math.Round(((SumCost - DiscountVal) + TaxValue - Paid).Value, 2, MidpointRounding.ToEven);
            //}

        }

        public void TotalEstimate(SchedulesModel SchModel, CustomersModel CustModel)
        {
            if (SchModel.Id != 0)
            {
                //decimal? SumCost = SchModel.LstScheduleItemsServices.Where(x => x.CostRate > 0 && x.Out == false).Sum(s => s.CostRate * s.Quantity);
                decimal? SumCost = LstItemsEstimate.Where(x => x.CostRate > 0 && x.Out == false).Sum(s => s.CostRate * s.Quantity);

                //decimal? DiscountVal = (SchModel.CustomerDTO != null && SchModel.CustomerDTO.MemeberType == false) ? (SumCost * SchModel.CustomerDTO.Discount / 100) : (SchModel.CustomerDTO != null && SchModel.CustomerDTO.MemeberType == true) ? (SumCost - SchModel.CustomerDTO.Discount) : 0;
                decimal? DiscountVal = (SchModel.CustomerDTO != null && SchModel.CustomerDTO.MemeberType == false) ? Discount != 0 ? (SumCost * Discount / 100) : 0 : (SchModel.CustomerDTO != null && SchModel.CustomerDTO.MemberDTO == null) ? Discount != 0 ? (SumCost * Discount / 100) : 0 : (SchModel.CustomerDTO != null && SchModel.CustomerDTO.MemberDTO.MemberType == false) ? Discount != 0 ? (SumCost * Discount / 100) : 0 : (Discount);

                decimal? TaxValue = SchModel.CustomerDTO.TaxDTO != null ? (SumCost - DiscountVal) * SchModel.CustomerDTO.TaxDTO.Rate / 100 : 0;

                SubTotalEst = Math.Round(SumCost.Value, 2, MidpointRounding.ToEven);
                PaidEst = 0;
                NetEst = Math.Round(((SumCost - DiscountVal) + TaxValue).Value, 2, MidpointRounding.ToEven);
                TotalDueEst = Math.Round(((SumCost - DiscountVal) + TaxValue - PaidEst).Value, 2, MidpointRounding.ToEven);
            }

            if (CustModel.Id != 0 && SchModel.Id == 0)
            {
                //decimal? SumCost = CustModel.LstCustItemsServices.Where(x => x.CostRate > 0 && x.Out == false).Sum(s => s.CostRate * s.Quantity);
                decimal? SumCost = LstItemsEstimate.Where(x => x.CostRate > 0 && x.Out == false).Sum(s => s.CostRate * s.Quantity);

                decimal? DiscountVal = (CustModel.MemeberType == false) ? Discount != 0 ? (SumCost * Discount / 100) : 0 : (CustModel.MemberDTO == null) ? Discount != 0 ? (SumCost * Discount / 100) : 0 : (CustModel.MemberDTO.MemberType == false) ? (SumCost * Discount / 100) : (Discount);

                decimal? TaxValue = CustModel.TaxDTO != null ? (SumCost - DiscountVal) * CustModel.TaxDTO.Rate / 100 : 0;

                SubTotalEst = Math.Round(SumCost.Value, 2, MidpointRounding.ToEven);
                PaidEst = 0;
                NetEst = Math.Round(((SumCost - DiscountVal) + TaxValue).Value, 2, MidpointRounding.ToEven);
                TotalDueEst = Math.Round(((SumCost - DiscountVal) + TaxValue - PaidEst).Value, 2, MidpointRounding.ToEven);
            }
        }

        //Get all Schedules in Branch
        async void GetAllSchedules()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<ObservableCollection<SchedulesModel>>(string.Format("api/Schedules/GetSchedules?" + "AccountId=" + Helpers.Settings.AccountId + "&" + "EmpId=" + Helpers.Settings.UserId + "&" + "EmpRole=" + Controls.StartData.EmployeeDataStatic.UserRole + "&" + "lstEmp=" + Helpers.Settings.UserEmployees + "&" + "TextSearch="), UserToken);

                if (json != null)
                {
                    //GetPerrmission();

                    //var cc = Controls.StartData.EmployeeDataStatic;
                    if (Controls.StartData.EmployeeDataStatic.ActiveAllScdTr_FaorTrOnly == false) //For Dispatch
                    {
                        LstSchedules = new ObservableCollection<SchedulesModel>(json.Where(x => x.OneScheduleDate.Active == true).ToList());
                    }
                    else
                    {
                        LstSchedules = json;
                    }

                    string day = DateTime.Now.ToString("yyyy-MM-dd");
                    CalendarDataToday = new ObservableCollection<SchedulesModel>(LstSchedules.Where(x => x.StartDate == day).ToList());

                    await GetEvents(LstSchedules);
                }

                UserDialogs.Instance.HideLoading();
            }
        }


        async Task GetEvents(ObservableCollection<SchedulesModel> Lstschedules)
        {
            if (LstSchedules.Count > 0)
            {
                LstScheduleEvevts = new CalendarEventCollection();
                ObservableCollection<SchedulesModel> groupedList = new ObservableCollection<SchedulesModel>();

                string Date = "";

                foreach (var item in LstSchedules.OrderBy(appointment => DateTime.Parse(appointment.StartDate)))
                {
                    if (item.StartDate != Date)
                    {
                        groupedList.Add(item);
                        Date = item.StartDate;
                    }
                }

                foreach (var group in groupedList)
                {
                    CalendarInlineEvent event1 = new CalendarInlineEvent();

                    DateTime Startday = DateTime.Parse(group.StartDate);
                    DateTime StartTime = DateTime.Parse(group.Time);

                    DateTime Endday = DateTime.Parse(group.StartDate);
                    DateTime EndTime = DateTime.Parse(group.TimeEnd);

                    event1.StartTime = new DateTime(Startday.Year, Startday.Month, Startday.Day, StartTime.Hour, StartTime.Minute, StartTime.Second);
                    event1.EndTime = new DateTime(Endday.Year, Endday.Month, Endday.Day, EndTime.Hour, EndTime.Minute, EndTime.Second);
                    event1.Subject = $"Job Title: {group.Title}";
                    event1.Color = Color.FromHex("#538dd4");

                    LstScheduleEvevts.Add(event1);
                }
            }
        }

        async void OnChangeTextSearchJobs(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                IsShowSearchByItem = true;
            }
            else
            {
                if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
                {
                    IsShowSearchByItem = false;
                    string UserToken = await _service.UserToken();
                    var json = await ORep.GetAsync<ObservableCollection<SchedulesModel>>(string.Format("api/Schedules/GetSchedules?" + "AccountId=" + Helpers.Settings.AccountId + "&" + "EmpId=" + Helpers.Settings.UserId + "&" + "EmpRole=" + Controls.StartData.EmployeeDataStatic.UserRole + "&" + "lstEmp=" + Helpers.Settings.UserEmployees + "&" + "TextSearch=" + text), UserToken);

                    if (json != null)
                    {
                        if (Controls.StartData.EmployeeDataStatic.ActiveAllScdTr_FaorTrOnly == false) //For Dispatch
                        {
                            LstSchedulesSearch = new ObservableCollection<SchedulesModel>(json.Where(x => x.OneScheduleDate.Active == true).ToList());
                        }
                        else
                        {
                            LstSchedulesSearch = json;
                        }
                    }
                }
            }
        }

        async Task GetServices()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<ObservableCollection<ItemsServicesModel>>(string.Format("api/Schedules/GetServices?" + "AccountId=" + Helpers.Settings.AccountId), UserToken);

                if (json != null)
                {
                    LstServices = json;

                    if (ScheduleDetails.OneScheduleService != null && ScheduleDetails.OneScheduleService.ScheduleDateId == null)
                    {
                        SelectedService = LstServices.Where(x => x.Id == ScheduleDetails.OneScheduleService.ItemsServicesId).FirstOrDefault();
                    }
                }

                UserDialogs.Instance.HideLoading();
            }
        }

        async void GetScheduleDates(int ScheduleId, int Type)
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<ObservableCollection<SchaduleDateModel>>(string.Format("api/Schedules/GetScheduleDates?" + "ScheduleId=" + ScheduleId + "&" + "Type=" + Type), UserToken);

                if (json != null)
                {
                    if (json.Count == 1)
                    {
                        LstEstimateSchaduleDatesActual = json;
                        StrEstimateDates = json.FirstOrDefault().Date;
                        LstInvoiceSchaduleDatesActual = json;
                        StrInvoiceDates = json.FirstOrDefault().Date;
                        LstEstimateSchaduleDates = json;
                        LstInvoiceSchaduleDates = json;
                        IsShowScheduleDates = false;
                    }
                    else
                    {
                        LstEstimateSchaduleDates = json;
                        LstInvoiceSchaduleDates = json;
                    }

                }

                UserDialogs.Instance.HideLoading();
            }
        }


        //Get One Schedule Details
        public async Task GetOneScheduleDetails(int ScheduleId, int ScheduleDateId)
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var Schedule = await ORep.GetAsync<SchedulesModel>(string.Format("api/Schedules/GetScheduleDetails?" + "ScheduleId=" + ScheduleId + "&" + "ScheduleDateId=" + ScheduleDateId), UserToken);

                if (Schedule != null)
                {
                    Schedule.CustomerDTO = Schedule.CustomerDTO == null ? new CustomersModel() : Schedule.CustomerDTO;
                    Schedule.OneScheduleDate = Schedule.OneScheduleDate == null ? new SchaduleDateModel() : Schedule.OneScheduleDate;


                    Schedule.LstScheduleEmployeeDTO = Schedule.LstScheduleEmployeeDTO == null ? new List<ScheduleEmployeesModel>() : Schedule.LstScheduleEmployeeDTO;

                    Schedule.LstScheduleItemsServices = Schedule.LstScheduleItemsServices == null ? new List<ScheduleItemsServicesModel>() : Schedule.LstScheduleItemsServices;
                    Schedule.LstSchedulePictures = Schedule.LstSchedulePictures == null ? new List<SchedulePicturesModel>() : Schedule.LstSchedulePictures;
                    Schedule.LstMaterialReceipt = Schedule.LstMaterialReceipt == null ? new List<ScheduleMaterialReceiptModel>() : Schedule.LstMaterialReceipt;

                    ScheduleDetails = Schedule;

                    //LstATwoPictures = new ObservableCollection<SchedulePicturesModel>(ScheduleDetails.LstSchedulePictures);

                    if (ScheduleDetails.LstScheduleItemsServices.Count > 4)
                    {
                        LstHeight = 1;
                    }

                    if (ScheduleDetails.EstimateDTO != null)
                    {
                        ShowEstimateButton = true;
                    }

                    InitData(ScheduleDetails);

                    LstEmps = new ObservableCollection<ScheduleEmployeesModel>(ScheduleDetails.LstScheduleEmployeeDTO);

                    foreach (var mod in ScheduleDetails.LstScheduleEmployeeDTO)
                    {
                        StrEmployeesId += ("," + mod.EmpId);
                    }
                    StrEmployeesId = StrEmployeesId?.Remove(0, 1);
                }

                UserDialogs.Instance.HideLoading();
            }
        }

        //Get Employees Category
        public async Task GetEmpCategories()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<ObservableCollection<EmployeesCategoryModel>>(string.Format("api/Employee/GetEmpCategory?" + "AccountId=" + Helpers.Settings.AccountId), UserToken);

                if (json != null)
                {
                    LstEmpCategory = json;

                    if (ScheduleDetails?.EmployeeCategoryId != null && ScheduleDetails?.LstScheduleEmployeeDTO.Count > 0)
                    {
                        SelectedCateory = LstEmpCategory.Where(x => x.Id == ScheduleDetails?.EmployeeCategoryId).FirstOrDefault();

                        string str = "";
                        foreach (ScheduleEmployeesModel Emp in ScheduleDetails?.LstScheduleEmployeeDTO)
                        {
                            str += ("," + Emp.EmpUserName);
                        }
                        StrEmployees = str.Remove(0, 1);
                    }
                    else
                    {
                        SelectedCateory = LstEmpCategory.FirstOrDefault();
                    }

                    //if (ScheduleDetails.EmployeeCategoryId != null && ScheduleDetails.LstEmployeeDTO.Count > 0)
                    //{
                    //    SelectedCateory = LstEmpCategory.Where(x => x.Id == ScheduleDetails.EmployeeCategoryId).FirstOrDefault();

                    //    string str = "";
                    //    foreach (EmployeeModel Emp in ScheduleDetails.LstEmployeeDTO)
                    //    {
                    //        str += ("," + Emp.UserName);
                    //    }
                    //    StrEmployees = str.Remove(0, 1);
                    //}
                }

                UserDialogs.Instance.HideLoading();
            }
        }

        // Get Employees in One Category
        async void OnSelectedEmpCategory(EmployeesCategoryModel model)
        {

            EmpCategory = model;

            //StrEmployees = "Choose Employees";
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<ObservableCollection<EmployeeModel>>(string.Format("api/Employee/GetEmpInOneCategory/{0}/{1}/{2}/{3}/{4}", Helpers.Settings.BranchId, EmpCategory.Id, Helpers.Settings.AccountId, Controls.StartData.EmployeeDataStatic.UserRole, Helpers.Settings.UserId), UserToken);

                if (json != null)
                {
                    LstEmpInOneCategory = json;
                }

                UserDialogs.Instance.HideLoading();
            }
        }

        //Get Pictuers
        async void GetPictuers(int ScheduleId)
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<ObservableCollection<SchedulePicturesModel>>(string.Format("api/Schedules/GetPictures?" + "ScheduleId=" + ScheduleId), UserToken);

                if (json != null)
                {
                    LstNewPictures = new ObservableCollection<SchedulePicturesModel>(); //Check if Show Button Done
                    ScheduleDetails.LstSchedulePictures = json.ToList();
                    LstAllPictures = json;

                    SetLstTwoSchedulePhotos(ScheduleDetails.LstSchedulePictures);
                }

                UserDialogs.Instance.HideLoading();
            }
        }


        async void OnCallCustomer(CustomersModel model)
        {
            try
            {
                PhoneDialer.Open(model.Phone1);
            }
            catch (FeatureNotSupportedException ex)
            {
                // Handle the case where the phone dialer is not supported on the device
                await App.Current.MainPage.DisplayAlert("Error", "Phone dialer is not supported on this device.", "OK");
            }
            catch (Exception ex)
            {
                // Handle other errors that might occur
                await App.Current.MainPage.DisplayAlert("Error", "Unable to dial this number.", "OK");
            }

        }

        async void OnMyWay(CustomersModel model)
        {
            IsBusy = true;
            var popupView = new OnMyWayViewModel(model);
            var page = new OnMyWayPopup();
            page.BindingContext = popupView;
            await PopupNavigation.Instance.PushAsync(page);
            IsBusy = false;
        }



        async void OnSelectedDispatch(SchaduleDateModel model)
        {
            IsBusy = true;
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                bool check = await App.Current.MainPage.DisplayAlert("FixPro", "Are you sure that you want to dispatch?", "Yes", "No");

                if (check == true)
                {
                    string UserToken = await _service.UserToken();

                    UserDialogs.Instance.ShowLoading();
                    string Json = await ORep.PutStrAsync("api/Schedules/PutScheduleDispatch", model, UserToken);
                    UserDialogs.Instance.HideLoading();

                    if (!string.IsNullOrEmpty(Json) && Json.Contains("Success Dispatch") == true)
                    {
                        await App.Current.MainPage.DisplayAlert("FixPro", "Schedule dispatched successfully", "Ok");
                        ShowDispatch = 2; //Don't Show Dispatch Button
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("FixPro", "Failed to dispatch this schedule", "Ok");
                    }
                }

            }
            IsBusy = false;
        }

        async void OnReturnBackFromScheduleImages(SchedulesModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            var popupView = new SchedulesViewModel(model.Id, model.OneScheduleDate.Id);
            //var page = new NewSchedulePage();
            var page = new ScheduleDetailsPage();
            page.BindingContext = popupView;
            await App.Current.MainPage.Navigation.PushAsync(page);
            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnSelecteNewItemsEstimate(SchedulesModel model)
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
                    var popupView = new ScheduleItemsServicesViewModel(ShowQty);
                    popupView.ItemClose += async (item) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        ScheduleItemsServicesModel ItemsModel = new ScheduleItemsServicesModel
                        {
                            AccountId = model.AccountId,
                            BrancheId = model.BrancheId,
                            ScheduleId = model.Id,
                            ItemsServicesId = item.Id,
                            ItemsServicesName = item.Name,
                            ItemServiceDescription = item.Description,
                            TaxId = item.TaxId,
                            Tax = item.Tax,
                            CostRate = item.CostperUnit,
                            Total = item.QTYTime != null && item.Tax != null ? (item.CostperUnit * item.QTYTime) + (item.CostperUnit * item.QTYTime * item.Tax / 100) : item.QTYTime == null && item.Tax != null ? item.CostperUnit + (item.CostperUnit * item.QTYTime * item.Tax / 100) : item.QTYTime != null && item.Tax == null ? item.CostperUnit * item.QTYTime : item.CostperUnit,
                            Notes = item.Notes,
                            Active = item.Active,
                            CreateUser = model.CreateUser,
                            CreateDate = item.CreateDate,
                            Taxable = item.Taxable,
                            Quantity = (item.QTYTime == null || item.QTYTime == 0) ? 1 : item.QTYTime,
                            Unit = item.Unit,
                        };

                        if (LstItemsEstimate.Count > 0)
                        {
                            var itm = LstItemsEstimate.Where(x => x.ItemsServicesId == item.Id).FirstOrDefault();
                            if (itm == null)
                            {
                                LstItemsEstimate.Add(ItemsModel);
                            }
                        }
                        else
                        {
                            LstItemsEstimate.Add(ItemsModel);
                        }

                        if (LstItemsEstimate.Count > 4)
                        {
                            LstHeight = 1;
                        }

                        TotalEstimate(model, CustomerDetails);

                        UserDialogs.Instance.HideLoading();
                    };

                    var page = new Views.SchedulePages.NewItemsServicesSchedulePage();
                    page.BindingContext = popupView;
                    await App.Current.MainPage.Navigation.PushAsync(page);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        void OnRemoveItemEstimate(ScheduleItemsServicesModel item)
        {
            IsBusy = true;

            LstItemsEstimate.Remove(item);

            TotalEstimate(ScheduleDetails, CustomerDetails);

            IsBusy = false;
        }

        async void OnSelecteNewItems(SchedulesModel model)
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
                    var popupView = new ScheduleItemsServicesViewModel(ShowQty);
                    popupView.ItemClose += async (item) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        ScheduleItemsServicesModel ItemsModel = new ScheduleItemsServicesModel
                        {
                            AccountId = model.AccountId,
                            BrancheId = model.BrancheId,
                            ScheduleId = model.Id,
                            ScheduleDateId = model.OneScheduleDate.Id,
                            ItemsServicesId = item.Id,
                            ItemsServicesName = item.Name,
                            ItemServiceDescription = item.Description,
                            TaxId = item.TaxId,
                            Tax = item.Tax,
                            CostRate = item.CostperUnit,
                            Price = item.CostperUnit,
                            Total = item.QTYTime != null && item.Tax != null ? (item.CostperUnit * item.QTYTime) + (item.CostperUnit * item.QTYTime * item.Tax / 100) : item.QTYTime == null && item.Tax != null ? item.CostperUnit + (item.CostperUnit * item.QTYTime * item.Tax / 100) : item.QTYTime != null && item.Tax == null ? item.CostperUnit * item.QTYTime : item.CostperUnit,
                            Notes = item.Notes,
                            Active = item.Active,
                            CreateUser = model.CreateUser,
                            CreateDate = item.CreateDate,
                            Taxable = item.Taxable,
                            Quantity = (item.QTYTime == null || item.QTYTime == 0) ? 1 : item.QTYTime,
                            Unit = item.Unit,
                        };

                        if (ShowQty == false)// add material
                        {
                            ScheduleItemsServicesModel scheduleItemsServicesModel = new ScheduleItemsServicesModel();

                            if (LstItems.Count > 0)
                            {
                                var itm = LstItems.Where(x => x.ItemsServicesId == item.Id).FirstOrDefault();
                                if (itm == null)
                                {
                                    scheduleItemsServicesModel = await InsertOneItemService(ItemsModel);
                                    if (scheduleItemsServicesModel != null)
                                    {
                                        LstItems.Add(scheduleItemsServicesModel);
                                    }
                                }
                            }
                            else
                            {
                                scheduleItemsServicesModel = await InsertOneItemService(ItemsModel);
                                if (scheduleItemsServicesModel != null)
                                {
                                    LstItems.Add(scheduleItemsServicesModel);
                                }
                            }
                        }
                        else // add invoice item or Estimate item
                        {
                            var itm2 = LstItemsInvoice.Where(x => x.ItemsServicesId == item.Id).FirstOrDefault();
                            if (itm2 == null)
                            {
                                LstItemsInvoice.Add(ItemsModel);
                            }
                        }

                        if (LstItemsInvoice.Count > 4)
                        {
                            LstHeight = 1;
                        }


                        if (LstItems.Count > 4)
                        {
                            LstHeight = 1;
                        }

                        TotalInvoice(model, CustomerDetails);


                        UserDialogs.Instance.HideLoading();
                    };

                    var page = new Views.SchedulePages.NewItemsServicesSchedulePage();
                    page.BindingContext = popupView;
                    await App.Current.MainPage.Navigation.PushAsync(page);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        async void OnSelecteNewFreeService(SchedulesModel model)
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
                    var popupView = new ScheduleFreeServicesViewModel(false);
                    popupView.ServiceClose += async (service) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        ScheduleItemsServicesModel ServiceModel = new ScheduleItemsServicesModel
                        {
                            AccountId = model.AccountId,
                            BrancheId = model.BrancheId,
                            ScheduleId = model.Id,
                            ScheduleDateId = model.OneScheduleDate.Id,
                            ItemsServicesId = service.Id,
                            ItemsServicesName = service.Name,
                            ItemServiceDescription = service.Description,
                            TaxId = service.TaxId,
                            Tax = service.Tax,
                            CostRate = service.CostperUnit,
                            Price = service.CostperUnit,
                            Total = service.QTYTime != null && service.Tax != null ? (service.CostperUnit * service.QTYTime) + (service.CostperUnit * service.QTYTime * service.Tax / 100) : service.QTYTime == null && service.Tax != null ? service.CostperUnit + (service.CostperUnit * service.QTYTime * service.Tax / 100) : service.QTYTime != null && service.Tax == null ? service.CostperUnit * service.QTYTime : service.CostperUnit,
                            Notes = service.Notes,
                            Active = service.Active,
                            CreateUser = model.CreateUser,
                            CreateDate = service.CreateDate,
                            Taxable = service.Taxable,
                            Quantity = (service.QTYTime == null || service.QTYTime == 0) ? 1 : service.QTYTime,
                            Unit = service.Unit,
                        };

                        ScheduleItemsServicesModel scheduleItemsServicesModel = new ScheduleItemsServicesModel();

                        scheduleItemsServicesModel = await InsertOneFreeService(ServiceModel);
                        if (scheduleItemsServicesModel != null)
                        {
                            LstFreeServices.Add(scheduleItemsServicesModel);
                        }

                        //if (LstFreeServices.Count > 0)
                        //{
                        //    var itm = LstFreeServices.Where(x => x.ItemsServicesId == service.Id).FirstOrDefault();
                        //    if (itm == null)
                        //    {
                        //        scheduleItemsServicesModel = await InsertOneFreeService(ServiceModel);
                        //        if (scheduleItemsServicesModel != null)
                        //        {
                        //            LstFreeServices.Add(scheduleItemsServicesModel);

                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    scheduleItemsServicesModel = await InsertOneFreeService(ServiceModel);
                        //    if (scheduleItemsServicesModel != null)
                        //    {
                        //        LstFreeServices.Add(scheduleItemsServicesModel);
                        //    }
                        //}

                        if (LstFreeServices.Count > 4)
                        {
                            LstHeight = 1;
                        }

                        UserDialogs.Instance.HideLoading();
                    };

                    var page = new Views.SchedulePages.ScheduleFreeServicesPage();
                    page.BindingContext = popupView;
                    await App.Current.MainPage.Navigation.PushAsync(page);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        public async Task<ScheduleItemsServicesModel> InsertOneItemService(ScheduleItemsServicesModel model)
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();
                var json = await ORep.PostDataAsync("api/Schedules/PostScheduleMaterials", model, UserToken);

                if (json != "Bad Request" && json != "api not responding" && json.Contains("Not_Enough") != true && json.Contains("This Invoice Already Exist") != true)
                {
                    var oModel = JsonConvert.DeserializeObject<ScheduleItemsServicesModel>(json);
                    return oModel;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", json, "Ok");
                    return null;
                }
            }
            else
            {
                return null;
            }
        }

        public async Task<ScheduleItemsServicesModel> InsertOneFreeService(ScheduleItemsServicesModel model)
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();
                var json = await ORep.PostStrAsync("api/Schedules/PostScheduleFreeServices", model, UserToken);

                if (!string.IsNullOrEmpty(json))
                {
                    var oModel = JsonConvert.DeserializeObject<ScheduleItemsServicesModel>(json);
                    return oModel;
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Failed to add this service", "Ok");
                    return null;
                }
            }
            else
            {
                return null;
            }

        }

        async void OnSelecteNewMaterialReceipt(SchedulesModel model)
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
                    var popupView = new ScheduleMaterialReceiptViewModel();
                    popupView.MaterialRcClose += async (item) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        string UserToken = await _service.UserToken();

                        ScheduleMaterialReceiptModel MaterialReceiptModel = new ScheduleMaterialReceiptModel
                        {
                            AccountId = model.AccountId,
                            BrancheId = model.BrancheId,
                            ScheduleId = model.Id,
                            ScheduleDateId = model.OneScheduleDate.Id,
                            SupplierId = item.SupplierId,
                            SupplierName = item.SupplierName,
                            TechnicianId = int.Parse(Helpers.Settings.UserId),
                            Cost = item.Cost,
                            Notes = item.Notes,
                            ReceiptPhoto = item.ReceiptPhoto,
                            CreateUser = model.CreateUser,
                            CreateDate = DateTime.Now,
                        };

                        var json = await ORep.PostStrAsync("api/Schedules/PostScheduleMaterialReceipt", MaterialReceiptModel, UserToken);

                        if (string.IsNullOrEmpty(json))
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Failed to add the material receipt", "Ok");
                        }
                        else
                        {
                            //ScheduleDetails.LstMaterialReceipt.Add(MaterialReceiptModel);
                            LstMaterialReceipt.Add(JsonConvert.DeserializeObject<ScheduleMaterialReceiptModel>(json));
                        }

                        UserDialogs.Instance.HideLoading();
                    };

                    var page = new Views.SchedulePages.MaterialReceiptPage();
                    page.BindingContext = popupView;
                    await App.Current.MainPage.Navigation.PushAsync(page);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        async void OnRemoveItemAsync(ScheduleItemsServicesModel item)
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                if (ShowQty == false) // Remove material
                {
                    string UserToken = await _service.UserToken();
                    var json = await ORep.PutStrAsync("api/Schedules/PutMaterial", item, UserToken);//Delete Material

                    if (string.IsNullOrEmpty(json))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Failed to delete the material", "Ok");
                    }
                    else
                    {
                        LstItems.Remove(item);
                        ScheduleDetails.LstScheduleItemsServices.Remove(item);
                    }
                }
                else
                {
                    if (LstItemsInvoice.Count > 0) //Remove invoice item
                    {
                        LstItemsInvoice.Remove(item);
                        //ScheduleDetails.LstScheduleItemsServices.Remove(item);

                        TotalInvoice(ScheduleDetails, CustomerDetails);
                    }

                }


                UserDialogs.Instance.HideLoading();
            }

            IsBusy = false;
        }

        async void OnRemoveService(ScheduleItemsServicesModel service)
        {
            IsBusy = true;
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.PutStrAsync("api/Schedules/PutFreeService", service, UserToken);//Delete Service

                if (string.IsNullOrEmpty(json))
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Failed to delete the service", "Ok");
                }
                else
                {
                    LstFreeServices.Remove(service);
                    ScheduleDetails.LstFreeServices.Remove(service);

                    //TotalInvoice(ScheduleDetails, CustomerDetails);
                }

                UserDialogs.Instance.HideLoading();
            }

            IsBusy = false;
        }

        async void OnFullScreenNote(string Note)
        {
            var popupView = new FullScreenNoteViewModel(Note);
            popupView.NoteClose += (note) =>
            {
                ScheduleDetails.Notes = note;
            };
            var page = new Views.SchedulePages.FullScreenNotePage();
            page.BindingContext = popupView;
            await App.Current.MainPage.Navigation.PushAsync(page);
        }

        async void OnRemoveMaterialReceipt(ScheduleMaterialReceiptModel item)
        {
            IsBusy = true;
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();

                string UserToken = await _service.UserToken();

                var json = await ORep.PutStrAsync("api/Schedules/PutMaterialReceipt", item, UserToken);//Delete Material Receipt

                if (string.IsNullOrEmpty(json))
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Failed to delete the material receipt", "Ok");
                }
                else
                {
                    LstMaterialReceipt.Remove(item);
                    ScheduleDetails.LstMaterialReceipt.Remove(item);

                }

                UserDialogs.Instance.HideLoading();
            }

            IsBusy = false;
        }

        void OnSelectedEmpInOneCategory(ObservableCollection<EmployeeModel> LstEmp)
        {
            if (LstEmp.Count != 0)
            {
                string str = "";
                string strId = "";
                foreach (var Emp in LstEmp)
                {
                    str += ("," + Emp.UserName);
                    strId += ("," + Emp.Id);
                }

                StrEmployees = str.Remove(0, 1);
                StrEmployeesId = strId.Remove(0, 1);
            }
        }

        async void OnOpenEstimateScheduleDates()
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
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
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
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    return;
                }
                else
                {
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
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        async void OnOpenEmployeesInOneCategory()
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
                    var popupView = new Views.PopupPages.EmployeesPopup(LstEmpInOneCategory);
                    popupView.EmployeesClose += (Empolyees) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        //LstEmps.Clear();
                        //StrEmployees = "";
                        //StrEmployeesId = "";

                        string str = "";
                        string strId = "";

                        if (Empolyees.Count != 0)
                        {

                            foreach (var Emp in Empolyees)
                            {
                                var obj = LstEmps.ToList().Find(x => x.EmpId == Emp.Id);
                                if (obj == null)
                                {
                                    str += ("," + Emp.UserName);
                                    strId += ("," + Emp.Id);
                                    LstEmps.Add(new ScheduleEmployeesModel
                                    {
                                        EmpId = Emp.Id,
                                        EmpFullName = Emp.FirstName + " " + Emp.LastName,
                                        EmpUserName = Emp.UserName,
                                    });
                                }

                            }

                            if (!string.IsNullOrEmpty(StrEmployees) && !string.IsNullOrEmpty(StrEmployeesId))
                            {
                                StrEmployees += str;
                                StrEmployeesId += strId;
                            }
                            else
                            {
                                StrEmployees = str.Remove(0, 1);
                                StrEmployeesId = strId.Remove(0, 1);
                            }
                        }

                        foreach (var Employee in LstEmpInOneCategory)
                        {
                            if (Employee.IsChecked == false)
                            {
                                var obj = LstEmps.ToList().Find(x => x.EmpId == Employee.Id);
                                if (obj != null && obj.Status != 1 && obj.Status != 2)
                                {
                                    LstEmps.Remove(obj);
                                    StrEmployees = "," + StrEmployees;
                                    StrEmployeesId = "," + StrEmployeesId;
                                    StrEmployees = StrEmployees.Replace(("," + obj.EmpUserName), string.Empty);
                                    StrEmployeesId = StrEmployeesId.Replace(("," + obj.EmpId.ToString()), string.Empty);

                                    if (StrEmployees.StartsWith(","))
                                    {
                                        StrEmployees = StrEmployees.Remove(0, 1);
                                        StrEmployeesId = StrEmployeesId.Remove(0, 1);
                                    }
                                }
                            }
                        }


                        UserDialogs.Instance.HideLoading();
                    };

                    await PopupNavigation.Instance.PushAsync(popupView);
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        void OnRemoveEmployee(ScheduleEmployeesModel employee)
        {
            LstEmps.Remove(employee);

            //Empolyees Names 
            int index = StrEmployees.IndexOf(employee.EmpUserName + ",");
            StrEmployees = (index < 0) ? StrEmployees : StrEmployees.Remove(index, (employee.EmpUserName + ",").Length);

            int index2 = StrEmployees.IndexOf("," + employee.EmpUserName);
            StrEmployees = (index2 < 0) ? StrEmployees : StrEmployees.Remove(index2, ("," + employee.EmpUserName).Length);

            int index3 = StrEmployees.IndexOf(employee.EmpUserName);
            StrEmployees = (index3 < 0) ? StrEmployees : StrEmployees.Remove(index3, (employee.EmpUserName).Length);


            //Empolyees Ids 
            int indexId = StrEmployeesId.IndexOf(employee.EmpId + ",");
            StrEmployeesId = (indexId < 0) ? StrEmployeesId : StrEmployeesId.Remove(indexId, (employee.EmpId + ",").Length);

            int indexId2 = StrEmployeesId.IndexOf("," + employee.EmpId);
            StrEmployeesId = (indexId2 < 0) ? StrEmployeesId : StrEmployeesId.Remove(indexId2, ("," + employee.EmpId).Length);

            int indexId3 = StrEmployeesId.IndexOf(employee.EmpId.ToString());
            StrEmployeesId = (indexId3 < 0) ? StrEmployeesId : StrEmployeesId.Remove(indexId3, (employee.EmpId.ToString()).Length);
        }

        void OnRemoveEstimateDate(SchaduleDateModel Date)
        {
            LstEstimateDates.Remove(Date);

            foreach (SchaduleDateModel dt in LstEstimateSchaduleDates)
            {
                if (dt.Id == Date.Id)
                {
                    dt.IsChecked = false;
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

            foreach (SchaduleDateModel dt in LstInvoiceSchaduleDates)
            {
                if (dt.Id == Date.Id)
                {
                    dt.IsChecked = false;
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


        async void OnSelectedSubmitSchedule(SchedulesModel model)
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
                    ScheduleDetails = model;
                    ScheduleDetails.CustomerDTO = CustomerDetails;
                    ScheduleDetails.CustomerName = CustomerDetails.FirstName + " " + CustomerDetails.LastName;
                    ScheduleDetails.AccountId = int.Parse(Helpers.Settings.AccountId);
                    ScheduleDetails.BrancheId = int.Parse(Helpers.Settings.BranchId);
                    ScheduleDetails.CreateUser = int.Parse(Helpers.Settings.UserId);
                    ScheduleDetails.Active = true;
                    ScheduleDetails.Recurring = false;
                    ScheduleDetails.FrequencyType = 1;
                    ScheduleDetails.EndType = 1;
                    ScheduleDetails.PriorityId = OnePriorityModel.Id;
                    ScheduleDetails.Time = TimeFrom.ToString(@"hh\:mm");
                    ScheduleDetails.TimeEnd = TimeTo.ToString(@"hh\:mm");
                    ScheduleDetails.ScheduleDate = ScheduleDetails.StartDate = ScheduleDate.ToString("yyyy-MM-dd");

                    ScheduleItemsServicesModel ItemsModel = new ScheduleItemsServicesModel
                    {
                        AccountId = model.AccountId,
                        BrancheId = model.BrancheId,
                        ScheduleId = model.Id,
                        ItemsServicesId = SelectedService?.Id,
                        ItemsServicesName = SelectedService?.Name,
                        ItemServiceDescription = SelectedService?.Description,
                        CostRate = SelectedService?.CostperUnit,
                        Notes = SelectedService?.Notes,
                        Active = SelectedService?.Active,
                        CreateUser = model.CreateUser,
                        CreateDate = DateTime.Now,
                    };
                    ScheduleDetails.OneScheduleService = ItemsModel;
                    //ScheduleDetails.LstScheduleItemsServices = LstItems.ToList();

                    if (CustomerDetails.Id != 0)
                    {
                        ScheduleDetails.CustomerId = CustomerDetails.Id;
                    }
                    ScheduleDetails.Location = CustomerDetails.Address;
                    ScheduleDetails.EmployeeCategoryId = EmpCategory.Id;
                    if (StrEmployeesId != null)
                    {
                        ScheduleDetails.Employees = StrEmployeesId;
                    }
                    //ScheduleDetails.CalendarColor = LstColors.Where(x => x.IsChecked == true).Select(c => c.ColorHex).FirstOrDefault();
                    ScheduleDetails.CalendarColor = "#5e92e6";
                    ScheduleDetails.CreateDate = DateTime.Now;

                    if (string.IsNullOrEmpty(ScheduleDetails.Title))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Title.", "Ok");
                    }
                    else if (string.IsNullOrEmpty(ScheduleDetails.ScheduleDate))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Schedule Date.", "Ok");
                    }
                    else if (string.IsNullOrEmpty(ScheduleDetails.Time))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Start Time.", "Ok");
                    }
                    else if (string.IsNullOrEmpty(ScheduleDetails.TimeEnd))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : End Time.", "Ok");
                    }
                    else if (ScheduleDetails.EmployeeCategoryId == null || ScheduleDetails.EmployeeCategoryId == 0)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Choose Employee Category.", "Ok");
                    }
                    else if (string.IsNullOrEmpty(ScheduleDetails.Employees))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Choose Employees.", "Ok");
                    }
                    else if ((ScheduleDetails.OneScheduleService?.ItemsServicesId == 0 || ScheduleDetails.OneScheduleService?.ItemsServicesId == null) && ScheduleDetails.LstFirstCreateServices.Count == 0)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Service.", "Ok");
                    }
                    else
                    {
                        if (ScheduleDetails != null)
                        {
                            string UserToken = await _service.UserToken();
                            var Json = "";
                            if (model.Id == 0)
                            {
                                ScheduleDetails.CallId = CustomerDetails.CallId;
                                UserDialogs.Instance.ShowLoading();
                                Json = await ORep.PostDataAsync("api/Schedules/PostSchedule", ScheduleDetails, UserToken);
                                UserDialogs.Instance.HideLoading();
                            }
                            else
                            {
                                UserDialogs.Instance.ShowLoading();
                                Json = await ORep.PutDataAsync("api/Schedules/PutSchedule", ScheduleDetails, UserToken);
                                UserDialogs.Instance.HideLoading();
                            }
                            if (Json != "Bad Request" && Json != "api not responding")
                            {
                                await App.Current.MainPage.DisplayAlert("FixPro", "Schedule saved successfully", "Ok");
                                if (model.Id == 0)
                                {
                                    var VM = new SchedulesViewModel();
                                    var page = new SchedulePage();
                                    page.BindingContext = VM;
                                    await App.Current.MainPage.Navigation.PushAsync(page);
                                    App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);

                                    string Massage = $"Hi {model.CustomerName}! Your service appointment with {Helpers.Settings.AccountNameWithSpace} has been scheduled for {DateTime.Parse(model.StartDate).ToString("MMMM dd, yyyy")}. Your technician will arrive between {DateTime.Parse(model.Time).ToString("hh:mmtt")} - {DateTime.Parse(model.TimeEnd).ToString("hh:mmtt")} CDT.";

                                    string returnMsg = Controls.StartData.SendSMS(ScheduleDetails.CustomerDTO.Phone1, Massage);
                                    if (string.IsNullOrEmpty(returnMsg))
                                    {
                                        await App.Current.MainPage.DisplayAlert("Alert", "Schedule saved successfully, but failed to send SMS to the customer", "Ok");
                                    }
                                }
                                else
                                {
                                    var VM = new SchedulesViewModel(model.Id, model.ScheduleDateId);
                                    //var page = new NewSchedulePage();
                                    var page = new ScheduleDetailsPage();
                                    page.BindingContext = VM;
                                    await App.Current.MainPage.Navigation.PushAsync(page);
                                    App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "Failed to add or edit this schedule", "Ok");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "OK");
            }

            IsBusy = false;
        }

        //string SendSMS(string Phone, string Msg)
        //{
        //    //var accountSid = "AC2aa33faec930e6bddfef1daa25e3b945";
        //    //var authToken = "744fd3259244985557d4d0c1aa2617eb";            
        //    var accountSid = Controls.StartData.Com_MainObj.TwilioAccountSid;
        //    var authToken = Controls.StartData.Com_MainObj.TwilioauthToken;
        //    TwilioClient.Init(accountSid, authToken);

        //    var messageOptions = new CreateMessageOptions(
        //      new PhoneNumber("+1" + Phone));

        //    messageOptions.From = new PhoneNumber(Controls.StartData.Com_MainObj.TwilioFromPhoneNumber);
        //    messageOptions.Body = Msg;
        //    var message = MessageResource.Create(messageOptions);

        //    return message.Sid;
        //}

        //string SendSMS(string Phone, string Code)
        //{
        //    string accountSid = "AC1e12d1c89d9d238bf945b2f9b8ebc6a2";
        //    string authToken = "d9212c43cc269700de08ef643b0d1dab";

        //    Twilio.TwilioClient.Init(accountSid, authToken);

        //    var message = Twilio.Rest.Api.V2010.Account.MessageResource.Create(
        //        body: "Your Hi Car verification code is:" + Code,
        //        from: new Twilio.Types.PhoneNumber("+12086182061"),
        //        to: new Twilio.Types.PhoneNumber("+1" + Phone)
        //    );

        //    return message.Sid;
        //}


        async void OnSelectJobDetails(SchedulesModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            var popupView = new SchedulesViewModel(model);
            var page = new Views.PopupPages.ScheduleJobDetailsPopup();
            page.BindingContext = popupView;
            await PopupNavigation.Instance.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnOpenImages(SchedulesModel model)
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
                    //model.GetPictures = true; //In Get Pictures Case Only
                    var popupView = new SchedulesViewModel(model);
                    var page = new Views.SchedulePages.SchedulePicturesPage();
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

        async void OnCreateScheduleInvoice(SchedulesModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            model.InvoiceOrEstimate = 1; //Invoice
            Controls.StaticMembers.WayAfterChooseCust = 0; //From Schedule

            if (model.InvoiceDTO != null)
            {
                var ViewModel = new CustomersViewModel(model.InvoiceDTO, model.CustomerDTO);
                var page = new Views.CustomerPages.InvoiceDetailsPage();
                page.BindingContext = ViewModel;
                await App.Current.MainPage.Navigation.PushAsync(page);
            }
            else
            {
                var popupView = new SchedulesViewModel(model);
                var page = new Views.SchedulePages.CreateInvoicePage();
                page.BindingContext = popupView;
                await App.Current.MainPage.Navigation.PushAsync(page);
            }

            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnCreateScheduleEstimate(SchedulesModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();

            Controls.StaticMembers.WayAfterChooseCust = 0; //From Schedule

            if (model.EstimateDTO == null)
            {
                model.InvoiceOrEstimate = 0; //Estimate
                var popupView = new SchedulesViewModel(model);
                var page = new Views.SchedulePages.CreateEstimatePage();
                page.BindingContext = popupView;
                await App.Current.MainPage.Navigation.PushAsync(page);
            }
            else
            {
                var popupView = new CustomersViewModel(model.EstimateDTO, model.CustomerDTO);
                var page = new Views.CustomerPages.EstimateDetailsPage();
                page.BindingContext = popupView;
                await App.Current.MainPage.Navigation.PushAsync(page);
            }

            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        void OnEditDiscountForCustomer(CustomersModel model)
        {
            Discount = model.Discount;
            TotalInvoice(ScheduleDetails, CustomerDetails);
        }

        void OnEditDiscountForCustomerEstimate(CustomersModel model)
        {
            Discount = model.Discount;

            TotalEstimate(ScheduleDetails, CustomerDetails);
        }

        async void OnOpenAddImagesPopup(SchedulesModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            var popupView = new SchedulesViewModel(model);
            var page = new Views.PopupPages.AddSchedulePhotoPupop();
            page.BindingContext = popupView;
            await PopupNavigation.Instance.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnStartScheduleOutSide(SchedulesModel model)
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
                    if (model != null)
                    {
                        await GetOneScheduleDetails(model.Id, model.ScheduleDateId);

                        if (ScheduleDetails != null && ScheduleDetails.OneScheduleDate != null)
                        {
                            string UserToken = await _service.UserToken();
                            ScheduleDetails.OneScheduleDate.StartTime = DateTime.Now.TimeOfDay.ToString(@"hh\:mm");
                            ScheduleDetails.OneScheduleDate.Status = 1;

                            UserDialogs.Instance.ShowLoading();

                            var json = await ORep.PutStrAsync("api/Schedules/PutScheduleEmployees", ScheduleDetails.OneScheduleDate, UserToken);

                            if (!string.IsNullOrEmpty(json))
                            {
                                await App.Current.MainPage.DisplayAlert("FixPro", "The job started successfully.", "Ok");
                                model.ShowCheckBtn = 1;

                                await App.Current.MainPage.Navigation.PushAsync(new SchedulePage());
                                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("FixPro", "The job start Failed.", "Ok");
                            }
                            UserDialogs.Instance.HideLoading();
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

        async void OnEndScheduleOutSide(SchedulesModel model)
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
                    if (model != null)
                    {
                        await GetOneScheduleDetails(model.Id, model.ScheduleDateId);
                        string UserToken = await _service.UserToken();

                        if (ScheduleDetails != null && ScheduleDetails.OneScheduleDate != null)
                        {
                            ScheduleDetails.OneScheduleDate.EndTime = DateTime.Now.TimeOfDay.ToString(@"hh\:mm");
                            ScheduleDetails.OneScheduleDate.Status = 2;
                            ScheduleDetails.OneScheduleDate.CalendarColor = "#676d75";

                            UserDialogs.Instance.ShowLoading();

                            var json = await ORep.PutStrAsync("api/Schedules/PutScheduleEmployees", ScheduleDetails.OneScheduleDate, UserToken);

                            if (!string.IsNullOrEmpty(json))
                            {
                                await App.Current.MainPage.DisplayAlert("FixPro", "The job ended successfully.", "Ok");

                                model.ShowCheckBtn = 2;

                                await App.Current.MainPage.Navigation.PushAsync(new SchedulePage());
                                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("FixPro", "The job end Failed.", "Ok");
                            }
                            UserDialogs.Instance.HideLoading();
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

        async void OnAddScheduleDate(SchedulesModel model)
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
                    if (model != null)
                    {
                        if (TimeFromAddDate != new TimeSpan(00, 0, 0, 00) && TimeToAddDate != new TimeSpan(00, 0, 0, 00) && SelectedEmployeeAddDate != null)
                        {
                            string UserToken = await _service.UserToken();

                            model.OneScheduleDate.Date = ScheduleAddDate.ToString("yyyy-MM-dd");
                            model.Time = model.OneScheduleDate.StartTime = TimeFromAddDate.ToString(@"hh\:mm");
                            model.TimeEnd = model.OneScheduleDate.EndTime = TimeToAddDate.ToString(@"hh\:mm");
                            model.OneScheduleDate.Status = 1;
                            model.CalendarColor = model.OneScheduleDate.CalendarColor = "#5e92e6";
                            //model.LstEmployeeDTO.Clear();
                            //model.LstEmployeeDTO.Add(SelectedEmployeeAddDate);
                            model.OneScheduleDate.OneEmployee = SelectedEmployeeAddDate;
                            //model.LstMaterialReceipt.Clear();
                            //model.LstScheduleItemsServices.Clear();
                            //model.LstSchedulePictures.Clear();
                            //model.Notes = null;


                            model.CreateDate = DateTime.Now;

                            UserDialogs.Instance.ShowLoading();

                            var json = await ORep.PostStrAsync("api/Schedules/PostAddScheduleDate", model, UserToken);

                            if (!string.IsNullOrEmpty(json))
                            {
                                await App.Current.MainPage.DisplayAlert("FixPro", "Date added successfully", "Ok");
                                var VM = new SchedulesViewModel();
                                var page = new SchedulePage();
                                page.BindingContext = VM;
                                await App.Current.MainPage.Navigation.PushAsync(page);
                                await PopupNavigation.Instance.PopAsync();
                                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);


                                if (model.Id == 0)
                                {
                                    string Massage = $"Hi {model.CustomerName}! Your service appointment with {Helpers.Settings.AccountNameWithSpace} has been scheduled for {DateTime.Parse(model.StartDate).ToString("MMMM dd, yyyy")}. Your technician will arrive between {DateTime.Parse(model.Time).ToString("hh:mmtt")} - {DateTime.Parse(model.TimeEnd).ToString("hh:mmtt")} CDT.";

                                    string returnMsg = Controls.StartData.SendSMS(ScheduleDetails.CustomerDTO.Phone1, Massage);
                                    if (string.IsNullOrEmpty(returnMsg))
                                    {
                                        await App.Current.MainPage.DisplayAlert("Alert", "Schedule saved successfully, but failed to send SMS to the customer", "Ok");
                                    }
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("FixPro", "Failed to add another date", "Ok");
                            }
                            UserDialogs.Instance.HideLoading();
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("FixPro", "Please complete all the fields", "Ok");
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

        async void OnSaveResponNotServiceScheduleDate(SchaduleDateModel model)
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
                    if (model.Reasonnotserve != null)
                    {
                        string UserToken = await _service.UserToken();
                        model.Status = 0;

                        model.CreateDate = DateTime.Now;

                        model.Reasonnotserve = OldResonNotServiced != null ? (OldResonNotServiced + " , " + model.Reasonnotserve + "-" + DateTime.Now.ToString()) : (model.Reasonnotserve + " - " + DateTime.Now.ToString());

                        UserDialogs.Instance.ShowLoading();
                        //var json = await Helpers.Utility.PutPosData("api/Schedules/PutScheduleDate", JsonConvert.SerializeObject(model));
                        var json = await ORep.PutAsync("api/Schedules/PutScheduleDate", model, UserToken);

                        if (json != null)
                        {
                            await App.Current.MainPage.DisplayAlert("FixPro", "The job is not serviced", "Ok");
                            var VM = new SchedulesViewModel(ScheduleDetails.Id, model.Id);
                            //var page = new NewSchedulePage();
                            var page = new ScheduleDetailsPage();
                            page.BindingContext = VM;
                            await App.Current.MainPage.Navigation.PushAsync(page);
                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("FixPro", "Enter Respon Not Service, please .", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        async void OnSaveReOpenScheduleDate(SchaduleDateModel model)
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
                    if (model != null)
                    {
                        string UserToken = await _service.UserToken();
                        model.Status = 1;

                        UserDialogs.Instance.ShowLoading();
                        //var json = await Helpers.Utility.PutPosData("api/Schedules/PutScheduleDate", JsonConvert.SerializeObject(model));
                        var json = await ORep.PutAsync("api/Schedules/PutScheduleDate", model, UserToken);

                        if (json != null)
                        {
                            await App.Current.MainPage.DisplayAlert("FixPro", "Job re opened successfully", "Ok");
                            var VM = new SchedulesViewModel(ScheduleDetails.Id, model.Id);
                            //var page = new NewSchedulePage();
                            var page = new ScheduleDetailsPage();
                            page.BindingContext = VM;
                            await App.Current.MainPage.Navigation.PushAsync(page);
                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        }
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("FixPro", "This Schedule Not found.", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        async void OnDoneScheduleDate(SchaduleDateModel model)
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
                    if (model != null)
                    {
                        //if (!string.IsNullOrEmpty(StartTime) && !string.IsNullOrEmpty(EndTime))
                        //{     
                        //model.StartTime = StartTime;
                        //model.EndTime = EndTime;
                        //model.SpentTimeHour = SpentHours;
                        //model.SpentTimeMin = SpentMin;
                        string UserToken = await _service.UserToken();
                        model.Status = 2;
                        model.CalendarColor = "#676d75";

                        model.CreateDate = DateTime.Now;

                        UserDialogs.Instance.ShowLoading();
                        //var json = await Helpers.Utility.PutPosData("api/Schedules/PutScheduleDate", JsonConvert.SerializeObject(model));
                        var json = await ORep.PutStrAsync("api/Schedules/PutScheduleDate", model, UserToken);

                        if (!string.IsNullOrEmpty(json) && json.Contains("Not Done All Employee") == true)
                        {
                            await App.Current.MainPage.DisplayAlert("FixPro", "Failed to end the scheduled job because the employee isn’t done", "Ok");
                        }
                        else if (!string.IsNullOrEmpty(json))
                        {
                            bool answer = await App.Current.MainPage.DisplayAlert("Question?", "Do you want to send a message to the customer?", "Yes", "No");
                            if (answer)
                            {
                                string Massage = $"Hello {model.CustomerName}, thank you for choosing us. We hope your experience was satisfactory. Your feedback means a lot to us! Please consider leaving a Google review here: {model.GoogleReviewLink}. Have a great day!";

                                string returnMsg = Controls.StartData.SendSMS(model.CustomerPhone, Massage);
                                if (string.IsNullOrEmpty(returnMsg))
                                {
                                    await App.Current.MainPage.DisplayAlert("Alert", "Schedule accepted successfully, but failed to send SMS to the customer", "Ok");
                                }
                            }

                            await App.Current.MainPage.DisplayAlert("FixPro", "Scheduled job completed successfully", "Ok");
                            var VM = new SchedulesViewModel(ScheduleDetails.Id, model.Id);
                            //var page = new NewSchedulePage();
                            var page = new ScheduleDetailsPage();
                            page.BindingContext = VM;
                            await App.Current.MainPage.Navigation.PushAsync(page);
                            await PopupNavigation.Instance.PopAsync();
                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("FixPro", "Failed to end the scheduled job", "Ok");
                        }
                        UserDialogs.Instance.HideLoading();
                        //}
                        //else
                        //{
                        //    await App.Current.MainPage.DisplayAlert("FixPro", "Select start and end date to check out, please .", "Ok");
                        //}
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

        async Task UploadPictures(List<SchedulePicturesModel> LstPhotos)
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();

                List<SchedulePicturesModel> LstFinalPhotos = LstPhotos.Where(x => x.Id == 0).ToList(); // if Id = 0 (Photo New)

                foreach (var source in LstFinalPhotos)
                {
                    if (source.PictureSource != null)
                        source.PictureSource = null;
                }
                //string Postjson = await Helpers.Utility.PostData("api/ImageMobile/ReplacePostOneImagesScheduleMobile", JsonConvert.SerializeObject(LstFinalPhotos, Formatting.None,
                //            new JsonSerializerSettings()
                //            {
                //                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                //            }));

                string UserToken = await _service.UserToken();
                string Postjson = await ORep.PostMultiPicAsync("api/ImageMobile/ReplacePostOneImagesScheduleMobile", LstFinalPhotos, UserToken);

                UserDialogs.Instance.HideLoading();
            }

            IsBusy = false;
        }

        //Pick Photo
        private async void OnSelectePickSchedulePhoto()
        {
            await PopupNavigation.Instance.PopAsync();
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "This is not support on your device.", "Ok");
                    return;
                }
                else
                {
                    var mediaOption = new PickMediaOptions()
                    {
                        PhotoSize = PhotoSize.Small,
                        CompressionQuality = 75,
                        CustomPhotoSize = 200,
                        MaxWidthHeight = 400,
                    };

                    _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                    if (_mediaFile == null) return;

                    //Upload Image To Server
                    UserDialogs.Instance.ShowLoading();

                    SchedulePhoto = ImageSource.FromStream(() =>
                    {
                        var stream = _mediaFile.GetStream();
                        return stream;
                    });

                    OnePictureModel = new SchedulePicturesModel
                    {
                        AccountId = ScheduleDetails.AccountId,
                        BrancheId = ScheduleDetails.BrancheId,
                        ScheduleId = ScheduleDetails.Id,
                        FileName = Convert.ToBase64String(Helpers.Utility.ReadToEnd(_mediaFile.GetStream())),
                        Active = true,
                        ShowToCust = true,
                        CreateUser = ScheduleDetails.CreateUser,
                        CreateDate = DateTime.Now,
                        ScheduleDateId = ScheduleDetails.OneScheduleDate.Id,
                        PictureSource = SchedulePhoto,
                        Flag = 0, // new photo
                    };


                    LstNewPictures.Add(OnePictureModel);
                    LstAllPictures.Add(OnePictureModel);
                    ScheduleDetails.LstSchedulePictures.Add(OnePictureModel);
                    ScheduleDetails.GetPictures = false; //Don't entrance GetPictures Method

                    var popupView = new SchedulesViewModel(ScheduleDetails);
                    var page = new Views.SchedulePages.SchedulePicturesPage();
                    page.BindingContext = popupView;
                    await App.Current.MainPage.Navigation.PushAsync(page);

                    App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);


                    DoneFlag = true;

                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "This is not support on your device.", "Ok");
            }

        }

        //Camera Photo
        private async void OnSelecteCamSchedulePhoto()
        {
            await PopupNavigation.Instance.PopAsync();
            try
            {
                await CrossMedia.Current.Initialize();

                if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "Ok");
                    return;
                }

                var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Sample",
                    Name = "test.jpg",
                    PhotoSize = PhotoSize.Small,
                    CompressionQuality = 75,
                    CustomPhotoSize = 200,
                    MaxWidthHeight = 400,
                });

                if (file == null)
                    return;

                //Upload Image To Server
                UserDialogs.Instance.ShowLoading();

                SchedulePhoto = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });


                OnePictureModel = new SchedulePicturesModel
                {
                    AccountId = ScheduleDetails.AccountId,
                    BrancheId = ScheduleDetails.BrancheId,
                    ScheduleId = ScheduleDetails.Id,
                    FileName = Convert.ToBase64String(Helpers.Utility.ReadToEnd(file.GetStream())),
                    Active = true,
                    ShowToCust = true,
                    CreateUser = ScheduleDetails.CreateUser,
                    CreateDate = DateTime.Now,
                    ScheduleDateId = ScheduleDetails.OneScheduleDate.Id,
                    PictureSource = SchedulePhoto,
                    Flag = 0, // new photo
                };


                LstNewPictures.Add(OnePictureModel);
                LstAllPictures.Add(OnePictureModel);
                ScheduleDetails.LstSchedulePictures.Add(OnePictureModel);
                ScheduleDetails.GetPictures = false; //Don't entrance GetPictures Method

                var popupView = new SchedulesViewModel(ScheduleDetails);
                var page = new Views.SchedulePages.SchedulePicturesPage();
                page.BindingContext = popupView;
                await App.Current.MainPage.Navigation.PushAsync(page);
                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);


                DoneFlag = true;

                UserDialogs.Instance.HideLoading();

            }

            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "Ok");
            }
        }


        async void OnDeleteSchedulePhoto(SchedulePicturesModel model)
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
                    if (model.Id == 0) //Id = 0 (Photo New)
                    {
                        LstNewPictures.Remove(model);
                        LstAllPictures.Remove(model);
                        ScheduleDetails.LstSchedulePictures.Remove(model);
                        LstATwoPictures.Remove(model);
                    }
                    else //Id != 0 (already Photo save)
                    {
                        UserDialogs.Instance.ShowLoading();

                        //DeleteSchedulePictureModel ModelWhitOutImageSource = new DeleteSchedulePictureModel
                        //{
                        //    Id = model.Id,
                        //    AccountId = model.AccountId,
                        //    BrancheId = model.BrancheId,
                        //    ScheduleId = model.Id,
                        //    FileName = model.FileName,
                        //    Active = model.Active,
                        //    CreateUser = model.CreateUser,
                        //    CreateDate = model.CreateDate,
                        //    ScheduleDateId = model.ScheduleDateId,
                        //};

                        string UserToken = await _service.UserToken();
                        //var json = await Helpers.Utility.DeleteData("api/ImageMobile/DeleteOneImage", JsonConvert.SerializeObject(ModelWhitOutImageSource));
                        var json = await ORep.DeleteStrItemAsync(string.Format("api/ImageMobile/DeleteOneImage/{0}", model.Id), UserToken);

                        if (json != null && json != "api not responding")
                        {
                            LstAllPictures.Remove(model);
                            ScheduleDetails.LstSchedulePictures.Remove(model);
                        }
                        UserDialogs.Instance.HideLoading();
                    }

                    List<SchedulePicturesModel> NewImg = ScheduleDetails.LstSchedulePictures.Where(x => x.Id == 0).ToList();
                    if (NewImg.Count > 0)
                    {
                        DoneFlag = true;
                    }

                    else
                    {
                        DoneFlag = false;
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
        }

        async Task CalcSchPhotoCount(int Count)
        {
            int Co = Count - 2;
            PhotosCount = 0;
            if (Co > 0)
            {
                PhotosCount = Co;
            }
        }

        async void OnDonePictures(SchedulesModel model)
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
                    if (model != null && model.LstSchedulePictures.Count > 0)
                    {
                        UserDialogs.Instance.ShowLoading();
                        await UploadPictures(model.LstSchedulePictures);

                        SetLstTwoSchedulePhotos(model.LstSchedulePictures);

                        //PhotosCount = model.LstSchedulePictures.Count-2;
                        await CalcSchPhotoCount(model.LstSchedulePictures.Count);

                        await App.Current.MainPage.DisplayAlert("FixPro", "Schedule pictures added successfully", "Ok");

                        UserDialogs.Instance.ShowLoading();
                        var popupView = new SchedulesViewModel(model.Id, model.OneScheduleDate.Id);
                        //var page = new NewSchedulePage();
                        var page = new ScheduleDetailsPage();
                        page.BindingContext = popupView;
                        await App.Current.MainPage.Navigation.PushAsync(page);
                        App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        UserDialogs.Instance.HideLoading();

                        //await App.Current.MainPage.Navigation.PopAsync();
                        UserDialogs.Instance.HideLoading();
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Please Choose Photos.", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            IsBusy = false;
        }

        public void SetLstTwoSchedulePhotos(List<SchedulePicturesModel> LstPhotos)
        {
            if (LstPhotos != null && LstPhotos.Count > 0)
            {
                ObservableCollection<SchedulePicturesModel> lstPictures = new ObservableCollection<SchedulePicturesModel>(LstPhotos);

                if (lstPictures.Count > 0)
                {
                    if (lstPictures.Count > 0 && lstPictures.Count < 2)
                    {
                        LstATwoPictures.Add(LstPhotos[0]);
                    }
                    else if (lstPictures.Count >= 2)
                    {
                        LstATwoPictures.Add(LstPhotos[0]);
                        LstATwoPictures.Add(LstPhotos[1]);
                    }
                    else
                    {
                        LstATwoPictures.Add(LstPhotos[0]);
                        LstATwoPictures.Add(LstPhotos[1]);
                    }

                }
            }
        }

        async void OnSubmitSchInvoiceOrEstimate(SchedulesModel model)
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
                    if (model != null)
                    {
                        if (model.InvoiceOrEstimate == 0)//Insert Estimate InvoiceOrEstimate == 0 (Schedule Page)
                        {
                            if (LstItemsEstimate.Count > 0)
                            {
                                string UserToken = await _service.UserToken();

                                if (LstEstimateSchaduleDatesActual.Count > 0)
                                {
                                    if (Pending == true || Accept == true || Declind == true)
                                    {

                                        OneEstimate.AccountId = model.AccountId;
                                        OneEstimate.BrancheId = model.BrancheId;
                                        OneEstimate.ScheduleId = model.Id;
                                        OneEstimate.EstimateDate = DateTime.Now;
                                        OneEstimate.CustomerId = model.CustomerId;
                                        OneEstimate.Total = SubTotalEst; //Total before discount and tax
                                        OneEstimate.TaxId = model.CustomerDTO.TaxId;
                                        OneEstimate.Tax = model.CustomerDTO.TaxDTO?.Rate;
                                        OneEstimate.Taxval = null;
                                        OneEstimate.SignatureDraw = SignatureImageByte64Estimate;
                                        //OneEstimate.Taxval = (model.CustomerDTO != null && model.CustomerDTO.MemeberType == false && model.CustomerDTO.TaxDTO != null) ? (SubTotal - (SubTotal * model.CustomerDTO.MemberDTO.MemberValue / 100) * model.CustomerDTO.TaxDTO.Rate / 100) : (model.CustomerDTO != null && model.CustomerDTO.TaxDTO != null && model.CustomerDTO.MemeberType == true && model.CustomerDTO.TaxDTO != null) ? ((SubTotal - model.CustomerDTO.Discount) * model.CustomerDTO.TaxDTO.Rate / 100) : 0;
                                        OneEstimate.MemberId = model.CustomerDTO.MemeberId;
                                        OneEstimate.Discount = Discount;
                                        //OneEstimate.DiscountAmountOrPercent = model.CustomerDTO.MemberDTO.MemberType == false ? "%" : "$";
                                        OneEstimate.DiscountAmountOrPercent = AmountOrPersent == false ? "%" : "$";
                                        OneEstimate.Net = NetEst;
                                        OneEstimate.Status = Accept == true ? 1 : Declind == true ? 2 : 0; //0 = Pending
                                        OneEstimate.SignaturePrintName = null;
                                        OneEstimate.Terms = null;
                                        OneEstimate.NotesForCustomer = model.CustomerDTO.Notes;
                                        OneEstimate.Notes = model.Notes;
                                        OneEstimate.Active = model.Active;
                                        OneEstimate.CreateUser = int.Parse(Helpers.Settings.UserId);
                                        OneEstimate.CreateDate = DateTime.Now;

                                        foreach (ScheduleItemsServicesModel item in LstItemsEstimate)
                                        {
                                            EstimateItemServicesModel ObjItem = new EstimateItemServicesModel
                                            {
                                                Id = item.Id,
                                                AccountId = model.AccountId,
                                                BrancheId = model.BrancheId,
                                                //TaxId = model.CustomerDTO.TaxId,
                                                //Tax = model.CustomerDTO.TaxDTO.Rate,
                                                //Taxable = (model.CustomerDTO.TaxDTO.Rate == null || model.CustomerDTO.TaxDTO.Rate == 0) ? false : true,
                                                Taxable = true,
                                                //Unit = item.Unit,
                                                Price = item.CostRate,
                                                Quantity = item.Quantity,
                                                //Discountable = model.CustomerDTO.MemberDTO.MemberValue != null ? true : false,
                                                Discountable = true,
                                                ItemsServicesId = item.ItemsServicesId,
                                                ItemsServicesName = item.ItemsServicesName,
                                                CreateUser = int.Parse(Helpers.Settings.UserId),
                                                CreateDate = DateTime.Now,
                                                Total = item.Quantity != null && item.Tax != null ? (item.CostRate * item.Quantity) + (item.CostRate * item.Quantity * item.Tax / 100) : item.Quantity == null && item.Tax != null ? item.CostRate + (item.CostRate * item.Quantity * item.Tax / 100) : item.Quantity != null && item.Tax == null ? item.CostRate * item.Quantity : item.CostRate,
                                                Active = model.Active,
                                            };
                                            OneEstimate.LstEstimateItemServices.Add(ObjItem);
                                        }

                                        OneEstimate.LstScdDate = LstEstimateSchaduleDatesActual.ToList();

                                        UserDialogs.Instance.ShowLoading();
                                        //var json = await Helpers.Utility.PostData("api/Estimates/PostEstimate", JsonConvert.SerializeObject(OneEstimate));
                                        var json = await ORep.PostDataAsync("api/Estimates/PostEstimate", OneEstimate, UserToken);
                                        UserDialogs.Instance.HideLoading();

                                        if (json != "Bad Request" && json != "api not responding" && json.Contains("Already Exist For This Schedule Date#") != true)
                                        {
                                            //await App.Current.MainPage.DisplayAlert("FixPro", "Success Save Estimate.", "Ok");
                                            Helpers.Messages.ShowSuccessSnackBar("Success Create Estimate.");

                                            bool answer = await App.Current.MainPage.DisplayAlert("Question?", "Do you want to send an email to the customer?", "Yes", "No");

                                            if (answer)//Send Email
                                            {

                                                UserDialogs.Instance.ShowLoading();
                                                var jsonEmail = await ORep.PostStrAsync("api/Estimates/PostEstimateEmail", OneEstimate, UserToken);
                                                UserDialogs.Instance.HideLoading();

                                                if (!string.IsNullOrEmpty(jsonEmail) && jsonEmail.Contains("Send Success") == true)
                                                {
                                                    Helpers.Messages.ShowSuccessSnackBar("Success Send Email to Customer.");
                                                    //await App.Current.MainPage.DisplayAlert("FixPro", "Email sent successfully to customer", "Ok");
                                                }
                                                else
                                                {
                                                    Helpers.Messages.ShowSuccessSnackBar("Failed to send e-mail to the customer");
                                                    //await App.Current.MainPage.DisplayAlert("FixPro", "Failed to send e-mail to the customer", "Ok");
                                                }
                                            }

                                            //await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                                            var VM = new SchedulesViewModel(model.Id, model.ScheduleDateId);
                                            //var page = new NewSchedulePage();
                                            var page = new ScheduleDetailsPage();
                                            page.BindingContext = VM;
                                            await App.Current.MainPage.Navigation.PushAsync(page);
                                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                                        }
                                        else
                                        {
                                            await App.Current.MainPage.DisplayAlert("Alert", json, "Ok");
                                        }

                                    }
                                    else
                                    {
                                        await App.Current.MainPage.DisplayAlert("Alert", "Please Choose Status for Estimate.", "Ok");
                                    }
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Alert", "No schedule dates chosen for this estimate!", "Ok");
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "No item/service chosen for this estimate!", "Ok");
                            }

                        }
                        else //Insert Invoice InvoiceOrEstimate == 1 (Schedule Page)
                        {
                            if (LstItemsInvoice.Count > 0)
                            {
                                string UserToken = await _service.UserToken();

                                if (LstInvoiceSchaduleDatesActual.Count > 0)
                                {
                                    var CheckItemoutFalse = LstItemsInvoice.Where(m => m.Out == false).FirstOrDefault();
                                    if (CheckItemoutFalse != null)
                                    {
                                        OneInvoice.AccountId = model.AccountId;
                                        OneInvoice.BrancheId = model.BrancheId;
                                        OneInvoice.ContractId = model.ContractId;
                                        OneInvoice.ScheduleId = model.Id;
                                        OneInvoice.InvoiceDate = DateTime.Now;
                                        OneInvoice.CustomerId = model.CustomerId;
                                        OneInvoice.Total = SubTotal;
                                        OneInvoice.TaxId = model.CustomerDTO.TaxId;
                                        OneInvoice.Tax = model.CustomerDTO.TaxDTO?.Rate;
                                        //OneInvoice.Taxval = (SubTotal - (SubTotal * model.CustomerDTO.MemberDTO.MemberValue / 100)) * model.CustomerDTO.TaxDTO.Rate / 100;
                                        //OneInvoice.Taxval = (model.CustomerDTO != null && model.CustomerDTO.MemeberType == false && model.CustomerDTO.TaxDTO != null) ? (SubTotal - (SubTotal * model.CustomerDTO.MemberDTO.MemberValue / 100) * model.CustomerDTO.TaxDTO.Rate / 100) : (model.CustomerDTO != null && model.CustomerDTO.TaxDTO != null && model.CustomerDTO.MemeberType == true && model.CustomerDTO.TaxDTO != null) ? ((SubTotal - model.CustomerDTO.Discount) * model.CustomerDTO.TaxDTO.Rate / 100) : 0;
                                        OneInvoice.Taxval = null;
                                        OneInvoice.MemberId = model.CustomerDTO.MemeberId;
                                        OneInvoice.Discount = Discount;
                                        //OneInvoice.DiscountAmountOrPercent = model.CustomerDTO.MemberDTO.MemberType == false ? "%" : "$";
                                        OneInvoice.DiscountAmountOrPercent = AmountOrPersent == false ? "%" : "$";
                                        OneInvoice.Paid = 0;
                                        OneInvoice.Net = Net;
                                        OneInvoice.Status = 0; //Draft status if(1=partail & 2=paid)
                                        OneInvoice.Type = 2; //Installment Payment type
                                        OneInvoice.SignaturePrintName = null;
                                        OneInvoice.SignatureDraw = null;
                                        OneInvoice.Terms = null;
                                        OneInvoice.NotesForCustomer = model.CustomerDTO.Notes;
                                        OneInvoice.Notes = model.Notes;
                                        OneInvoice.Active = model.Active;
                                        OneInvoice.CreateUser = int.Parse(Helpers.Settings.UserId);
                                        OneInvoice.CreateDate = DateTime.Now;

                                        foreach (ScheduleItemsServicesModel item in LstItemsInvoice)
                                        {
                                            InvoiceItemServicesModel ObjItem = new InvoiceItemServicesModel
                                            {
                                                Id = item.Id,
                                                AccountId = model.AccountId,
                                                BrancheId = model.BrancheId,
                                                ItemServiceDescription = item.ItemServiceDescription,
                                                //TaxId = model.CustomerDTO.TaxId,
                                                //Tax = model.CustomerDTO.TaxDTO.Rate,
                                                //Taxable = (model.CustomerDTO.TaxDTO.Rate == null || model.CustomerDTO.TaxDTO.Rate == 0) ? false : true,
                                                Taxable = true,
                                                //Unit = item.Unit,
                                                Price = item.CostRate,
                                                Quantity = item.Quantity,
                                                //Discountable = model.CustomerDTO.MemberDTO.MemberValue != null ? true : false,
                                                Discountable = true,
                                                ItemsServicesId = item.ItemsServicesId,
                                                ItemsServicesName = item.ItemsServicesName,
                                                CreateUser = int.Parse(Helpers.Settings.UserId),
                                                CreateDate = DateTime.Now,
                                                SkipOfTotal = item.Out,
                                                Total = item.Quantity != null && item.Tax != null ? (item.CostRate * item.Quantity) + (item.CostRate * item.Quantity * item.Tax / 100) : item.Quantity == null && item.Tax != null ? item.CostRate + (item.CostRate * item.Quantity * item.Tax / 100) : item.Quantity != null && item.Tax == null ? item.CostRate * item.Quantity : item.CostRate,
                                                Active = model.Active,
                                            };
                                            OneInvoice.LstInvoiceItemServices.Add(ObjItem);
                                        }

                                        OneInvoice.LstScdDate = LstInvoiceSchaduleDatesActual.ToList();

                                        UserDialogs.Instance.ShowLoading();
                                        //var json = await Helpers.Utility.PostMData("api/Invoices/PostInvoice", JsonConvert.SerializeObject(OneInvoice));
                                        var json = await ORep.PostDataAsync("api/Invoices/PostInvoice", OneInvoice, UserToken);
                                        UserDialogs.Instance.HideLoading();

                                        if (json != "Bad Request" && json != "api not responding" && json.Contains("Not_Enough") != true && json.Contains("This Invoice Already Exist") != true)
                                        {
                                            //await App.Current.MainPage.DisplayAlert("FixPro", "Success Create Invoice for this Job.", "Ok");
                                            Helpers.Messages.ShowSuccessSnackBar("Success Create Invoice for this Job.");

                                            bool answer = await App.Current.MainPage.DisplayAlert("Question?", "Do you want to send an email to the customer?", "Yes", "No");

                                            if (answer)//Send Email
                                            {
                                                UserDialogs.Instance.ShowLoading();
                                                var jsonEmail = await ORep.PostStrAsync("api/Invoices/PostInvoiceEmail", OneInvoice, UserToken);
                                                UserDialogs.Instance.HideLoading();

                                                if (!string.IsNullOrEmpty(jsonEmail) && jsonEmail.Contains("Send Success") == true)
                                                {
                                                    Helpers.Messages.ShowSuccessSnackBar("Success Send Email to Customer.");
                                                    //await App.Current.MainPage.DisplayAlert("FixPro", "Email sent successfully to customer", "Ok");
                                                }
                                                else
                                                {
                                                    Helpers.Messages.ShowSuccessSnackBar("Failed to send e-mail to the customer");
                                                    //await App.Current.MainPage.DisplayAlert("FixPro", "Failed to send e-mail to the customer", "Ok");
                                                }
                                            }

                                            if (OneInvoice.Net > 0)
                                            {
                                                OneInvoice.Id = int.Parse(json.Replace("\"", "").Trim());
                                                var ViewModel = new SchedulesViewModel(OneInvoice, CustomerDetails);
                                                var page = new Views.PopupPages.PaymentMethodsPopup();
                                                page.BindingContext = ViewModel;
                                                await PopupNavigation.Instance.PushAsync(page);
                                                //await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                                            }
                                            else
                                            {
                                                var VM = new SchedulesViewModel(model.Id, model.ScheduleDateId);
                                                //var page = new NewSchedulePage();
                                                var page = new ScheduleDetailsPage();
                                                page.BindingContext = VM;
                                                await App.Current.MainPage.Navigation.PushAsync(page);
                                                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                                            }


                                        }
                                        else
                                        {
                                            await App.Current.MainPage.DisplayAlert("Alert", json, "Ok");
                                            //await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                                        }
                                    }
                                    else
                                    {
                                        await App.Current.MainPage.DisplayAlert("Alert", "Please don’t check all the items/services out for this invoice", "Ok");
                                    }
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Alert", "No schedule dates chosen for this invoice!", "Ok");
                                }

                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "No item/service chosen for this invoice", "Ok");
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

        async void OnSubmitCustInvoiceOrEstimate(CustomersModel model)
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

                    if (model != null)
                    {
                        if (Controls.StaticMembers.WayAfterChooseCust == 1)//Insert Estimate WayAfterChooseCust == 0 (Customer Page)
                        {
                            if (LstItemsEstimate.Count > 0)
                            {
                                if (Pending == true || Accept == true || Declind == true)
                                {

                                    OneEstimate.AccountId = model.AccountId;
                                    OneEstimate.BrancheId = model.BrancheId;
                                    //OneEstimate.ScheduleId = model.Id;
                                    //OneEstimate.ScheduleDateId = model.OneScheduleDate.Id;
                                    OneEstimate.EstimateDate = DateTime.Now;
                                    OneEstimate.CustomerId = model.Id;
                                    OneEstimate.Total = SubTotalEst;
                                    OneEstimate.TaxId = model.TaxId;
                                    OneEstimate.Tax = model.TaxDTO?.Rate;
                                    //OneEstimate.Taxval = (SubTotal - (SubTotal * model.MemberDTO.MemberValue / 100)) * model.TaxDTO.Rate / 100;
                                    //OneEstimate.Taxval = (model.MemeberType == false && model.TaxDTO != null) ? (SubTotal - (SubTotal * model.MemberDTO.MemberValue / 100) * model.TaxDTO.Rate / 100) : (model.TaxDTO != null && model.MemeberType == true && model.TaxDTO != null) ? ((SubTotal - model.Discount) * model.TaxDTO.Rate / 100) : 0;
                                    OneEstimate.Taxval = null;
                                    OneEstimate.MemberId = model.MemeberId;
                                    OneEstimate.Discount = Discount;
                                    OneEstimate.SignatureDraw = SignatureImageByte64Estimate;
                                    //OneEstimate.DiscountAmountOrPercent = model.MemberDTO.MemberType == false ? "%" : "$";
                                    OneEstimate.DiscountAmountOrPercent = AmountOrPersent == false ? "%" : "$";
                                    OneEstimate.Net = NetEst;
                                    OneEstimate.Status = Accept == true ? 1 : Declind == true ? 2 : 0; //0 = Pending
                                    OneEstimate.SignaturePrintName = null;
                                    OneEstimate.Terms = null;
                                    OneEstimate.NotesForCustomer = model.Notes;
                                    //OneEstimate.Notes = model.Notes;
                                    OneEstimate.Active = model.Active;
                                    OneEstimate.CreateUser = int.Parse(Helpers.Settings.UserId);
                                    OneEstimate.CreateDate = DateTime.Now;

                                    foreach (ScheduleItemsServicesModel item in LstItemsEstimate)
                                    {
                                        EstimateItemServicesModel ObjItem = new EstimateItemServicesModel
                                        {
                                            Id = item.Id,
                                            AccountId = model.AccountId,
                                            BrancheId = model.BrancheId,
                                            //TaxId = model.TaxId,
                                            //Tax = model.TaxDTO.Rate,
                                            //Taxable = (model.TaxDTO.Rate == null || model.TaxDTO.Rate == 0) ? false : true,
                                            Taxable = true,
                                            //Unit = item.Unit,
                                            Price = item.CostRate,
                                            Quantity = item.Quantity,
                                            //Discountable = model.MemberDTO.MemberValue != null ? true : false,
                                            Discountable = true,
                                            ItemsServicesId = item.ItemsServicesId,
                                            ItemsServicesName = item.ItemsServicesName,
                                            CreateUser = int.Parse(Helpers.Settings.UserId),
                                            CreateDate = DateTime.Now,
                                            Total = item.Quantity != null && item.Tax != null ? (item.CostRate * item.Quantity) + (item.CostRate * item.Quantity * item.Tax / 100) : item.Quantity == null && item.Tax != null ? item.CostRate + (item.CostRate * item.Quantity * item.Tax / 100) : item.Quantity != null && item.Tax == null ? item.CostRate * item.Quantity : item.CostRate,
                                            Active = model.Active,
                                        };
                                        OneEstimate.LstEstimateItemServices.Add(ObjItem);
                                    }

                                    UserDialogs.Instance.ShowLoading();
                                    //var json = await Helpers.Utility.PostData("api/Estimates/PostEstimate", JsonConvert.SerializeObject(OneEstimate));
                                    var json = await ORep.PostDataAsync("api/Estimates/PostEstimate", OneEstimate, UserToken);
                                    UserDialogs.Instance.HideLoading();

                                    if (json != "Bad Request" && json != "api not responding" && json.Contains("Already Exist For This Schedule Date#") != true)
                                    {
                                        //await App.Current.MainPage.DisplayAlert("FixPro", "Success Create Estimate for This Job.", "Ok");

                                        Helpers.Messages.ShowSuccessSnackBar("Success Create Estimate for This Job.");

                                        bool answer = await App.Current.MainPage.DisplayAlert("Question?", "Do you want to send an email to the customer?", "Yes", "No");

                                        if (answer)//Send Email
                                        {
                                            UserDialogs.Instance.ShowLoading();
                                            var jsonEmail = await ORep.PostStrAsync("api/Estimates/PostEstimateEmail", OneEstimate, UserToken);
                                            UserDialogs.Instance.HideLoading();

                                            if (!string.IsNullOrEmpty(jsonEmail) && jsonEmail.Contains("Send Success") == true)
                                            {
                                                Helpers.Messages.ShowSuccessSnackBar("Success Send Email to Customer.");
                                                //await App.Current.MainPage.DisplayAlert("FixPro", "Email sent successfully to customer", "Ok");
                                            }
                                            else
                                            {
                                                Helpers.Messages.ShowSuccessSnackBar("Failed to send e-mail to the customer");
                                                //await App.Current.MainPage.DisplayAlert("FixPro", "Failed to send e-mail to the customer", "Ok");
                                            }
                                        }

                                        var ViewModel = new CustomersViewModel(CustomerDetails);
                                        var page = new Views.CustomerPages.CustomersDetailsPage();
                                        page.BindingContext = ViewModel;
                                        await App.Current.MainPage.Navigation.PushAsync(page);

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
                                        //await App.Current.MainPage.DisplayAlert("Alert", "Faild Create Estimate.", "Ok");
                                    }


                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Alert", "Please Choose Status for Estimate.", "Ok");
                                }
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "No item/service chosen for this estimate!", "Ok");
                            }
                        }
                        else //Insert Invoice WayAfterChooseCust == 2 (Customer Page)
                        {
                            if (LstItemsInvoice.Count > 0)
                            {
                                var CheckItemoutFalse = LstItemsInvoice.Where(m => m.Out == false).FirstOrDefault();
                                if (CheckItemoutFalse != null)
                                {
                                    OneInvoice.AccountId = model.AccountId;
                                    OneInvoice.BrancheId = model.BrancheId;
                                    //OneInvoice.ContractId = model.ContractId;
                                    //OneInvoice.ScheduleDateId = model.OneScheduleDate.Id;
                                    //OneInvoice.ScheduleId = model.Id;
                                    OneInvoice.InvoiceDate = DateTime.Now;
                                    OneInvoice.CustomerId = model.Id;
                                    OneInvoice.Total = SubTotal;
                                    OneInvoice.TaxId = model.TaxId;
                                    OneInvoice.Tax = model.TaxDTO?.Rate;
                                    //OneInvoice.Taxval = (SubTotal - (SubTotal * model.MemberDTO.MemberValue / 100)) * model.TaxDTO.Rate / 100;
                                    //OneInvoice.Taxval = (model.MemeberType == false && model.TaxDTO != null) ? (SubTotal - (SubTotal * model.MemberDTO.MemberValue / 100) * model.TaxDTO.Rate / 100) : (model.TaxDTO != null && model.MemeberType == true && model.TaxDTO != null) ? ((SubTotal - model.Discount) * model.TaxDTO.Rate / 100) : 0;
                                    OneInvoice.Taxval = null;
                                    OneInvoice.MemberId = model.MemeberId;
                                    OneInvoice.Discount = Discount;
                                    OneInvoice.DiscountAmountOrPercent = AmountOrPersent == false ? "%" : "$";
                                    OneInvoice.Paid = 0;
                                    OneInvoice.Net = Net;
                                    OneInvoice.Status = 0; //Draft status if(1=partail & 2=paid)
                                    OneInvoice.Type = 2; //Installment Payment type
                                    OneInvoice.SignaturePrintName = null;
                                    OneInvoice.SignatureDraw = null;
                                    OneInvoice.Terms = null;
                                    OneInvoice.NotesForCustomer = model.Notes;
                                    //OneInvoice.Notes = model.Notes;
                                    OneInvoice.Active = model.Active;
                                    OneInvoice.CreateUser = int.Parse(Helpers.Settings.UserId);
                                    OneInvoice.CreateDate = DateTime.Now;

                                    foreach (ScheduleItemsServicesModel item in LstItemsInvoice)
                                    {
                                        InvoiceItemServicesModel ObjItem = new InvoiceItemServicesModel
                                        {
                                            Id = item.Id,
                                            AccountId = model.AccountId,
                                            BrancheId = model.BrancheId,
                                            ItemServiceDescription = item.ItemServiceDescription,
                                            //TaxId = model.TaxId,
                                            //Tax = model.TaxDTO.Rate,
                                            //Taxable = (model.TaxDTO.Rate == null || model.TaxDTO.Rate == 0) ? false : true,
                                            Taxable = true,
                                            //Unit = item.Unit,
                                            Price = item.CostRate,
                                            Quantity = item.Quantity,
                                            //Discountable = model.MemberDTO.MemberValue != null ? true : false,
                                            Discountable = true,
                                            ItemsServicesId = item.ItemsServicesId,
                                            ItemsServicesName = item.ItemsServicesName,
                                            CreateUser = int.Parse(Helpers.Settings.UserId),
                                            CreateDate = DateTime.Now,
                                            SkipOfTotal = item.Out,
                                            Total = item.Quantity != null && item.Tax != null ? (item.CostRate * item.Quantity) + (item.CostRate * item.Quantity * item.Tax / 100) : item.Quantity == null && item.Tax != null ? item.CostRate + (item.CostRate * item.Quantity * item.Tax / 100) : item.Quantity != null && item.Tax == null ? item.CostRate * item.Quantity : item.CostRate,
                                            Active = model.Active,
                                        };
                                        OneInvoice.LstInvoiceItemServices.Add(ObjItem);
                                    }

                                    UserDialogs.Instance.ShowLoading();
                                    //var json = await Helpers.Utility.PostMData("api/Invoices/PostInvoice", JsonConvert.SerializeObject(OneInvoice));
                                    var json = await ORep.PostDataAsync("api/Invoices/PostInvoice", OneInvoice, UserToken);
                                    UserDialogs.Instance.HideLoading();

                                    if (json != "Bad Request" && json != "api not responding" && json.Contains("Not_Enough") != true && json.Contains("This Invoice Already Exist") != true)
                                    {
                                        //await App.Current.MainPage.DisplayAlert("FixPro", "Success Create Invoice for This Job.", "Ok");
                                        Helpers.Messages.ShowSuccessSnackBar("Success Create Invoice for This Job.");

                                        bool answer = await App.Current.MainPage.DisplayAlert("Question?", "Do you want to send an email to the customer?", "Yes", "No");

                                        if (answer)//Send Email
                                        {
                                            UserDialogs.Instance.ShowLoading();
                                            var jsonEmail = await ORep.PostStrAsync("api/Invoices/PostInvoiceEmail", OneInvoice, UserToken);
                                            UserDialogs.Instance.HideLoading();

                                            if (!string.IsNullOrEmpty(jsonEmail) && jsonEmail.Contains("Send Success") == true)
                                            {
                                                Helpers.Messages.ShowSuccessSnackBar("Success Send Email to Customer.");
                                                //await App.Current.MainPage.DisplayAlert("FixPro", "Email sent successfully to customer", "Ok");
                                            }
                                            else
                                            {
                                                Helpers.Messages.ShowSuccessSnackBar("Failed to send e-mail to the customer");
                                                //await App.Current.MainPage.DisplayAlert("FixPro", "Failed to send e-mail to the customer", "Ok");
                                            }
                                        }

                                        if (OneInvoice.Net > 0)
                                        {
                                            OneInvoice.Id = int.Parse(json.Replace("\"", "").Trim());
                                            var ViewModel = new SchedulesViewModel(OneInvoice, CustomerDetails);
                                            var page = new Views.PopupPages.PaymentMethodsPopup();
                                            page.BindingContext = ViewModel;
                                            await PopupNavigation.Instance.PushAsync(page);
                                            //await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                                            // App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                                        }
                                        else
                                        {
                                            var ViewModel = new CustomersViewModel(CustomerDetails);
                                            var page = new Views.CustomerPages.CustomersDetailsPage();
                                            page.BindingContext = ViewModel;
                                            await App.Current.MainPage.Navigation.PushAsync(page);
                                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                                        }

                                    }
                                    else
                                    {
                                        await App.Current.MainPage.DisplayAlert("Alert", json, "Ok");
                                        await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                                    }
                                }
                                else
                                {
                                    await App.Current.MainPage.DisplayAlert("Alert", "Please don’t check all the items/services out for this invoice", "Ok");
                                }

                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "No item/service chosen for this invoice", "Ok");
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

        async void OnOpenCustomerDetails(CustomersModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            Controls.StaticMembers.WayCreateCust = 3;//From Schedule can edit customer and return schedule again
            Controls.StaticMembers.ScheduleIdStatic = ScheduleDetails.Id;
            Controls.StaticMembers.ScheduleDateIdStatic = ScheduleDetails.ScheduleDateId;
            var popupView = new CustomersViewModel(model);
            var page = new Views.CustomerPages.CustomersDetailsPage();
            page.BindingContext = popupView;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnCreditPayment(InvoiceModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            Controls.StaticMembers.PayCashOrCredit = 2;
            await PopupNavigation.Instance.PopAsync();
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
            await PopupNavigation.Instance.PopAsync();
            var ViewModel = new PaymentsViewModel(model, CustomerDetails);
            var page = new Views.CustomerPages.CashOrCreditPaymentPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnOpenMaterialDetails(ScheduleItemsServicesModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            await App.Current.MainPage.Navigation.PushAsync(new Views.SchedulePages.NewItemsServicesSchedulePage(model));
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnOpenMaterialReceiptDetails(ScheduleMaterialReceiptModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            await App.Current.MainPage.Navigation.PushAsync(new Views.SchedulePages.MaterialReceiptPage(model));
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnOpenServiceDetails(ScheduleItemsServicesModel model)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            await App.Current.MainPage.Navigation.PushAsync(new Views.SchedulePages.ScheduleFreeServicesPage(model));
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnOutScheduleImage(SchedulePicturesModel image)
        {
            IsBusy = true;
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                string UserToken = await _service.UserToken();
                var json = await ORep.PutStrAsync("api/Schedules/PutOutPicture", image, UserToken);
                if (json == "false")
                {
                    Helpers.Messages.ShowSuccessSnackBar("Don't Show unchecked photos to customer");
                }
                else
                {
                    Helpers.Messages.ShowSuccessSnackBar("Show checked photos to customer");
                }
            }
            IsBusy = false;
        }

        async void OnOpenFullScreenSchImage(string ImageName)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            await PopupNavigation.Instance.PushAsync(new Views.PopupPages.FullScreenImagePopup(ImageName));
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }

        async void OnOpenFullScreenSchImageBeforeInsert(ImageSource ImageName)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();
            await PopupNavigation.Instance.PushAsync(new Views.PopupPages.FullScreenImagePopup(ImageName));
            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }
    }
}
