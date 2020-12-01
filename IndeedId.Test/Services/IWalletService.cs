using System.Threading.Tasks;
using IndeedId.Test.Dto;

namespace IndeedId.Test.Services
{
	public interface IWalletService
	{
		public Task<FinancesViewModel> GetUserFinances(int userId);

		public Task<OperationResult> Deposit(int userId, decimal amount, string currency);

		public Task<OperationResult> Withdraw(int userId, decimal amount, string currency);

		public Task<OperationResult> Convert(int userId, decimal amount, string fromCurrency, string toCurrency);
	}
}