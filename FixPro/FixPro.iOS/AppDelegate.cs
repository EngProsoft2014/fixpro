using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;

using Syncfusion.ListView.XForms.iOS;
using Syncfusion.SfCalendar.XForms.iOS;
using Syncfusion.XForms.iOS.EffectsView;
using UIKit;
using Xamarin.Forms;


namespace FixPro.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        UIWindow window;
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {

            //Xamarin.FormsGoogleMaps.Init("AIzaSyARWU8W2TY1AIC-5vgRAs5ulMJeh6Qpw7c");

            Rg.Plugins.Popup.Popup.Init();
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NTM2MzEwQDMxMzkyZTMzMmUzMExmNTFqY1piMlA5L3VpMVZ6eGtadDZIK2tscjV2MnVMdGpWZzlrOHp0cU09");

            new Syncfusion.SfNavigationDrawer.XForms.iOS.SfNavigationDrawerRenderer();
            Syncfusion.SfSchedule.XForms.iOS.SfScheduleRenderer.Init();
            Syncfusion.XForms.iOS.TabView.SfTabViewRenderer.Init();
            Syncfusion.XForms.iOS.ComboBox.SfComboBoxRenderer.Init();
            SfCalendarRenderer.Init();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();

            global::Xamarin.Forms.Forms.Init();

            SfListViewRenderer.Init();
            SfEffectsViewRenderer.Init();

            Xamarin.FormsMaps.Init();
            
            //Xamarin.FormsGoogleMaps.Init("AIzaSyARWU8W2TY1AIC-5vgRAs5ulMJeh6Qpw7c");

            LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }


        // The following Exports are needed to run OneSignal in the iOS Simulator.
        //   The simulator doesn't support push however this prevents a crash due to a Xamarin bug
        //   https://bugzilla.xamarin.com/show_bug.cgi?id=52660
        [Export("oneSignalApplicationDidBecomeActive:")]
        public void OneSignalApplicationDidBecomeActive(UIApplication application)
        {
            // Remove line if you don't have a OnActivated method.
            OnActivated(application);
        }

        [Export("oneSignalApplicationWillResignActive:")]
        public void OneSignalApplicationWillResignActive(UIApplication application)
        {
            // Remove line if you don't have a OnResignActivation method.
            OnResignActivation(application);
        }

        [Export("oneSignalApplicationDidEnterBackground:")]
        public void OneSignalApplicationDidEnterBackground(UIApplication application)
        {
            // Remove line if you don't have a DidEnterBackground method.
            DidEnterBackground(application);
        }

        [Export("oneSignalApplicationWillTerminate:")]
        public void OneSignalApplicationWillTerminate(UIApplication application)
        {
            // Remove line if you don't have a WillTerminate method.
            WillTerminate(application);
        }

    }
}
