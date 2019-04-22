using System.ComponentModel;
using System.Runtime.Serialization;

namespace TypographyServiceDAL.ViewModels
{
    [DataContract]
    public class ItemPartViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ItemId { get; set; }
        [DataMember]
        public int PartId { get; set; }
        [DataMember]
        [DisplayName("Канцтовар")]
        public string PartName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Cnt { get; set; }
    }
}
