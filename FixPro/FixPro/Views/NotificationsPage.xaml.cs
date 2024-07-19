
using Acr.UserDialogs;
using FixPro.Controls;
using FixPro.ViewModels;
using Syncfusion.SfCalendar.XForms;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using static FixPro.ViewModels.HomeViewModel;

namespace FixPro.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotificationsPage : Controls.CustomsPage
    {

        public NotificationsPage()
        {
            InitializeComponent();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await App.Current.MainPage.Navigation.PopAsync();
            await App.Current.MainPage.Navigation.PushAsync(new MainPage());
        }

        protected override bool OnBackButtonPressed()
        {
            App.Current.MainPage.Navigation.PopAsync();
            App.Current.MainPage.Navigation.PushAsync(new MainPage());
            return true;
        }
    }
}