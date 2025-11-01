using ESA.Processor;
using ESA.Processor.Admin;
using ESA.Processor.Payroll.Setup;
using ESA.Repository;
using ESA.Views.Shared;
using ESA.Repository.Report;
using ESA.Shared;
using ESA.Repository.Admin;
using ESA.Views.StudentMaster;

namespace ESA.Environment.Register
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

            #region StudentMaster

            ConfigurateStudentMasterProcessor(services);
            #endregion


        }

        private static void ConfigurateStudentMasterProcessor(IServiceCollection services)
        {
            services.AddScoped<IProcessor<CourseBaseModel>, CourseProcessor>();
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