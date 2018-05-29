using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PieShop.Models;

namespace PieShop
{
	public class Program
	{
		public static void Main(string[] args)
		{
			IWebHost webHost = BuildWebHost(args);

			using (IServiceScope scope = webHost.Services.CreateScope())
			{
				IServiceProvider serviceProvider = scope.ServiceProvider;
				try
				{
					AppDbContext appDbContext = serviceProvider.GetRequiredService<AppDbContext>();
					DbInitializer.Seed(appDbContext);
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
					throw;
				}
			}

			webHost.Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
				.UseStartup<Startup>()
				.Build();
	}
}
