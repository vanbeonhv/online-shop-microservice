using Contracts.Common.Interfaces;
using Product.API.Entities;
using Product.API.Persistence;

namespace Product.API.Repositories.Interfaces;

public interface IProductRepository: IRepositoryBaseAsync<CatalogProduct, long, ProductContext>
{
    Task<List<CatalogProduct>> GetProducts();
    Task<CatalogProduct> GetProductById(long id);
    Task CreateProduct(CatalogProduct product);
    Task UpdateProduct(CatalogProduct product);
    Task DeleteProduct(CatalogProduct product);
    Task<CatalogProduct> GetProductsByNo(string productNo);
}