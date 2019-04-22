using System.ComponentModel;
using System.Runtime.Serialization;

namespace TypographyServiceDAL.ViewModels
{
    [DataContract]
    public class StoragePartViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StorageId { get; set; }
        [DataMember]
        public int PartId { get; set; }
        [DataMember]
        [DisplayName("Название концеляции")]
        public string PartName { get; set; }
        [DataMember]
        [DisplayName("Количество")]
        public int Cnt { get; set; }
    }
}
