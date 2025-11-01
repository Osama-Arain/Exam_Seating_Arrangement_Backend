using ESA.Layers.ContextLayer;
using ESA.Model;

public static class DataSeeder
{
    public static async void SeedData(AppDBContext context)
    {
        
        if (!context.Users.Any())
        {
            //var FoxitBranch = new Tenant{ Id = Guid.NewGuid(),Code = 1, Name = "Foxit Branch", ShortName = "FOX" , Active = true, Action = "A",UserIdInsert = Guid.Parse("89a9d9fe-df21-4c01-a198-2aca4b12299f"),InsertDate = DateTime.Now, Type = "S"};
            
            //// Add Default Roles
            //var SuperAdminRole = new UserRole{ Id = Guid.NewGuid(),Code = 1, Role = "SuperAdmin", Active = true, Action = "A",UserIdInsert = Guid.Parse("89a9d9fe-df21-4c01-a198-2aca4b12299f"),InsertDate = DateTime.Now,Type = "S"};
            //var AdminRole = new UserRole{ Id = Guid.NewGuid(),Code = 2, Role = "Admin", Active = true, Action = "A",UserIdInsert = Guid.Parse("89a9d9fe-df21-4c01-a198-2aca4b12299f"),InsertDate = DateTime.Now,Type = "S"};
            //var EmployeeRole = new UserRole{ Id = Guid.NewGuid(),Code = 3, Role = "Employee", Active = true, Action = "A",UserIdInsert = Guid.Parse("89a9d9fe-df21-4c01-a198-2aca4b12299f"),InsertDate = DateTime.Now,Type = "S"};

            //// Add the default user
            //var defaultUser = new User
            //{
            //    Id = Guid.Parse("89a9d9fe-df21-4c01-a198-2aca4b12299f"),
            //    Code = 1,
            //    FirstName = "Noor",
            //    LastName = "Ahmed",
            //    NormalizedName = "Noor Ahmed",
            //    Contact = "03152289013",
            //    TenantId = FoxitBranch.Id,
            //    CNIC = "4220166397604",
            //    Email = "noor@foxit.pk",
            //    HashPassword = "17n3BeZaYx2aI5YYBxACvg==",
            //    RoleId = SuperAdminRole.Id,
            //    Active = true,
            //    Action = "A",
            //    Type = "S",
            //    TenantsCheck = true
            //};// Add the default user
            
            //context.Users.Add(defaultUser);

            //// Add your data seeding code here for menu modules
            //var ConfigurationModule = new MenuModule { Id = Guid.NewGuid(), Code = 1, Name = "Configuration", Icon = "fas fa-fw fa-user-cog", Type = "S", Active = true, Action = "A", TenantId = FoxitBranch.Id, UserIdInsert = defaultUser.Id, InsertDate = DateTime.Now };


            ////var SetupMenuCategory = new MenuCategory { Id = Guid.NewGuid(), Code = 1, Name = "Setups", Icon = "fab fa-fw fa-wpforms", Type = "S", Active = true, Action = "A", TenantId = FoxitBranch.Id, InsertDate = DateTime.Now, UserIdInsert = defaultUser.Id };
            ////context.MenuCategories.Add(SetupMenuCategory);

            //var menuSubCategory1 = new MenuSubCategory { Id = Guid.NewGuid(), Code = 1, MenuModuleId = ConfigurationModule.Id, Name = "MenuModule", Alias = "Menu Module", Icon = "fab fa-fw fa-wpforms", Type = "S", Active = true, View = true, Action = "A", InsertDate = DateTime.Now, UserIdInsert = defaultUser.Id};
            //var menuSubCategory2 = new MenuSubCategory { Id = Guid.NewGuid(), Code = 2, MenuModuleId = ConfigurationModule.Id, Name = "MenuCategory", Alias = "Menu Category", Icon = "fab fa-fw fa-wpforms", Type = "S", Active = true, View = true, Action = "A", InsertDate = DateTime.Now, UserIdInsert = defaultUser.Id};
            //var menuSubCategory3 = new MenuSubCategory { Id = Guid.NewGuid(), Code = 3, MenuModuleId = ConfigurationModule.Id, Name = "MenuSubCategory", Alias = "Menu Sub-Category", Icon = "fab fa-fw fa-wpforms", Type = "S", Active = true, View = true, Action = "A", InsertDate = DateTime.Now, UserIdInsert = defaultUser.Id};
            //var menuSubCategory4 = new MenuSubCategory { Id = Guid.NewGuid(), Code = 4, MenuModuleId = ConfigurationModule.Id, Name = "Branch", Alias = "Tenant", Icon = "fab fa-fw fa-wpforms", Type = "S", Active = true, View = true, Action = "A", InsertDate = DateTime.Now, UserIdInsert = defaultUser.Id};
            //var menuSubCategory5 = new MenuSubCategory { Id = Guid.NewGuid(), Code = 5, MenuModuleId = ConfigurationModule.Id, Name = "UserRole", Alias = "UserRole", Icon = "fab fa-fw fa-wpforms", Type = "S", Active = true, View = true, Action = "A", InsertDate = DateTime.Now, UserIdInsert = defaultUser.Id};
            //var menuSubCategory6 = new MenuSubCategory { Id = Guid.NewGuid(), Code = 6, MenuModuleId = ConfigurationModule.Id, Name = "User", Alias = "User", Icon = "fab fa-fw fa-wpforms", Type = "S", Active = true, View = true, Action = "A", InsertDate = DateTime.Now, UserIdInsert = defaultUser.Id};

            //var userPermissionForMenu1 = new UsersPermissions
            //{
            //    Id = Guid.NewGuid(), 
            //    Show_Permission = true,
            //    Insert_Permission = true,
            //    Update_Permission = true,
            //    Delete_Permission = true,
            //    Print_Permission = true,
            //    Check_Permission = true,
            //    Approve_Permission = true,
            //    RoleId = SuperAdminRole.Id,
            //    MenuId = menuSubCategory1.Id,
            //    UserId = defaultUser.Id,
            //    Type = "S",
            //    Active = true,
            //    Action = "A",
            //    InsertDate = DateTime.Now,
            //    UserIdInsert = defaultUser.Id,
            //};

            //var userPermissionForMenu2 = new UsersPermissions
            //{
            //    Id = Guid.NewGuid(), 
            //    Show_Permission = true,
            //    Insert_Permission = true,
            //    Update_Permission = true,
            //    Delete_Permission = true,
            //    Print_Permission = true,
            //    Check_Permission = true,
            //    Approve_Permission = true,
            //    RoleId = SuperAdminRole.Id,
            //    MenuId = menuSubCategory2.Id,
            //    UserId = defaultUser.Id,
            //    Type = "S",
            //    Active = true,
            //    Action = "A",
            //    InsertDate = DateTime.Now,
            //    UserIdInsert = defaultUser.Id,
            //};

            //var userPermissionForMenu3 = new UsersPermissions
            //{
            //    Id = Guid.NewGuid(), 
            //    Show_Permission = true,
            //    Insert_Permission = true,
            //    Update_Permission = true,
            //    Delete_Permission = true,
            //    Print_Permission = true,
            //    Check_Permission = true,
            //    Approve_Permission = true,
            //    RoleId = SuperAdminRole.Id,
            //    MenuId = menuSubCategory3.Id,
            //    UserId = defaultUser.Id,
            //    Type = "S",
            //    Active = true,
            //    Action = "A",
            //    InsertDate = DateTime.Now,
            //    UserIdInsert = defaultUser.Id,
            //};

            //var userPermissionForMenu4 = new UsersPermissions
            //{
            //    Id = Guid.NewGuid(), 
            //    Show_Permission = true,
            //    Insert_Permission = true,
            //    Update_Permission = true,
            //    Delete_Permission = true,
            //    Print_Permission = true,
            //    Check_Permission = true,
            //    Approve_Permission = true,
            //    RoleId = SuperAdminRole.Id,
            //    MenuId = menuSubCategory4.Id,
            //    UserId = defaultUser.Id,
            //    Type = "S",
            //    Active = true,
            //    Action = "A",
            //    InsertDate = DateTime.Now,
            //    UserIdInsert = defaultUser.Id,
            //};

            //var userPermissionForMenu5 = new UsersPermissions
            //{
            //    Id = Guid.NewGuid(), 
            //    Show_Permission = true,
            //    Insert_Permission = true,
            //    Update_Permission = true,
            //    Delete_Permission = true,
            //    Print_Permission = true,
            //    Check_Permission = true,
            //    Approve_Permission = true,
            //    RoleId = SuperAdminRole.Id,
            //    MenuId = menuSubCategory5.Id,
            //    UserId = defaultUser.Id,
            //    Type = "S",
            //    Active = true,
            //    Action = "A",
            //    InsertDate = DateTime.Now,
            //    UserIdInsert = defaultUser.Id,
            //};

            //var userPermissionForMenu6 = new UsersPermissions
            //{
            //    Id = Guid.NewGuid(), 
            //    Show_Permission = true,
            //    Insert_Permission = true,
            //    Update_Permission = true,
            //    Delete_Permission = true,
            //    Print_Permission = true,
            //    Check_Permission = true,
            //    Approve_Permission = true,
            //    RoleId = SuperAdminRole.Id,
            //    MenuId = menuSubCategory6.Id,
            //    UserId = defaultUser.Id,
            //    Type = "S",
            //    Active = true,
            //    Action = "A",
            //    InsertDate = DateTime.Now,
            //    UserIdInsert = defaultUser.Id,
            //};


            //context.SaveChanges();

        }   

          
    }
}