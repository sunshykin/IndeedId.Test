using System.ComponentModel.DataAnnotations.Schema;

namespace IndeedId.Test.Models
{
	public class UserWallet
	{
		public int Id { get; set; }
		public string Currency { get; set; }
		public decimal Amount { get; set; }


		[ForeignKey("UserId")]
		public int UserId { get; set; }
	}
}