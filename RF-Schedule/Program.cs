using System;
using System.Windows.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RFScheduling.Infrastructure;


namespace RF_Schedule {
    internal static class Program 
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() 
        {
            ApplicationConfiguration.Initialize(); //新版Winform幫我初始化

            DevExpress.Skins.SkinManager.EnableFormSkins();
            DevExpress.UserSkins.BonusSkins.Register();

            var host = CreateHostBuilder().Build(); //host 是「裝了所有服務的總容器」；CreateHostBuilder()：自己寫的方法，用來告訴 host 「我要註冊哪些服務」

            using (var scope = host.Services.CreateScope())
            { 
                var services = scope.ServiceProvider;
                Application.Run(services.GetRequiredService<MainForm>());
            }
        }

        static IHostBuilder CreateHostBuilder()
        {
            return Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) =>
                {
                    var connectionString =
                    "Server=(localdb)\\MSSQLLocalDB;Database=RFScheduling;Trusted_Connection=True;TrustServerCertificate=True;";

                    services.AddDbContext<AppDbContext>(options =>
                        options.UseSqlServer(connectionString));

                    services.AddTransient<MainForm>(); //Transient：每次要就 new 新的
                });
        }
    }
}