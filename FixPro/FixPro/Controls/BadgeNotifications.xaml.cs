using FixPro.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BadgeNotifications : Grid
    {
        //HomeViewModel ViewModel { get => BindingContext as HomeViewModel; set => BindingContext = value; }
        public int Num { get; set; } = 0;
        public BadgeNotifications()
        {
            InitializeComponent();

            //Num = Controls.StartData.Messages.Count; 
            lblBadge.Text = Convert.ToString(Num);

            if (lblBadge.Text == "0")
                lblBadge.IsVisible = false;
        }

   

    }
}