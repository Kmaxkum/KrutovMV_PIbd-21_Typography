using System.ComponentModel;
using System.Runtime.Serialization;

namespace TypographyServiceDAL.ViewModels
{
    [DataContract]
    public class PartViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Название концелярии")]
        public string PartName { get; set; }
    }
}
