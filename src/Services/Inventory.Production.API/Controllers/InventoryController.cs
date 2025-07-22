using System.ComponentModel.DataAnnotations;
using Inventory.Production.API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Shared.DTOs.Inventory;

namespace Inventory.Production.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InventoryController : ControllerBase
{
    private readonly IInventoryService _inventoryService;

    public InventoryController(IInventoryService inventoryService)
    {
        _inventoryService = inventoryService;
    }

    [HttpGet("items/{itemNo}")]
    public async Task<IActionResult> GetAllByItemNoAsync([Required] string itemNo)
    {
        var result = await _inventoryService.GetAllByItemNoAsync(itemNo);
        return Ok(result);
    }

    [HttpGet("items/{itemNo}/paging")]
    public async Task<IActionResult> GetAllByItemNoPagingAsync([FromQuery] GetInventoryPagingQuery query, string itemNo)
    {
        query.itemNo = itemNo;
        var result = await _inventoryService.GetAllByItemNoPagingAsync(query);
        return Ok(result);
    }
    
    
    [HttpGet("{orderId:long}")]
    public async Task<IActionResult> GetOneByIdAsync(long orderId)
    {
        var result = await _inventoryService.GetOneByIdAsync(orderId);
        return Ok(result);
    }
    /// <summary>
    /// Purchase more product from supplier. Basically is a "Add new" method
    /// </summary>
    /// <param name="itemNo"></param>
    /// <param name="inventoryDto"></param>
    /// <returns></returns>
    [HttpPost("purchase/{itemNo}")]
    public async Task<IActionResult> PurchaseItemAsync(string itemNo, [FromBody] InventoryDto inventoryDto)
    {
        var result = await _inventoryService.PurchaseItemAsync(itemNo, inventoryDto);
        return Ok(result);
    }
}