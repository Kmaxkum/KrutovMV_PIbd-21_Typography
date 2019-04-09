using System.ComponentModel;

namespace TypographyServiceDAL.ViewModels
{
    public class PartViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название концелярии")]
        public string PartName { get; set; }
    }
}
