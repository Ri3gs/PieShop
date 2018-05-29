using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PieShop.Models;

namespace PieShop
{
	public class Startup
	{
		public IConfiguration Configuration { get; }

		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc();
			services.AddDbContext<AppDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddIdentity<IdentityUser, IdentityRole>(options =>
				{
					options.Password.RequireDigit = false;
					options.Password.RequireLowercase = false;
					options.Password.RequireNonAlphanumeric = false;
					options.Password.RequireUppercase = false;
					options.Password.RequiredLength = 5;
				})
				.AddEntityFrameworkStores<AppDbContext>();

			services.AddTransient<IPieRepository, PieRepository>();
			services.AddTransient<ICategoryRepository, CategoryRepository>();
			services.AddTransient<IFeedbackRepository, FeedbackRepository>();
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			services.AddTransient<IOrderRepository, OrderRepository>();
			services.AddScoped<ShoppingCart>(ShoppingCart.GetCart);
			services.AddMemoryCache();
			services.AddSession();
		}

		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage()
					.UseStatusCodePages();
			}
			else
			{
				app.UseExceptionHandler("/AppException");
			}

			app.UseStaticFiles()
				.UseAuthentication()
				.UseSession()
				.UseMvc(routes =>
				{
					routes.MapRoute(
						name: "categoryFilter",
						template: "Pie/{action}/{category?}",
						defaults: new {Controller = "Pie", action = "List"});

					routes.MapRoute(
						name: "default",
						template: "{controller=Home}/{action=Index}/{id?}");
				});
		}
	}
}
