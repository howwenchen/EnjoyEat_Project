using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;

namespace EnjoyEat.Controllers.API
{
    [Route("api/contactus/[action]")]
    [ApiController]
    public class CustomerServiceAPIController : ControllerBase
    {
        private readonly db_a989fe_thm101team6Context db;

        public CustomerServiceAPIController(db_a989fe_thm101team6Context db)
        {
            this.db = db;
        }

        //POST: api/CustomerServiceAPI
        //To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostCustomerService(CustomerServiceViewModel customerServiceViewModel)
        {
            CustomerService customerservice = new CustomerService
            {
                QuestionKeynote = customerServiceViewModel.QuestionKeynote,
                QuestionContent = customerServiceViewModel.QuestionContent,
                Email = customerServiceViewModel.Email,
                Phone = customerServiceViewModel.Phone,
                CustomerName = customerServiceViewModel.CustomerName,
                ServiceOption = customerServiceViewModel.ServiceOption,
                //QuestionDatetime = DateTime.Now,
            };

            db.CustomerServices.Add(customerservice);
            await db.SaveChangesAsync();
            return "我們已收到您的建議，將盡快回覆您";
        }


        private bool CustomerServiceExists(int id)
        {
            return (db.CustomerServices?.Any(e => e.QuestionId == id)).GetValueOrDefault();
        }
    }
}
