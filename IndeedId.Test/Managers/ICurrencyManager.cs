using System.Threading.Tasks;

namespace IndeedId.Test.Managers
{
	public interface ICurrencyManager
	{
		public Task<decimal?> GetCurrencyRate(string currency, string @base);
	}
}