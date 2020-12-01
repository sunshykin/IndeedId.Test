using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using IndeedId.Test.Models;
using Microsoft.EntityFrameworkCore;

namespace IndeedId.Test.Database
{
	public interface IAppContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<UserWallet> Wallets { get; set; }
		Task<int> SaveChangesAsync();
	}

	public class AppContext : DbContext, IAppContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<UserWallet> Wallets { get; set; }
		public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

		public AppContext(DbContextOptions options) : base(options)
		{
			SeedDatabase();
		}

		private void SeedDatabase()
		{
			if (Users.Any())
				return;

			Users.AddRange(new [] {
				new User { Id = 1, Name = "User1" },
				new User { Id = 2, Name = "User2" },
				new User { Id = 3, Name = "User3" },
			});

			Wallets.AddRange(new []
			{
				new UserWallet { UserId = 1, Currency = "RUB", Amount = 10_000 },
				new UserWallet { UserId = 1, Currency = "USD", Amount = 100 },
				new UserWallet { UserId = 1, Currency = "EUR", Amount = 200 },

				new UserWallet { UserId = 2, Currency = "IDR", Amount = 300_000 },

				new UserWallet { UserId = 3, Currency = "RUB", Amount = 1_500_000 },
				new UserWallet { UserId = 3, Currency = "ZAR", Amount = 370_000_000 },
			});

			SaveChanges();
		}
	}
}