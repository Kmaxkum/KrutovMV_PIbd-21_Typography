using System.Collections.Generic;

namespace TypographyServiceDAL.BindingModels
{
    public class ItemBindingModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public decimal Cost { get; set; }
        public List<ItemPartBindingModel> ItemPart { get; set; }
    }
}
