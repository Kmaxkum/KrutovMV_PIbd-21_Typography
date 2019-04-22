using System.Runtime.Serialization;

namespace TypographyServiceDAL.ViewModels
{
    [DataContract]
    public class CustomerBookingViewModel
    {
        [DataMember]
        public string CustomerFIO { get; set; }
        [DataMember]
        public string DateCreate { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public int Cnt { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public string State { get; set; }
    }
}
