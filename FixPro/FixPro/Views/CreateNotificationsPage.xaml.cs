using FixPro.Models;
using FixPro.ViewModels;
using Syncfusion.DataSource.Extensions;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CreateNotificationsPage : Controls.CustomsPage
	{
        HomeViewModel ViewModel { get => BindingContext as HomeViewModel; set => BindingContext = value; }

        public CreateNotificationsPage()
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

        private void chBxAllEmployees_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value == false)
            {
                ViewModel.GetEmployeesInAccountId(int.Parse(Helpers.Settings.AccountId));
            }
        }
    }
}