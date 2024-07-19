﻿using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FixPro.Exceptions;
using Newtonsoft.Json;
using Polly;
using Acr.UserDialogs;
using Stripe;
using Xamarin.Essentials;
using System.Text;
using System.Threading;
using OneSignalSDK.Xamarin.Core;
using Newtonsoft.Json.Linq;
using FixPro.Models;
using System.Collections.Generic;
using GoogleApi.Entities.Translate.Common.Enums;

namespace FixPro.Helpers
{
    public interface IGenericRepository
    {
        Task<T> GetAsync<T>(string uri, string authToken = "");
        Task<Models.EmployeeModel> GetLoginAsync<T>(string uri, string authToken = "");
        Task<T> PostAsync<T>(string uri, T data, string authToken = "");
        Task<string> PostStrAsync<T>(string uri, T data, string authToken = "");
        Task<string> PostDataAsync<T>(string uri, T data, string authToken = "");
        Task<string> PostMData<T>(string uri, T data, string authToken = "");
        Task<string> PostMultiPicAsync<T>(string uri, T data, string authToken = "");
        Task<T> PutAsync<T>(string uri, T data, string authToken = "");
        Task<string> PutStrAsync<T>(string uri, T data, string authToken = "");
        Task<string> PutDataAsync<T>(string uri, T data, string authToken = "");
        Task DeleteAsync(string uri, string authToken = "");
        Task<string> DeleteStrItemAsync(string uri, string authToken = "");
        Task<R> PostAsync<T, R>(string uri, T data, string authToken = "");
    }


