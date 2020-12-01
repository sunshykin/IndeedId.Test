using System.Linq;
using System.Threading.Tasks;
using IndeedId.Test.Database;
using IndeedId.Test.Models;
using Microsoft.EntityFrameworkCore;

namespace IndeedId.Test.Managers
{
	public class WalletManager : IWalletManager
	{
		private readonly IAppContext context;

		public WalletManager(IAppContext context)
		{
			this.context = context;
		}

		public async Task<UserWallet[]> GetUserWallets(int userId)
		{
			return await context.Wallets
				.AsNoTracking()
				.Where(w => w.UserId == userId)
				.ToArrayAsync();
		}

		public async Task<UserWallet> GetUserWallet(int userId, string currency)
		{
			return await context.Wallets
				.AsNoTracking()
				.FirstOrDefaultAsync(w => w.UserId == userId && w.Currency == currency);
		}

		public async Task<UserWallet> CreateUserWallet(int userId, string currency, decimal amount)
		{
			var newWallet = new UserWallet {UserId = userId, Amount = amount, Currency = currency};

			await context.Wallets.AddAsync(newWallet);
			await context.SaveChangesAsync();

			return newWallet;
		}

		public async Task UpdateUserWallet(UserWallet wallet)
		{
			context.Wallets.Update(wallet);
			await context.SaveChangesAsync();
		}

		public async Task UpdateUserWallets(UserWallet walletOne, UserWallet walletTwo)
		{
			context.Wallets.Update(walletOne);
			context.Wallets.Update(walletTwo);
			await context.SaveChangesAsync();
		}
	}
}