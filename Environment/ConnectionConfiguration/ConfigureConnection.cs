using System;
using System.Runtime.InteropServices;
using payday_server.Layers.ContextLayer;
using Microsoft.EntityFrameworkCore;

namespace payday_server.Environment.Register{
    public static class ConfigureConnection
    {
        public static void ConnectionConfigure(this IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false)
                .Build();

            var connection = String.Empty;
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                connection = configuration.GetConnectionString("Local").ToString();
            }
            if (connection != string.Empty)
            {
                services.AddDbContext<AppDBContext>(Options => Options.UseSqlServer(connection));

                using (var scope = services.BuildServiceProvider().CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<AppDBContext>();
                    DataSeeder.SeedData(context);
                }
            }
        }
    }
}