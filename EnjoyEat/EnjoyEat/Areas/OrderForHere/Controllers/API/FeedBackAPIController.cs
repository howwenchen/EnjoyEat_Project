using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Areas.OrderForHere.Controllers.API
{
    [Route("api/OrderForHere/[action]")]
    [ApiController]
    public class FeedBackAPIController : ControllerBase
    {
        private readonly db_a989fe_thm101team6Context db;
        public FeedBackAPIController(db_a989fe_thm101team6Context db)
        {
            this.db = db;
        }

        [HttpPost]
        public async Task<IActionResult> PostFeedBack(FeedBackViewModel feedbackViewModel)
        {
            FeedBack feedback = new FeedBack()
            {
                OrderId = feedbackViewModel.OrderId,
                FeedBackName = feedbackViewModel.FeedBackName,
                Email = feedbackViewModel.Email,
                Age = feedbackViewModel.Age,
                Frequency = feedbackViewModel.Frequency,
                Enviroment = feedbackViewModel.Enviroment,
                Serve = feedbackViewModel.Serve,
                Dish = feedbackViewModel.Dish,
                Price = feedbackViewModel.Price,
                Overall = feedbackViewModel.Overall,
                Suggestion = feedbackViewModel.Suggestion,
            };
            db.Add(feedback);
            db.SaveChanges();
            return Ok(feedback);
        }
    }
}

