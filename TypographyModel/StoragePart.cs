namespace TypographyModel
{
    public class StoragePart
    {
        public int Id { get; set; }
        public int StorageId { get; set; }
        public int PartId { get; set; }
        public int Cnt { get; set; }
        public virtual Part Parts { get; set; }
        public virtual Storage Storages { get; set; }

    }
}
