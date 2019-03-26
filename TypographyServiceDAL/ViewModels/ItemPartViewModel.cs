using System.ComponentModel;

namespace TypographyServiceDAL.ViewModels
{
    public class ItemPartViewModel
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public int PartId { get; set; }
        [DisplayName("Канцтовар")]
        public string PartName { get; set; }
        [DisplayName("Количество")]
        public int Cnt { get; set; }
    }
}
