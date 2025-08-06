using System.Net;
using Domain.Dtos.InventoryItem;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Interfaces;
using Infrastructure.Repositories.InventoryItemRepositories;
using Infrastructure.Response;

namespace Infrastructure.Services;

public class InventoryItemService(IInventoryItemRepository repository) : IInventoryItemService
{
    public async Task<PaginationResponse<List<GetInventoryItemDto>>> GetAllInventoryItemAsync(
        InventoryItemFilter filter)
    {
        var inventoryItem = await repository.GetAll(filter);
        var totalRecords = inventoryItem.Count;
        var data = inventoryItem
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();
        var result = data.Select(i => new GetInventoryItemDto()
        {
            Id = i.Id,
            Name = i.Name,
            InventoryNumber = i.InventoryNumber,
            AcquisitionDate = i.AcquisitionDate,
            EmployeeId = i.EmployeeId,
            Unit = i.Unit,
        }).ToList();
        return new PaginationResponse<List<GetInventoryItemDto>>(result, totalRecords, filter.PageNumber,
            filter.PageSize);
    }

    public async Task<ApiResponse<GetInventoryItemDto>> GetByIdAsync(int id)
    {
        var inventoryItem = await repository.GetInventoryItem(q => q.Id == id);
        if (inventoryItem == null)
        {
            return new ApiResponse<GetInventoryItemDto>(HttpStatusCode.NotFound, "InventoryItem not found");
        }

        var result = new GetInventoryItemDto()
        {
            Id = inventoryItem.Id,
            Name = inventoryItem.Name,
            InventoryNumber = inventoryItem.InventoryNumber,
            AcquisitionDate = inventoryItem.AcquisitionDate,
            EmployeeId = inventoryItem.EmployeeId,
            Unit = inventoryItem.Unit,
        };
        return new ApiResponse<GetInventoryItemDto>(result);
    }

    public async Task<ApiResponse<string>> CreateAsync(AddInventoryItemDto request)
    {
        var inventoryItem = new InventoryItem()
        {
            Name = request.Name,
            InventoryNumber = request.InventoryNumber,
            AcquisitionDate = request.AcquisitionDate,
            EmployeeId = request.EmployeeId,
            Unit = request.Unit,
        };
        var result = await repository.CreateInventoryItem(inventoryItem);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }

    public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateInventoryItemDto request)
    {
        var inventoryItem = await repository.GetInventoryItem(q => q.Id == id);
        if (inventoryItem == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "InventoryItem not found");
        }

        inventoryItem.Id = inventoryItem.Id;
        inventoryItem.Name = inventoryItem.Name;
        inventoryItem.InventoryNumber = inventoryItem.InventoryNumber;
        inventoryItem.AcquisitionDate = inventoryItem.AcquisitionDate;
        inventoryItem.EmployeeId = inventoryItem.EmployeeId;
        inventoryItem.Unit = inventoryItem.Unit;
        var result = await repository.UpdateInventoryItem(inventoryItem);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }

    public async Task<ApiResponse<string>> DeleteAsync(int id)
    {
        var inventoryItem = await repository.GetInventoryItem(q => q.Id == id);
        if (inventoryItem == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "InventoryItem not found");
        }

        var result = await repository.DeleteInventoryItem(inventoryItem);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }
}