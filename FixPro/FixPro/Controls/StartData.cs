using Acr.UserDialogs;
using Newtonsoft.Json;
//using Org.BouncyCastle.Ocsp;
using Plugin.Geolocator;
using FixPro.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TcxTools;
using Xamarin.Essentials;
using Xamarin.Forms;
using Twilio.TwiML.Messaging;
using System.Net.Http;
using static System.Net.WebRequestMethods;
using Twilio.Http;
using Rg.Plugins.Popup.Services;
using Stripe;

namespace FixPro.Controls
{
    public static class StartData
    {
        static readonly Services.Data.ServicesService _service = new Services.Data.ServicesService();
        public static ObservableCollection<EmployeeModel> LstWorkingEmployeesStatic { get; set; }
        public static EmployeeModel EmployeeDataStatic { get; set; } = new EmployeeModel();
        public static DeviceDetailsModel DeviceDetails { get; set; }
        public static bool IsRunning { get; set; } = true;

        static Helpers.GenericRepository ORep = new Helpers.GenericRepository();

        static DeviceDetailsModel oDevice { get; set; }

        //Get Working Employees
        public async static Task GetWorkingEmployees(int AccountId, string ScheduleDate)
        {
            UserDialogs.Instance.ShowLoading();

            //string json = await Helpers.Utility.CallWebApi("api/Employee/GetEmpWorking?" + "AccountId=" + AccountId + "&" + "ScheduleDate=" + ScheduleDate);

            var json = await ORep.GetAsync<ObservableCollection<EmployeeModel>>("api/Employee/GetEmpWorking?" + "AccountId=" + AccountId + "&" + "ScheduleDate=" + ScheduleDate);

            if (json != null)
            {
                //List<EmployeeModel> Employee = JsonConvert.DeserializeObject<List<EmployeeModel>>(json);

                //LstWorkingEmployeesStatic = new ObservableCollection<EmployeeModel>(Employee);
                LstWorkingEmployeesStatic = json;
            }

            UserDialogs.Instance.HideLoading();
        }

        public async static Task CheckPermissionEmployee()
        {
            if (Helpers.Settings.UserName != "" && Helpers.Settings.Password != "")
            {
                //string UserToken = await _service.UserToken();
                //string json = await Helpers.Utility.CallWebApi("api/Employee/GetLogin?" + "UserName=" + Helpers.Settings.UserName + "&" + "Password=" + Helpers.Settings.Password);

                var json = await ORep.GetAsync<EmployeeModel>("api/Login/GetLogin?" + "UserName=" + Helpers.Settings.UserName + "&" + "Password=" + Helpers.Settings.Password);

                if (json != null)
                {
                    //EmployeeModel MLogin = JsonConvert.DeserializeObject<EmployeeModel>(json);

                    //if (MLogin != null)
                    //{
                    //    EmployeeDataStatic = MLogin;
                    //}

                    EmployeeDataStatic = json;
                }
            }
        }

        //public async static Task CheckDevice(int? AccountId, int EmpId, string PlayerId)
        //{
        //    if (!string.IsNullOrEmpty(PlayerId) && PlayerId != "0")
        //    {
        //        //await GetDeviceDetails();
        //    }
        //    //else
        //    //{
        //    //    oDevice = CreateDevice();

        //    //    Helpers.Settings.PlayerId = await ORep.PostDataAsync(string.Format("api/Notifications/PostDevice?" + "AccountId=" + AccountId + "&" + "EmpId=" + EmpId), oDevice);
        //    //}
        //}

        //public async static Task GetDeviceDetails()
        //{
        //    if(IsReplay == true) // infinty timer loop
        //    {
        //        if (DeviceDetails.device_model != null)
        //        {

        //            if (DeviceDetails.device_model != (DeviceInfo.Model + Helpers.Settings.DeviceId))
        //            {

        //                if (Helpers.Settings.UserName != "" && Helpers.Settings.Password != "")
        //                {
        //                    IsReplay = false;

        //                    oDevice = CreateDevice();

        //                    await ORep.PutDataAsync(string.Format("api/Notifications/PutPlayerId?" + "PlayerId=" + Helpers.Settings.PlayerId), oDevice);

