using ESA.Layers.ContextLayer;


namespace ESA.Repository.Admin
{
    public interface IAdminServiceRepository
    {
        //LOV's

    }

    public class AdminServiceRepository : IAdminServiceRepository
    {
        private readonly AppDBContext _context;
        public AdminServiceRepository(AppDBContext context)
        {
            _context = context;
        }
    }
}
