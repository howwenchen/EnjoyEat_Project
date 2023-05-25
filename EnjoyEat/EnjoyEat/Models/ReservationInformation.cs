using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
	public partial class ReservationInformation
	{
		public string PhoneNumber { get; set; } = null!;
		public int? ReserveId { get; set; }
		public string? ReservationName { get; set; }
		public string? Email { get; set; }
		public string? Note { get; set; }

		public virtual Reservation? Reserve { get; set; }
	}
}