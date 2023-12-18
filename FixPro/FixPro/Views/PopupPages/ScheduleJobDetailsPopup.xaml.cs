using FixPro.ViewModels;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.ComboBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Views.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScheduleJobDetailsPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        SchedulesViewModel ViewModel { get => BindingContext as SchedulesViewModel; set => BindingContext = value; }

        public ScheduleJobDetailsPopup()
        {
            InitializeComponent();
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
            await PopupNavigation.Instance.PopAsync();
        }

        //private void TimePicker_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (tmEnd.Time != new TimeSpan(00, 0, 0, 00))
        //    {
        //        ViewModel.SpentHours = (ViewModel.EndTime - ViewModel.StartTime).Hours.ToString();
        //        ViewModel.SpentMin = (ViewModel.EndTime - ViewModel.StartTime).Minutes.ToString();
        //    }
        //}

        //private void tmStart_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        //{
        //    if (tmEnd.Time != new TimeSpan(00, 0, 0, 00))
        //    {
        //        ViewModel.SpentHours = (ViewModel.EndTime - ViewModel.StartTime).Hours.ToString();
        //        ViewModel.SpentMin = (ViewModel.EndTime - ViewModel.StartTime).Minutes.ToString();
        //    }
        //}

        private void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var selectedOption = (sender as SfComboBox).SelectedItem;
            ViewModel.SelectedEmpCategory.Execute(selectedOption);
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            stkStEdTime.IsVisible = false;
            stkSpHourMin.IsVisible = false;
            stkButtons.IsVisible = false;
            stkResponNotServ.IsVisible = true;
            stkResponButtons.IsVisible = true;
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            stkStEdTime.IsVisible = true;
            stkSpHourMin.IsVisible = true;
            stkButtons.IsVisible = true;
            stkResponNotServ.IsVisible = false;
            stkResponButtons.IsVisible = false;
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            stkStEdTime.IsVisible = true;
            stkSpHourMin.IsVisible = true;
            stkButtons.IsVisible = true;
            stkDate.IsVisible = true;
            stkAddJobDate.IsVisible = false;
            stkAddJobTimes.IsVisible = false;
            stkAddJobEmployee.IsVisible = false;
            stkAddJobDateButtons.IsVisible = false;
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            stkStEdTime.IsVisible = false;
            stkSpHourMin.IsVisible = false;
            stkButtons.IsVisible = false;
            stkDate.IsVisible = false;
            stkAddJobDate.IsVisible = true;
            stkAddJobTimes.IsVisible = true;
            stkAddJobEmployee.IsVisible = true;
            stkAddJobDateButtons.IsVisible = true;
        }
    }
}