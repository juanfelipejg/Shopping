namespace Shopping.Application.Services.Orders
{
	using Microsoft.EntityFrameworkCore;
	using Products;
	using Shopping.Domain.Models.Orders;
	using Shopping.Domain.Models.Products;
	using Shopping.Infrastructure.Data;

	public class OrderService: IOrderService
	{
		private readonly ShoppingContext _context;
		private readonly IProductService _productService;

		public OrderService( ShoppingContext context, IProductService productService )
		{
			this._context = context;
			this._productService = productService;
		}

		public IEnumerable<Order> GetOrders()
		{
			return this._context.Orders.Select( o => o ).Include( o => o.OrderProducts );
		}

		public async Task<Order> GetOrderAsync( int id )
		{
			return await this._context.Orders.FirstOrDefaultAsync( o => o.Id == id );
		}

		public async Task<Order> CreateOrderAsync( Order order )
		{
			await this.ValidateInventory( order.OrderProducts );

			Order orderCreated = this._context.Orders.Add( order ).Entity;

			this._context.SaveChanges();

			await this.UpdateInventory( order.OrderProducts );

			return orderCreated;
		}

		private async Task UpdateInventory( List<OrderProduct> orderProducts )
		{
			foreach( OrderProduct orderProduct in orderProducts.ToList() )
			{
				Product productToUpdate = await this._productService.GetProductAsync( orderProduct.ProductId );

				productToUpdate.Inventory -= orderProduct.Quantity;

				_ = this._productService.UpdateProduct( productToUpdate );
			}
		}

		private async Task ValidateInventory( IEnumerable<OrderProduct> orderProducts )
		{
			foreach( OrderProduct orderProduct in orderProducts )
			{
				Product product = await this._productService.GetProductAsync( orderProduct.ProductId );

				if( product.Inventory < orderProduct.Quantity )
				{
					throw new Exception( $"No hay suficiente {product.Name}" );
				}

				if( orderProduct.Quantity > product.Max || orderProduct.Quantity < product.Min )
				{
					throw new Exception( $"{product.Name} no cumple con el maximo ({product.Max}) o minimo ({product.Min}) permitido" );
				}
			}
		}
	}
}
