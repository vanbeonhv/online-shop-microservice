using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Product.API.Entities;
using Product.API.Repositories.Interfaces;
using Shared.DTOs.Product;

namespace Product.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    #region Additional Resources

    [HttpGet("get-products-by-no/{productNo}")]
    public async Task<IActionResult> GetProductsByNo(string productNo)
    {
        var products = await _repository.GetProductsByNo(productNo);
        if (products == null)
            return NotFound();

        var result = _mapper.Map<ProductDto>(products);
        return Ok(result);
    }

    #endregion

    #region CRUD

    [HttpGet]
    public async Task<IActionResult> GetAllProducts()
    {
        var products = await _repository.GetProducts();
        var result = _mapper.Map<List<ProductDto>>(products);
        return Ok(result);
    }

    [HttpGet("{id:long}")]
    public async Task<IActionResult> GetProductById(long id)
    {
        var product = await _repository.GetProductById(id);
        if (product == null)
            return NotFound();

        var result = _mapper.Map<ProductDto>(product);
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateProduct([FromBody] ProductCreateDto productCreateDto)
    {
        var entity = await _repository.GetProductsByNo(productCreateDto.No);
        if (entity != null)
        {
            return BadRequest("Product with this No already exists.");
        }

        var product = _mapper.Map<CatalogProduct>(productCreateDto);
        await _repository.CreateProduct(product);
        await _repository.SaveChangesAsync();

        var result = _mapper.Map<ProductDto>(product);
        return Ok(result);
    }

    [HttpPut("{id:long}")]
    public async Task<IActionResult> UpdateProduct(long id, [FromBody] ProductUpdateDto productUpdateDto)
    {
        var product = await _repository.GetProductById(id);
        if (product == null)
            return NotFound();

        _mapper.Map(productUpdateDto, product);
        await _repository.UpdateProduct(product);
        await _repository.SaveChangesAsync();

        return NoContent();
    }

    [HttpDelete("{id:long}")]
    public async Task<IActionResult> DeleteProduct(long id)
    {
        var product = await _repository.GetProductById(id);
        if (product == null)
            return NotFound();

        await _repository.DeleteProduct(product);
        await _repository.SaveChangesAsync();

        return NoContent();
    }

    #endregion
}