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
	public partial class CustomersDetailsPage : Controls.CustomsPage
	{
        public CustomersDetailsPage()
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

    }
}