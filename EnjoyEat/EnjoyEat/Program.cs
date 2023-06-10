using EnjoyEat.Areas.OrderForHere.Models;
using EnjoyEat.Models;
using EnjoyEat.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
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
            builder.Services.AddDbContext<SQL8005site4nownetContext>(options =>
                options.UseSqlServer(EnjoyEatConnectionString));
            builder.Services.AddControllersWithViews();
            builder.Services.AddLogging();
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.Cookie.Name = "Session";
                options.IdleTimeout = TimeSpan.FromHours(6); // 設定 Session 閒置超時時間
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
            {
                // Add cookie authentication options here
            })
            .AddFacebook(facebookOptions =>
            {
                facebookOptions.AppId = builder.Configuration["Authentication:Facebook:AppId"];
                facebookOptions.AppSecret = builder.Configuration["Authentication:Facebook:AppSecret"];
            });
            builder.Services.AddTransient<AesService>();
            builder.Services.AddTransient<HashService>();
            builder.Services.AddTransient<EncryptService>();
            builder.Services.AddTransient<IPaymentService, PaymentService>();

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

            app.UseSession();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "Area",
                pattern: "{area}/{controller}/{action}/{id?}",
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
