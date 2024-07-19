using Acr.UserDialogs;
using GoogleApi.Entities.Maps.StreetView.Request.Enums;
using GoogleApi.Entities.Translate.Common.Enums;
using Newtonsoft.Json;
using Plugin.PayCards;
using FixPro.Models;
using Stripe;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.Dynamic;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Input;
using Twilio.Jwt.AccessToken;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace FixPro.ViewModels
{
    public class PaymentsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();

        InvoiceModel _OneInvoice;
        public InvoiceModel OneInvoice
        {
            get
            {
                return _OneInvoice;
            }
            set
            {
                _OneInvoice = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OneInvoice"));
                }
            }
        }

        PaymentsModel _OnePayment;
        public PaymentsModel OnePayment
        {
            get
            {
                return _OnePayment;
            }
            set
            {
                _OnePayment = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("OnePayment"));
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

        int _CashOrCredit;
        public int CashOrCredit
        {
            get
            {
                return _CashOrCredit;
            }
            set
            {
                _CashOrCredit = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("CashOrCredit"));
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

        string _SignatureImageByte64;
        public string SignatureImageByte64
        {
            get
            {
                return _SignatureImageByte64;
            }
            set
            {
                _SignatureImageByte64 = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("SignatureImageByte64"));
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

        Helpers.GenericRepository ORep = new Helpers.GenericRepository();

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

        public ICommand CashPayNow { get; set; }
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

        public PaymentsViewModel()
        {

        }

        public PaymentsViewModel(InvoiceModel model, CustomersModel Cust)
        {
            CashOrCredit = Controls.StaticMembers.PayCashOrCredit; //Show Credit Or Cash in View
            OnePayment = new PaymentsModel();
            OneInvoice = new InvoiceModel();
            StripeModel = new StripeAccountModel();

            OneInvoice = model;
            CustomerDetails = Cust;

            CashPayNow = new Command<InvoiceModel>(OnCashPayNow);
            CreditPayNow = new Command<InvoiceModel>(OnCreditPayNow);
        }

        async void OnCashPayNow(InvoiceModel model)
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
                    OnePayment.Type = 1;
                    OnePayment.Method = 1; //Cash

                    await InitiolizModel(model);
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            IsBusy = false;
        }

        async void OnCreditPayNow(InvoiceModel model)
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
                    OneInvoice = model;

                    OnePayment.Type = 0;
                    OnePayment.Method = 3; //Debit Card

                    await PayViaStripe();
                    UserDialogs.Instance.HideLoading();
                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "Please complete all the payment fields", "OK");
            }

            IsBusy= false;
        }

        public async Task InitiolizModel(InvoiceModel model)
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
                    if(!string.IsNullOrEmpty(SignatureImageByte64))
                    {
                        if (OnePayment.Amount <= model.Net || OnePayment.Amount == null)
                        {
                            string UserToken = await _service.UserToken();

                            OnePayment.AccountId = model.AccountId;
                            OnePayment.BrancheId = model.BrancheId;
                            OnePayment.CustomerId = model.CustomerId;
                            //OnePayment.ContractId = model.ContractId;
                            OnePayment.InvoiceId = model.Id;
                            //OnePayment.ExpensesId = model.ExpensesId;
                            OnePayment.PaymentDate = DateTime.Now;
                            OnePayment.SignatureDraw = SignatureImageByte64;

                            OnePayment.Amount = OnePayment.Amount == null ? model.Net : OnePayment.Amount;
                            //OnePayment.OverAmount = model.OverAmount;

                            //OnePayment.IncreaseDecrease = model.IncreaseDecrease;
                            //OnePayment.TransactionID = model.TransactionID;
                            //OnePayment.CheckNumber = model.CheckNumber;
                            //OnePayment.BankName = model.BankName;
                            //OnePayment.AccountNumber = model.AccountNumber;
                            OnePayment.Notes = model.Notes;
                            OnePayment.Active = model.Active;
                            OnePayment.CreateUser = model.CreateUser;
                            OnePayment.CreateDate = model.CreateDate;

                            UserDialogs.Instance.ShowLoading();
                            //var json = await Helpers.Utility.PostData("api/Payments/InsertPayment", JsonConvert.SerializeObject(OnePayment));
                            var json = await ORep.PostDataAsync("api/Payments/InsertPayment", OnePayment, UserToken);
                            UserDialogs.Instance.HideLoading();

                            if (json != null && json != "api not responding")
                            {
                                await App.Current.MainPage.DisplayAlert("FixPro", "Payment completed successfully.", "Ok");
                                await App.Current.MainPage.Navigation.PushAsync(new MainPage());
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("FixPro", "Payment not accepted for this job", "Ok");
                            }
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("FixPro", "Please enter the right amount", "Ok");
                        }
                    }   
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("FixPro", "Please sign in the signature field", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", ex.Message, "OK");
            }
            
        }

        async Task GetSkretKey(int? BranchId)
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                UserDialogs.Instance.ShowLoading();
                string UserToken = await _service.UserToken();

                var json = await ORep.GetAsync<StripeAccountModel>(string.Format("api/Payments/GetStripeAccount?" + "BranchId=" + BranchId), UserToken);

                if (json != null)
                {
                    StripeModel = json;
                }

                UserDialogs.Instance.HideLoading();
            }         
        }

        public async Task PayViaStripe()
        {
            if (Connectivity.NetworkAccess == Xamarin.Essentials.NetworkAccess.Internet)
            {
                await GetSkretKey(OneInvoice.BrancheId);

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
                    Name = CustomerDetails.FirstName + "" + CustomerDetails.LastName,
                    Email = CustomerDetails.Email,
                    Description = OneInvoice.ScheduleName,
                    Address = new AddressOptions { City = CustomerDetails.City, Country = CustomerDetails.Country, Line1 = CustomerDetails.Address, Line2 = "", PostalCode = CustomerDetails.PostalcodeZIP, State = CustomerDetails.State }
                };

                var customerService = new CustomerService();
                var cust = customerService.Create(customer);

                // step 5: charge option
                var chargeoption = new ChargeCreateOptions
                {
                    Amount = Convert.ToInt64(OneInvoice.Total * 100),
                    Currency = "USD",
                    ReceiptEmail = CustomerDetails.Email,
                    Customer = cust.Id,
                    Source = source.Id
                };

                // step 6: charge the customer
                var chargeService = new ChargeService();
                Charge charge = chargeService.Create(chargeoption);
                if (charge.Status == "succeeded")
                {
                    // success
                    await InitiolizModel(OneInvoice);
                }
                else
                {
                    // failed
                    await App.Current.MainPage.DisplayAlert("Alert", "Job payment failed!", "Ok");
                }
            }
               
        }
    }
}
