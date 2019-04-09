using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TypographyModel
{
    public class Customer
    {
        public int Id { get; set; }
        [Required]
        public string CustomerFIO { get; set; }
        [ForeignKey("CustomerId")]
        public virtual List<Booking> Bookings { get; set; }
    }
}
