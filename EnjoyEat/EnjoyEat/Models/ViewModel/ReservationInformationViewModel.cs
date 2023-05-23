namespace EnjoyEat.Models.ViewModel
{
    public class ReservationInformationViewModel
    {
        public string PhoneNumber { get; set; } = null!;
        public string? ReservationName { get; set; }
        public string? EMail { get; set; }

        public virtual Reservation PhoneNumberNavigation { get; set; } = null!;
    }
}
