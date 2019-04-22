using System.Runtime.Serialization;

namespace TypographyServiceDAL.BindingModels
{
    [DataContract]
    public class ItemPartBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public int ItemId { get; set; }
        [DataMember]
        public int PartId { get; set; }
        [DataMember]
        public int Cnt { get; set; }

    }
}
