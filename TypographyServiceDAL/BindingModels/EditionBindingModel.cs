using System;
using System.Runtime.Serialization;

namespace TypographyServiceDAL.BindingModels
{
    [DataContract]
    public class EditionBindingModel
    {
        [DataMember]
        public string FilePath { get; set; }
        [DataMember]
        public DateTime? DateFrom { get; set; }
        [DataMember]
        public DateTime? DateTo { get; set; }
    }
}
