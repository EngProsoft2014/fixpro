using Acr.UserDialogs;
using FixPro.ViewModels;
using Microsoft.IdentityModel.Tokens;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Views.CallPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewCallPage : Controls.CustomsPage
    {
        CallsViewModel ViewModel { get => BindingContext as CallsViewModel; set => BindingContext = value; }

        private bool _isHandlingFocus;

        public NewCallPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();


            if (string.IsNullOrEmpty(entryNameGet.Text))
            {
                entryNameGet.Placeholder = "Enter customer";
            }
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private void actIndLoading_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (actIndLoading.IsRunning == true)
                this.IsEnabled = false;
            else
                this.IsEnabled = true;
        }

        private async void entryPhone_Focused(object sender, FocusEventArgs e)
        {
            if (_isHandlingFocus) return;
            _isHandlingFocus = true;

            if (e.IsFocused == true)
            {
                actIndLoading.IsRunning = true;

                entryPhone.IsReadOnly = true;

                try
                {
                    if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                        return;
                    }
                    else
                    {
                        var popupView = new Views.CallPages.SearchCustomerPopup();

                        popupView.DidClose += (str) =>
                        {
                            entryPhone.Text = ViewModel.OneCall.PhoneNum = str.Phone1;
                            ViewModel.OneCall.CustomerId = str.Id;
                            ViewModel.OneCall.CustomerName = str.FirstName + " " + str.LastName;

                            if (!string.IsNullOrEmpty(str.FirstName) && !string.IsNullOrEmpty(str.LastName))
                            {
                                stkCustNameGet.IsVisible = true;
                                entryNameGet.Text = str.FirstName + " " + str.LastName;
                                entryNameGet.IsReadOnly = true;
                            }
                            else
                            {
                                ViewModel.OneCall.CustomerName = entryNameGet.Text = "";
                                entryNameGet.Placeholder = "Enter customer";
                                entryNameGet.IsReadOnly = false;
                            }
                        };

                        await PopupNavigation.Instance.PushAsync(popupView);
                    }
                }
                catch (Exception)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    throw;
                }

                entryPhone.IsReadOnly = false;
                actIndLoading.IsRunning = false;
            }

            _isHandlingFocus = false;
        }

        private void entryPhone_Unfocused(object sender, FocusEventArgs e)
        {
            actIndLoading.IsRunning = true;
            entryPhone.IsReadOnly = false;
            actIndLoading.IsRunning = false;
        }



        //private async void entryNameGet_Focused(object sender, FocusEventArgs e)
        //{
        //    if (_isHandlingFocus) return;
        //    _isHandlingFocus = true;

        //    if (e.IsFocused == true)
        //    {
        //        actIndLoading.IsRunning = true;
        //        entryNameGet.IsReadOnly = true;

        //        try
        //        {
        //            if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
        //            {
        //                await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
        //                return;
        //            }
        //            else
        //            {

        //                if (ViewModel.OneCall.CustomerId == 0)
        //                {
        //                    var popupView = new Views.CallPages.SearchCustomerPopup();

        //                    popupView.DidClose += (str) =>
        //                    {
        //                        //ViewModel.OneCall.PhoneNum = str.Phone1;
        //                        ViewModel.OneCall.CustomerId = str.Id;
        //                        ViewModel.OneCall.CustomerName = str.FirstName + " " + str.LastName;

        //                        if (!string.IsNullOrEmpty(str.FirstName) && !string.IsNullOrEmpty(str.LastName))
        //                        {
        //                            stkCustNameGet.IsVisible = true;
        //                            entryNameGet.Text = str.FirstName + " " + str.LastName;
        //                        }
        //                        else
        //                        {
        //                            entryNameGet.Placeholder = "Please Choose Customer";
        //                        }
        //                    };

        //                    await PopupNavigation.Instance.PushAsync(popupView);
        //                } 
        //            }
        //        }

        //        catch (Exception)
        //        {
        //            await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
        //            throw;
        //        }

        //        entryNameGet.IsReadOnly = false;
        //        actIndLoading.IsRunning = false;
        //    }

        //    _isHandlingFocus = false;
        //}

        //private void entryNameGet_Unfocused(object sender, FocusEventArgs e)
        //{
        //    actIndLoading.IsRunning = true;
        //    entryNameGet.IsReadOnly = false;
        //    actIndLoading.IsRunning = false;
        //}
    }
}





//using Acr.UserDialogs;
//using FixPro.ViewModels;
//using Microsoft.IdentityModel.Tokens;
//using Rg.Plugins.Popup.Services;
//using Syncfusion.SfCalendar.XForms;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Xamarin.Essentials;
//using Xamarin.Forms;
//using Xamarin.Forms.Xaml;

