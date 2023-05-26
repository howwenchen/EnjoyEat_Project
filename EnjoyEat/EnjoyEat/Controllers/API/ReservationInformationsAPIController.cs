using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EnjoyEat.Models;
using EnjoyEat.Models.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
		public async Task<IActionResult> PostReservationInformation(ReservationInformationViewModel reservationinformationViewModel)
		{
			ReservationInformation reservationInformation = new ReservationInformation
			{
				ReserveId = reservationinformationViewModel.ReserveId,
				PhoneNumber = reservationinformationViewModel.PhoneNumber,
				ReservationName = reservationinformationViewModel.ReservationName,
				Email = reservationinformationViewModel.Email,
				Note = reservationinformationViewModel.Note,
			};
			db.ReservationInformations.Add(reservationInformation);
			await db.SaveChangesAsync();
			var x = reservationInformation.ReserveId;
			return Ok(x);
		}

		private bool ReservationInformationExists(string id)
		{
			return (db.ReservationInformations?.Any(e => e.PhoneNumber == id)).GetValueOrDefault();
		}
	}
}
