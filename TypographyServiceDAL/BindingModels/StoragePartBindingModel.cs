using System.Runtime.Serialization;

namespace TypographyServiceDAL.BindingModels
{
    [DataContract]
    public class StoragePartBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int StorageId { get; set; }
        [DataMember]
        public int PartId { get; set; }
        [DataMember]
        public int Cnt { get; set; }
    }
}
