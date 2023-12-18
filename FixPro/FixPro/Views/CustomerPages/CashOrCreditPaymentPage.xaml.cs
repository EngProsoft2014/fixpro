using Microsoft.IdentityModel.Tokens;
using FixPro.ViewModels;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.IO;
using System.Threading;
using GoogleApi.Entities.Maps.StaticMaps.Request.Enums;
using SignaturePad.Forms;
using Plugin.Media.Abstractions;

namespace FixPro.Views.CustomerPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CashOrCreditPaymentPage : Controls.CustomsPage
    {

        PaymentsViewModel ViewModel { get => BindingContext as PaymentsViewModel; set => BindingContext = value; }

        public CashOrCreditPaymentPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            if (ViewModel != null && ViewModel.OneInvoice.Id == 0) 
            {
                await App.Current.MainPage.Navigation.PushAsync(new MainPage());
            }
            else
            {
                await Navigation.PopAsync();
            }
        }

        private void Entry_TextChanged(object sender, TextChangedEventArgs e)
        {
            if(string.IsNullOrEmpty(e.NewTextValue) != true)
            {
                ViewModel.OnePayment.Amount = decimal.Parse(e.NewTextValue);
                btnPayCredit.Text = string.Format("Pay USD ${0}", e.NewTextValue);
                btnPayCash.Text = string.Format("Pay USD ${0}", e.NewTextValue);
                lblPayCash.Text = e.NewTextValue;
            }
            else
            {
                ViewModel.OnePayment.Amount = ViewModel.OneInvoice.Net;
                btnPayCredit.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
                btnPayCash.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
                lblPayCash.Text = ViewModel.OneInvoice.Net.ToString();
            }  
        }

        private void swtPayCredit_Toggled(object sender, ToggledEventArgs e)
        {
            if(e.Value == true)
            {
                ViewModel.OnePayment.Amount = ViewModel.OneInvoice.Net;
                btnPayCredit.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
                btnPayCash.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
                lblPayCash.Text = ViewModel.OneInvoice.Net.ToString();
            }  
            else
            {
                if(string.IsNullOrEmpty(entryNewAmount.Text) != true)
                {
                    ViewModel.OnePayment.Amount = decimal.Parse(entryNewAmount.Text);
                    btnPayCredit.Text = string.Format("Pay USD ${0}", entryNewAmount.Text);  
                }
                else if(string.IsNullOrEmpty(entryCashNewAmount.Text) != true)
                {
                    ViewModel.OnePayment.Amount = decimal.Parse(entryCashNewAmount.Text);
                    btnPayCash.Text = string.Format("Pay USD ${0}", entryCashNewAmount.Text);
                    lblPayCash.Text = entryCashNewAmount.Text;
                }
                else
                {
                    ViewModel.OnePayment.Amount = ViewModel.OneInvoice.Net;
                    btnPayCredit.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
                    btnPayCash.Text = string.Format("Pay USD ${0}", ViewModel.OneInvoice.Net);
                    lblPayCash.Text = ViewModel.OneInvoice.Net.ToString();
                }
            }
        }

        private void actIndLoading_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (actIndLoading.IsRunning == true)
                this.IsEnabled = false;
            else
                this.IsEnabled = true;
        }


        //Save Credit
        private async void Button_Clicked(object sender, EventArgs e)
        {
           Stream stream = await signaturePadCredit.GetImageStreamAsync(SignatureImageFormat.Png);

           ViewModel.SignatureImageByte64 = Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream));

            Helpers.Messages.ShowSuccessSnackBar("Success for save your signature");
        }

        //Clear Credit
        private void Button_Clicked_1(object sender, EventArgs e)
        {
            signaturePadCredit.Clear();
        }

        //Save Cash
        private async void Button_Clicked_2(object sender, EventArgs e)
        {
            Stream stream = await signaturePadCash.GetImageStreamAsync(SignatureImageFormat.Png);

            ViewModel.SignatureImageByte64 = Convert.ToBase64String(Helpers.Utility.ReadToEnd(stream));

            Helpers.Messages.ShowSuccessSnackBar("Success for save your signature");
        }

        //Clear Cash
        private void Button_Clicked_3(object sender, EventArgs e)
        {
            signaturePadCash.Clear();
        }
    }
}