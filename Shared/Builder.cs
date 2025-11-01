using ESA.Layers.ContextLayer;
using ESA.Manager;
using ESA.Manager.Configuration;



namespace ESA.Shared
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
                case Enums.ModuleClassName.Course:
                    {
                        return new CourseManager(_context);
                    }

                default:
                    return null;
            }
        }
    }
}