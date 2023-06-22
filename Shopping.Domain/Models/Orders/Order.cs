using System.ComponentModel.DataAnnotations;
using Shopping.Domain.Models.Products;

namespace Shopping.Domain.Models.Orders
{
    public class Order
    {
		public int Id { get; set; }

        public DateTime Date { get; set; }

		[Required]
        public string IdType { get; set; }

		[Required]
        public string ClientName { get; set; }

		[Required]
		[MinLength( 1, ErrorMessage = "La lista de productos no puede estar vacía." )]
		public List<OrderProduct> OrderProducts { get; set; }
    }
}
