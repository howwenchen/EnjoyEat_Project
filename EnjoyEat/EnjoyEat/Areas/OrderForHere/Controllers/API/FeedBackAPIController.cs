using EnjoyEat.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.OrderForHere.Controllers.API
{
    [Route("api/order/[action]")]
    [ApiController]
    public class FeedBackAPIController : ControllerBase
    {
        private readonly db_a989fe_thm101team6Context db;
        public FeedBackAPIController(db_a989fe_thm101team6Context db)
        {
            this.db = db;
        }

        //[HttpPost]
        //public IActionResult FeedBack
    }
}
