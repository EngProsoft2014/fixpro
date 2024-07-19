using FixPro.Models;
using FixPro.ViewModels;
using FixPro.Views.PopupPages;
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
	public partial class MaterialReceiptPage : Controls.CustomsPage
    {
        ScheduleMaterialReceiptViewModel ViewModel { get => BindingContext as ScheduleMaterialReceiptViewModel; set => BindingContext = value; }

        public MaterialReceiptPage()
		{
			InitializeComponent ();
		}

        public MaterialReceiptPage(ScheduleMaterialReceiptModel model)
        {
            InitializeComponent();

            ViewModel.OneSupplier.Id = model.SupplierId.Value;
            ViewModel.OneSupplier.FirstName = model.SupplierName;

            comxLstSuppliers.Title = model.SupplierName;
            comxLstSuppliers.SelectedItem = ViewModel.OneSupplier;
            entryCost.Text = model.Cost.ToString();
            edtNotes.Text = model.Notes;
            imgReceipt.Source = model.ReceiptPhotoView;

            comxLstSuppliers.IsEnabled = false;
            entryCost.IsReadOnly = true;
            edtNotes.IsReadOnly = true;
            stkImgBtns.IsEnabled = false;
            stkBtns.IsVisible = false;
        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
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


        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PushAsync(new AddSchedulePhotoPupop());
        }

        private void comxLstSuppliers_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedOption = (sender as Picker).SelectedItem;
            if(selectedOption != null)
                ViewModel?.SelectSupplier.Execute(selectedOption);
        }

        private void comxLstSuppliers_SelectionChanged(object sender, Syncfusion.XForms.ComboBox.SelectionChangedEventArgs e)
        {
            var selectedOption = (sender as SfComboBox).SelectedItem;
            ViewModel?.SelectSupplier.Execute(selectedOption);
        }
    }
}