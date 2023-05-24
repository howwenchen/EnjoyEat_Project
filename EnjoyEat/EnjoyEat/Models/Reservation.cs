using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Reservation
    {
        public Reservation()
        {
            ReservationInformations = new HashSet<ReservationInformation>();
        }

        public int ReserveId { get; set; }
        public string? PhoneNumber { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public string? NumberofAdultGuest { get; set; }
        public string? NumberofKidGuest { get; set; }
        public string? ReservationTime { get; set; }

        public virtual ICollection<ReservationInformation> ReservationInformations { get; set; }
    }
}
