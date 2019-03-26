using TypographyModel;
using System.Collections.Generic;

namespace TypographyServiceImplementList
{
    class DataListSingleton
    {
        private static DataListSingleton instance;
        public List<Customer> Customers { get; set; }
        public List<Part> Parts { get; set; }
        public List<Booking> Bookings { get; set; }
        public List<Item> Items { get; set; }
        public List<ItemPart> ItemParts { get; set; }
        private DataListSingleton()
        {
            Customers = new List<Customer>();
            Parts = new List<Part>();
            Bookings = new List<Booking>();
            Items = new List<Item>();
            ItemParts = new List<ItemPart>();
        }
        public static DataListSingleton GetInstance()
        {
            if (instance == null)
            {
                instance = new DataListSingleton();
            }
            return instance;
        }
    }
}
