using System.Threading.Tasks;
using IndeedId.Test.Models;

namespace IndeedId.Test.Managers
{
	public interface IWalletManager
	{
		public Task<UserWallet[]> GetUserWallets(int userId);

		public Task<UserWallet> GetUserWallet(int userId, string currency);

		public Task<UserWallet> CreateUserWallet(int userId, string currency, decimal amount);

		public Task UpdateUserWallet(UserWallet wallet);

		public Task UpdateUserWallets(UserWallet walletOne, UserWallet walletTwo);
	}
}