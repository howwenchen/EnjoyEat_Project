using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class ReservationInformation
    {
        public string PhoneNumber { get; set; } = null!;
        public string? ReservationName { get; set; }
        public string? EMail { get; set; }
        public string? Note { get; set; }

        public virtual Reservation PhoneNumberNavigation { get; set; } = null!;
    }
}
