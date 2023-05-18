using EnjoyEat.Models;

namespace EnjoyEat.Services
{
    public class MemberLoginService
    {
        private readonly db_a989fe_thm101team6Context _context;
        public MemberLoginService(db_a989fe_thm101team6Context context)
        {
            _context = context;
        }
        public IEnumerable<MemberLogin> GetAllByAccount(string account )
        {
            var memberLoginList =(from a in _context.MemberLogins where a.Account==account select a).ToList();  

            return memberLoginList; 

        }
    }
}
