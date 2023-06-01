namespace EnjoyEat.Models.ViewModel
{
    public class ReservationManagemanetViewModel
    {
        public int ReserveId { get; set; }
        public DateTime? ReservationDate { get; set; }
        public string? ReservationTime { get; set; }
        public string? NumberofAdultGuest { get; set; }
        public string? NumberofKidGuest { get; set; }
        public string? PhoneNumber { get; set; }
        public string? ReservationName { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
    }
}
