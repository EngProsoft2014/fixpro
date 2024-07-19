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
using GoogleApi.Entities.Translate.Common.Enums;
using Xamarin.Essentials;
using System.Linq;

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
        public ICommand OpenFilterMaterial { get; set; }
        public ICommand CreateNewItem { get; set; }


        public ScheduleItemsServicesViewModel()
        {
            GetPerrmission();
        }

        public ScheduleItemsServicesViewModel(bool ShowQTY)
        {
            ShowQty = ShowQTY;

            GetPerrmission();

            AccountIdVM = int.Parse(Helpers.Settings.AccountId);
            LstItemCategory = new ObservableCollection<ItemsServicesModel>();
            ItemDetails = new ItemsServicesModel();
            ApplyItems = new Command<ItemsServicesModel>(OnApplyItems);
            SelectedItemForGetCost = new Command<ItemsServicesModel>(OnSelectedItemForGetCost);
            OpenFilterMaterial = new Command(OnOpenFilterMaterial);
            CreateNewItem = new Command(OnCreateNewItem);

            CheckWayForGetRightData(ShowQty);

        }

        void CheckWayForGetRightData(bool ShowQTY)
        {
            if (ShowQTY == true) // Show All Items And Services in Estimate or Invoice
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
                else //Way from Estimate or Invoice for Customer Direct
                {
                    GetAllItemsServices();
                }
            }
            else // show item invintory only in schedule material
            {
                GetItemsInventory();
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

        async void OnApplyItems(ItemsServicesModel model)
        {
            IsBusy = true;

            //model.OneItemService = SelectedServiceCateory as ItemsServicesModel;
            if (model.CategoryId != null)
            {

                if (model.Type == 2 && model.QTYTime < 1) //items
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "Enter QTY field bigger than 0", "Ok");
                }
                else
                {
                    if (model.Type == 1) //Service
                    {
                        model.QTYTime = 1;
                    }

                    ItemClose.Invoke(model);
                    await App.Current.MainPage.Navigation.PopAsync();
                }
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Complete All Fields.", "Ok");
            }
            
            IsBusy = false;
        }

        public async void GetItemsInventory()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<ObservableCollection<ItemsServicesModel>>(string.Format("api/Schedules/GetItemsInventory?" + "AccountId=" + AccountIdVM), UserToken);

                if (json != null)
                {
                    LstItemCategory = json;
                }

                UserDialogs.Instance.HideLoading();
            }
        }

        public async void GetFreeServices()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
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
        }

        public async void GetAllItemsServices()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
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
        }

        async void OnSelectedItemForGetCost(ItemsServicesModel model)
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
                        ItemDetails = json;
                    }
                }

                UserDialogs.Instance.HideLoading();
            }
        }

        async void OnOpenFilterMaterial()
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
                    var pageView = new Views.SchedulePages.FilterMaterials(LstItemCategory);
                    pageView.DidClose += (item) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        ItemDetails = item;

                        UserDialogs.Instance.HideLoading();
                    };

                    await App.Current.MainPage.Navigation.PushAsync(pageView);
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to choose this item", "OK");
            }

        }

        async void OnCreateNewItem()
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
                    var popupView = new CreateItemViewModel();
                    popupView.AddItemClose += async (item) =>
                    {
                        UserDialogs.Instance.ShowLoading();

                        if (item != null)
                        {
                            await App.Current.MainPage.Navigation.PopAsync();
                            CheckWayForGetRightData(ShowQty);
                        }

                        UserDialogs.Instance.HideLoading();
                    };
                    var page = new Views.SchedulePages.CreateItemPage();
                    page.BindingContext = popupView;
                    await App.Current.MainPage.Navigation.PushAsync(page);
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to add this item", "OK");
            }
            IsBusy = false;
        }
    }
}
