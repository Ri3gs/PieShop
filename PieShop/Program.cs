using System;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using PieShop.Auth;
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

			using (IServiceScope scope = webHost.Services.CreateScope())
			{
				IServiceProvider serviceProvider = scope.ServiceProvider;
				try
				{
					UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

					if (userManager.FindByNameAsync("admin").Result == null)
					{
						var adminApplicationUser = new ApplicationUser("admin");
						IdentityResult identityResult = userManager.CreateAsync(adminApplicationUser, "12345").Result;

						if (!identityResult.Succeeded)
						{
							throw new Exception("admin cannot be inserted");
						}
					}

					RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

					if (roleManager.FindByNameAsync("Administrators").Result == null)
					{
						IdentityResult identityResult = roleManager.CreateAsync(new IdentityRole("Administrators")).Result;

						if (!identityResult.Succeeded)
						{
							throw new Exception("admin role cannot be inserted");
						}
					}

					ApplicationUser admin = userManager.FindByNameAsync("admin").Result;
					if (!userManager.IsInRoleAsync(admin, "Administrators").Result)
					{
						IdentityResult identityResult = userManager.AddToRoleAsync(admin, "Administrators").Result;

						if (!identityResult.Succeeded)
						{
							throw new Exception("admin user cannot be added to admin role");
						}
					}
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
