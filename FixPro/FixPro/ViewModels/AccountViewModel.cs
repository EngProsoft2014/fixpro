﻿using Acr.UserDialogs;
using Newtonsoft.Json;
using Plugin.Media;
using Plugin.Media.Abstractions;
using FixPro.Models;
using FixPro.Views.MenuPages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;
using Stripe;

namespace FixPro.ViewModels
{
    public class AccountViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        private MediaFile _mediaFile;

        EmployeeModel _LoginModel;
        public EmployeeModel Login
        {
            get
            {
                return _LoginModel;
            }
            set
            {
                _LoginModel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Login"));
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

        ImageSource _AccountPhoto;
        public ImageSource AccountPhoto
        {
            get
            {
                return _AccountPhoto;
            }
            set
            {
                _AccountPhoto = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("AccountPhoto"));
                }
            }
        }

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();
        public ICommand SelecteCamAccountPhoto { get; set; }
        public ICommand SelectePickAccountPhoto { get; set; }

        public AccountViewModel()
        {
            SelecteCamAccountPhoto = new Command(OnSelecteCamAccountPhoto);
            SelectePickAccountPhoto = new Command(OnSelectePickAccountPhoto);

            Init();
        }

        void Init()
        {
            Login = new EmployeeModel();

            Login.Id = Helpers.Settings.UserId != null ? int.Parse(Helpers.Settings.UserId) : 0;
            Login.UserName = Helpers.Settings.UserName != null ? Helpers.Settings.UserName : "";
            Login.FirstName = Helpers.Settings.UserFristName != null ? Helpers.Settings.UserFristName : "";
            Login.LastName = Helpers.Settings.UserLastName != null ? Helpers.Settings.UserLastName : "";
            Login.EmailUserName = Helpers.Settings.Email != null ? Helpers.Settings.Email : "";
            Login.Phone1 = Helpers.Settings.Phone != null ? Helpers.Settings.Phone : "";
            Login.Password = Helpers.Settings.Password != null ? Helpers.Settings.Password : "";

            if (Helpers.Settings.CreateDate != "")
            {
                Login.CreateDate = Convert.ToDateTime(Helpers.Settings.CreateDate);
            }

            try
            {
                var cc = Helpers.Settings.UserPricture;
                if (Helpers.Settings.UserPricture != "" && Helpers.Settings.UserPricture != null)
                {
                    AccountPhoto = Helpers.Settings.UserPricture;
                    //var byteArray = new WebClient().DownloadData(Helpers.Settings.UserPricture);
                    //AccountPhoto = ImageSource.FromStream(() => new MemoryStream(byteArray));
                }
                else
                {
                    AccountPhoto = "avatar.png";
                }
            }
            catch (Exception)
            {
                AccountPhoto = "avatar.png";
            }

            Login.OldPicture = Controls.StaticMembers.OldProfileImageSt;
        }

        //Pick Photo
        private async void OnSelectePickAccountPhoto()
        {
            string UserToken = await _service.UserToken();

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
                        PhotoSize = PhotoSize.Medium
                    };

                    _mediaFile = await CrossMedia.Current.PickPhotoAsync();
                    if (_mediaFile == null) return;

                    //Upload Image To Server
                    UserDialogs.Instance.ShowLoading();

                    Login.Picture = Convert.ToBase64String(Helpers.Utility.ReadToEnd(_mediaFile.GetStream()));

                    //string Postjson = await Helpers.Utility.PostData(string.Format("api/ImageMobile/ReplacePostOneImageProfileImageOnlyMobile"), JsonConvert.SerializeObject(Login));
                    var Postjson = await ORep.PostAsync(string.Format("api/ImageMobile/ReplacePostOneImageProfileImageOnlyMobile"), Login, UserToken);

                    if (Postjson != null)
                    {
                        //EmployeeModel UserInfo = JsonConvert.DeserializeObject<EmployeeModel>(Postjson);

                        Helpers.Settings.UserPricture = Helpers.Utility.PathServerProfileImages + Postjson.Picture;
                        Login.Picture = Postjson.Picture;
                        Controls.StaticMembers.OldProfileImageSt = Login.OldPicture = Postjson.Picture;
                        AccountPhoto = ImageSource.FromStream(() => _mediaFile.GetStream());
                    }
                    UserDialogs.Instance.HideLoading();

                    await App.Current.MainPage.Navigation.PushAsync(new AccountPage());
                    App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);

                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                //throw ex;
                await App.Current.MainPage.DisplayAlert("Error", "This is not support on your device.", "Ok");
            }
        }

        //Camera Photo
        private async void OnSelecteCamAccountPhoto()
        {
            string UserToken = await _service.UserToken();

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
                    Name = "test.jpg"
                });

                if (file == null)
                    return;

                //Upload Image To Server
                UserDialogs.Instance.ShowLoading();

                Login.Picture = Convert.ToBase64String(Helpers.Utility.ReadToEnd(file.GetStream()));

                //string Postjson = await Helpers.Utility.PostData(string.Format("api/ImageMobile/ReplacePostOneImageProfileImageOnlyMobile"), JsonConvert.SerializeObject(Login));
                var Postjson = await ORep.PostAsync(string.Format("api/ImageMobile/ReplacePostOneImageProfileImageOnlyMobile"), Login, UserToken);


                if (Postjson != null)
                {
                    //EmployeeModel UserInfo = JsonConvert.DeserializeObject<EmployeeModel>(Postjson);

                    Helpers.Settings.UserPricture = Helpers.Utility.PathServerProfileImages + Postjson.Picture;
                    Login.Picture = Postjson.Picture;
                    Controls.StaticMembers.OldProfileImageSt = Login.OldPicture = Postjson.Picture;
                    AccountPhoto = ImageSource.FromStream(() => file.GetStream());
                }
                UserDialogs.Instance.HideLoading();

                await App.Current.MainPage.Navigation.PushAsync(new AccountPage());
                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);

                UserDialogs.Instance.HideLoading();
            }

            catch (Exception ex)
            {
                //throw ex;
                await App.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "Ok");
            }
        }

    }
}
