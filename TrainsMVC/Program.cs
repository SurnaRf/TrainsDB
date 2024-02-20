using BusinessLayer;
using DataLayer;
using Microsoft.EntityFrameworkCore;
using ServiceLayer;

namespace TrainsMVC
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            // Connection, Location, Locomotive, TrainCar, TrainComposition
            string connectionString = builder.Configuration["ConnectionString"];
            builder.Services.AddDbContext<TrainDbContext>(options =>
            {
                options.UseSqlServer(connectionString); 
            }, ServiceLifetime.Scoped);

            builder.Services.AddScoped<IDb<Connection, int>, ConnectionContext>();
            builder.Services.AddScoped<IDb<Location, int>, LocationContext>();
            builder.Services.AddScoped<IDb<Locomotive, int>, LocomotiveContext>();
            builder.Services.AddScoped<IDb<TrainCar, int>, TrainCarContext>();
            builder.Services.AddScoped<IDb<TrainComposition, int>, TrainCompositionContext>();

            builder.Services.AddScoped<GenericManager<Connection, int>,       GenericManager<Connection, int>>();
            builder.Services.AddScoped<GenericManager<Location, int>,         GenericManager<Location, int>>();
            builder.Services.AddScoped<GenericManager<Locomotive, int>,       GenericManager<Locomotive, int>>();
            builder.Services.AddScoped<GenericManager<TrainCar, int>,         GenericManager<TrainCar, int>>();
            builder.Services.AddScoped<GenericManager<TrainComposition, int>, GenericManager<TrainComposition, int>>();

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
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}