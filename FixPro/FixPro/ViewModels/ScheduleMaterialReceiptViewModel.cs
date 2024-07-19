using Acr.UserDialogs;
using Acr.UserDialogs.Infrastructure;
using FixPro.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfCalendar.XForms;
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

namespace FixPro.ViewModels
{
    public class ScheduleMaterialReceiptViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        private MediaFile _mediaFile;

        ScheduleMaterialReceiptModel _MaterialReceipt;
        public ScheduleMaterialReceiptModel MaterialReceipt
        {
            get
            {
                return _MaterialReceipt;
            }
            set
            {
                _MaterialReceipt = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("MaterialReceipt"));
                }
            }
        }

        ObservableCollection<CustomersModel> _LstSuppliers;
        public ObservableCollection<CustomersModel> LstSuppliers
        {
            get
            {
                return _LstSuppliers;
            }
            set
            {
                _LstSuppliers = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstSuppliers"));
                }
            }
        }

        CustomersModel _OneSupplier;
        public CustomersModel OneSupplier
        {
            get
            {
                return _OneSupplier;
            }
            set
            {
                _OneSupplier = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneSupplier"));
                }
            }
        }


        ImageSource _ReceiptImage;
        public ImageSource ReceiptImage
        {
            get
            {
                return _ReceiptImage;
            }
            set
            {
                _ReceiptImage = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ReceiptImage"));
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

        public delegate void MaterialRcDelegte(ScheduleMaterialReceiptModel MaterialRc);
        public event MaterialRcDelegte MaterialRcClose;

        public ICommand Apply { get; set; }
        public ICommand SelectePickSchedulePhoto { get; set; }
        public ICommand SelecteCamSchedulePhoto { get; set; }
        public ICommand SelectSupplier { get; set; }


        public ScheduleMaterialReceiptViewModel()
        {
            AccountIdVM = int.Parse(Helpers.Settings.AccountId);
            MaterialReceipt = new ScheduleMaterialReceiptModel();
            LstSuppliers = new ObservableCollection<CustomersModel>();
            OneSupplier = new CustomersModel();

            Apply = new Command<ScheduleMaterialReceiptModel>(OnApply);
            SelectePickSchedulePhoto = new Command(OnSelectePickSchedulePhoto);
            SelecteCamSchedulePhoto = new Command(OnSelecteCamSchedulePhoto);
            SelectSupplier = new Command<CustomersModel>(OnSelectSupplier);

            ReceiptImage = "photodef.png";

            GetSuppliers();
        }

        public async void GetSuppliers()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();
                var json = await ORep.GetAsync<ObservableCollection<CustomersModel>>(string.Format("api/Customers/GetAllCustSuppliers?" + "AccountId=" + AccountIdVM), UserToken);

                if (json != null)
                {
                    LstSuppliers = json;

                }

                UserDialogs.Instance.HideLoading();
            }                
        }

        async void OnApply(ScheduleMaterialReceiptModel model)
        {
            IsBusy = true;

            if (model != null)
            {
                model.SupplierId = OneSupplier.Id;
                model.SupplierName = OneSupplier.FullName;
                MaterialRcClose.Invoke(model);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please Complete All Fields.", "Ok");
            }
            await App.Current.MainPage.Navigation.PopAsync();

            IsBusy = false;
        }

        void OnSelectSupplier(CustomersModel model)
        {
            IsBusy = true;
            OneSupplier = model;
            IsBusy = false;
        }

        //Pick Photo
        private async void OnSelectePickSchedulePhoto()
        {

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

                    ReceiptImage = ImageSource.FromStream(() =>
                    {
                        var stream = _mediaFile.GetStream();
                        return stream;
                    });

                    MaterialReceipt.ReceiptPhoto = Convert.ToBase64String(Helpers.Utility.ReadToEnd(_mediaFile.GetStream()));

                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to choose the material receipt!", "Ok");
            }

        }

        //Camera Photo
        private async void OnSelecteCamSchedulePhoto()
        {

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

                ReceiptImage = ImageSource.FromStream(() =>
                {
                    var stream = file.GetStream();
                    return stream;
                });

                MaterialReceipt.ReceiptPhoto = Convert.ToBase64String(Helpers.Utility.ReadToEnd(file.GetStream()));

                
                UserDialogs.Instance.HideLoading();
            }

            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to choose the material receipt!", "Ok");
            }
        }

       
    }
}
