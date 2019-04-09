using System;
using System.Collections.Generic;

namespace TypographyServiceDAL.ViewModels
{
    public class StorageLoadViewModel
    {
        public string StorageName { get; set; }
        public int TotalCnt { get; set; }
        public IEnumerable<Tuple<string, int>> Parts { get; set; }
    }
}
