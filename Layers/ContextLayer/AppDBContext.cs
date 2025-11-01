using Microsoft.EntityFrameworkCore;
using ESA.Model;
using ESA.Model.Report;
using ESA.Model.Attendance;


namespace ESA.Layers.ContextLayer
{
    public class AppDBContext : DbContext
    {

        private readonly IHttpContextAccessor _httpContextAccessor;
        public AppDBContext(DbContextOptions<AppDBContext> option) : base(option)
        {

        }

        public AppDBContext(DbContextOptions<AppDBContext> options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        #region CONFIGURATION
        public DbSet<User> Users { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UsersPermissions> UsersPermissions { get; set; }

        #endregion

        #region REPORT 

        public DbSet<PayrollReports> PayrollReport { get; set; }
        public DbSet<PayrollMods> PayrollMod { get; set; }

        #endregion

        #region Configurations

        //public DbSet<Department> Departments { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes()) //removes aspnet prefix from all identity tables
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }

            modelBuilder.Entity<UserEventLog>()
               .HasKey(u => u.Id);

            modelBuilder.Entity<UserEventLog>() //auto generate Id when entries are added
                .Property(u => u.Id)
                .ValueGeneratedOnAdd();
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys())) //Restrict delete behavior for all relationships
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
        }
    }    
}