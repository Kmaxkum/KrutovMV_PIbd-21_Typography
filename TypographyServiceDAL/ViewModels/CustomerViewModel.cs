using System.ComponentModel;

namespace TypographyServiceDAL.ViewModels
{
    public class CustomerViewModel
    {
        public int Id { get; set; }
        [DisplayName("ФИО Клиента")]
        public string CustomerFIO { get; set; }
    }
}
