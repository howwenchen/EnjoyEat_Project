using EnjoyEat.Areas.OrderForHere.Models;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Areas.OrderForHere.Controllers
{
    [Area("OrderForHere")]
    public class StartOrderController : Controller
    {
        private readonly SQL8005site4nownetContext _context;
        public StartOrderController(SQL8005site4nownetContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult FixedIndex()
        {
            return View();
        }

        // GET: OrderForHere/StartOrder/SubCategories
        [HttpGet("OrderForHere/StartOrder/SubCategories")]
        public async Task<ActionResult<IEnumerable<SubCategories>>> SubCategories()
        {
            try
            {
                var subCategories = await _context.SubCategories.ToListAsync();
                return Ok(subCategories);
            }
            catch (Exception ex)
            {
                // log the exception message to diagnose the problem
                Console.WriteLine(ex.ToString());
                return StatusCode(500, "Internal server error");
            }
        }


        // GET: OrderForHere/StartOrder/Products
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Products>>> Products()
        {
            var products = await _context.Products.ToListAsync();
            return Ok(products);
        }
    }
}

