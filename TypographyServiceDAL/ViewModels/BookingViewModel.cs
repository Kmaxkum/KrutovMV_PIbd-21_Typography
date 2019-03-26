using System.ComponentModel;

namespace TypographyServiceDAL.ViewModels
{
    public class BookingViewModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        [DisplayName("ФИО Покупателя")]
        public string CustomerFIO { get; set; }
        public int ItemId { get; set; }
        [DisplayName("Печатное изделие")]
        public string ItemName { get; set; }
        [DisplayName("Количество")]
        public int Cnt { get; set; }
        [DisplayName("Сумма")]
        public decimal TotalSum { get; set; }
        [DisplayName("Статус")]
        public string State { get; set; }
        [DisplayName("Дата создания")]
        public string DateCreate { get; set; }
        [DisplayName("Дата выполнения")]
        public string DateImplement { get; set; }

    }
}
