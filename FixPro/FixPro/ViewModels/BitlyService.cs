using System;
using System.Net.Http;
using System.Threading.Tasks;

public class BitlyService
{
    private const string AccessToken = "3e935c3f2589c6fae349892b3551c3cb213a7a7f";
    private const string BitlyApiEndpoint = "https://api-ssl.bitly.com/v4/shorten";

    public async Task<string> ShortenUrl(string longUrl)
    {
        try
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);

            var requestData = new
            {
                long_url = longUrl
            };

            var content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(requestData), System.Text.Encoding.UTF8, "application/json");

            var response = await client.PostAsync(BitlyApiEndpoint, content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var shortenedUrl = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(result);
                return shortenedUrl.link;
            }
            else
            {
                // Handle error if the request fails
                return null;
            }
        }
        catch (Exception ex)
        {
            // Handle exception
            Console.WriteLine($"Error: {ex.Message}");
            return null;
        }
    }
}
