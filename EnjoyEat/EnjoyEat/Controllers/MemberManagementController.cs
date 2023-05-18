using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers
{
    public class MemberManagementController : Controller
    {
        public IActionResult Index()
        {
<<<<<<< Updated upstream
=======
            using (var _context = new db_a989fe_thm101team6Context())
            {
                var memberData = _context.Members;
                var orderData= _context.Orders;
                var orderDetailData = _context.OrderDetails;
                var viewModel = memberData.Select(m => new MemberViewModel
                {
                    MemberId = m.MemberId,
                    FirstName = m.FirstName,
                    LastName = m.LastName,
                    Gender = m.Gender,
                    Birthday = m.Birthday,
                    Address = m.Address,
                    Phone = m.Phone,
                    Email = m.Email,
                    LevelName = m.LevelName,
                });
                return View(viewModel);
            }
        }
        public IActionResult EditPassword()
        {
>>>>>>> Stashed changes
            return View();
        }
    }
}
