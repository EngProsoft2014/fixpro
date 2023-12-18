
using FixPro.Model;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfCalendar.XForms;
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
    public partial class DatePopup : Rg.Plugins.Popup.Pages.PopupPage
    {

        public delegate void RangeDelegte(CalendarModel calendar);
        public event RangeDelegte RangeClose;

        CalendarModel Cal;
        public DatePopup()
        {
            InitializeComponent();

            if (Controls.StaticMembers.SelectedDate != null)
            {
                calendar.SelectedDate = Controls.StaticMembers.SelectedDate;
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

        private async void btnOk_Clicked(object sender, EventArgs e)
        {
            Cal = new CalendarModel();

            if (calendar.SelectedRange != null)
            {
                Cal.SelectedRange = (SelectionRange)calendar.SelectedRange;

                Controls.StaticMembers.SelectedDate = Cal.SelectedRange.StartDate;

                Cal.StartDate = Cal.SelectedRange.StartDate;
                Cal.EndDate = Cal.SelectedRange.EndDate;

                RangeClose?.Invoke(Cal);

            }
            else if (calendar.SelectedDate != null)
            {
                Cal.StartDate = Cal.EndDate = calendar.SelectedDate.Value;

                Controls.StaticMembers.SelectedDate = calendar.SelectedDate.Value;

                RangeClose?.Invoke(Cal);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Please select a date", "Ok");
            }

            await PopupNavigation.Instance.PopAsync();

        }
    }
}