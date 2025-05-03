using Contracts.Common.Interfaces;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Product.API.Entities;
using Product.API.Persistence;
using Product.API.Repositories.Interfaces;

namespace Product.API.Repositories;

public class ProductRepository : RepositoryBaseAsync<CatalogProduct, long, ProductContext>, IProductRepository
{
    public ProductRepository(ProductContext dbContext, IUnitOfWork<ProductContext> unitOfWork) : base(dbContext,
        unitOfWork)
    {
    }

    public async Task<List<CatalogProduct>> GetProducts() => await FindAll().ToListAsync();

    public async Task<CatalogProduct> GetProductById(long id) =>
        await FindByCondition(x => x.Id == id).FirstOrDefaultAsync();

    public async Task CreateProduct(CatalogProduct product) => await CreateAsync(product);


    public async Task UpdateProduct(CatalogProduct product) => await UpdateAsync(product);

    public async Task DeleteProduct(CatalogProduct product) => await DeleteAsync(product);

    public async Task<CatalogProduct> GetProductsByNo(string productNo)
    {
        return await FindByCondition(x => x.No == productNo).FirstOrDefaultAsync();
    }
}