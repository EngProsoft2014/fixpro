using FixPro.ViewModels;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Views.CustomerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomersPage : Controls.CustomsPage
    {
        CustomersViewModel ViewModel { get => BindingContext as CustomersViewModel; set => BindingContext = value; }

        public CustomersPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
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

        private void srchPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            lstCustomers.ItemsSource = ViewModel.LstCustomers.Where(x => (x.Phone1).Contains(srchPhoneOrAddress.Text) || (x.Address.ToLower()).Contains(srchPhoneOrAddress.Text.ToLower())
            || (x.FirstName.ToLower() + x.LastName.ToLower()).Contains(srchPhoneOrAddress.Text.ToLower()));
        }

        private void srchAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            //lstCustomers.ItemsSource = ViewModel.LstCustomers.Where(x => (x.Address.ToLower()).Contains(srchAddress.Text.ToLower()));
        }

    }
}