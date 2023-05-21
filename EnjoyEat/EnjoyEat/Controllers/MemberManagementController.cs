//using EnjoyEat.Models;
//using EnjoyEat.Models.ViewModel;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.EntityFrameworkCore;
//using static NuGet.Packaging.PackagingConstants;

//namespace EnjoyEat.Controllers
//{
//    public class MemberManagementController : Controller
//    {
//        private readonly db_a989fe_thm101team6Context _db;
//        public MemberManagementController(db_a989fe_thm101team6Context projectContext)
//        {

//            using (var _context = new db_a989fe_thm101team6Context())
//            {
//                var memberData = _context.Members;
//                var orderData= _context.Orders;
//                var orderDetailData = _context.OrderDetails;
//                var viewModel = memberData.Select(m => new MemberViewModel
//                {
//                    MemberId = m.MemberId,
//                    FirstName = m.FirstName,
//                    LastName = m.LastName,
//                    Gender = m.Gender,
//                    Birthday = m.Birthday,
//                    Address = m.Address,
//                    Phone = m.Phone,
//                    Email = m.Email,
//                    LevelName = m.LevelName,
//                    //Password = m.Password,
//                });
//                return View(viewModel);
//            }
//        }
//            public IActionResult Index()
//        {
//                return View();
            
//        }

//        public IActionResult EditPassword()
//        { 
//                return View();
//        }

//        public async Task<IActionResult> GetMember()
//        {
//            var userId = 2023001;
//            var user=_db.Members.FirstOrDefault(x =>x.MemberId == userId);
//            if(user == null){
//                return NotFound();
//            }
//            var member=new MemberViewModel()
//            {
//                MemberId = user.MemberId,
//                FirstName = user.FirstName,
//                LastName = user.LastName,
//                Email = user.Email,
//                Gender = user.Gender,
//                Birthday = user.Birthday,
//                Address = user.Address,
//                Phone = user.Phone,
//                LevelName = user.LevelName,
//                DiscountRate = user.LevelNameNavigation.DiscountRate,
//                Orders = user.Orders.Select(x => new MemberOrderViewModel
//                {
//                    OrderDate = x.OrderDate,
//                    OrderId = x.OrderId,
//                    TableId = x.TableId,
//                    TotalPrice = x.TotalPrice,
//                }).ToList(),
//            };
//            return Ok(member);
//            ////var member = await _db.Members.Include(p => p.Orders).Include(p =>p.LevelNameNavigation).ToListAsync();
//            //member.Select(x => new MemberViewModel()
//            //{
//            //    MemberId = x.MemberId,
//            //    FirstName = x.FirstName,
//            //    LastName = x.LastName,
//            //    Gender = x.Gender,
//            //    Birthday = x.Birthday,
//            //    Address = x.Address,
//            //    Phone = x.Phone,
//            //    Email = x.Email,
//            //    LevelName= x.LevelName,
//            //    OrderId=x.Orders.Select(o => o.OrderId),

//            //});
//        }
//    }
//}
