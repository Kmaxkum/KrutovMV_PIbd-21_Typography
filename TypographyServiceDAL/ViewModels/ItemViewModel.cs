using System.Collections.Generic;
using System.ComponentModel;

namespace TypographyServiceDAL.ViewModels
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название продукта")]
        public string ItemName { get; set; }
        [DisplayName("Цена")]
        public decimal Cost { get; set; }
        public List<ItemPartViewModel> ItemPart { get; set; }
    }
}
