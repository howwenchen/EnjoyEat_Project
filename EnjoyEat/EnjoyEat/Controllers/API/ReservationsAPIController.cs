using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;

namespace EnjoyEat.Controllers.API
{
    [Route("api/reservation/[action]")]
    [ApiController]
    public class ReservationsAPIController : ControllerBase
    {
        private readonly db_a989fe_thm101team6Context db;

        public ReservationsAPIController(db_a989fe_thm101team6Context db)
        {
            this.db = db;
        }

        // POST: api/ReservationsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostReservation(ReservationViewModel reservationViewModel)
        {

            Reservation reservation = new Reservation
            {
                ReserveId = reservationViewModel.ReserveId,
                ReservationDate = reservationViewModel.ReservationDate,
                NumberofAdultGuest = reservationViewModel.NumberofAdultGuest,
                NumberofKidGuest = reservationViewModel.NumberofKidGuest,
                ReservationTime = reservationViewModel.ReservationTime,

            };
            db.Reservations.Add(reservation);
            await db.SaveChangesAsync();
            return Ok(reservation);
        }

        //private bool ReservationExists(string id)
        //{
        //    return (db.Reservations?.Any(e => e.PhoneNumber == id)).GetValueOrDefault();
        //}
    }
}
