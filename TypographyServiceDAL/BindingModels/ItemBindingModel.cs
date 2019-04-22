using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TypographyServiceDAL.BindingModels
{
    [DataContract]
    public class ItemBindingModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        public string ItemName { get; set; }
        [DataMember]
        public decimal Cost { get; set; }
        [DataMember]
        public List<ItemPartBindingModel> ItemPart { get; set; }
    }
}
