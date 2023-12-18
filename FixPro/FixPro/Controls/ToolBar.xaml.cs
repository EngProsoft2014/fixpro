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
    public partial class ToolBar : StackLayout
    {
        public static readonly BindableProperty TitleProperty = BindableProperty.Create("Title", typeof(string), typeof(StackLayout), "", propertyChanged: OnEventNameChanged);

        public static readonly BindableProperty HasBackButtonProperty = BindableProperty.Create("HasBackButton", typeof(bool), typeof(StackLayout), true, propertyChanged: OnBackButtonChanged);

        public string Title
        {
            get { return (string)base.GetValue(TitleProperty); }
            set
            {
                base.SetValue(TitleProperty, value);
            }
        }

        public bool HasBackButton
        {
            get { return (bool)base.GetValue(HasBackButtonProperty); }
            set
            {
                base.SetValue(HasBackButtonProperty, value);
            }
        }
        public ToolBar()
        {
            InitializeComponent();       
            //imgBack.Source = ImageSource.FromResource("FixPro.Images.icons8-left-35.png");
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            //await Navigation.PushAsync(new MainPage());
            //App.Current.MainPage.Navigation.RemovePage(App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 2]);
        }

        static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;
            var tolbal = bindable as ToolBar;
            tolbal.lblTitle.Text = newValue.ToString();
        }

        static void OnBackButtonChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (newValue == null)
                return;
            var tolbal = bindable as ToolBar;
            tolbal.imgBack.IsVisible = (bool)newValue;
        }
    }
}