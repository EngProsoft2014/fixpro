using Acr.UserDialogs;
using FixPro.Models;
using FixPro.ViewModels;
using GoogleApi.Entities.Search.Common;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfCalendar.XForms;
using Syncfusion.SfSchedule.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            await Navigation.PushAsync(new MainPage());
        }

        protected override bool OnBackButtonPressed()
        {
            Device.BeginInvokeOnMainThread(async () =>
            {
                await Navigation.PushAsync(new MainPage());
            });
            return true;
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
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
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
                        await App.Current.MainPage.DisplayAlert("Warning", "You don’t have an access to create a schedule!", "OK");
                    }
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
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
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    //return;
                }
                else
                {
                    if (e.Appointment != null)
                    {
                        SchedulesModel ScheduleId = e.Appointment as SchedulesModel;
                        var VM = new SchedulesViewModel(ScheduleId.Id, ScheduleId.OneScheduleDate.Id);
                        //var page = new NewSchedulePage();
                        var page = new ScheduleDetailsPage();
                        page.BindingContext = VM;
                        await App.Current.MainPage.Navigation.PushAsync(page);
                    }
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                //throw;
            }

            UserDialogs.Instance.HideLoading();
        }

        private void calendar_SelectionChanged(object sender, Syncfusion.SfCalendar.XForms.SelectionChangedEventArgs e)
        {
            string day = e.Calendar.SelectedDate.Value.ToString("yyyy-MM-dd");
            var Fird = ViewModel.LstSchedules.Where(x => x.StartDate == day).ToList();
            var Scon = Fird.OrderBy(o => o.From);
            colJobs.ItemsSource = new ObservableCollection<SchedulesModel>(Scon);

            //if(schedulesModels.Count == 0)
            //{
            //    stkNoData.IsVisible = true;
            //    colJobs.IsVisible = false;
            //}
            //else
            //{
            //    stkNoData.IsVisible = false;
            //    colJobs.IsVisible = true;
            //}

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

        //Search Btn
        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            calendar.IsVisible = false;
            stkSwtScheduleView.IsVisible = false;
            schedule.IsVisible = false;
            colJobs.IsVisible = false;
            stkListOrCalAndWeekOrDays.IsVisible = false;
            stkSearch.IsVisible = true;
            lblSearch.IsVisible = false;
            lblCalender.IsVisible = true;
            stkSearchItems.IsVisible = true;
            srchJobs.Text = "";
            colSearchJobs.IsVisible = false;
        }

        //Calendar Btn
        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            if (swchCalenderOrListView.IsToggled == true)
            {
                //Schedule
                schedule.IsVisible = true;
                calendar.IsVisible = false;
                colJobs.IsVisible = false;
                stkSwtScheduleView.IsVisible = true;
                colSearchJobs.IsVisible = false;
            }
            else
            {
                //Calendar
                calendar.IsVisible = true;
                schedule.IsVisible = false;
                colJobs.IsVisible = true;
                stkSwtScheduleView.IsVisible = false;
                colSearchJobs.IsVisible = false;
            }

            stkListOrCalAndWeekOrDays.IsVisible = true;
            stkSearch.IsVisible = false;
            lblSearch.IsVisible = true;
            lblCalender.IsVisible = false;
        }

        private void srchJobs_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(e.NewTextValue))
            {
                stkSearchItems.IsVisible = false;
                colSearchJobs.IsVisible = true;
            }
            else
            {
                stkSearchItems.IsVisible = true;
                colSearchJobs.IsVisible = false;
            }
        }
    }
}