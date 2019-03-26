using System.ComponentModel;

namespace TypographyServiceDAL.ViewModels
{
    public class PartViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название компонента")]
        public string PartName { get; set; }
    }
}
