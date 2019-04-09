using System.ComponentModel;

namespace TypographyServiceDAL.ViewModels
{
    public class StoragePartViewModel
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int PartId { get; set; }
        [DisplayName("Название концеляции")]
        public string PartName { get; set; }
        [DisplayName("Количество")]
        public int Cnt { get; set; }
    }
}
