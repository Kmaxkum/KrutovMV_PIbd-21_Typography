using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace TypographyModel
{
    public class Worker
    {
        public int Id { get; set; }
        [Required]
        public string WorkerFIO { get; set; }
        [ForeignKey("WorkerId")]
        public virtual List<Booking> Bookings { get; set; }
    }
}
