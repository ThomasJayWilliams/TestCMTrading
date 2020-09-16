using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using RestSharp.Authenticators;
using TestCMTrading.Infrastructure.Interfaces;
using TestCMTrading.Services;

namespace TestCMTrading
{
	public class Startup
	{
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews();
			services.AddTransient<IIdentityService, IdentityService>();
			services.AddTransient<IRestClient, RestClient>(factory =>
			{
				var config = factory.GetService<IConfiguration>();
				var apiSection = config.GetSection(Constants.API_CFG_SECTION);
				var apiUrl = apiSection.GetValue<string>(Constants.API_CFG_URL);

				return new RestClient(apiSection.GetValue<string>(Constants.API_CFG_URL))
				{
					Authenticator = new HttpBasicAuthenticator(
						apiSection.GetValue<string>(Constants.API_CFG_USERNAME),
						apiSection.GetValue<string>(Constants.API_CFG_PASSWORD))
				};
			});
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();

			else
				app.UseExceptionHandler("/Home/Error");

			app.UseHttpsRedirection();
			app.UseStaticFiles();
			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}");
			});
		}
	}
}