        //                    await App.Current.MainPage.DisplayAlert("Alert", "Sorry, Login again please, because the App is open from another device", "Ok");
        //                    Helpers.Settings.UserId = "0";
        //                    Helpers.Settings.UserName = "";
        //                    Helpers.Settings.UserFristName = "";
        //                    Helpers.Settings.Email = "";
        //                    Helpers.Settings.Phone = "";
        //                    Helpers.Settings.Password = "";
        //                    Helpers.Settings.CreateDate = "";
        //                    Helpers.Settings.BranchId = "";
        //                    Helpers.Settings.BranchName = "";
        //                    Helpers.Settings.UserRole = "";
        //                    Helpers.Settings.PlayerId = "0";
        //                    await App.Current.MainPage.Navigation.PushAsync(new Views.LoginPage());
        //                }
        //            }
        //        }
        //    }         

        //}


        //public static DeviceDetailsModel CreateDevice()
        //{
        //    oDevice = new DeviceDetailsModel()
        //    {
        //        app_id = Helpers.Settings.OneSignalAppId,
        //        device_type = DeviceInfo.Platform.ToString() == "iOS" ? 0 : DeviceInfo.Platform.ToString() == "Android" ? 1 : 3,
        //        language = "en",
        //        timezone = DateTime.Now.Second.ToString(),
        //        game_version = "",
        //        device_model = DeviceInfo.Model + Helpers.Settings.DeviceId,
        //        device_os = DeviceInfo.Version.ToString(),
        //        session_count = 1,
        //        notification_types = 1,

        //    };

        //    return oDevice;
        //}

        public async static Task GetAccountKeysAsync()
        {
            string UserToken = await _service.UserToken();
            Helpers.GenericRepository ORep = new Helpers.GenericRepository();
            var Account = await ORep.GetAsync<AccountModel>("api/Notifications/GetAccountKeys?" + "AccountId=" + Helpers.Settings.AccountId, UserToken);
            if (Account != null)
            {
                Helpers.Settings.OneSignalAppId = Account.OneSignalAppId;
                Helpers.Settings.OneSignalRest = Account.OneSignalRestApikey;
                Helpers.Settings.OneSignalAuth = Account.OneSignalAuthApi;
            }
        }

        public async static Task GetMyDeviceDetails()
        {
            if (Helpers.Settings.PlayerId != "0")
            {
                string UserToken = await _service.UserToken();

                string json = await ORep.GetAsync<string>("api/Notifications/GetDevice?" + "AccountId=" + Helpers.Settings.AccountId + "&" + "PlayerId=" + Helpers.Settings.PlayerId, UserToken); // Get Device Details

                if (json != null && json.Contains("-"))
                {
                    DeviceDetails = JsonConvert.DeserializeObject<DeviceDetailsModel>(json);
                }
            }
        }



        private const string BaseUrl = "https://realty-mole-property-api.p.rapidapi.com/";
        private const string ApiKey = "47e9dcb44emsh0d093dc49db704fp123a6ejsn5eedc26ab2cd";

        // Define a method to get a property by its address
        public async static Task<string> GetPropertyByAddress(string address)
        {
            // Create an HttpClient object and set the headers
            using (var client = new System.Net.Http.HttpClient())
            {
                client.DefaultRequestHeaders.Add("accept", "application/json");
                client.DefaultRequestHeaders.Add("X-RapidAPI-Key", ApiKey);
                client.DefaultRequestHeaders.Add("X-RapidAPI-Host", "realty-mole-property-api.p.rapidapi.com");

                // Build the request URL with the address parameter
                var requestUrl = BaseUrl + "properties?address=" + address;

                // Send the request and get the response
                var response = await client.GetAsync(requestUrl);

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var responseContent = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON string into a Property object
                    //var property = JsonConvert.DeserializeObject<List<HouseProperty>>(responseContent);

                    // Return the property object
                    //return property[0];

                    return responseContent;
                }
                else
                {
                    // Throw an exception if the response is not successful
                    //throw new Exception(response.ReasonPhrase);
                    return "";
                }

            }

        }


