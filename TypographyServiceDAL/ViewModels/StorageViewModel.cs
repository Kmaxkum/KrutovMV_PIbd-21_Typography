using System.Collections.Generic;
using System.ComponentModel;

namespace TypographyServiceDAL.ViewModels
{
    public class StorageViewModel
    {
        public int Id { get; set; }
        [DisplayName("Название склада")]
        public string StorageName { get; set; }
        public List<StoragePartViewModel> StoragePart { get; set; }
    }
}
