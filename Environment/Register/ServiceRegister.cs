using payday_server.Processor;
using payday_server.Processor.Admin;
using payday_server.Processor.Payroll.Setup;
using payday_server.Repository;
using payday_server.Views.Shared;
using payday_server.Repository.Report;
using payday_server.Shared;
using payday_server.Repository.Admin;

namespace payday_server.Environment.Register
{
    public static class ServiceRegister
    {
        public static void ConfigureProcessor(this IServiceCollection services)
        {
            #region CONFIGURATION
                ConfigureConfigurationProcessor(services);
            #endregion

            #region SERVICE REPOSITORY
                ConfigureServiceRepository(services);
            #endregion

            #region DASHBOARD REPOSITORY
                DashboardServiceRepository(services);
            #endregion

            services.AddScoped<Algorithms>();


        }

        private static void ConfigureConfigurationProcessor(IServiceCollection services)
        {
            services.AddScoped<IProcessor<UserRoleBaseModel>, UserRoleProcessor>();
            services.AddScoped<IProcessor<UsersBaseModel>, UserProcessor>();
        }
        
        public static void ConfigureServiceRepository(IServiceCollection services){
            #region ServiceRepository
                services.AddScoped<IConfigurationServiceRepository, ConfigurationServiceRepository>();
                services.AddScoped<IAuthServiceRepository, AuthServiceRepository>();
                services.AddScoped<IReportServiceRepository, ReportServiceRepository>();
                services.AddScoped<IAdminServiceRepository, AdminServiceRepository>();

            #endregion
        }



        public static void DashboardServiceRepository(IServiceCollection services)
        {
            #region DashboardService
            services.AddScoped<IDashboardServiceRepository, DashboardServiceRepository>();
            #endregion
        }            

    }
}