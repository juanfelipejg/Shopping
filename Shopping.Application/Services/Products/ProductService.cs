﻿namespace Shopping.Application.Services.Products
{
	using System.Data;
	using Dapper;
	using Shopping.Domain.Models.Products;
	using Shopping.Infrastructure.Data;

	public class ProductService: IProductService
	{
		private readonly ShoppingContext _context;
		private readonly IDbConnection _connection;

		public ProductService( ShoppingContext context, IDbConnection dbConnection )
		{
			this._context = context;
			this._connection = dbConnection;
		}

		public IEnumerable<Product> GetProducts( int pageNumber, int pageSize )
		{
			return this._context.Products.Skip( ( pageNumber - 1 ) * pageSize )
										 .Take( pageSize )
										 .ToList();
		}

		public async Task<Product> GetProductAsync( int id )
		{
			return await this._context.Products.FindAsync( id );
		}

		public Product CreateProduct( Product product )
		{
			this._context.Products.Add( product );
			this._context.SaveChanges();
			return product;
		}

		public void DeleteProduct( int id )
		{
			var product = this._context.Products.FirstOrDefault( x => x.Id == id );
			this._context.Products.Remove( product );
			this._context.SaveChanges();
		}

		public Product UpdateProduct( Product product )
		{
			Product productToUpdate = this._context.Products.FirstOrDefault( x => x.Id == product.Id );

			if ( productToUpdate != null )
			{
				productToUpdate.Enabled = product.Enabled;
				productToUpdate.OrderProducts = product.OrderProducts;
				productToUpdate.Inventory = product.Inventory;
				productToUpdate.Max = product.Max;
				productToUpdate.Min = product.Min;
				productToUpdate.Name = product.Name;
			}

			this._context.SaveChanges();

			return productToUpdate;
		}
	}
}