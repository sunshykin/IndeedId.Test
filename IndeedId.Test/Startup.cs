using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using IndeedId.Test.Managers;
using IndeedId.Test.Services;
using Microsoft.EntityFrameworkCore;

namespace IndeedId.Test
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddDbContext<Database.AppContext>(opt => opt.UseInMemoryDatabase("Wallet"));
			services.AddScoped<Database.IAppContext, Database.AppContext>();

			services.AddScoped<IWalletManager, WalletManager>();
			services.AddScoped<ICurrencyManager, CurrencyManager>();
			services.AddScoped<IUserManager, UserManager>();

			services.AddScoped<IWalletService, WalletService>();

			services.AddControllers();
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
