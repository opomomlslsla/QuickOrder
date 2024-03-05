using System.ComponentModel.DataAnnotations.Schema;

namespace QuickOrder.Domain.Models
{
    public class Order
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public int SerialNumber { get; set; }
        public DateTime Date { get; set; }
        public int Weight { get; set; }
        public string SendersCity { get; set; }
        public string SendersAdress { get; set; }
        public string DestinationAdress { get; set; }
        public string DestinationCity { get; set; }

    }
}
