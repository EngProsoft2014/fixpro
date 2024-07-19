using FixPro.ViewModels;
using SignaturePad.Forms;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateEstimateWithoutSchedulePage : Controls.CustomsPage
    {
        SchedulesViewModel ViewModel { get => BindingContext as SchedulesViewModel; set => BindingContext = value; }

        public CreateEstimateWithoutSchedulePage()
        {
            InitializeComponent();
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

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            stkEditDiscount.IsVisible = true;
        }

        private void TapGestureRecognizer_Tapped_2(object sender, EventArgs e)
        {
            stkEditDiscount.IsVisible = false;
        }

        private void entryDiscount_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (e.NewTextValue != null && e.NewTextValue != "")
            {
                pnkSave.IsVisible = true;
            }
            else
            {
                pnkSave.IsVisible = false;
            }
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            ViewModel.TotalInvoice(ViewModel.ScheduleDetails, ViewModel.CustomerDetails);
        }

        private void ShowSignatureBtn_Clicked(object sender, EventArgs e)
        {
            ShowSignatureBtn.IsVisible = false;
            CloseSignatureBtn.IsVisible = true;
            frmSignature.IsVisible = true;
        }

        private void CloseSignatureBtn_Clicked(object sender, EventArgs e)
        {
            CloseSignatureBtn.IsVisible = false;
            ShowSignatureBtn.IsVisible = true;
            frmSignature.IsVisible = false;
        }

        //Save Credit
        private async void Button_Clicked_1(object sender, EventArgs e)
        {
            Stream stream = await signaturePadEstimate.GetImageStreamAsync(SignatureImageFormat.Png);

            ViewModel.SignatureImageByte64Estimate = Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream));

            Helpers.Messages.ShowSuccessSnackBar("Success for save your signature");
        }


        //Clear Credit
        private void Button_Clicked_2(object sender, EventArgs e)
        {
            signaturePadEstimate.Clear();
        }
    }
}