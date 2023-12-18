
using FixPro.Models;
using FixPro.ViewModels;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfCalendar.XForms;
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
	public partial class EmployeesPopup : Rg.Plugins.Popup.Pages.PopupPage
    {
        public delegate void EmployeesDelegte(List<EmployeeModel> Employees);
        public event EmployeesDelegte EmployeesClose;

        SchedulesViewModel ViewModel = new SchedulesViewModel();

        public EmployeesPopup()
		{
			InitializeComponent();
        }

        public EmployeesPopup(ObservableCollection<EmployeeModel> LstEmps)
        {
            InitializeComponent();
            lstEmployees.ItemsSource = ViewModel.LstEmpInOneCategory = LstEmps;
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
            List<EmployeeModel> LstEmps = new List<EmployeeModel>();
            LstEmps = ViewModel.LstEmpInOneCategory.Where(x => x.IsChecked == true).ToList();

            if (LstEmps != null)
            {
                EmployeesClose.Invoke(LstEmps);
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Warning", "Please Choose Empolyee !!", "OK");
            }
            await PopupNavigation.Instance.PopAsync();
        }
    }
}