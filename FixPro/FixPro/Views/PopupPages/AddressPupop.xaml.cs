using FixPro.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace FixPro.Views.PopupPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressPupop : Rg.Plugins.Popup.Pages.PopupPage
    {
        public delegate void CustomDelegte(SuggestionAddressModel str);
        public event CustomDelegte DidClose;

        public AddressPupop()
        {
            InitializeComponent();

            //if (App.Lang == "ar")
            //    this.FlowDirection = FlowDirection.RightToLeft;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
        }

        // ### Methods for supporting animations in your popup page ###

        // Invoked before an animation appearing
        protected override void OnAppearingAnimationBegin()
        {
            base.OnAppearingAnimationBegin();
        }

        // Invoked after an animation appearing
        protected override void OnAppearingAnimationEnd()
        {
            base.OnAppearingAnimationEnd();
        }

        // Invoked before an animation disappearing
        protected override void OnDisappearingAnimationBegin()
        {
            base.OnDisappearingAnimationBegin();
        }

        // Invoked after an animation disappearing
        protected override void OnDisappearingAnimationEnd()
        {
            base.OnDisappearingAnimationEnd();
        }

        protected override Task OnAppearingAnimationBeginAsync()
        {
            return base.OnAppearingAnimationBeginAsync();
        }

        protected override Task OnAppearingAnimationEndAsync()
        {
            return base.OnAppearingAnimationEndAsync();
        }

        protected override Task OnDisappearingAnimationBeginAsync()
        {
            return base.OnDisappearingAnimationBeginAsync();
        }

        protected override Task OnDisappearingAnimationEndAsync()
        {
            return base.OnDisappearingAnimationEndAsync();
        }

        // ### Overrided methods which can prevent closing a popup page ###

        // Invoked when a hardware back button is pressed
        protected override bool OnBackButtonPressed()
        {
            // Return true if you don't want to close this popup page when a back button is pressed
            return base.OnBackButtonPressed();
        }

        // Invoked when background is clicked
        protected override bool OnBackgroundClicked()
        {
            // Return false if you don't want to close this popup page when a background of the popup page is clicked
            return base.OnBackgroundClicked();
        }

        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            if (stkManuallyAddress.IsVisible == false)
            {
                stkManuallyAddress.IsVisible = true;
            }
            else
            {
                stkManuallyAddress.IsVisible = false;
            }
        }
        //public static async Task<List<SuggestionAddress>> GetPlacesAutocompleteAsync(string search)
        //{
        //    var request = string.Format("https://maps.googleapis.com/maps/api/place/queryautocomplete/xml?input= + {0} + &language={1}&key={2}",
        //        search, App.Lang , "AIzaSyBrriO9GGKoeIAIiS3L8asTps80-sXzQgo");

        //    //var request = string.Format("https://maps.googleapis.com/maps/api/place/queryautocomplete/xml?input=" + search + "&language=ar&key=AIzaSyBrriO9GGKoeIAIiS3L8asTps80-sXzQgo");

        //    HttpClient client = new HttpClient();
        //    client.MaxResponseContentBufferSize = 256000;
        //    var xml = await client.GetStringAsync(request);
        //    var results = XDocument.Parse(xml).Element("AutocompletionResponse")?.Elements("prediction");

        //    List<SuggestionAddress> ListsuggestionsAddress = new List<SuggestionAddress>();
        //    SuggestionAddress suggestionAddress;

        //    if (results != null)
        //        foreach (var result in results)
        //        {
        //            var suggestion = result.Element("description")?.Value;
        //            int indx = 0;
        //            if (suggestion.Contains('،') == true)
        //            {
        //                indx = suggestion.IndexOf('،');
        //                string SubAr = suggestion.Substring(0, indx);
        //                string SubEn = suggestion.Substring(indx, suggestion.Length - indx);
        //            }
        //            else if (suggestion.Contains(',') == true)
        //            {
        //                indx = suggestion.IndexOf(',');
        //                string SubAr = suggestion.Substring(0, indx);
        //                string SubEn = suggestion.Substring(indx, suggestion.Length - indx);
        //            }
        //            else
        //            {
        //                string SubAr = suggestion;
        //                string SubEn = suggestion;
        //            }

        //            suggestionAddress = new SuggestionAddress
        //            {
        //                FullAddress = suggestion,
        //                ArAddress = suggestion.Substring(0, indx),
        //                EnAdress = suggestion.Substring(indx, suggestion.Length - indx)
        //            };

        //            ListsuggestionsAddress.Add(suggestionAddress);
        //        }

        //    return ListsuggestionsAddress;
        //}


        //=============================================================================



        //public static async Task<List<SuggestionAddressModel>> GetPlacesAutocompleteAsync(string search)
        //{
        //    string[] MAddress = new string[3];
        //    GoogleApi.Entities.Places.AutoComplete.Request.PlacesAutoCompleteRequest request = new GoogleApi.Entities.Places.AutoComplete.Request.PlacesAutoCompleteRequest();

        //    request.Input = search;

        //    //if (App.Lang == "ar")
        //    //{
        //    //    request.Language = GoogleApi.Entities.Common.Enums.Language.Arabic;
        //    //}
        //    //else
        //    //{
        //    request.Language = GoogleApi.Entities.Common.Enums.Language.English;
        //    //}

        //    request.Key = "AIzaSyBrriO9GGKoeIAIiS3L8asTps80-sXzQgo";

        //    var response = GoogleApi.GooglePlaces.AutoComplete.Query(request, null);

        //    List<SuggestionAddressModel> ListsuggestionsAddress = new List<SuggestionAddressModel>();
        //    SuggestionAddressModel suggestionAddress;

        //    foreach (var item in response.Predictions)
        //    {
        //        var _request2 = new GoogleApi.Entities.Places.Details.Request.PlacesDetailsRequest
        //        {
        //            Key = request.Key,
        //            PlaceId = item.PlaceId.ToString(),
        //            //Region = "EG",
        //            //Language = App.Lang == "ar" ? GoogleApi.Entities.Common.Enums.Language.Arabic : GoogleApi.Entities.Common.Enums.Language.English
        //            Language = GoogleApi.Entities.Common.Enums.Language.English,
        //        };

        //        var _response2 = GoogleApi.GooglePlaces.Details.Query(_request2);

        //        string[] ArrAdd = new string[7];
        //        //int i = 0;
        //        foreach (var itemAdd in _response2.Result.AddressComponents)
        //        {
        //            switch (itemAdd.Types.FirstOrDefault())
        //            {
        //                case GoogleApi.Entities.Common.Enums.AddressComponentType.Street_Number:
        //                    {
        //                        ArrAdd[0] = itemAdd.LongName;
        //                    }
        //                    break;
        //                case GoogleApi.Entities.Common.Enums.AddressComponentType.Route:
        //                    {
        //                        ArrAdd[1] = itemAdd.LongName;
        //                    }
        //                    break;
        //                case GoogleApi.Entities.Common.Enums.AddressComponentType.Administrative_Area_Level_3:
        //                    {
        //                        ArrAdd[2] = itemAdd.LongName;
        //                    }
        //                    break;
        //                case GoogleApi.Entities.Common.Enums.AddressComponentType.Administrative_Area_Level_2:
        //                    {
        //                        ArrAdd[3] = itemAdd.LongName;
        //                    }
        //                    break;
        //                case GoogleApi.Entities.Common.Enums.AddressComponentType.Administrative_Area_Level_1:
        //                    {
        //                        ArrAdd[4] = itemAdd.LongName;
        //                    }
        //                    break;
        //                case GoogleApi.Entities.Common.Enums.AddressComponentType.Country:
        //                    {
        //                        ArrAdd[5] = itemAdd.LongName;
        //                    }
        //                    break;
        //                case GoogleApi.Entities.Common.Enums.AddressComponentType.Postal_Code:
        //                    {
        //                        ArrAdd[6] = itemAdd.LongName;
        //                    }
        //                    break;
        //            }
        //            //ArrAdd[i] = itemAdd.LongName;
        //            //i++;
        //        }

        //        suggestionAddress = new SuggestionAddressModel
        //        {
        //            PalceId = _response2.Result.PlaceId,
        //            Latitude = _response2.Result.Geometry.Location.Latitude,
        //            Longitude = _response2.Result.Geometry.Location.Longitude,
        //            FullAddress = ArrAdd[0] + " " + ArrAdd[1] + " " + ArrAdd[2] + " " + ArrAdd[3] + " " + ArrAdd[4] + " " + ArrAdd[5],
        //            FullAddressAr = ArrAdd[0] + " " + ArrAdd[1] + " " + ArrAdd[2] + " " + ArrAdd[3] + " " + ArrAdd[4] + " " + ArrAdd[5],
        //            MainAddress = ArrAdd[0] + " " + ArrAdd[1] + " " + ArrAdd[2],
        //            SubAddress = ArrAdd[3] + " " + ArrAdd[4],
        //            Street = ArrAdd[1],
        //            StreetAr = ArrAdd[1],
        //            Locality = ArrAdd[2],
        //            LocalityAr = ArrAdd[2],
        //            State = ArrAdd[3],
        //            StateAr = ArrAdd[3],
        //            City = ArrAdd[4],
        //            CityAr = ArrAdd[4],
        //            Country = ArrAdd[5],
        //            CountryAr = ArrAdd[5],
        //            Zip = ArrAdd[6],
        //        };

        //        ListsuggestionsAddress.Add(suggestionAddress);
        //    }

        //    return ListsuggestionsAddress;
        //}


        private void SearchBar_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            countryListView.IsVisible = true;
            countryListView.BeginRefresh();

            try
            {
                Device.BeginInvokeOnMainThread(async () =>
                {
                    try
                    {
                        List<SuggestionAddressModel> x = await GetPlacesAutocompleteAsync(searchBar.Text);
                        //string[] x = await GetPlacesAutocompleteAsync(searchBar.Text);
                        countryListView.ItemsSource = x;
                    }
                    catch (Exception err)
                    {
                        System.Diagnostics.Debug.WriteLine("Exception with autocomplete: " + err.Message + " stack :" + err.StackTrace);
                    }
                });
            }
            catch (Exception ex)
            {
                countryListView.IsVisible = false;

            }
            countryListView.EndRefresh();
        }

        private async void ListView_OnItemTapped(Object sender, ItemTappedEventArgs e)
        {
            //EmployeeListView.IsVisible = false;  
            SuggestionAddressModel Listed = e.Item as SuggestionAddressModel;

            var _request2 = new GoogleApi.Entities.Places.Details.Request.PlacesDetailsRequest
            {
                Key = "AIzaSyBrriO9GGKoeIAIiS3L8asTps80-sXzQgo",
                PlaceId = Listed.PalceId,
                //Language = App.Lang == "ar" ? GoogleApi.Entities.Common.Enums.Language.English : GoogleApi.Entities.Common.Enums.Language.Arabic
            };

            var _response2 = GoogleApi.GooglePlaces.Details.Query(_request2);

            string[] ArrAdd = new string[7];

            foreach (var itemAdd in _response2.Result.AddressComponents)
            {
                switch (itemAdd.Types.FirstOrDefault())
                {
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Street_Number:
                        {
                            ArrAdd[0] = itemAdd.LongName;
                        }
                        break;
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Route:
                        {
                            ArrAdd[1] = itemAdd.LongName;
                        }
                        break;
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Administrative_Area_Level_3:
                        {
                            ArrAdd[2] = itemAdd.LongName;
                        }
                        break;
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Administrative_Area_Level_2:
                        {
                            ArrAdd[3] = itemAdd.LongName;
                        }
                        break;
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Administrative_Area_Level_1:
                        {
                            ArrAdd[4] = itemAdd.LongName;
                        }
                        break;
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Country:
                        {
                            ArrAdd[5] = itemAdd.LongName;
                        }
                        break;
                    case GoogleApi.Entities.Common.Enums.AddressComponentType.Postal_Code:
                        {
                            ArrAdd[6] = itemAdd.LongName;
                        }
                        break;
                }
            }

            searchBar.Text = Listed.FullAddress;
            countryListView.IsVisible = false;

            ((ListView)sender).SelectedItem = null;

            //DidClose?.Invoke(searchBar.Text);
            DidClose?.Invoke(Listed);

            await PopupNavigation.Instance.PopAsync();
        }

        private async void OnImageNameTapped_CloseIcon(object sender, EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }


        public static async Task<List<SuggestionAddressModel>> GetPlacesAutocompleteAsync(string search)
        {
            GoogleApi.Entities.Places.AutoComplete.Request.PlacesAutoCompleteRequest request = new GoogleApi.Entities.Places.AutoComplete.Request.PlacesAutoCompleteRequest();
            request.Input = search;
            request.Key = "AIzaSyBrriO9GGKoeIAIiS3L8asTps80-sXzQgo";

            var response = await GoogleApi.GooglePlaces.AutoComplete.QueryAsync(request, null);

            List<Task<GoogleApi.Entities.Places.Details.Response.PlacesDetailsResponse>> detailsTasks = new List<Task<GoogleApi.Entities.Places.Details.Response.PlacesDetailsResponse>>();

            foreach (var prediction in response.Predictions)
            {
                var detailsRequest = new GoogleApi.Entities.Places.Details.Request.PlacesDetailsRequest
                {
                    Key = request.Key,
                    PlaceId = prediction.PlaceId.ToString(),
                    Language = GoogleApi.Entities.Common.Enums.Language.English
                };

                detailsTasks.Add(GoogleApi.GooglePlaces.Details.QueryAsync(detailsRequest));
            }

            var detailsResponses = await Task.WhenAll(detailsTasks);

            var suggestions = detailsResponses.Select(detailsResponse =>
            {
                var addressComponents = detailsResponse.Result.AddressComponents;

                string[] ArrAdd = new string[7];

                foreach (var itemAdd in addressComponents)
                {
                    switch (itemAdd.Types.FirstOrDefault())
                    {
                        case GoogleApi.Entities.Common.Enums.AddressComponentType.Street_Number:
                            {
                                ArrAdd[0] = itemAdd.LongName;
                            }
                            break;
                        case GoogleApi.Entities.Common.Enums.AddressComponentType.Route:
                            {
                                ArrAdd[1] = itemAdd.LongName;
                            }
                            break;
                        case GoogleApi.Entities.Common.Enums.AddressComponentType.Administrative_Area_Level_3:
                            {
                                ArrAdd[2] = itemAdd.LongName;
                            }
                            break;
                        case GoogleApi.Entities.Common.Enums.AddressComponentType.Administrative_Area_Level_2:
                            {
                                ArrAdd[3] = itemAdd.LongName;
                            }
                            break;
                        case GoogleApi.Entities.Common.Enums.AddressComponentType.Administrative_Area_Level_1:
                            {
                                ArrAdd[4] = itemAdd.LongName;
                            }
                            break;
                        case GoogleApi.Entities.Common.Enums.AddressComponentType.Country:
                            {
                                ArrAdd[5] = itemAdd.LongName;
                            }
                            break;
                        case GoogleApi.Entities.Common.Enums.AddressComponentType.Postal_Code:
                            {
                                ArrAdd[6] = itemAdd.LongName;
                            }
                            break;
                    }
                }

                return new SuggestionAddressModel
                {
                    PalceId = detailsResponse.Result.PlaceId,
                    Latitude = detailsResponse.Result.Geometry.Location.Latitude,
                    Longitude = detailsResponse.Result.Geometry.Location.Longitude,
                    FullAddress = ArrAdd[0] + " " + ArrAdd[1] + " " + ArrAdd[2] + " " + ArrAdd[3] + " " + ArrAdd[4] + " " + ArrAdd[5],
                    FullAddressAr = ArrAdd[0] + " " + ArrAdd[1] + " " + ArrAdd[2] + " " + ArrAdd[3] + " " + ArrAdd[4] + " " + ArrAdd[5],
                    MainAddress = ArrAdd[0] + " " + ArrAdd[1] + " " + ArrAdd[2],
                    SubAddress = ArrAdd[3] + " " + ArrAdd[4],
                    Street = ArrAdd[1],
                    StreetAr = ArrAdd[1],
                    Locality = ArrAdd[2],
                    LocalityAr = ArrAdd[2],
                    State = ArrAdd[3],
                    StateAr = ArrAdd[3],
                    City = ArrAdd[4],
                    CityAr = ArrAdd[4],
                    Country = ArrAdd[5],
                    CountryAr = ArrAdd[5],
                    Zip = ArrAdd[6],
                };
            }).ToList();

            return suggestions;
        }

       
    }
}