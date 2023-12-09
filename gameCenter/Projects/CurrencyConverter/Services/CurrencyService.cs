using gameCenter.Projects.CurrencyConverter.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace gameCenter.Projects.CurrencyConverter.Services
{
    internal class CurrencyService
    {
        private const string BaseApiEndPoint = "http://api.exchangeratesapi.io/latest";

        private const string ApiKey = "620f03531b3007552225473b2bb6aa69";

        private HttpClient Http_Client = new HttpClient();

        public async Task<Dictionary<string, double>> GetExchangeRatesAsync()

        {

            string requestUrl = $"{BaseApiEndPoint}?access_key={ApiKey}";

            string response = await Http_Client.GetStringAsync(requestUrl);

            JsonSerializerOptions options = new JsonSerializerOptions

            {

                PropertyNameCaseInsensitive = true,

            };

            ExchangeResponse exchangeData = JsonSerializer.Deserialize<ExchangeResponse>(response, options);

            if (exchangeData == null || exchangeData.Rates == null)

                throw new InvalidOperationException("Failed to fetch exchange rates.");


            return exchangeData.Rates;

        }
    }
}
