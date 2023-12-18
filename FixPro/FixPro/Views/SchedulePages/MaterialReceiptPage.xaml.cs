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
        ScheduleItemsServicesViewModel ViewModel { get => BindingContext as ScheduleItemsServicesViewModel; set => BindingContext = value; }

        public MaterialReceiptPage()
		{
			InitializeComponent ();
		}

        public MaterialReceiptPage(ScheduleMaterialReceiptModel model)
        {
            InitializeComponent();

            comxLstSuppliers.Text = model.SupplierName;
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


    }
}