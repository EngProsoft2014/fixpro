using FixPro.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace FixPro.ViewModels
{
    public class OnMyWayViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<TimeModel> _LstTimes;
        public ObservableCollection<TimeModel> LstTimes
        {
            get
            {
                return _LstTimes;
            }
            set
            {
                _LstTimes = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstTimes"));
                }
            }
        }

        CustomersModel _CustomerDetails;
        public CustomersModel CustomerDetails
        {
            get
            {
                return _CustomerDetails;
            }
            set
            {
                _CustomerDetails = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CustomerDetails"));
                }
            }
        }

        TimeModel _TimeDetails;
        public TimeModel TimeDetails
        {
            get
            {
                return _TimeDetails;
            }
            set
            {
                _TimeDetails = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("TimeDetails"));
                }
            }
        }

        string _MyWayTextMsg;
        public string MyWayTextMsg
        {
            get
            {
                return _MyWayTextMsg;
            }
            set
            {
                _MyWayTextMsg = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("MyWayTextMsg"));
                }
            }
        }

        public ICommand SendMyWayTextMsg { get; set; }
        public ICommand SelectTime { get; set; }

        public OnMyWayViewModel()
        {
            Init();
        }

        public OnMyWayViewModel(CustomersModel model)
        {
            Init();

            CustomerDetails = model;    
        }

        void Init()
        {
            TimeDetails = new TimeModel();
            LstTimes = new ObservableCollection<TimeModel>();

            SendMyWayTextMsg = new Command<string>(OnSendMyWayTextMsg);
            SelectTime = new Command<string>(OnSelectTime);

            LstTimes.Add(new TimeModel() { Time = "5", BackgroundColor = "#ffffff", TextColor = "#0098BC", IsChecked = false, });
            LstTimes.Add(new TimeModel() { Time = "10", BackgroundColor = "#ffffff", TextColor = "#0098BC", IsChecked = false, });
            LstTimes.Add(new TimeModel() { Time = "15", BackgroundColor = "#ffffff", TextColor = "#0098BC", IsChecked = false, });
            LstTimes.Add(new TimeModel() { Time = "30", BackgroundColor = "#ffffff", TextColor = "#0098BC", IsChecked = false, });
            LstTimes.Add(new TimeModel() { Time = "45", BackgroundColor = "#ffffff", TextColor = "#0098BC", IsChecked = false, });
            LstTimes.Add(new TimeModel() { Time = "60", BackgroundColor = "#ffffff", TextColor = "#0098BC", IsChecked = false, });

            MyWayTextMsg = $"Hello! This is {Helpers.Settings.AccountNameWithSpace}. We wil arrive in approximately (0) minutes.";

        }

        void OnSelectTime(string time)
        {
            MyWayTextMsg = $"Hello! This is {Helpers.Settings.AccountNameWithSpace}. We wil arrive in approximately ({time}) minutes.";
        }

        async void OnSendMyWayTextMsg(string text)
        {
            string returnMsg = Controls.StartData.SendSMS(CustomerDetails.Phone1, text);
            if (string.IsNullOrEmpty(returnMsg))
            {
                await App.Current.MainPage.DisplayAlert("Alert", "Failed to send SMS to customer!", "Ok");
            }
        }


    }
}
