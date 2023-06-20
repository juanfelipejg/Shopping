using Shopping.Application.Models.Products;

namespace Shopping.Application.Models.Buys
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public IdentificationEnum IdType { get; set; }

        public string ClientName { get; set; }

        public List<OrderProduct> OrderProducts { get; set; } 
    }
}
