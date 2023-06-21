using Shopping.Domain.Models.Products;

namespace Shopping.Application.Services.Products
{
	public interface IProductService
	{
		IEnumerable<Product> GetAll();

		Task<Product> GetByIdAsync( int id );

		Product Add( Product product );

		//Product Update( Product product );

		//void Delete( int id );
	}
}
