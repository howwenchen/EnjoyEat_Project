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
				ReservationDate = reservationViewModel.ReservationDate,
				NumberofAdultGuest = reservationViewModel.NumberofAdultGuest,
				NumberofKidGuest = reservationViewModel.NumberofKidGuest,
				ReservationTime = reservationViewModel.ReservationTime,

			};
			db.Reservations.Add(reservation);
			await db.SaveChangesAsync();
			return Ok(reservation);
		}




		[HttpGet]
		public async Task<IActionResult> GetReservationInformation([FromQuery] int reserveId)
		{

			var reservationInfo = db.Reservations.Where(x => x.ReserveId == reserveId).Select(r => new ReserveSuccess()
			{
				ReservationName = r.ReservationInformation.ReservationName,
				ReservationDate = r.ReservationDate,
				ReservationTime = r.ReservationTime,
				NumberofAdultGuest = r.NumberofAdultGuest,
				NumberofKidGuest = r.NumberofKidGuest,
				Note=r.ReservationInformation.Note,
			}).ToList();
			return Ok(reservationInfo);
		}

	}
}