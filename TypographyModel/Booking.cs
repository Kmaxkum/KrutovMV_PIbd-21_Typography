﻿using System;

namespace TypographyModel
{
    public class Booking
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public int? WorkerId { get; set; }
        public int Cnt { get; set; }
        public decimal TotalSum { get; set; }
        public BookingStatus State { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateImplement { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Item Item { get; set; }
        public virtual Worker Worker { get; set; }
    }
}
