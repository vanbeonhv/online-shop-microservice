using Product.API.Entities;

namespace Product.API.Persistence;

public class ProductContextSeed
{
    public static async Task SeedProductAsync(ProductContext productContext, Serilog.ILogger logger)
    {
        if (!productContext.CatalogProducts.Any())
        {
            productContext.AddRange(GetCatalogProducts());
            await productContext.SaveChangesAsync();
            logger.Information("Seeded data for ProductDb ");
        }
    }


    private static IEnumerable<CatalogProduct> GetCatalogProducts()
    {
        return new List<CatalogProduct>()
        {
            new() { No = "Lotus", Name = "Laptop", Description = "High-performance laptop", Price = 999.99M },
            new() { No = "IP003", Name = "Smartphone", Description = "Latest model smartphone", Price = 799.99M },
            new() { No = "IP001", Name = "Headphones", Description = "Noise-cancelling headphones", Price = 199.99M }
        };
    }
}