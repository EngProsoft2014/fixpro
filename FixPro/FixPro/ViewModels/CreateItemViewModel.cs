using Acr.UserDialogs;
using FixPro.Models;
using FixPro.Views.SchedulePages;
using GoogleApi.Entities.Translate.Common.Enums;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FixPro.ViewModels
{
    public class CreateItemViewModel : INotifyPropertyChanged
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

        ItemsServicesCategoryModel _OneItemsServicesCategory;
        public ItemsServicesCategoryModel OneItemsServicesCategory
        {
            get
            {
                return _OneItemsServicesCategory;
            }
            set
            {
                _OneItemsServicesCategory = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneItemsServicesCategory"));
                }
            }
        }

        ItemsServicesSubCategoryModel _OneItemsServicesSubCategory;
        public ItemsServicesSubCategoryModel OneItemsServicesSubCategory
        {
            get
            {
                return _OneItemsServicesSubCategory;
            }
            set
            {
                _OneItemsServicesSubCategory = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneItemsServicesSubCategory"));
                }
            }
        }

        ItemsServicesTypes _OneItemsServicesType;
        public ItemsServicesTypes OneItemsServicesType
        {
            get
            {
                return _OneItemsServicesType;
            }
            set
            {
                _OneItemsServicesType = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneItemsServicesType"));
                }
            }
        }

        ObservableCollection<ItemsServicesCategoryModel> _LstItemsServicesCategories;
        public ObservableCollection<ItemsServicesCategoryModel> LstItemsServicesCategories
        {
            get
            {
                return _LstItemsServicesCategories;
            }
            set
            {
                _LstItemsServicesCategories = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstItemsServicesCategories"));
                }
            }
        }

        ObservableCollection<ItemsServicesSubCategoryModel> _LstItemsServicesSubCategories;
        public ObservableCollection<ItemsServicesSubCategoryModel> LstItemsServicesSubCategories
        {
            get
            {
                return _LstItemsServicesSubCategories;
            }
            set
            {
                _LstItemsServicesSubCategories = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstItemsServicesSubCategories"));
                }
            }
        }

        ObservableCollection<ItemsServicesTypes> _LstItemsServicesTypes;
        public ObservableCollection<ItemsServicesTypes> LstItemsServicesTypes
        {
            get
            {
                return _LstItemsServicesTypes;
            }
            set
            {
                _LstItemsServicesTypes = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstItemsServicesTypes"));
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

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        public delegate void AddItemDelegte(ItemsServicesModel item);
        public event AddItemDelegte AddItemClose;

        public ICommand AddNewItemService { get; set; }

        public CreateItemViewModel()
        {
            Init();
        }

        async void Init()
        {
            ItemDetails = new ItemsServicesModel();
            OneItemsServicesCategory = new ItemsServicesCategoryModel();
            OneItemsServicesSubCategory = new ItemsServicesSubCategoryModel();
            LstItemsServicesCategories = new ObservableCollection<ItemsServicesCategoryModel>();
            LstItemsServicesSubCategories = new ObservableCollection<ItemsServicesSubCategoryModel>();
            LstItemsServicesTypes = new ObservableCollection<ItemsServicesTypes>();
            OneItemsServicesType = new ItemsServicesTypes();

            AddNewItemService = new Command<ItemsServicesModel>(OnAddNewItemService);

            LstItemsServicesTypes.Add(new ItemsServicesTypes() { Id = 1, Name = "Service" });
            LstItemsServicesTypes.Add(new ItemsServicesTypes() { Id = 2, Name = "Item" });
            LstItemsServicesTypes.Add(new ItemsServicesTypes() { Id = 3, Name = "Lebor" });
            LstItemsServicesTypes.Add(new ItemsServicesTypes() { Id = 4, Name = "Other" });

            await GetItemsServicesCategories();
            await GetItemsServicesSubCategories();
        }

        async Task GetItemsServicesCategories()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<ObservableCollection<ItemsServicesCategoryModel>>(string.Format("api/Schedules/GetItemsServicesCategories?" + "AccountId=" + Helpers.Settings.AccountId), UserToken);

                if (json != null)
                {
                    LstItemsServicesCategories = json;
                }

                UserDialogs.Instance.HideLoading();
            }
        }

        async Task GetItemsServicesSubCategories()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<ObservableCollection<ItemsServicesSubCategoryModel>>(string.Format("api/Schedules/GetItemsServicesSubCategories?" + "AccountId=" + Helpers.Settings.AccountId), UserToken);

                if (json != null)
                {
                    LstItemsServicesSubCategories = json;
                }

                UserDialogs.Instance.HideLoading();
            }
        }

        async void OnAddNewItemService(ItemsServicesModel item)
        {
            IsBusy = true;
            UserDialogs.Instance.ShowLoading();

            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    return;
                }
                else
                {
                    if (string.IsNullOrEmpty(item.Name))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Name.", "Ok");
                    }
                    else if (OneItemsServicesType.Id == 0 || OneItemsServicesType == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Type.", "Ok");
                    }
                    else if (OneItemsServicesCategory.Id == 0 || OneItemsServicesCategory == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Category.", "Ok");
                    }
                    else if (OneItemsServicesSubCategory.Id == 0 || OneItemsServicesSubCategory == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Sub Category.", "Ok");
                    }
                    else if (item.QTYTime == 0 || item.QTYTime == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : QTY.", "Ok");
                    }
                    else if (item.CostperUnit == 0 || item.CostperUnit == null)
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Price.", "Ok");
                    }
                    else
                    {
                        if (ItemDetails != null)
                        {
                            string UserToken = await _service.UserToken();

                            ItemDetails.AccountId = int.Parse(Helpers.Settings.AccountId);
                            ItemDetails.BrancheId = int.Parse(Helpers.Settings.BranchId);
                            ItemDetails.CategoryId = OneItemsServicesCategory.Id;
                            ItemDetails.SubCategoryId = OneItemsServicesSubCategory.Id;
                            ItemDetails.Type = OneItemsServicesType.Id;
                            ItemDetails.InventoryItem = OneItemsServicesType.Id == 2 ? true : false;
                            ItemDetails.Active = true;
                            ItemDetails.CreateUser = int.Parse(Helpers.Settings.UserId);
                            ItemDetails.CreateDate = DateTime.Now;

                            UserDialogs.Instance.ShowLoading();
                            var Json = await ORep.PostStrAsync("api/Schedules/PostAddItemService", ItemDetails, UserToken);
                            UserDialogs.Instance.HideLoading();

                            if (Json != "")
                            {
                                await App.Current.MainPage.DisplayAlert("FixPro", "Item/service added successfully", "Ok");

                                var ReturnItem = JsonConvert.DeserializeObject<ItemsServicesModel>(Json);
                                AddItemClose.Invoke(ReturnItem);
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("FixPro", "Failed to add this item/service", "Ok");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

            UserDialogs.Instance.HideLoading();
            IsBusy = false;
        }
    }
}
