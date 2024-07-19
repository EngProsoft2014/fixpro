using Acr.UserDialogs;
using FixPro.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FixPro.ViewModels
{
    public class ScheduleFreeServicesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        ItemsServicesModel _ServiceDetails;
        public ItemsServicesModel ServiceDetails
        {
            get
            {
                return _ServiceDetails;
            }
            set
            {
                _ServiceDetails = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ServiceDetails"));
                }
            }
        }

        ObservableCollection<ItemsServicesModel> _LstServiceCategory;
        public ObservableCollection<ItemsServicesModel> LstServiceCategory
        {
            get
            {
                return _LstServiceCategory;
            }
            set
            {
                _LstServiceCategory = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstServiceCategory"));
                }
            }
        }

        Object _SelectedServiceCateory;
        public Object SelectedServiceCateory
        {
            get
            {
                return _SelectedServiceCateory;
            }
            set
            {
                _SelectedServiceCateory = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SelectedServiceCateory"));
                }
            }
        }

        bool _ShowQty;
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

        public int AccountIdVM;

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        public delegate void ServiceDelegte(ItemsServicesModel item);
        public event ServiceDelegte ServiceClose;

        public ICommand ApplyService { get; set; }
        public ICommand SelectedServiceForGetCost { get; set; }

        public ScheduleFreeServicesViewModel()
        {

        }

        public ScheduleFreeServicesViewModel(bool ShowQTY) 
        {
            ShowQty = ShowQTY;

            AccountIdVM = int.Parse(Helpers.Settings.AccountId);
            LstServiceCategory = new ObservableCollection<ItemsServicesModel>();
            ServiceDetails = new ItemsServicesModel();
            ApplyService = new Command<ItemsServicesModel>(OnApplyService);
            SelectedServiceForGetCost = new Command<ItemsServicesModel>(OnSelectedServiceForGetCost);

            GetItemsServices();
        }

        async void OnApplyService(ItemsServicesModel model)
        {
            IsBusy = true;

            //model.OneItemService = SelectedServiceCateory as ItemsServicesModel;
            if (model.CategoryId != null)
            {
                ServiceClose.Invoke(model);
                await App.Current.MainPage.Navigation.PopAsync();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Complete All Fields.", "Ok");
            }
     
            IsBusy = false;
        }

        public async void GetItemsServices()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<ObservableCollection<ItemsServicesModel>>(string.Format("api/Schedules/GetServices?" + "AccountId=" + AccountIdVM), UserToken);

                if (json != null)
                {
                    LstServiceCategory = json;
                }

                UserDialogs.Instance.HideLoading();
            }          
        }

        async void OnSelectedServiceForGetCost(ItemsServicesModel model)
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                if (model != null)
                {
                    string UserToken = await _service.UserToken();
                    var json = await ORep.GetAsync<ItemsServicesModel>(string.Format("api/Schedules/GetServiceCost?" + "AccountId=" + AccountIdVM + "&" + "ServiceId=" + model.Id), UserToken);

                    if (json != null)
                    {
                        ServiceDetails = json;
                    }
                }

                UserDialogs.Instance.HideLoading();
            }        
        }
    }
}
