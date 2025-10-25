using payday_server.Layers.ContextLayer;

namespace payday_server.Shared
{
    public class Algorithms
    {
        private readonly AppDBContext _context;

        public Algorithms(AppDBContext context)
        {
            _context = context;
        }

    }
}

