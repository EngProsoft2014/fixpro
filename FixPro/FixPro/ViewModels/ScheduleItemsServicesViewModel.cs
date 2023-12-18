using Acr.UserDialogs;
using Newtonsoft.Json;
using FixPro.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FixPro.ViewModels
{
    public class ScheduleItemsServicesViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        ItemsServicesModel _ItemDetails;
        public ItemsServicesModel ItemDetails
        {
            get
            {
                return _ItemDetails;
            }
            set
            {
                _ItemDetails = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ItemDetails"));
                }
            }
        }

        ObservableCollection<ItemsServicesModel> _LstItemCategory;
        public ObservableCollection<ItemsServicesModel> LstItemCategory
        {
            get
            {
                return _LstItemCategory;
            }
            set
            {
                _LstItemCategory = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstItemCategory"));
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

        public delegate void ItemDelegte(ItemsServicesModel item);
        public event ItemDelegte ItemClose;

        public ICommand ApplyItems { get; set; }
        public ICommand SelectedItemForGetCost { get; set; }

        public ScheduleItemsServicesViewModel()
        {

        }

        public ScheduleItemsServicesViewModel(bool ShowQTY)
        {
            ShowQty = ShowQTY;

            AccountIdVM = int.Parse(Helpers.Settings.AccountId);
            LstItemCategory = new ObservableCollection<ItemsServicesModel>();
            ItemDetails = new ItemsServicesModel();
            ApplyItems = new Command<ItemsServicesModel>(OnApplyItems);
            SelectedItemForGetCost = new Command<ItemsServicesModel>(OnSelectedItemForGetCost);


            if(ShowQTY == true) // Show All Items And Services in Estimate or Invoice
            {
                if (Controls.StaticMembers.WayAfterChooseCust == 0) //Way from Estimate or Invoice for Schedule
                {
                    //scd system plan  1 all , 2 Service                   
                    if (Helpers.Settings.TypeTrackingSch_Invo == "2")
                    {
                        GetFreeServices();
                    }
                    else
                    {
                        GetAllItemsServices();
                    }
                }
                else //Way from Estimate or Invoice for Customer Directe
                {
                    GetAllItemsServices();
                }    
            }
            else // show item invintory only in schedule material
            {
                GetItemsInventory();
            }
            
        }

        async void OnApplyItems(ItemsServicesModel model)
        {
            IsBusy = true;

            //model.OneItemService = SelectedServiceCateory as ItemsServicesModel;
            if (model.CategoryId != null)
            {
                ItemClose.Invoke(model);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Complete All Fields.", "Ok");
            }
            await App.Current.MainPage.Navigation.PopAsync();
            IsBusy = false;
        }

        public async void GetItemsInventory()
        {
            UserDialogs.Instance.ShowLoading();
            string UserToken = await _service.UserToken();
            var json = await ORep.GetAsync<ObservableCollection<ItemsServicesModel>>(string.Format("api/Schedules/GetItemsInventory?" + "AccountId=" + AccountIdVM),UserToken);

            if (json != null)
            {
                LstItemCategory = json;
            }

            UserDialogs.Instance.HideLoading();
        }

        public async void GetFreeServices()
        {
            UserDialogs.Instance.ShowLoading();
            string UserToken = await _service.UserToken();
            var json = await ORep.GetAsync<ObservableCollection<ItemsServicesModel>>(string.Format("api/Schedules/GetServices?" + "AccountId=" + AccountIdVM), UserToken);

            if (json != null)
            {
                LstItemCategory = json;
            }

            UserDialogs.Instance.HideLoading();
        }

        public async void GetAllItemsServices()
        {
            UserDialogs.Instance.ShowLoading();

            string UserToken = await _service.UserToken();
            var json = await ORep.GetAsync<ObservableCollection<ItemsServicesModel>>(string.Format("api/Schedules/GetAllItemsServices?" + "AccountId=" + AccountIdVM), UserToken);

            if (json != null)
            {
                LstItemCategory = json;
            }

            UserDialogs.Instance.HideLoading();
        }


        async void OnSelectedItemForGetCost(ItemsServicesModel model)
        {
            UserDialogs.Instance.ShowLoading();
            if(model != null)
            {
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<ItemsServicesModel>(string.Format("api/Schedules/GetServiceCost?" + "AccountId=" + AccountIdVM + "&" + "ServiceId=" + model.Id), UserToken);

                if (json != null)
                {
                    ItemDetails = json;
                }
            }

            UserDialogs.Instance.HideLoading();
        }
    }
}
