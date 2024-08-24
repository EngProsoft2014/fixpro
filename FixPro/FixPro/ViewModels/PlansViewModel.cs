using Acr.UserDialogs;
using Acr.UserDialogs.Infrastructure;
using FixPro.Models;
using FixPro.Views;
using GoogleApi.Entities.Translate.Common.Enums;
using Plugin.PayCards;
using Stripe;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FixPro.ViewModels
{
    public class PlansViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ObservableCollection<PlansModel> _LstPricing;
        public ObservableCollection<PlansModel> LstPricing
        {
            get
            {
                return _LstPricing;
            }
            set
            {
                _LstPricing = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("LstPricing"));
                }
            }
        }

        PlansModel _OnePlan;
        public PlansModel OnePlan
        {
            get
            {
                return _OnePlan;
            }
            set
            {
                _OnePlan = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OnePlan"));
                }
            }
        }

        StripeAccountModel _StripeModel;
        public StripeAccountModel StripeModel
        {
            get
            {
                return _StripeModel;
            }
            set
            {
                _StripeModel = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("StripeModel"));
                }
            }
        }

        bool _IsBusy;
        public bool IsBusy
        {
            get
            {
                return _IsBusy;
            }
            set
            {
                _IsBusy = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsBusy"));
                }
            }
        }

        string _CardNumber;
        public string CardNumber
        {
            get
            {
                return _CardNumber;
            }
            set
            {
                _CardNumber = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CardNumber"));
                }
            }
        }

        string _HolderName;
        public string HolderName
        {
            get
            {
                return _HolderName;
            }
            set
            {
                _HolderName = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("HolderName"));
                }
            }
        }

        string _ExpirationDate;
        public string ExpirationDate
        {
            get
            {
                return _ExpirationDate;
            }
            set
            {
                _ExpirationDate = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ExpirationDate"));
                }
            }
        }

        string _cvv;
        public string cvv
        {
            get
            {
                return _cvv;
            }
            set
            {
                _cvv = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("cvv"));
                }
            }
        }

        bool _IsMonthly; 
        public bool IsMonthly
        {
            get
            {
                return _IsMonthly;
            }
            set
            {
                _IsMonthly = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsMonthly"));
                }
            }
        }

        bool _IsYearly = true;
        public bool IsYearly
        {
            get
            {
                return _IsYearly;
            }
            set
            {
                _IsYearly = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("IsYearly"));
                }
            }
        }

        private PayCard _card;
        public PayCard Card
        {
            set
            {
                if (_card != value)
                {
                    _card = value;

                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Card)));
                }
            }
            get
            {
                CardNumber = _card.CardNumber;
                HolderName = _card.HolderName;
                ExpirationDate = _card.ExpirationDate;
                return _card;
            }
        }

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        public ICommand SelectedPlan { get; set; }
        public ICommand CreditPayNow { get; set; }
        public ICommand ScanCommand
        {
            get
            {
                return new Command(async () =>
                {
                    try
                    {
                        Card = await CrossPayCards.Current.ScanAsync().ConfigureAwait(true);
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine(ex);
                    }
                });
            }
        }

        public PlansViewModel()
        {
            LstPricing = new ObservableCollection<PlansModel>();
            OnePlan = new PlansModel();
            SelectedPlan = new Command<PlansModel>(OnSelectedPlan);
            Init();
        }

        public PlansViewModel(PlansModel model)
        {
            OnePlan = model;
            CreditPayNow = new Command<PlansModel>(OnCreditPayNow);
        }

        async void Init()
        {
            await GetPlans();
        }

        async Task GetPlans()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();

                var json = await ORep.GetAsync<ObservableCollection<PlansModel>>("api/Plans/GetPlans");

                if (json != null && json.Count != 0)
                {
                    LstPricing = json;
                }

                UserDialogs.Instance.HideLoading();
            }
        }

        async void OnSelectedPlan(PlansModel model)
        {
            var ViewModel = new PlansViewModel(model);
            var page = new Views.PlansPages.PlanPaymentPage();
            page.BindingContext = ViewModel;
            await App.Current.MainPage.Navigation.PushAsync(page);
        }


        async void OnCreditPayNow(PlansModel model)
        {
            IsBusy = true;

            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    return;
                }
                else
                {
                    UserDialogs.Instance.ShowLoading();
                    OnePlan = model;

                    if (string.IsNullOrEmpty(CardNumber))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Card Number.", "Ok");
                    }
                    else if (string.IsNullOrEmpty(HolderName))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Holder Name.", "Ok");
                    }
                    else if (string.IsNullOrEmpty(ExpirationDate))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : Expiration Date.", "Ok");
                    }
                    else if (string.IsNullOrEmpty(cvv))
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", $"Please Complete This Field Required : CVV.", "Ok");
                    }
                    else
                    {
                        if ((IsYearly == true && IsMonthly == false) || (IsYearly == false && IsMonthly == true))
                        {
                            if (IsYearly == true && IsMonthly == false)
                            {
                                //await PayViaStripe(model.AnnualPrice);
                                await PayForTest(model, true);
                            }
                            else
                            {
                                //await PayViaStripe(model.MonthlyPrice);
                                await PayForTest(model, false);
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Error", "Please Select Billing Cycle", "OK");
                        }
                    }

                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please complete all the payment fields", "OK");
            }

            IsBusy = false;
        }

        public async Task InitiolizModel(PlansModel model)
        {
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    return;
                }
                else
                {

                        //OnePayment.AccountId = model.AccountId;
                        //OnePayment.BrancheId = model.BrancheId;
                        //OnePayment.CustomerId = model.CustomerId;
                        ////OnePayment.ContractId = model.ContractId;
                        //OnePayment.InvoiceId = model.Id;
                        ////OnePayment.ExpensesId = model.ExpensesId;
                        //OnePayment.PaymentDate = DateTime.Now;
                        //OnePayment.SignatureDraw = SignatureImageByte64;

                        //OnePayment.Amount = OnePayment.Amount == null ? model.Net : OnePayment.Amount;
                        //OnePayment.OverAmount = model.OverAmount;

                        //OnePayment.IncreaseDecrease = model.IncreaseDecrease;
                        //OnePayment.TransactionID = model.TransactionID;
                        //OnePayment.CheckNumber = model.CheckNumber;
                        //OnePayment.BankName = model.BankName;
                        //OnePayment.AccountNumber = model.AccountNumber;
                        //OnePayment.Notes = model.Notes;
                        //OnePayment.Active = model.Active;
                        //OnePayment.CreateUser = model.CreateUser;
                        //OnePayment.CreateDate = model.CreateDate;

                        //UserDialogs.Instance.ShowLoading();
                        ////var json = await Helpers.Utility.PostData("api/Payments/InsertPayment", JsonConvert.SerializeObject(OnePayment));
                        //var json = await ORep.PostDataAsync("api/Payments/InsertPayment", OnePayment, UserToken);
                        //UserDialogs.Instance.HideLoading();

                        //if (json != null && json != "api not responding")
                        //{
                        //    await App.Current.MainPage.DisplayAlert("FixPro", "Payment completed successfully.", "Ok");
                        //    await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                        //}
                        //else
                        //{
                        //    await App.Current.MainPage.DisplayAlert("FixPro", "Payment not accepted", "Ok");
                        //}
 

                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }

        async Task GetSkretKey(int? AccountId, int? BranchId)
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();

                var json = await ORep.GetAsync<StripeAccountModel>(string.Format("api/Login/GetStripeAccount?" + "AccountId=" + AccountId + "&" + "BranchId=" + BranchId));

                if (json != null)
                {
                    StripeModel = json;
                }

                UserDialogs.Instance.HideLoading();
            }
        }

        public async Task PayViaStripe(string Price)
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                await GetSkretKey(int.Parse(Helpers.Settings.AccountId),int.Parse(Helpers.Settings.BranchId));

                StripeConfiguration.ApiKey = StripeModel.SecretKey;

                //StripeConfiguration.ApiKey = "sk_test_IHINMHgrNTLUWqh3IcTcMdNB";

                // step 2: Assign card to token object
                var stripeCard = new TokenCreateOptions
                {
                    Card = new TokenCardOptions
                    {
                        Number = CardNumber,
                        Name = HolderName,
                        ExpMonth = ExpirationDate.Split('/')[0],
                        ExpYear = ExpirationDate.Split('/')[1],
                        Cvc = cvv,
                    }
                };

                Stripe.TokenService service = new Stripe.TokenService();
                Stripe.Token newToken = service.Create(stripeCard);

                // step 3: assign the token to the source
                var option = new SourceCreateOptions
                {
                    Type = SourceType.Card,
                    Currency = "USD",
                    Token = newToken.Id
                };

                var sourceService = new SourceService();
                Stripe.Source source = sourceService.Create(option);

                // step 4: create customer
                CustomerCreateOptions customer = new CustomerCreateOptions
                {
                    Name = Helpers.Settings.UserFristName + "" + Helpers.Settings.UserLastName,
                    Email = Helpers.Settings.Email,
                    Description = OnePlan.Name,
                };

                var customerService = new CustomerService();
                var cust = customerService.Create(customer);

                // step 5: charge option
                var chargeoption = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt64(decimal.Parse(Price) * 100),
                    Currency = "USD",
                    ReceiptEmail = Helpers.Settings.Email,
                    Customer = cust.Id,
                    Source = source.Id
                };

                // step 6: charge the customer
                var chargeService = new ChargeService();
                Charge charge = chargeService.Create(chargeoption);
                if (charge.Status == "succeeded")
                {
                    // success
                    await InitiolizModel(OnePlan);
                }
                else
                {
                    // failed
                    await App.Current.MainPage.DisplayAlert("Alert", "Job payment failed!", "Ok");
                }
            }

        }



        public async Task PayForTest(PlansModel model,bool PriceType)
        {
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet connection!", "OK");
                    return;
                }
                else
                {

                    string AnnualMonthly = "";
                    string Amount = "";
                    if (PriceType == true)
                    {
                        AnnualMonthly = "Annual";
                        Amount = model.AnnualPrice;
                    }
                    else
                    {
                        AnnualMonthly = "Monthly";
                        Amount = model.MonthlyPrice;
                    }

                    UserDialogs.Instance.ShowLoading();

                    var json = await ORep.GetAsync<string>("api/Login/GetAccountToPayUpgrade?Email=" + Helpers.Settings.Email + "&Plan=" + model.Id + "&AnnualMonthly=" + AnnualMonthly + "&Amount=" + Amount + "&TransactionId=" + "12225" + "&OrderIdMySql=&UserIdMysql=");
                    
                    UserDialogs.Instance.HideLoading();

                    if (json == null || json == "")
                    {
                        await App.Current.MainPage.DisplayAlert("FixPro", "Payment completed successfully.", "Ok");
                        await App.Current.MainPage.Navigation.PushAsync(new LoginPage());
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("FixPro", "Payment not accepted", "Ok");
                    }

                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }

        }

    }
}
