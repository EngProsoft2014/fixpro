using Acr.UserDialogs.Infrastructure;
using Acr.UserDialogs;
using Newtonsoft.Json;
using Plugin.Media.Abstractions;
using Plugin.Media;
using FixPro.Models;
using FixPro.Views.MenuPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Rg.Plugins.Popup.Services;
using FixPro.ViewModels;
using Xamarin.Forms.Maps;
using GoogleApi.Entities.Translate.Common.Enums;

namespace FixPro.Views.SchedulePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePicturesPage : Controls.CustomsPage
    {
        SchedulesViewModel ViewModel { get => BindingContext as SchedulesViewModel; set => BindingContext = value; }
        public SchedulePicturesPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                ViewModel.IsBusy = true;
                UserDialogs.Instance.ShowLoading();
                var popupView = new SchedulesViewModel(ViewModel.ScheduleDetails.Id, ViewModel.ScheduleDetails.OneScheduleDate.Id);
                var page = new Views.SchedulePages.NewSchedulePage();
                page.BindingContext = popupView;
                await App.Current.MainPage.Navigation.PushAsync(page);
                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
                UserDialogs.Instance.HideLoading();
                ViewModel.IsBusy = false;
            });
            return true;
        }


    }
}