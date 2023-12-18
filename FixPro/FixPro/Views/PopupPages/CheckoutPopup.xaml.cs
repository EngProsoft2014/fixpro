using GoogleApi.Entities.Translate.Common.Enums;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Views.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CheckoutPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public delegate void TimeDelegte(TimeSpan time);
        public event TimeDelegte TimeDidClose;
        public CheckoutPopup()
        {
            InitializeComponent();
        }

        public CheckoutPopup(int id, string time)
        {
            InitializeComponent();
            btnCheck.Text = "Check In";

            if (time != null && time != "")
            {
                timeCheckOut.Time = TimeSpan.Parse(time);
            }
        }

        public CheckoutPopup(string time)
        {
            InitializeComponent();

            btnCheck.Text = "Check Out";

            if (time != null && time != "")
            {
                timeCheckOut.Time = TimeSpan.Parse(time);
            }
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

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            if (timeCheckOut.Time != null)
            {
                TimeDidClose?.Invoke(timeCheckOut.Time);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please select a time", "Ok");
            }
            await PopupNavigation.Instance.PopAsync();
            await App.Current.MainPage.Navigation.PushAsync(new TimeSheetPage());
            App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
        }
    }
}