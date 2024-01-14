namespace QuickOrder.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public int Weight { get; set; }
        public string SendersCity { get; set; }
        public string SendersAdress { get; set; }
        public string DestinationAdress { get; set; }
        public string DestinationCity { get; set; }

    }
}
