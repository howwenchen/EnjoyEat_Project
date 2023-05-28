﻿using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Controllers.API
{
    [Route("api/menu/[action]")]
    [ApiController]
    public class MenuAPIController : ControllerBase
    {
        private readonly db_a989fe_thm101team6Context _context;
        public MenuAPIController(db_a989fe_thm101team6Context context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetMenu()
        {
            var dbContext = _context.Products.Include(t => t.SubCategory);
            var temp = dbContext.Select(pro => new MenuViewModel
            {
                ProductId = pro.ProductId,
                MealImg = pro.MealImg,
                ProductName = pro.ProductName,
                UnitPrice = pro.UnitPrice,
                Description = pro.Description,
                CategoryName = pro.SubCategory.Category.CategoryName
            });
            return Ok(temp);
        }
    }
}