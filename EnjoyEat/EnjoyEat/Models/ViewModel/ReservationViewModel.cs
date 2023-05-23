namespace EnjoyEat.Models.ViewModel
{
    public class ReservationViewModel
    {
        public string PhoneNumber { get; set; } = null!;
        public DateTime ReservationDate { get; set; }
        public DateTime ConfirmationDate { get; set; }
        public string NumberofAdultGuest { get; set; } = null!;
        public string? NumberofKidGuest { get; set; }
        public DateTime ReservationTime { get; set; }

        public virtual ReservationInformation? ReservationInformation { get; set; }
    }
}
