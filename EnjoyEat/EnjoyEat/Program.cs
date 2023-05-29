using EnjoyEat.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
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
			builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
			   .AddCookie(opt =>
			   {
				   opt.LoginPath = "/MemberLogin/login";
				   opt.LogoutPath = "/Home/Index";
				   opt.AccessDeniedPath = "//AccessDenied";
				   opt.ExpireTimeSpan = TimeSpan.FromMinutes(30);
			   });
			var services = builder.Services;
			var congiuguration = builder.Configuration;

			builder.Services.AddAuthentication().AddFacebook(facebookOptions =>
			{
				facebookOptions.AppId = "918040199252221";
				facebookOptions.AppSecret = "d7c585ee57ae96fa1d9d3e130ecf6436";
			});
			builder.Services.AddSession(options =>
			{
				options.Cookie.Name = "ºô¯¸¦WºÙ.Session";
				options.IdleTimeout= TimeSpan.FromSeconds(30);
				options.Cookie.IsEssential = true;
				options.Cookie.HttpOnly = true;
				options.Cookie.SecurePolicy = CookieSecurePolicy.Always;

			});
			//builder.Services.AddAuthentication().AddGoogle(GoogleOopitons =>
			//{
			//	GoogleOptions.ClientId = configuration["Authentication:Google:ClientId"];
   //             GoogleOptions.ClientSecret = configuration["Authenticaiton:Google:ClientSecret"];
			//});
            var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseSession();
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			
			app.UseAuthentication();
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