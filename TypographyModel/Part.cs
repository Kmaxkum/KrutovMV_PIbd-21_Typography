using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace TypographyModel
{
    public class Part
    {
        public int Id { get; set; }
        [Required]
        public string PartName { get; set; }
        public virtual List<ItemPart> ItemParts { get; set; }
        public virtual List<StoragePart> StorageParts { get; set; }
    }
}
