using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;

namespace EnjoyEat.Controllers.API
{
    [Route("api/reservation/booking/[action]")]
    [ApiController]
    public class ReservationInformationsAPIController : ControllerBase
    {
        private readonly db_a989fe_thm101team6Context db;

        public ReservationInformationsAPIController(db_a989fe_thm101team6Context db)
        {
            this.db = db;
        }

        // POST: api/ReservationInformationsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<string> PostReservationInformation(ReservationInformationViewModel reservationinformationViewModel)
        {
            ReservationInformation reservationInformation = new ReservationInformation
            {
                PhoneNumber = reservationinformationViewModel.PhoneNumber,
                ReservationName = reservationinformationViewModel.ReservationName,
                EMail = reservationinformationViewModel.EMail,
                Note = reservationinformationViewModel.Note,
            };
            db.ReservationInformations.Add(reservationInformation);
            await db.SaveChangesAsync();
            return "訂位成功!";
        }

        private bool ReservationInformationExists(string id)
        {
            return (db.ReservationInformations?.Any(e => e.PhoneNumber == id)).GetValueOrDefault();
        }
    }
}
