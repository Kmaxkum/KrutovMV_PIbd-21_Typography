using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace TypographyServiceDAL.ViewModels
{
    [DataContract]
    public class StorageViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Название склада")]
        public string StorageName { get; set; }
        [DataMember]
        public List<StoragePartViewModel> StoragePart { get; set; }
    }
}
