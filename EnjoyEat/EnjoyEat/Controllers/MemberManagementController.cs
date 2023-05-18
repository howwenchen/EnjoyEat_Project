using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static NuGet.Packaging.PackagingConstants;

namespace EnjoyEat.Controllers
{
    public class MemberManagementController : Controller
    {
        
        public IActionResult Index()
        {
            using (var _context = new db_a989fe_thm101team6Context())
            {
                var memberData = _context.Members;
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
                    MemberPoints = m.MemberPoints,
                    Orders = m.Orders,
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
