using EnjoyEat.Models;

namespace EnjoyEat.Services
{
    public class MembersService
    {
        private readonly db_a989fe_thm101team6Context _context;
        public MembersService(db_a989fe_thm101team6Context context)
        {
            _context = context;
        }
        public IEnumerable<Member> GetAllByEmail(string email)
        {
            var membersService = (from a in _context.Members where a.Email==email select a).ToList();

            return membersService;

        }
    }
}
