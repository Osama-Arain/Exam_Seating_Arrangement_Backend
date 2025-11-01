using ESA.Model;

namespace ESA.Shared
{
    public class Enums {
        public enum ModuleClassName
        {
            UserRole,
            User,
            Course,
            Department,
            UserEventLogs,
          
        }

        public enum UserRoles { 
            Admin,
            Student,
            Invigilator
        }

        public enum Operations
        {
            A,
            D,
            E,
            U,
            S,
        }


        public enum Misc
        {
            UserId,
            UserName,
            Email,
            Key

        }
    }
}