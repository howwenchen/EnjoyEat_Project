using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Reservation
    {
        public int ReserveId { get; set; }
        public DateTime? ReservationDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public string? NumberofAdultGuest { get; set; }
        public string? NumberofKidGuest { get; set; }
        public string? ReservationTime { get; set; }

        public virtual ReservationInformation? ReservationInformation { get; set; }
    }
}
