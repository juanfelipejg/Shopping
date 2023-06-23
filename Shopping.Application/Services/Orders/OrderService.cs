namespace Shopping.Application.Services.Orders
{
	using Microsoft.EntityFrameworkCore;
	using Products;
	using Shopping.Domain.Models.Orders;
	using Shopping.Domain.Models.Products;
	using Shopping.Infrastructure.Data;

	public class OrderService: IOrderService
	{
		private readonly IShoppingContext _context;
		private readonly IProductService _productService;

		public OrderService( IShoppingContext context, IProductService productService )
		{
			this._context = context;
			this._productService = productService;
		}

		public IEnumerable<Order> GetOrders()
		{
			try
			{
				return this._context.Orders.Select( o => o ).Include( o => o.OrderProducts ).ThenInclude( op => op.Product );
			}
			catch( DbUpdateException ex )
			{
				throw new Exception( "Un error ocurrió mientras se consultaba las ordenes", ex );
			}
		}

		public async Task<Order> GetOrderAsync( int id )
		{
			var order = await this._context.Orders.Include( o => o.OrderProducts ).ThenInclude( op => op.Product ).FirstOrDefaultAsync( o => o.Id == id );
			
			if ( order != null ) 
			{
				try
				{
					return order;
				}

				catch( DbUpdateException ex )
				{
					throw new Exception( "Un error ocurrió mientras se consultaba la orden", ex );
				}
			}

			else
			{
				throw new NotFoundException( "Orden no encontrada" );
			}
		}

		public async Task<Order> CreateOrderAsync( Order order )
		{
			try
			{
				await this.ValidateInventory( order.OrderProducts );

				Order orderCreated = this._context.Orders.Add( order ).Entity;

				this._context.SaveChanges();

				await this.UpdateInventory( order.OrderProducts );

				return orderCreated;
			}

			catch( DbUpdateException ex )
			{
				throw new Exception( "Un error ocurrió mientras se creaba la orden", ex );
			}
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
					throw new BadRequestException( $"No hay suficiente {product.Name}" );
				}

				if( orderProduct.Quantity > product.Max || orderProduct.Quantity < product.Min )
				{
					throw new BadRequestException( $"{product.Name} no cumple con el maximo ({product.Max}) o minimo ({product.Min}) permitido" );
				}
			}
		}
	}
}
