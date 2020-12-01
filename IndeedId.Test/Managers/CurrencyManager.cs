using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace IndeedId.Test.Managers
{
	public class CurrencyManager : ICurrencyManager
	{
		private readonly RestClient client;

		private const string BaseParameter = "base";

		public CurrencyManager()
		{
			client = new RestClient(Constants.ExchangeApiUrl);
		}


		public async Task<decimal?> GetCurrencyRate(string currency, string @base)
		{
			var request = new RestRequest(Method.GET);
			request.AddParameter(BaseParameter, @base);

			var response = await client.ExecuteAsync(request);

			var model = JsonConvert.DeserializeObject<Model>(response.Content);

			var rate = model.Rates[currency];

			if (rate == null)
			{
				return null;
			}

			return Convert.ToDecimal(rate.Value);
		}
	}

	public class Model
	{
		public string Base { get; set; }
		public DateTime Date { get; set; }
		public dynamic Rates { get; set; }
	}
}