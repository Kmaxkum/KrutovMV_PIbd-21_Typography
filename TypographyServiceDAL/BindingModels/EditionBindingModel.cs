using System;

namespace TypographyServiceDAL.BindingModels
{
    public class EditionBindingModel
    {
        public string FilePath { get; set; }
        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }
    }
}
