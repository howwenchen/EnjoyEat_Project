using EnjoyEat.Models;
using EnjoyEat.Models.DTO;
using EnjoyEat.Models.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace EnjoyEat.Areas.backend.Controllers.Api
{
    [Route("api/ReservationManagementApi/[action]")]
    [ApiController]
    public class ReservationManagementAPIController : ControllerBase
    {
        private readonly db_a989fe_thm101team6Context db;

        public ReservationManagementAPIController(db_a989fe_thm101team6Context db)
        {
            this.db = db;
        }

        [HttpGet]

        public async Task<IActionResult> GetReservationManagement()
        {
            var reserve = db.Reservations.Include(x => x.ReservationInformation).Select(x => new ReservationManagemanetViewModel
            {
                ReserveId = x.ReserveId,
                ReservationName = x.ReservationInformation.ReservationName,
                PhoneNumber = x.ReservationInformation.PhoneNumber,
                NumberofAdultGuest = x.NumberofAdultGuest,
                NumberofKidGuest = x.NumberofKidGuest,
                ReservationDate = x.ReservationDate,
                ReservationTime = x.ReservationTime,
                Note = x.ReservationInformation.Note,
            }).ToList();
            return Ok(reserve);
        }
    }
}
