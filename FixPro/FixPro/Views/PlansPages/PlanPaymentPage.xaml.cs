using FixPro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Views.PlansPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlanPaymentPage : Controls.CustomsPage
    {
        PlansViewModel ViewModel { get => BindingContext as PlansViewModel; set => BindingContext = value; }

        public PlanPaymentPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();    
        }

        private void rdYearly_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            pnkcardMethod.BackgroundColor = Color.White;
            pnkcashMethod.BackgroundColor = Color.LightBlue;
        }

        private void rdMonthly_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            pnkcashMethod.BackgroundColor = Color.White;
            pnkcardMethod.BackgroundColor = Color.LightBlue;
        }
    }
}