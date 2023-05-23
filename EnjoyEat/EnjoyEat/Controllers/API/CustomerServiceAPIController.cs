using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using EnjoyEat.Controllers.DTO;

namespace EnjoyEat.Controllers.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerServiceAPIController : ControllerBase
    {
        private readonly db_a989fe_thm101team6Context db;

        public CustomerServiceAPIController(db_a989fe_thm101team6Context db)
        {
            this.db = db;
        }
       
        // POST: api/CustomerServiceAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<CustomerService>> PostCustomerService(CustomerService customerService)
        //{
        // CustomerService customerservice=new CustomerService
        // {

        // }
        //}

 
        private bool CustomerServiceExists(int id)
        {
            return (db.CustomerServices?.Any(e => e.QuestionId == id)).GetValueOrDefault();
        }
    }
}
