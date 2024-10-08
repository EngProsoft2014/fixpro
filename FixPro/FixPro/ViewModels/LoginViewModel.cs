﻿using Acr.UserDialogs;
using Newtonsoft.Json;
using FixPro.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Windows.Input;

using Xamarin.Forms;
using OneSignalSDK.Xamarin;
using Akavache;
using FixPro.Services.Data;
using System.Reactive.Linq;
using Xamarin.Essentials;

namespace FixPro.ViewModels
{
    internal class LoginViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

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

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        public ICommand ClickLogin { get; set; }

        public LoginViewModel()
        {
            ClickLogin = new Command<EmployeeModel>(OnClickLogin);

            if (Helpers.Settings.UserId != "0" && Helpers.Settings.UserName != "")
            {
                Login = new EmployeeModel()
                {
                    Id = int.Parse(Helpers.Settings.UserId),
                    UserName = Helpers.Settings.UserName,
                    FirstName = Helpers.Settings.UserFristName,
                    Password = Helpers.Settings.Password,
                    EmailUserName = Helpers.Settings.Email,
                    Phone1 = Helpers.Settings.Phone,
                    BrancheId = int.Parse(Helpers.Settings.BranchId),
                    BranchName = Helpers.Settings.BranchName,
                    CreateDate = Helpers.Settings.CreateDate == "" ? DateTime.Now : DateTime.Parse(Helpers.Settings.CreateDate),
                    OneSignalPlayerId = Helpers.Settings.PlayerId,
                };
            }
            else
            {
                Login = new EmployeeModel();
            }
        }

        async void OnClickLogin(EmployeeModel model)
        {
            IsBusy = true;

            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();

                Login = model;

                if (string.IsNullOrEmpty(Login.UserName))
                {
                    await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : User Name.", "Ok");
                }
                else if (string.IsNullOrEmpty(Login.Password))
                {
                    await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Password.", "Ok");
                }
                else
                {

                    var MLogin = await ORep.GetLoginAsync<EmployeeModel>("api/Login/GetLogin?" + "UserName=" + model.UserName + "&" + "Password=" + model.Password + "&" + "PlayerId=" + Helpers.Settings.PlayerId);

                    if (MLogin.EmployeeStatus?.Contains("Try Again") == true)//UserName Or Password is Wrong
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Wrong Username Or Password.", "Ok");
                    }
                    else if (MLogin.EmployeeStatus?.Contains("Account Is Expired") == true)
                    {
                        Helpers.Settings.Email = MLogin.EmailUserName;
                        bool answer = await App.Current.MainPage.DisplayAlert("Question?", "Account expired, Do you want to buy a pricing plan?", "Yes", "No");
                        if(answer)
                        {
                            var ViewModel = new PlansViewModel();
                            var page = new Views.PlansPages.ChoosePlanPage();
                            page.BindingContext = ViewModel;
                            await App.Current.MainPage.Navigation.PushAsync(page);
                        }
                    }
                    else
                    {

                        if (MLogin.ActiveAccount == true)
                        {
                            if (MLogin.ActiveMobileLogin == true)
                            {

                                Helpers.Settings.UserId = MLogin.Id.ToString();
                                Helpers.Settings.AccountId = MLogin.AccountId.ToString();
                                Helpers.Settings.UserName = MLogin.UserName;
                                Helpers.Settings.Password = model.Password;
                                Helpers.Settings.AccountName = MLogin.AccountName.ToString();
                                Helpers.Settings.AccountDayExpired = DateTime.Now.ToString();//for check one time only in day
                                Helpers.Settings.AccountNameWithSpace = MLogin.CompanyNameWithSpace.ToString();                
                                Helpers.Settings.UserFristName = MLogin.FirstName;
                                Helpers.Settings.UserLastName = MLogin.LastName;
                                Helpers.Settings.Email = MLogin.EmailUserName;
                                Helpers.Settings.Phone = MLogin.Phone1; 
                                Helpers.Settings.CreateDate = MLogin.CreateDate.ToString();
                                Helpers.Settings.AccountId = MLogin.AccountId.ToString();
                                Helpers.Settings.BranchId = MLogin.BrancheId.ToString();
                                Helpers.Settings.BranchName = MLogin.BranchName;
                                Helpers.Settings.UserRole = MLogin.UserRole.ToString();
                                Helpers.Settings.UserEmployees = MLogin.Employees;
                                Helpers.Settings.TypeTrackingSch_Invo = MLogin.TypeTrackingSch_Invo.ToString();
                                Helpers.Utility.ServerUrl = !string.IsNullOrEmpty(MLogin.AccountSubdomainApiURL) ? MLogin.AccountSubdomainApiURL : Helpers.Utility.ServerUrl;

                                await BlobCache.LocalMachine.InsertObject(ServicesService.UserTokenServiceKey, MLogin.GernToken, DateTimeOffset.Now.AddHours(24));

                                Helpers.Settings.UserPricture = Helpers.Utility.PathServerProfileImages + Helpers.Settings.AccountName + "/" + MLogin.Picture;

                                //await Controls.StartData.CheckPermissionEmployee(); //Check Permission employee


                                await App.Current.MainPage.Navigation.PushAsync(new MainPage());



                                //if (MLogin.OneSignalPlayerId != Helpers.Settings.PlayerId)
                                //{
                                //    UpdatePlayerIdModel oUpdatePlayerIdModel = new UpdatePlayerIdModel
                                //    {
                                //        AccountId = Helpers.Settings.AccountId,
                                //        PlayerId = Helpers.Settings.PlayerId = OneSignal.Default.DeviceState.userId != null ? OneSignal.Default.DeviceState.userId.ToString() : null,
                                //        UserId = Helpers.Settings.UserId,
                                //    };

                                //    Controls.StartData.IsRunning = true;
                                //    string UserToken = await _service.UserToken();
                                //    string Re = await ORep.PostDataAsync("api/Notifications/PostPlayerId", oUpdatePlayerIdModel, UserToken);
                                //    if (Re != "api not responding")
                                //    {
                                //        Helpers.Settings.PlayerId = Re;
                                //    }
                                //}

                                //await ((App)Application.Current).SignalRservice();


                                //Device.StartTimer(new TimeSpan(0, 0, 3), () =>
                                //{
                                //    if (Helpers.Settings.PlayerId != MLogin.OneSignalPlayerId && Helpers.Settings.PlayerId != "0")
                                //    {
                                //        UpdatePlayerIdModel oUpdatePlayerIdModel = new UpdatePlayerIdModel
                                //        {
                                //            AccountId = Helpers.Settings.AccountId,
                                //            PlayerId = Helpers.Settings.PlayerId,
                                //            UserId = Helpers.Settings.UserId,
                                //        };

                                //        // do something every 1 seconds
                                //        Device.BeginInvokeOnMainThread(async () =>
                                //        {
                                //            Controls.StartData.IsRunning = true;
                                //            string UserToken = await _service.UserToken();
                                //            string Re = await ORep.PostDataAsync("api/Notifications/PostPlayerId", oUpdatePlayerIdModel, UserToken);
                                //            if (Re != "api not responding")
                                //            {
                                //                Helpers.Settings.PlayerId = Re;
                                //                await ((App)Application.Current).RunThread(7);
                                //            }
                                //        });

                                //        return false;
                                //    }

                                //    return true;
                                //    // runs again, or false to stop
                                //});
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Alert", "Access denied to login on mobile for this username!", "Ok");
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Sorry, Your Account is not active", "Ok");
                        }
                    }
                }

                UserDialogs.Instance.HideLoading();
            }

            IsBusy = false;
        }


    }
}
