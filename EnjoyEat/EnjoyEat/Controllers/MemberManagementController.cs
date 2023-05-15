using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Controllers
{/*信箱驗證 208頁*/
    public class MemberManagementController : Controller
    {
        public IActionResult Index()
        {
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
                    Password = m.Password,
                });
                return View(viewModel);
            }
        }
        public IActionResult EditPassword()
        {
            return View();
        }
    }
}
