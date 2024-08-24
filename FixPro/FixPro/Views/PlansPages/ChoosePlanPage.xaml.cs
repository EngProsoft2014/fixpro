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
    public partial class ChoosePlanPage : Controls.CustomsPage
    {
        public ChoosePlanPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}