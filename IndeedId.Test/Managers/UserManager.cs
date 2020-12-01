using System.Threading.Tasks;
using IndeedId.Test.Database;
using IndeedId.Test.Models;
using Microsoft.EntityFrameworkCore;

namespace IndeedId.Test.Managers
{
	public class UserManager : IUserManager
	{
		private readonly IAppContext context;

		public UserManager(IAppContext context)
		{
			this.context = context;
		}

		public async Task<User> GetUserById(int userId)
		{
			return await context.Users
				.AsNoTracking()
				.FirstOrDefaultAsync(u => u.Id == userId);
		}
	}
}