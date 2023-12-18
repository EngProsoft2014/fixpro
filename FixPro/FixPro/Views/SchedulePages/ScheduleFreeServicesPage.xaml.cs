using FixPro.Models;
using FixPro.ViewModels;
using GoogleApi.Entities.Translate.Common.Enums;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Syncfusion.XForms.ComboBox;
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
	public partial class ScheduleFreeServicesPage : Controls.CustomsPage 
	{
        ScheduleFreeServicesViewModel ViewModel { get => BindingContext as ScheduleFreeServicesViewModel; set => BindingContext = value; }

        public ScheduleFreeServicesPage()
        {
            InitializeComponent();
        }
        public ScheduleFreeServicesPage(ScheduleItemsServicesModel model)
        {
			InitializeComponent ();

            cobxLstServices.Text = model.ItemsServicesName;
            entryCost.Text = model.CostRate.ToString();
            entryQty.Text = model.Quantity.ToString();
            edtDescription.Text = model.ItemServiceDescription;

            cobxLstServices.IsEnabled = false;
            entryCost.IsReadOnly = true;
            entryQty.IsReadOnly = true;
            edtDescription.IsReadOnly = true;
            stkBtns.IsVisible = false;
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
            await App.Current.MainPage.Navigation.PopAsync();
        }


        private void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var selectedOption = (sender as SfComboBox).SelectedItem;
            ViewModel.SelectedServiceForGetCost.Execute(selectedOption);
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }
    }
}