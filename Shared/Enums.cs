using payday_server.Model;

namespace payday_server.Shared
{
    public class Enums {
        public enum ModuleClassName
        {
            UserRole,
            User,

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