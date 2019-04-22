using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TypographyServiceDAL.ViewModels
{
    [DataContract]
    public class StorageLoadViewModel
    {
        [DataMember]
        public string StorageName { get; set; }
        [DataMember]
        public int TotalCnt { get; set; }
        [DataMember]
        public IEnumerable<Tuple<string, int>> Parts { get; set; }
    }
}
