using FixPro.ViewModels;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Views.SchedulePages
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ChooseCustomerPage : Controls.CustomsPage
	{
        CustomersViewModel ViewModel { get => BindingContext as CustomersViewModel; set => BindingContext = value; }
        public ChooseCustomerPage()
		{
			InitializeComponent ();
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
            await Navigation.PopAsync();
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new Views.SchedulePages.NewSchedulePage());
        }

        private void srchPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            //lstCustomers.ItemsSource = ViewModel.LstCustomers.Where(x => (x.Phone1).Contains(srchPhone.Text));
            lstCustomers.ItemsSource = ViewModel.LstCustomers.Where(x => (x.Phone1).Contains(srchPhoneOrAddress.Text) || (x.Address.ToLower()).Contains(srchPhoneOrAddress.Text.ToLower()));
        }

        private void srchAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            //lstCustomers.ItemsSource = ViewModel.LstCustomers.Where(x => (x.Address.ToLower()).Contains(srchAddress.Text.ToLower()));
        }

    }
}