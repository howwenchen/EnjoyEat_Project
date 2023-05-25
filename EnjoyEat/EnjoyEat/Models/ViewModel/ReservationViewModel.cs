namespace EnjoyEat.Models.ViewModel
{
	public class ReservationViewModel
	{
		public int ReserveId { get; set; }
		public DateTime? ReservationDate { get; set; }
		public string? NumberofAdultGuest { get; set; }
		public string? NumberofKidGuest { get; set; }
		public string? ReservationTime { get; set; }

	}
}