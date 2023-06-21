using Shopping.Domain.Models.Products;

namespace Shopping.Application.Services.Products
{
	public interface IProductService
	{
		IEnumerable<Product> GetProducts();

		Task<Product> GetProduct( int id );

		Product CreateProduct( Product product );

		Product UpdateProduct( Product product );

		void DeleteProduct( int id );
	}
}
