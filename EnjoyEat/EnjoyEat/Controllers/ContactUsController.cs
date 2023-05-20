using EnjoyEat.Models;
using Microsoft.AspNetCore.Mvc;

namespace EnjoyEat.Controllers
{
    public class ContactUsController : Controller
    {
            private readonly db_a989fe_thm101team6Context _db;
            public ContactUsController(db_a989fe_thm101team6Context projectContext)
            {
                this._db = projectContext;
            }
             
            public IActionResult ContactUs()
            {
                return View();
            }
        }
    }

