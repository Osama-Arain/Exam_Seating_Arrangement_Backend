using payday_server.Layers.ContextLayer;
using payday_server.Manager;
using payday_server.Manager.Configuration;



namespace payday_server.Shared
{
    public static class Builder
    {
        public static IManager? MakeManagerClass(Enums.ModuleClassName ClassName, AppDBContext _context)
        {
            switch (ClassName)
            {

                case Enums.ModuleClassName.UserRole:
                    {
                        return new UserRoleManager(_context);
                    }
                case Enums.ModuleClassName.User:
                    {
                        return new UserManager(_context);
                    }

                default:
                    return null;
            }
        }
    }
}