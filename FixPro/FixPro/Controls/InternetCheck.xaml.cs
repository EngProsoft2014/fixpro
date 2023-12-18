using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InternetCheck : StackLayout
    {
        public InternetCheck()
        {
            InitializeComponent();
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            if (e.NetworkAccess != NetworkAccess.Internet)
            {
                // Connection to internet is Not available
                stkNoInternet.IsVisible = true;
                StaticMembers.YesOrNoInternet = 1;
            }
            else
            {
                // Connection to internet is available
                stkNoInternet.IsVisible = false;
                StaticMembers.YesOrNoInternet = 2;
            }
        }
    }
}