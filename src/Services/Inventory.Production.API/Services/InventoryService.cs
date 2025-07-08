using AutoMapper;
using Contracts.Common.Interfaces;
using Inventory.Production.API.Persistence;
using Inventory.Production.API.Repositories;
using Inventory.Production.API.Repositories.Interfaces;
using Inventory.Production.API.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Shared.DTOs.Inventory;
using Shared.SeedWork;

namespace Inventory.Production.API.Services;

public class InventoryService : InventoryRepository, IInventoryService
{
    private readonly IInventoryRepository _inventoryRepository;
    private readonly IMapper _mapper;

    public InventoryService(InventoryContext dbContext, IUnitOfWork<InventoryContext> unitOfWork,
        IInventoryRepository inventoryRepository, IMapper mapper) : base(dbContext, unitOfWork)
    {
        _inventoryRepository = inventoryRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<InventoryDto>> GetAllByItemNoAsync(string itemNo)
    {
        var entities = await _inventoryRepository.FindAllByItemNoAsync(itemNo);
        var result = _mapper.Map<IEnumerable<InventoryDto>>(entities);
        return result;
    }

    public async Task<IEnumerable<InventoryDto>> GetAllByItemNoPagingAsync(GetInventoryPagingQuery query)
    {
        var baseQuery = _inventoryRepository.GetBaseQueryByItemNo(query.itemNo, query.SearchTerm);
        var totalItems = await baseQuery.CountAsync();
        var entities = await baseQuery.Skip((query.PageNumber - 1) * query.PageSize).Take(query.PageSize).ToListAsync();
        var dto = _mapper.Map<List<InventoryDto>>(entities);
        var result = new PagedList<InventoryDto>(dto, totalItems, query.PageNumber, query.PageSize);
        return result;
    }

    public async Task<InventoryDto> GetOneByIdAsync(long id)
    {
        var entity = await _inventoryRepository.GetByIdAsync(id);
        var result = _mapper.Map<InventoryDto>(entity);
        return result;
    }

    public async Task<InventoryDto> PurchaseItemAsync(string itemNo, InventoryDto inventoryDto)
    {
        var entity = new Entities.Inventory()
        {
            ItemNo = itemNo,
            Quantity = inventoryDto.Quantity
        };
        await _inventoryRepository.CreateAsync(entity);
        await _inventoryRepository.SaveChangesAsync();
        var result = _mapper.Map<InventoryDto>(entity);
        return result;
    }
}