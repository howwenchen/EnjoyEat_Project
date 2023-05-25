namespace EnjoyEat.Models.ViewModel
{
    public class ReservationInformationViewModel
    {
        public string PhoneNumber { get; set; } = null!;
        public int? ReserveId { get; set; }
        public string? ReservationName { get; set; }
        public string? Email { get; set; }
        public string? Note { get; set; }
    }
}
