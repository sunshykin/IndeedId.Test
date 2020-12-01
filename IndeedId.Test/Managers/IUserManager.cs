using System.Threading.Tasks;
using IndeedId.Test.Models;

namespace IndeedId.Test.Managers
{
	public interface IUserManager
	{
		public Task<User> GetUserById(int userId);
	}
}