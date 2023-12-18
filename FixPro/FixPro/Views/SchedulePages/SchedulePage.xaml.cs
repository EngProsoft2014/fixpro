using Acr.UserDialogs;
using FixPro.Models;
using FixPro.ViewModels;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfCalendar.XForms;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Views.SchedulePages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SchedulePage : Controls.CustomsPage
    {
        SchedulesViewModel ViewModel { get => BindingContext as SchedulesViewModel; set => BindingContext = value; }

        public SchedulePage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
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

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                    //return;
                }
                else
                {
                    if (Controls.StartData.EmployeeDataStatic.ActiveCreateSchedule == true)
                    {
                        Controls.StaticMembers.WayAfterChooseCust = 0; //Create New Schedule
                        await App.Current.MainPage.Navigation.PushAsync(new Views.SchedulePages.ChooseCustomerPage());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Warning", "Sorry, You don't have access to create schedule", "OK");
                    }
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                //throw;
            }
        }

        private void swchCalenderView_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == false)
            {
                schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.WeekView;
            }
            if (e.Value == true)
            {
                schedule.ScheduleView = Syncfusion.SfSchedule.XForms.ScheduleView.DayView;
            }
        }


        private async void schedule_CellTapped(object sender, CellTappedEventArgs e)
        {
            UserDialogs.Instance.ShowLoading();
            try
            {
                if (Connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                    //return;
                }
                else
                {
                    if (e.Appointment != null)
                    {
                        SchedulesModel ScheduleId = e.Appointment as SchedulesModel;
                        var VM = new SchedulesViewModel(ScheduleId.Id, ScheduleId.OneScheduleDate.Id);
                        var page = new NewSchedulePage();
                        page.BindingContext = VM;
                        await App.Current.MainPage.Navigation.PushAsync(page);
                    }
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                //throw;
            }

            UserDialogs.Instance.HideLoading();
        }

        private void calendar_SelectionChanged(object sender, Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs e)
        {
            string day = e.Calendar.SelectedDate.Value.ToString("yyyy-MM-dd");

            colJobs.ItemsSource = new System.Collections.ObjectModel.ObservableCollection<SchedulesModel>(ViewModel.LstSchedules.Where(x => x.StartDate == day).ToList());
        }

        private void swchCalenderOrListView_Toggled(object sender, ToggledEventArgs e)
        {
            if (e.Value == false)
            {
                calendar.IsVisible = true;
                stkSwtScheduleView.IsVisible = false;
                schedule.IsVisible = false;
                colJobs.IsVisible = true;
            }
            if (e.Value == true)
            {
                schedule.IsVisible = true;
                calendar.IsVisible = false;
                stkSwtScheduleView.IsVisible = true;
                colJobs.IsVisible = false;
            }
        }
    }
}