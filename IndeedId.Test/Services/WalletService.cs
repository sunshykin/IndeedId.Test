using System;
using System.Linq;
using System.Threading.Tasks;
using IndeedId.Test.Dto;
using IndeedId.Test.Managers;

namespace IndeedId.Test.Services
{
	public class WalletService : IWalletService
	{
		private readonly IWalletManager walletManager;
		private readonly ICurrencyManager currencyManager;
		private readonly IUserManager userManager;

		public WalletService(IWalletManager walletManager, ICurrencyManager currencyManager, IUserManager userManager)
		{
			this.walletManager = walletManager;
			this.currencyManager = currencyManager;
			this.userManager = userManager;
		}

		public async Task<FinancesViewModel> GetUserFinances(int userId)
		{
			var user = await userManager.GetUserById(userId);
			var wallets = await walletManager.GetUserWallets(userId);

			return new FinancesViewModel
			{
				UserName = user.Name,
				Wallets = wallets
					.Select(w => new WalletViewModel
						{
							Amount = w.Amount,
							Currency = w.Currency
						}
					)
					.ToArray()
			};
		} 

		public async Task<OperationResult> Deposit(int userId, decimal amount, string currency)
		{
			var wallet = await walletManager.GetUserWallet(userId, currency);

			if (wallet == null)
			{
				await walletManager.CreateUserWallet(userId, currency, amount);
			}
			else
			{
				wallet.Amount += amount;
				await walletManager.UpdateUserWallet(wallet);
			}

			return new OperationResult(true, String.Empty);
		}

		public async Task<OperationResult> Withdraw(int userId, decimal amount, string currency)
		{
			var wallet = await walletManager.GetUserWallet(userId, currency);

			if (wallet == null)
			{
				return new OperationResult(false, Constants.WalletNotExistsMsg);
			}

			if (wallet.Amount < amount)
			{
				return new OperationResult(false, Constants.WalletHasNotEnoughMoneyMsg);
			}

			wallet.Amount -= amount;
			await walletManager.UpdateUserWallet(wallet);

			return new OperationResult(true, String.Empty);
		}

		public async Task<OperationResult> Convert(int userId, decimal amount, string fromCurrency,
			string toCurrency)
		{
			var fromWallet = await walletManager.GetUserWallet(userId, fromCurrency);

			if (fromWallet == null)
			{
				return new OperationResult(false, Constants.WalletNotExistsMsg);
			}

			if (fromWallet.Amount < amount)
			{
				return new OperationResult(false, Constants.WalletHasNotEnoughMoneyMsg);
			}

			var toWallet = await walletManager.GetUserWallet(userId, toCurrency) 
				?? await walletManager.CreateUserWallet(userId, toCurrency, 0);

			var currencyRate = await currencyManager.GetCurrencyRate(fromCurrency, toCurrency);

			if (!currencyRate.HasValue)
			{
				return new OperationResult(false, Constants.CurrencyNotSupported);
			}

			fromWallet.Amount -= amount;
			toWallet.Amount += Decimal.Round(amount / currencyRate.Value, 2, MidpointRounding.ToNegativeInfinity);

			await walletManager.UpdateUserWallets(fromWallet, toWallet);

			return new OperationResult(true, String.Empty);
		}
	}
}