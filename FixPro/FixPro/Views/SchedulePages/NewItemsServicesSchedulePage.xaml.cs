using FixPro.Models;
using FixPro.ViewModels;
using Rg.Plugins.Popup.Services;
using Syncfusion.SfCalendar.XForms;
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
	public partial class NewItemsServicesSchedulePage : Controls.CustomsPage
    {
        ScheduleItemsServicesViewModel ViewModel { get => BindingContext as ScheduleItemsServicesViewModel; set => BindingContext = value; }

    
        public NewItemsServicesSchedulePage()
		{
			InitializeComponent ();
		}

        public NewItemsServicesSchedulePage(ScheduleItemsServicesModel model)
        {
            InitializeComponent();

            //cobxLstItems.Text = model.ItemsServicesName;

            entryName.Text = model.ItemsServicesName;
            entryCost.Text = model.CostRate.ToString();
            entryQty.Text = model.Quantity.ToString();
            edtDescription.Text = model.ItemServiceDescription;

            //cobxLstItems.IsEnabled = false;
            entryName.IsReadOnly = true;
            entryCost.IsReadOnly = true;
            entryQty.IsReadOnly = true;
            edtDescription.IsReadOnly = true;
            stkBtns.IsVisible = false;
            YumAddItemService.IsVisible = false;
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

        //private void SfComboBox_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        //{
        //    var selectedOption = (sender as SfComboBox).SelectedItem;
        //    ViewModel.SelectedItemForGetCost.Execute(selectedOption);
        //}

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            var selectedOption = (sender as Entry).Text;
            ViewModel?.OpenFilterMaterial.Execute(selectedOption);
        }
    }
}