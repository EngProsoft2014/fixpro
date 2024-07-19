using Acr.UserDialogs;
using Newtonsoft.Json;
using FixPro.Models;
using FixPro.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FixPro.ViewModels
{
    public class TimeSheetViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        ObservableCollection<CheckInOutModel> _LstEmployeesIn;
        public ObservableCollection<CheckInOutModel> LstEmployeesIn
        {
            get
            {
                return _LstEmployeesIn;
            }
            set
            {
                _LstEmployeesIn = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEmployeesIn"));
                }
            }
        }

        ObservableCollection<CheckInOutModel> _LstEmployeesOut;
        public ObservableCollection<CheckInOutModel> LstEmployeesOut
        {
            get
            {
                return _LstEmployeesOut;
            }
            set
            {
                _LstEmployeesOut = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstEmployeesOut"));
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

        string _Date;
        public string Date
        {
            get
            {
                return _Date;
            }
            set
            {
                _Date = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Date"));
                }
            }
        }

        string _NumIn;
        public string NumIn
        {
            get
            {
                return _NumIn;
            }
            set
            {
                _NumIn = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("NumIn"));
                }
            }
        }

        string _NumOut;
        public string NumOut
        {
            get
            {
                return _NumOut;
            }
            set
            {
                _NumOut = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("NumOut"));
                }
            }
        }

        DateTime date;

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        public ICommand NextDay { get; set; }
        public ICommand BackDay { get; set; }
        public ICommand RefreshLstEmployees { get; set; }
        public ICommand SelectedDate { get; set; }
        public ICommand SelectedTimeIn { get; set; }
        public ICommand SelectedTimeOut { get; set; }
        public ICommand SelectedTimeMyStart { get; set; }
        public ICommand SelectedTimeMyEnd { get; set; }


        public TimeSheetViewModel()
        {
            Init();
            NextDay = new Command(OnNextDay);
            BackDay = new Command(OnBackDay);
            RefreshLstEmployees = new Command(OnRefreshLstEmployees);
            SelectedDate = new Command(OnSelectedDate);
            SelectedTimeIn = new Command<CheckInOutModel>(OnSelectedTimeIn);
            SelectedTimeOut = new Command<CheckInOutModel>(OnSelectedTimeOut);
            SelectedTimeMyStart = new Command<CheckInOutModel>(OnSelectedTimeMyStart);
            SelectedTimeMyEnd = new Command<CheckInOutModel>(OnSelectedTimeMyEnd);
        }

        void Init()
        {
            if (Controls.StaticMembers.SelectedDate.ToString("MM-dd-yyyy") == DateTime.Now.ToString("MM-dd-yyyy"))
            {
                date = DateTime.Now;
                Date = "Today";
            }
            else
            {
                date = Controls.StaticMembers.SelectedDate;
                Date = Controls.StaticMembers.SelectedDate.ToString("MM-dd-yyyy");
            }

            LstEmployeesIn = new ObservableCollection<CheckInOutModel>();
            LstEmployeesOut = new ObservableCollection<CheckInOutModel>();

            GetCheckInOutEmployees(date.ToString("MM-dd-yyyy"));
        }

        async void GetCheckInOutEmployees(string date)
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<List<CheckInOutModel>>("api/TimeSheet/GetCheckInOut?" + "date=" + date + "&" + "userId=" + Helpers.Settings.UserId + "&" + "userRole=" + Controls.StartData.EmployeeDataStatic.UserRole.ToString(), UserToken);

                if (json != null)
                {
                    LstEmployeesIn = new ObservableCollection<CheckInOutModel>(json.Where(x => x.HoursTo == "" || x.HoursTo == null).OrderBy(x => x.EmployeeName).ToList());
                    LstEmployeesOut = new ObservableCollection<CheckInOutModel>(json.Where(x => x.HoursTo != "" && x.HoursTo != null).OrderBy(x => x.EmployeeName).ToList());

                    NumIn = LstEmployeesIn.Count.ToString();
                    NumOut = LstEmployeesOut.Count.ToString();
                }
                else
                {
                    NumIn = "0";
                    NumOut = "0";
                }

                UserDialogs.Instance.HideLoading();
            }
        }

        void OnRefreshLstEmployees()
        {
            IsRefresh = true;

            GetCheckInOutEmployees(date.ToString("MM-dd-yyyy"));

            IsRefresh = false;
        }

        void OnNextDay()
        {
            IsBusy = true;
            date = Controls.StaticMembers.SelectedDate;
            Date = date.AddDays(1).ToString("MM-dd-yyyy") == DateTime.Now.ToString("MM-dd-yyyy") ? "Today" : date.AddDays(1).ToString("MM-dd-yyyy");
            Controls.StaticMembers.SelectedDate = date = date.AddDays(1);
            OnRefreshLstEmployees();
            IsBusy = false;
        }

        void OnBackDay()
        {
            IsBusy = true;
            date = Controls.StaticMembers.SelectedDate;
            Date = date.AddDays(-1).ToString("MM-dd-yyyy") == DateTime.Now.ToString("MM-dd-yyyy") ? "Today" : date.AddDays(-1).ToString("MM-dd-yyyy");
            Controls.StaticMembers.SelectedDate = date = date.AddDays(-1);
            OnRefreshLstEmployees();
            IsBusy = false;
        }

        async void OnSelectedDate()
        {
            IsBusy = true;
            var popupView = new Views.PopupPages.DatePopup();
            popupView.RangeClose += (calendar) =>
            {
                UserDialogs.Instance.ShowLoading();

                GetCheckInOutEmployees(calendar.StartDate.Value.ToString("MM-dd-yyyy"));

                Date = calendar.StartDate.Value.ToString("MM-dd-yyyy");

                UserDialogs.Instance.HideLoading();
            };

            await PopupNavigation.Instance.PushAsync(popupView);
            IsBusy = false;
        }

        async void OnSelectedTimeIn(CheckInOutModel model)
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
                    var popupView = new Views.PopupPages.CheckoutPopup(model.Id, model.HoursFrom);
                    popupView.TimeDidClose += async (time) =>
                    {
                        UserDialogs.Instance.ShowLoading();
                        string UserToken = await _service.UserToken();
                        model.HoursFrom = string.Format(time.ToString(@"hh\:mm"));
                        model.Active = true;

                        //string json = await Helpers.Utility.PutPosData(string.Format("api/TimeSheet/PutCheckInOut/{0}", model.EmployeeId), JsonConvert.SerializeObject(model));

                        var json = await ORep.PutAsync(string.Format("api/TimeSheet/PutCheckInOut/{0}", model.EmployeeId), model, UserToken);

                        if (json != null)
                        {
                            await App.Current.MainPage.DisplayAlert("FixPro", "Check in time saved successfully", "Ok");
                            await App.Current.MainPage.Navigation.PushAsync(new Views.TimeSheetPage());
                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
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

        async void OnSelectedTimeOut(CheckInOutModel model)
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
                    var popupView = new Views.PopupPages.CheckoutPopup(model.HoursFrom);
                    popupView.TimeDidClose += async (time) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        if (time > TimeSpan.Parse(model.HoursFrom))
                        {
                            string UserToken = await _service.UserToken();
                            //model.HoursTo = string.Format("{0:hh:mm}", time.ToString());
                            model.HoursTo = string.Format(time.ToString(@"hh\:mm"));
                            model.DurationHours = (time - TimeSpan.Parse(model.HoursFrom)).Hours.ToString();
                            model.DurationMinutes = (time - TimeSpan.Parse(model.HoursFrom)).Minutes.ToString();

                            //await Helpers.Utility.PutPosData(string.Format("api/TimeSheet/PutCheckInOut/{0}", model.EmployeeId), JsonConvert.SerializeObject(model));

                            await ORep.PutAsync(string.Format("api/TimeSheet/PutCheckInOut/{0}", model.EmployeeId), model, UserToken);

                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Check out not determined! Choose the check in time first please", "Ok");
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

        async void OnSelectedTimeMyStart(CheckInOutModel model)
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

                    string UserToken = await _service.UserToken();

                    model.HoursFrom = string.Format(DateTime.Now.ToString(@"hh\:mm"));
                    model.Date = Controls.StaticMembers.SelectedDate.ToString("yyyy-MM-dd");
                    model.CreateDate = DateTime.Now;
                    model.CreateUser = int.Parse(Helpers.Settings.UserId);
                    model.SheetColor = "#26cc8a";
                    model.Active = true;

                    //string json = await Helpers.Utility.PostData("api/TimeSheet/PostCheckInOut", JsonConvert.SerializeObject(model));

                    var json = await ORep.PostAsync("api/TimeSheet/PostCheckInOut", model, UserToken);

                    //if (json != null && json != "api not responding")
                    if (json != null && json.Id != 0)
                    {
                        await App.Current.MainPage.DisplayAlert("FixPro", "Check in time saved successfully", "Ok");
                        await App.Current.MainPage.Navigation.PushAsync(new Views.TimeSheetPage());
                        App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("FixPro", "Failed to save the check in time!", "Ok");
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

        async void OnSelectedTimeMyEnd(CheckInOutModel model)
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

                    TimeSpan time = DateTime.Now.TimeOfDay;

                    if (time > TimeSpan.Parse(model.HoursFrom))
                    {
                        string UserToken = await _service.UserToken();

                        model.HoursTo = string.Format(time.ToString(@"hh\:mm"));
                        model.DurationHours = (time - TimeSpan.Parse(model.HoursFrom)).Hours.ToString();
                        model.DurationMinutes = (time - TimeSpan.Parse(model.HoursFrom)).Minutes.ToString();

                        //string json = await Helpers.Utility.PutPosData(string.Format("api/TimeSheet/PutCheckInOut/{0}", model.EmployeeId), JsonConvert.SerializeObject(model));

                        var json = await ORep.PutAsync(string.Format("api/TimeSheet/PutCheckInOut/{0}", model.EmployeeId), model, UserToken);

                        //if (json != null && json != "api not responding")
                        if (json != null)
                        {
                            await App.Current.MainPage.DisplayAlert("FixPro", "Check out time saved successfully", "Ok");
                            await App.Current.MainPage.Navigation.PushAsync(new Views.TimeSheetPage());
                            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                        }
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Check out not determined! Choose the check in time first please", "Ok");
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

    }
}
