using TypographyModel;
using System.Data.Entity;

namespace TypographyServiceImplementDataBase
{
    public class TypographyDbContext : DbContext
    {
        public TypographyDbContext() : base("TypographyDataBase1")
        {
            //настройки конфигурации для entity
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied =
           System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Worker> Workers { get; set; }
        public virtual DbSet<Part> Parts { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<ItemPart> ItemParts { get; set; }
        public virtual DbSet<Storage> Storages { get; set; }
        public virtual DbSet<StoragePart> StorageParts { get; set; }
    }
}