//namespace FixPro.Views.CallPages
//{
//    [XamlCompilation(XamlCompilationOptions.Compile)]
//    public partial class NewCallPage : Controls.CustomsPage
//    {
//        CallsViewModel ViewModel { get => BindingContext as CallsViewModel; set => BindingContext = value; }

//        public NewCallPage()
//        {
//            InitializeComponent();
//        }

//        protected override void OnAppearing()
//        {
//            base.OnAppearing();


//            if (string.IsNullOrEmpty(entryNameGet.Text))
//            {
//                entryNameGet.Placeholder = "Customer Name";
//            }
//        }

//        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
//        {
//            await App.Current.MainPage.Navigation.PopAsync();
//        }

//        private void actIndLoading_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
//        {
//            if (actIndLoading.IsRunning == true)
//                this.IsEnabled = false;
//            else
//                this.IsEnabled = true;
//        }

//        private async void entryPhone_Focused(object sender, FocusEventArgs e)
//        {

//            if (e.IsFocused == true)
//            {
//                actIndLoading.IsRunning = true;
//                entryPhone.IsReadOnly = true;

//                //var entry = (Entry)sender;

//                //entry.IsEnabled = false;

//                try
//                {
//                    if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
//                    {
//                        await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
//                        return;
//                    }
//                    else
//                    {

//                        var popupView = new Views.CallPages.SearchCustomerPopup();

//                        popupView.DidClose += (str) =>
//                        {
//                            entryPhone.Text = ViewModel.OneCall.PhoneNum = str.Phone1;
//                            ViewModel.OneCall.CustomerId = str.Id;
//                            //ViewModel.OneCall.CustomerName = str.FirstName + " " + str.LastName;

//                            if (!string.IsNullOrEmpty(str.FirstName) && !string.IsNullOrEmpty(str.LastName))
//                            {
//                                stkCustNameGet.IsVisible = true;
//                                ViewModel.OneCall.CustomerName = entryNameGet.Text = str.FirstName + " " + str.LastName;
//                                entryNameGet.IsReadOnly = true;
//                            }
//                            else
//                            {
//                                ViewModel.OneCall.CustomerName = entryNameGet.Text = "";
//                                entryNameGet.Placeholder = "Customer Name";
//                                entryNameGet.IsReadOnly = false;
//                            }
//                        };

//                        await PopupNavigation.Instance.PushAsync(popupView);
//                    }
//                }
//                catch (Exception)
//                {
//                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
//                    throw;
//                }

//                actIndLoading.IsRunning = false;
//                entryPhone.IsReadOnly = false;

//            }


//        }

//        private void entryPhone_Unfocused(object sender, FocusEventArgs e)
//        {
//            actIndLoading.IsRunning = true;
//            //var entry = (Entry)sender;
//            //entry.IsEnabled = true;
//            entryPhone.IsReadOnly = false;
//            actIndLoading.IsRunning = false;
//        }



//        private async void entryNameGet_Focused(object sender, FocusEventArgs e)
//        {
//            //if (e.IsFocused == true)
//            //{
//            //    actIndLoading.IsRunning = true;
//            //    var entry = (Entry)sender;
//            //    entry.IsEnabled = false;

//            //    try
//            //    {
//            //        if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
//            //        {
//            //            await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
//            //            return;
//            //        }
//            //        else
//            //        {

//            //            if (ViewModel.OneCall.CustomerId == null)
//            //            {
//            //                var popupView = new Views.CallPages.SearchCustomerPopup();

//            //                popupView.DidClose += (str) =>
//            //                {
//            //                    //ViewModel.OneCall.PhoneNum = str.Phone1;
//            //                    ViewModel.OneCall.CustomerId = str.Id;
//            //                    ViewModel.OneCall.CustomerName = str.FirstName + " " + str.LastName;

//            //                    if (!string.IsNullOrEmpty(str.FirstName) && !string.IsNullOrEmpty(str.LastName))
//            //                    {
//            //                        stkCustNameGet.IsVisible = true;
//            //                        entryNameGet.Text = str.FirstName + " " + str.LastName;
//            //                    }
//            //                    else
//            //                    {
//            //                        entryNameGet.Placeholder = "Customer Name";
//            //                    }
//            //                };

//            //                await PopupNavigation.Instance.PushAsync(popupView);
//            //            }

//            //            actIndLoading.IsRunning = false;
//            //        }
//            //    }

//            //    catch (Exception)
//            //    {
//            //        await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
//            //        throw;
//            //    }
//            //}

//        }

//        private void entryNameGet_Unfocused(object sender, FocusEventArgs e)
//        {
//            //actIndLoading.IsRunning = true;
//            ////var entry = (Entry)sender;
//            ////entry.IsEnabled = true;
//            //entryNameGet.IsEnabled = true;
//            //actIndLoading.IsRunning = false;
//        }
//    }
//}