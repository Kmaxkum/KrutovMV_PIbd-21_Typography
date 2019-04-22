using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace TypographyServiceDAL.ViewModels
{
    [DataContract]
    public class ItemViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Название продукта")]
        public string ItemName { get; set; }
        [DataMember]
        [DisplayName("Цена")]
        public decimal Cost { get; set; }
        [DataMember]
        public List<ItemPartViewModel> ItemPart { get; set; }
    }
}
