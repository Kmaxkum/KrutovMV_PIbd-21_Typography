using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace TypographyModel
{
    public class Storage
    {
        public int Id { get; set; }
        [Required]
        public string StorageName { get; set; }
        public virtual List<StoragePart> StorageParts { get; set; }
    }
}
