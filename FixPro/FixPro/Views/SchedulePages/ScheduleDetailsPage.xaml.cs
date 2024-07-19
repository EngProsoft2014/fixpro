﻿using Acr.UserDialogs;
using FixPro.Models;
using FixPro.ViewModels;
using FixPro.Views.PopupPages;
using GoogleApi.Entities.Translate.Common.Enums;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.ComboBox;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace FixPro.Views.SchedulePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleDetailsPage : Controls.CustomsPage
    {
        SchedulesViewModel ViewModel { get => BindingContext as SchedulesViewModel; set => BindingContext = value; }

        public ScheduleDetailsPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (ViewModel?.CustomerDetails != null)
            {
                if (!string.IsNullOrEmpty(ViewModel.CustomerDetails.locationlatitude) && !string.IsNullOrEmpty(ViewModel.CustomerDetails.locationlongitude))
                {
                    ObservableCollection<CustomersModel> LstCust = new ObservableCollection<CustomersModel>();
                    LstCust.Add(ViewModel.CustomerDetails);

                    map.ItemsSource = LstCust;
                    map.Pins.FirstOrDefault().Label = ViewModel.CustomerDetails.Address;

                    map.MoveToRegion(MapSpan.FromCenterAndRadius(
                    new Position(double.Parse(ViewModel.CustomerDetails.locationlatitude), double.Parse(ViewModel.CustomerDetails.locationlongitude)), Distance.FromMiles(2)));
                }
            }

        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            if (stkScheduleInfo.IsVisible == false)
            {
                stkScheduleInfo.IsVisible = true;
            }
            else
            {
                stkScheduleInfo.IsVisible = false;
            }
        }

        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            if (stkTeamAssign.IsVisible == false)
            {
                stkTeamAssign.IsVisible = true;
            }
            else
            {
                stkTeamAssign.IsVisible = false;
            }
        }

        private void TapGestureRecognizer_Tapped_7(object sender, EventArgs e)
        {
            if (stkPriorityFirstCreateServices.IsVisible == false)
            {
                stkPriorityFirstCreateServices.IsVisible = true;
            }
            else
            {
                stkPriorityFirstCreateServices.IsVisible = false;
            }
        }

        private async void TapGestureRecognizer_Tapped1(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void actIndLoading_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (actIndLoading.IsRunning == true)
                this.IsEnabled = false;
            else
                this.IsEnabled = true;
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }


        //private void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        //{
        //    var selectedOption = (sender as SfComboBox).SelectedItem;
        //    if (selectedOption != null)
        //    {
        //        ViewModel.SelectedEmpCategory.Execute(selectedOption);
        //    }

        //}


        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            //ViewModel.ScheduleDetails.GetPictures = true; //In Get Pictures Case Only
            var popupView = new SchedulesViewModel(ViewModel.ScheduleDetails);
            var page = new Views.SchedulePages.SchedulePicturesPage();
            page.BindingContext = popupView;
            await App.Current.MainPage.Navigation.PushAsync(page);
            //await App.Current.MainPage.Navigation.PushAsync(new SchedulePicturesPage());
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (ViewModel?.ScheduleDetails?.Id != 0)
            {
                ViewModel.IsBusy = true;

                try
                {
                    if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                    {
                        await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                        return;
                    }
                    else
                    {
                        var page = new MapTypePopup();
                        page.MapTypeDelegteClose += async (map) =>
                        {
                            UserDialogs.Instance.ShowLoading();

                            var location = new Location(double.Parse(ViewModel?.CustomerDetails?.locationlatitude), double.Parse(ViewModel?.CustomerDetails?.locationlongitude));

                            var options = new MapLaunchOptions { NavigationMode = NavigationMode.Driving };
                            //await Xamarin.Essentials.Map.OpenAsync(location, options);

                            if (map == "Google")
                            {
                                await Launcher.OpenAsync($"https://www.google.com/maps/search/?api=1&query={Uri.EscapeDataString(ViewModel?.CustomerDetails?.Address)}");
                            }
                            else
                            {
                                await Launcher.OpenAsync($"http://maps.apple.com/?q={Uri.EscapeDataString(ViewModel?.CustomerDetails?.Address)}");
                            }
                            UserDialogs.Instance.HideLoading();
                        };

                        await PopupNavigation.Instance.PushAsync(page);
                    }
                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
                }

                ViewModel.IsBusy = false;

            }
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedOption = (sender as Picker).SelectedItem;
            if (selectedOption != null)
            {
                ViewModel.SelectedEmpCategory.Execute(selectedOption);
            }
        }

        private void TapGestureRecognizer_Tapped_4(object sender, EventArgs e)
        {
            frmMaterial.IsVisible = true;
            frmMaterialReceipt.IsVisible = false;
            frmServies.IsVisible = false;
        }

        private void TapGestureRecognizer_Tapped_5(object sender, EventArgs e)
        {
            frmMaterial.IsVisible = false;
            frmMaterialReceipt.IsVisible = true;
            frmServies.IsVisible = false;
        }

        private void TapGestureRecognizer_Tapped_6(object sender, EventArgs e)
        {
            frmMaterial.IsVisible = false;
            frmMaterialReceipt.IsVisible = false;
            frmServies.IsVisible = true;
        }


    }
}