        public static CustomersModel GetAddressDetails(CustomersModel model)
        {
            string JsonData = "[{\"addressLine1\":\"29022 Crystal Rose Ln\",\"city\":\"Fulshear\",\"state\":\"TX\",\"zipCode\":\"77441\",\"formattedAddress\":\"29022 Crystal Rose Ln, Fulshear, TX 77441\",\"assessorID\":\"8004-01-001-0100-901\",\"county\":\"Fort Bend\",\"legalDescription\":\"THE BROOKS AT CROSS CREEK RANCH SEC 1, BLOCK 1, LOT 10\",\"ownerOccupied\":true,\"squareFootage\":2548,\"subdivision\":\"THE BROOKS AT CROSS CREEK RANCH SEC 1\",\"yearBuilt\":2016,\"bathrooms\":2.5,\"lotSize\":6383,\"propertyType\":\"Single Family\",\"lastSaleDate\":\"2019-02-28T00:00:00.000Z\",\"bedrooms\":3,\"features\":{\"cooling\":true,\"coolingType\":\"Central\",\"exteriorType\":\"Brick Veneer\",\"floorCount\":2,\"garage\":true,\"garageSpaces\":2,\"garageType\":\"Attached\",\"heating\":true,\"heatingType\":\"Central\",\"roofType\":\"Composition Shingle\"},\"taxAssessment\":{\"2021\":{\"value\":294510,\"land\":51410,\"improvements\":243100},\"2022\":{\"value\":323960,\"land\":51410,\"improvements\":272550}},\"propertyTaxes\":{\"2021\":{\"total\":5223}},\"owner\":{\"names\":[\"TAREK GABER\"],\"mailingAddress\":{\"id\":\"29022-Crystal-Rose-Ln,-Fulshear,-TX-77441\",\"addressLine1\":\"29022 Crystal Rose Ln\",\"city\":\"Fulshear\",\"state\":\"TX\",\"zipCode\":\"77441\"}},\"id\":\"29022-Crystal-Rose-Ln,-Fulshear,-TX-77441\",\"longitude\":-95.876308,\"latitude\":29.70583}]";

            var data = JsonConvert.DeserializeObject<List<dynamic>>(JsonData);
            var yearBuilt = data.Select(m => m.yearBuilt).FirstOrDefault();
            if (yearBuilt != null)
            {
                yearBuilt = yearBuilt.ToString();
                if (yearBuilt != "")
                {
                    string yearBuilt_ = model.YearBuit = yearBuilt;
                }
            }

            var squareFootage = data.Select(m => m.squareFootage).FirstOrDefault();
            if (squareFootage != null)
            {
                squareFootage = squareFootage.ToString();
                if (squareFootage != "")
                {
                    string squareFootage_ = model.Squirefootage = squareFootage;
                }
            }

            var taxAssessments = data.Select(m => m.taxAssessment).FirstOrDefault();
            if (taxAssessments != null)
            {
                taxAssessments = taxAssessments.ToString();
                if (taxAssessments != "")
                {
                    var datas = JsonConvert.DeserializeObject<Dictionary<int, YearData>>(taxAssessments);
                    List<YearData> AllDat = new List<YearData>();
                    foreach (KeyValuePair<int, YearData> item in datas)
                    {
                        int Year = item.Key;
                        int EstimateValue = item.Value.Value;
                        AllDat.Add(new YearData { Year = Year, Value = EstimateValue });
                    }
                    if (AllDat.Count > 0)
                    {
                        int MaxYear = AllDat.Max(m => m.Year);
                        int ValueYear = AllDat.Where(y => y.Year == MaxYear).FirstOrDefault().Value;

                        model.YearEstimedValue = MaxYear.ToString();
                        model.EstimedValue = ValueYear.ToString();
                    }
                }
            }

            if(model.Id != 0) //if this customer already creation
            {
                UpdateAddressDetailsCustomer(model);
            }

            return model;
        }


        static async void UpdateAddressDetailsCustomer(CustomersModel model)
        {
            try
            {
                if (Connectivity.NetworkAccess != Xamarin.Essentials.NetworkAccess.Internet)
                {
                    await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
                }
                else
                {

                    var json = await ORep.PutAsync("api/Customers/PutCustomerAddress", model);

                }
            }
            catch (Exception)
            {
                await App.Current.MainPage.DisplayAlert("Error", "No Internet Avialable !!!", "OK");
            }
        }

    }
}
