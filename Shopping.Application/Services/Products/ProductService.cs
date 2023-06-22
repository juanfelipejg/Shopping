namespace Shopping.Application.Services.Products
{
	using Microsoft.Data.SqlClient;	
	using Microsoft.EntityFrameworkCore;
	using Shopping.Domain.Models.Products;
	using Shopping.Infrastructure.Data;
	using Shopping.Infrastructure.Repository;

	public class ProductService: IProductService
	{
		private readonly ShoppingContext _context;
		private readonly IRepositoryProduct _repositoryProduct;

		public ProductService( ShoppingContext context, IRepositoryProduct repositoryProduct )
		{
			this._context = context;
			this._repositoryProduct = repositoryProduct;
		}

		public IEnumerable<Product> GetProducts( int pageNumber, int pageSize ) 
		{
			try
			{
				return this._repositoryProduct.GetProducts( pageNumber, pageSize );
			}

			catch ( SqlException ex )
			{
				throw new Exception( "Un error ocurrió mientras se consultaban los productos", ex );
			}			
		}

		public async Task<Product> GetProductAsync( int id )
		{
			Product? product = await this._context.Products.FindAsync( id );

			if( product != null )
			{
				try
				{
					return product;
				}
				catch( DbUpdateException ex )
				{
					throw new Exception( "Un error ocurrió mientras se consultaba el producto", ex );
				}
			}

			else
			{
				throw new NotFoundException( "Producto no encontrado" );
			}
		}

		public Product CreateProduct( Product product )
		{
			try
			{
				this._context.Products.Add( product );
				this._context.SaveChanges();
				return product;
			}

			catch( DbUpdateException ex )
			{
				throw new Exception( "Un error ocurrió mientras se creaba el producto", ex );
			}
		}

		public void DeleteProduct( int id )
		{
			Product? product = this._context.Products.FirstOrDefault( x => x.Id == id );

			if ( product != null )
			{
				try
				{
					this._context.Products.Remove( product );
					this._context.SaveChanges();
				}
				catch( DbUpdateException ex )
				{
					throw new Exception( "Un error ocurrió mientras se eliminaba el producto", ex );
				}
			}

			else
			{
				throw new NotFoundException( "Producto no encontrado" );
			}
		}

		public Product UpdateProduct( Product product )
		{
			Product? productToUpdate = this._context.Products.FirstOrDefault( x => x.Id == product.Id );

			if ( productToUpdate != null )
			{
				try
				{
					productToUpdate.Enabled = product.Enabled;
					productToUpdate.OrderProducts = product.OrderProducts;
					productToUpdate.Inventory = product.Inventory;
					productToUpdate.Max = product.Max;
					productToUpdate.Min = product.Min;
					productToUpdate.Name = product.Name;

					this._context.SaveChanges();
					
					return productToUpdate;
				}
				catch ( DbUpdateException ex )
				{
					throw new Exception( "Un error ocurrió mientras se actualizaba el producto", ex );
				}
			}

			else
			{
				throw new NotFoundException( "Producto no encontrado" );
			}			
		}
	}
}