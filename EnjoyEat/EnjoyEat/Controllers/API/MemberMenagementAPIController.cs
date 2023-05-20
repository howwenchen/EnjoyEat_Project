using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers.API
{
    [Route("api/member/[action]")]
    [ApiController]
    public class MemberMenagementAPIController :ControllerBase
    {
        private readonly db_a989fe_thm101team6Context db;
        public MemberMenagementAPIController(db_a989fe_thm101team6Context db)
        {
            this.db = db;
        }

        [HttpGet]
        public IActionResult GetMember()
        {
            var userId = 20230006;
            var user = db.Members.FirstOrDefault(x => x.MemberId == userId);
            if (user == null)
            {
                return NotFound();
            }
            var member = new MemberViewModel()
            {
                MemberId = user.MemberId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Gender = user.Gender,
                Birthday = user.Birthday,
                Address = user.Address,
                Phone = user.Phone,
                LevelName = user.LevelName,
                DiscountRate = user.LevelNameNavigation.DiscountRate,
                Orders = user.Orders.Select(x => new MemberOrderViewModel
                {
                    OrderDate = x.OrderDate,
                    OrderId = x.OrderId,
                    TableId = x.TableId,
                    TotalPrice = x.TotalPrice,
                }).ToList(),
            };
            return Ok(member);
        }
    }
}
