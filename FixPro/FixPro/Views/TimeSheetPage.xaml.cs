using OneSignalSDK.Xamarin;
using FixPro.Controls;
using FixPro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.Converters;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TimeSheetPage : Controls.CustomsPage
    {
        TimeSheetViewModel ViewModel { get => BindingContext as TimeSheetViewModel; set => BindingContext = value; }

        public TimeSheetPage()
        {
            InitializeComponent();
            string mxxx = "";

            if (lstEmployeesIn.ItemsSource.Equals(0))
            {
                stkNoData.IsVisible = true;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            Connectivity.ConnectivityChanged -= Connectivity_ConnectivityChanged;
        }

        private void actIndLoading_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (actIndLoading.IsRunning == true)
                this.IsEnabled = false;
            else
                this.IsEnabled = true;
        }

        //Employees In
        private void TapGestureRecognizer_Tapped_3(object sender, EventArgs e)
        {
            lstEmployeesOut.IsVisible = false;
            lstEmployeesIn.IsVisible = true;
            yumCheckIn.BackgroundColor = Color.FromHex("#b66dff");
            lblCheckIn.TextColor = Color.White;

            yumCheckOut.BackgroundColor = Color.FromHex("#eedeff");
            lblCheckOut.TextColor = Color.Black;

            if (ViewModel.LstEmployeesIn.Count == 0)
            {
                stkNoData.IsVisible = true;
            }
            else
            {
                stkNoData.IsVisible = false;
            }
        }

        //Employees Out
        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            lstEmployeesIn.IsVisible = false;
            lstEmployeesOut.IsVisible = true;

            yumCheckIn.BackgroundColor = Color.FromHex("#eedeff");
            lblCheckIn.TextColor = Color.Black;

            yumCheckOut.BackgroundColor = Color.FromHex("#b66dff");
            lblCheckOut.TextColor = Color.White;

            if (ViewModel.LstEmployeesOut.Count == 0)
            {
                stkNoData.IsVisible = true;
            }
            else
            {
                stkNoData.IsVisible = false;
            }
        }


        private void srcBarEmployee_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstEmployeesIn.ItemsSource = ViewModel.LstEmployeesIn.Where(x => (x.EmployeeName).Contains(srcBarEmployee.Text));
            lstEmployeesOut.ItemsSource = ViewModel.LstEmployeesOut.Where(x => (x.EmployeeName).Contains(srcBarEmployee.Text));
        }


        async void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                // Connection to internet is Not available
                stkNoInternet.IsVisible = true;
            }
            else
            {
                // Connection to internet is available
                stkNoInternet.IsVisible = false;
                await App.Current.MainPage.Navigation.PushAsync(new Views.TimeSheetPage());
                App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
            }
        }

        private async void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}