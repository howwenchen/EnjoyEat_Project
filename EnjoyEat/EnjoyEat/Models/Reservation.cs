﻿using System;
using System.Collections.Generic;

namespace EnjoyEat.Models
{
    public partial class Reservation
    {
        public string PhoneNumber { get; set; } = null!;
        public DateTime? ReservationDate { get; set; }
        public DateTime? ConfirmationDate { get; set; }
        public string? NumberofGuest { get; set; }
        public DateTime? ReservationTime { get; set; }

        public virtual ReservationInformation? ReservationInformation { get; set; }
    }
}