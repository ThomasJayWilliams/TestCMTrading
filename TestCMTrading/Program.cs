using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace TestCMTrading
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var host = CreateHostBuilder(args).Build();

			using var dbContext = new DataContext();

			dbContext.Database.EnsureCreated();

			host.Run();
		}

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
				});
	}
}
