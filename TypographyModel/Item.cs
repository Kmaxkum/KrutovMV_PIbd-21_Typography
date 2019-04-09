using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TypographyModel
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        public string ItemName { get; set; }
        [Required]
        public decimal Cost { get; set; }
        public virtual List<Booking> Bookings { get; set; }
    }
}
