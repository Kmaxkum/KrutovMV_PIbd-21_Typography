namespace TypographyServiceDAL.BindingModels
{
    public class BookingBindingModel
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public int ItemId { get; set; }
        public int Cnt { get; set; }
        public decimal TotalSum { get; set; }
    }
}
