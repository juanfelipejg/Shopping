namespace Shopping.Application.Services.Products
{
	using Shopping.Domain.Models.Products;
	using Shopping.Infrastructure.Data;

	public class ProductService: IProductService
	{
		private readonly ShoppingContext _context;

		public ProductService( ShoppingContext context )
		{
			this._context = context;
		}

		public Product Add( Product product )
		{
			this._context.Products.Add( product );
			this._context.SaveChanges();
			return product;
		}

		//public void Delete( int id )
		//{
		//	throw new NotImplementedException();
		//}

		public IEnumerable<Product> GetAll()
		{
			return this._context.Products.Select( p => p );
		}

		public async Task<Product> GetByIdAsync( int id )
		{
			return await this._context.Products.FindAsync( id );
		}

		//public Product Update( Product product )
		//{
		//	throw new NotImplementedException();
		//}
	}
}