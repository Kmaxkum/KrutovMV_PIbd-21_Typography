using System.ComponentModel;
using System.Runtime.Serialization;

namespace TypographyServiceDAL.ViewModels
{
    [DataContract]
    public class BookingViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        [DisplayName("ФИО Покупателя")]
        public string CustomerFIO { get; set; }
        public int ItemId { get; set; }
        [DataMember]
        [DisplayName("Печатное изделие")]
        public string ItemName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Cnt { get; set; }
        [DataMember]
        [DisplayName("Сумма")]
        public decimal TotalSum { get; set; }
        [DataMember]
        [DisplayName("Статус")]
        public string State { get; set; }
        [DataMember]
        [DisplayName("Дата создания")]
        public string DateCreate { get; set; }
        [DataMember]
        [DisplayName("Дата выполнения")]
        public string DateImplement { get; set; }
    }
}
