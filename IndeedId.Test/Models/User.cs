using System.Collections.Generic;

namespace IndeedId.Test.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Name { get; set; }

		public virtual List<UserWallet> Wallets { get; set; }
	}
}