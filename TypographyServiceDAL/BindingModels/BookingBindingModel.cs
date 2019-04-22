using System.Runtime.Serialization;

namespace TypographyServiceDAL.BindingModels
{
    [DataContract]
    public class BookingBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int CustomerId { get; set; }
        [DataMember]
        public int ItemId { get; set; }
        [DataMember]
        public int? WorkerId { get; set; }
        [DataMember]
        public int Cnt { get; set; }
        [DataMember]
        public decimal TotalSum { get; set; }
    }
}
