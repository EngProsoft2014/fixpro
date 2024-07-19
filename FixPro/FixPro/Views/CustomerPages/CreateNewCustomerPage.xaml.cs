using FixPro.ViewModels;
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
    public partial class CreateNewCustomerPage : Controls.CustomsPage
    {
        CustomersViewModel ViewModel { get => BindingContext as CustomersViewModel; set => BindingContext = value; }
        public CreateNewCustomerPage()
        {
            InitializeComponent();
        }

        private void rdBtnYes_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if(e.Value == true)
            {
                ViewModel.CustomerDetails.Discount = null;
                stkMember.IsVisible= true;
                stkDiscount.IsVisible= false;
            }
            else
            {
                ViewModel.CustomerDetails.MemberDTO = null;
                stkMember.IsVisible = false;
                stkDiscount.IsVisible = true;
            }
        }

        private void rdBtnNo_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == true)
            {
                ViewModel.CustomerDetails.MemberDTO = null;
                stkMember.IsVisible = false;
                stkDiscount.IsVisible = true;
            }
            else
            {
                ViewModel.CustomerDetails.Discount = null;
                stkMember.IsVisible = true;
                stkDiscount.IsVisible = false;
            }
        }

        private void swtTaxable_Toggled(object sender, ToggledEventArgs e)
        {
            if(e.Value == true)
            {
                stkTexable.IsVisible= true;
            }
            else
            {
                stkTexable.IsVisible = false;
            }
        }

        private void Picker_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            var selectedOption = (sender as Picker).SelectedItem;
            ViewModel?.ChooseCustomerCategory.Execute(selectedOption);
        }

        private void Picker_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedOption = (sender as Picker).SelectedItem;
            ViewModel?.ChooseCustomerMemberShip.Execute(selectedOption);
        }

        private void Picker_SelectedIndexChanged_2(object sender, EventArgs e)
        {
            var selectedOption = (sender as Picker).SelectedItem;
            ViewModel?.ChooseCustomerTax.Execute(selectedOption);
        }

        private void Picker_SelectedIndexChanged_3(object sender, EventArgs e)
        {
            var selectedOption = (sender as Picker).SelectedItem;
            ViewModel?.ChooseCustomerCampaign.Execute(selectedOption);
        }

        //Address
        private void Entry_Focused(object sender, FocusEventArgs e)
        {
            var selectedOption = (sender as Entry).Text;
            ViewModel?.SelecteAddress.Execute(selectedOption);
        }
    }
}