using FixPro.Models;
using FixPro.ViewModels;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Views.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleDatesPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public delegate void DatesDelegte(List<SchaduleDateModel> Dates);
        public event DatesDelegte DatesClose;

        SchedulesViewModel ViewModel = new SchedulesViewModel();

        public ScheduleDatesPopup()
        {
            InitializeComponent();
        }
        public ScheduleDatesPopup(ObservableCollection<SchaduleDateModel> LstDates)
        {
            InitializeComponent();
            lstDates.ItemsSource = ViewModel.LstEstimateSchaduleDates = ViewModel.LstInvoiceSchaduleDates = LstDates;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
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
            List<SchaduleDateModel> LstDates = new List<SchaduleDateModel>();
            LstDates = ViewModel.LstEstimateSchaduleDates.Where(x => x.IsChecked == true).ToList();

            if (LstDates != null)
            {
                DatesClose.Invoke(LstDates);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Warning", "Please Choose Empolyee !!", "OK");
            }
            await PopupNavigation.Instance.PopAsync();
        }
    }
}