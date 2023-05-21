using EnjoyEat.Models;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat
{
    public class Program
	{
		public static void Main(string[] args)

		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to DI the container.
			var EnjoyEatConnectionString = builder.Configuration.GetConnectionString("EnjoyEat");
			builder.Services.AddDbContext<db_a989fe_thm101team6Context>(options =>
			options.UseSqlServer(EnjoyEatConnectionString));

			builder.Services.AddControllersWithViews();


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "Area",
				pattern: "{area:exists}/{controller}/{action}/{id?}",
				defaults: new { controller = "Home", action = "Index" }
			);

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller}/{action}/{id?}",
				defaults: new { controller = "Home", action = "Index" }
			);

			app.MapControllerRoute(
				name: "Index",
				pattern: "{controller}/{action}/{id?}",
				defaults: new { controller = "Home", action = "Index" }
			);

            app.Run();
		}
	}
}