    public class GenericRepository : IGenericRepository
    {
        private HttpClient CreateHttpClient(string authToken)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrEmpty(authToken))
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            }
            return httpClient;
        }


        public async Task<T> GetAsync<T>(string uri, string authToken = "")
        {

            try
            {
                HttpClient httpClient = CreateHttpClient(Utility.ServerUrl + uri);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () =>
                    await httpClient.GetAsync(Utility.ServerUrl + uri));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult =
                        await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                //throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);
                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var json1 = JsonConvert.DeserializeObject<T>(jsonResult);
                return json1;

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }


        public async Task<Models.EmployeeModel> GetLoginAsync<T>(string uri, string authToken = "")
        {

            try
            {
                HttpClient httpClient = CreateHttpClient(Utility.ServerUrl + uri);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () =>
                    await httpClient.GetAsync(Utility.ServerUrl + uri));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<EmployeeModel>(jsonResult);
                    return json;
                }
                else
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    Models.EmployeeModel obj = new Models.EmployeeModel()
                    {
                        EmployeeStatus = jsonResult
                    };
                    return obj;
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<T> PostAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Utility.ServerUrl + uri);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(Utility.ServerUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                var json2 = JsonConvert.DeserializeObject<T>(jsonResult);
                return json2;
                //throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }


        public async Task<string> PostStrAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Utility.ServerUrl + uri);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var cc = JsonConvert.SerializeObject(data);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(Utility.ServerUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    //var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return jsonResult;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return "";
                }

                return "";

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                return "";
            }
        }

     

        public async Task<string> PostDataAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Utility.ServerUrl + uri);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var cc = JsonConvert.SerializeObject(data);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                //var responseMessage= await httpClient.PostAsync(uri, content);

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(Utility.ServerUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<object>(jsonResult);
                    return json.ToString();
                }
                else
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    if (responseData.Contains("Not_Enough") == true)
                    {
                        return responseData.Replace("\"", "").Replace("Message", "").Replace(":", "").Replace("{", "").Replace("}", "").Trim();
                    }
                    else if (responseData.Contains("Already Exist For This Schedule Date#") == true)//Duplicate Schedule Dates for Estimate or Invoice
                    {
                        return responseData.Replace("\"", "").Replace("Message", "").Replace(":", "").Replace("{", "").Replace("}", "").Trim();
                    }
                    else if (responseMessage.ReasonPhrase == "Multiple Choices")
                    {
                        return "Multiple Choices";
                    }
                    else
                    {
                        return "api not responding";
                    }
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<string> PostMData<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Utility.ServerUrl + uri);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(Utility.ServerUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<object>(jsonResult);
                    return json.ToString();
                }
                else if (responseMessage.ReasonPhrase == "Multiple Choices")
                {
                    return "Multiple Choices";
                }
                else
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    if (responseData.Contains("Not_Enough") == true)
                    {
                        return responseData.Replace("\"", "").Replace("Message", "").Replace(":", "").Replace("{", "").Replace("}", "").Trim();
                    }
                    else if (responseData.Contains("already exists") == true)
                    {
                        return responseData.Replace("\"", "").Replace("Message", "").Replace(":", "").Replace("{", "").Replace("}", "").Trim();
                    }
                    else if (responseData.Contains("The_invoice_exists") == true)
                    {
                        return "This Invoice Already Exist From Estimate Or Schedule";
                    }
                    else
                    {
                        return "api not responding";
                    }
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<string> PostMultiPicAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Utility.ServerUrl + uri);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);


                var content = new StringContent(JsonConvert.SerializeObject(data, Formatting.None,
                        new JsonSerializerSettings()
                        {
                            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                        }));


                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(Utility.ServerUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json.ToString();
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    //throw new ServiceAuthenticationException(jsonResult);
                    var jsonResult2 = await responseMessage.Content.ReadAsStringAsync();
                    var json2 = JsonConvert.DeserializeObject<T>(jsonResult2);
                    return json2.ToString();
                }

                var jsonResult3 = await responseMessage.Content.ReadAsStringAsync();
                var json3 = JsonConvert.DeserializeObject<T>(jsonResult3);
                return json3.ToString();

                //throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<T> PutAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Utility.ServerUrl + uri);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string cc = JsonConvert.SerializeObject(data);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(Utility.ServerUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task<string> PutStrAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Utility.ServerUrl + uri);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string cc = JsonConvert.SerializeObject(data);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(Utility.ServerUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    //var json = JsonConvert.DeserializeObject<T>(jsonResult);
                    return jsonResult;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    return "";
                }

                jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                return jsonResult;

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                return "";
            }
        }

        public async Task<string> PutDataAsync<T>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Utility.ServerUrl + uri);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                string cc = JsonConvert.SerializeObject(data);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PutAsync(Utility.ServerUrl + uri, content, CancellationToken.None));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<object>(jsonResult);
                    return json.ToString();
                }
                else
                {
                    var responseData = await responseMessage.Content.ReadAsStringAsync();
                    if (responseData.Contains("Not_Enough") == true)
                    {
                        return responseData.Replace("\"", "").Replace("Message", "").Replace(":", "").Replace("{", "").Replace("}", "").Trim();
                    }
                    else if(responseData.Contains("Already Exist For This Schedule Date#") == true)//Duplicate Schedule Dates for Estimate or Invoice
                    {
                        return responseData.Replace("\"", "").Replace("Message", "").Replace(":", "").Replace("{", "").Replace("}", "").Trim();
                    }
                    else
                    {
                        return "api not responding";
                    }
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }

        public async Task DeleteAsync(string uri, string authToken = "")
        {
            HttpClient httpClient = CreateHttpClient(authToken);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            await httpClient.DeleteAsync(uri);
        }

        public async Task DeleteItemAsync(string uri, string authToken = "")
        {
            HttpClient httpClient = CreateHttpClient(authToken);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);
            await httpClient.DeleteAsync(uri);
        }

        public async Task<string> DeleteStrItemAsync(string uri, string authToken = "")
        {
            var httpLient = new HttpClient();
            HttpResponseMessage response = new HttpResponseMessage();
            httpLient.DefaultRequestHeaders.Accept.Clear();
            //httpLient.DefaultRequestHeaders.Add("authorization", "Basic ");
            httpLient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

            try
            {
                response = await httpLient.DeleteAsync(Utility.ServerUrl + uri);
                var responseData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {   
                    return responseData;
                }
                else if (responseData.Contains("Is Not Deleted") == true)
                {
                    return responseData.Replace("\"", "").Replace("Message", "").Replace(":", "").Replace("{", "").Replace("}", "").Trim();
                }
                else
                    return "api not responding";
            }
            catch (Exception ex)
            {
                return "api not responding";
            }
        }

        public async Task<TR> PostAsync<T, TR>(string uri, T data, string authToken = "")
        {
            try
            {
                HttpClient httpClient = CreateHttpClient(Utility.ServerUrl + uri);
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", authToken);

                var content = new StringContent(JsonConvert.SerializeObject(data));
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                string jsonResult = string.Empty;

                var responseMessage = await Policy
                    .Handle<WebException>(ex =>
                    {
                        Debug.WriteLine($"{ex.GetType().Name + " : " + ex.Message}");
                        return true;
                    })
                    .WaitAndRetryAsync
                    (
                        5,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt))
                    )
                    .ExecuteAsync(async () => await httpClient.PostAsync(Utility.ServerUrl + uri, content));

                if (responseMessage.IsSuccessStatusCode)
                {
                    jsonResult = await responseMessage.Content.ReadAsStringAsync().ConfigureAwait(false);
                    var json = JsonConvert.DeserializeObject<TR>(jsonResult);
                    return json;
                }

                if (responseMessage.StatusCode == HttpStatusCode.Forbidden ||
                    responseMessage.StatusCode == HttpStatusCode.Unauthorized)
                {
                    throw new ServiceAuthenticationException(jsonResult);
                }

                throw new HttpRequestExceptionEx(responseMessage.StatusCode, jsonResult);

            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.GetType().Name + " : " + e.Message}");
                throw;
            }
        }
    }
}
