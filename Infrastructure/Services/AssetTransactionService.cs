using System.Net;
using Domain.Dtos.AssetTransactionDto;
using Domain.Entities;
using Infrastructure.Interfaces;
using Infrastructure.Repositories.AssetTransactionRepositories;
using Infrastructure.Response;

namespace Infrastructure.Services;

public class AssetTransactionService(IAssetTransactionRepository repository) : IAssetTransactionService
{
    public async Task<PaginationResponse<List<GetAssetTransactionDto>>> GetAllAssetTransactionAsync(
        AssetTransactionFilter filter)
    {
        var assetTransaction = await repository.GetAll(filter);
        var totalRecords = assetTransaction.Count;
        var data = assetTransaction
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();
        var result = data.Select(a => new GetAssetTransactionDto()
        {
            Id = a.Id,
            FixedAssetId = a.FixedAssetId,
            InventoryItemId = a.InventoryItemId,
            TransactionType = a.TransactionType,
            TransactionDate = a.TransactionDate,
            FromEmployeeId = a.FromEmployeeId,
            ToEmployeeId = a.ToEmployeeId
        }).ToList();
        return new PaginationResponse<List<GetAssetTransactionDto>>(result, totalRecords, filter.PageNumber,
            filter.PageSize);
    }

    public async Task<ApiResponse<GetAssetTransactionDto>> GetByIdAsync(int id)
    {
        var assetTransaction = await repository.GetAssetTransaction(q => q.Id == id);
        if (assetTransaction == null)
        {
            return new ApiResponse<GetAssetTransactionDto>(HttpStatusCode.NotFound, "AssetTransaction not found");
        }

        var result = new GetAssetTransactionDto()
        {
            Id = assetTransaction.Id,
            FixedAssetId = assetTransaction.FixedAssetId,
            InventoryItemId = assetTransaction.InventoryItemId,
            TransactionType = assetTransaction.TransactionType,
            TransactionDate = assetTransaction.TransactionDate,
            FromEmployeeId = assetTransaction.FromEmployeeId,
            ToEmployeeId = assetTransaction.ToEmployeeId
        };
        return new ApiResponse<GetAssetTransactionDto>(result);
    }

    public async Task<ApiResponse<string>> CreateAsync(AddAssetTransactionDto request)
    {
        var assetTransaction = new AssetTransaction()
        {
            FixedAssetId = request.FixedAssetId,
            InventoryItemId = request.InventoryItemId,
            TransactionType = request.TransactionType,
            TransactionDate = request.TransactionDate,
            FromEmployeeId = request.FromEmployeeId,
            ToEmployeeId = request.ToEmployeeId
        };
        var result = await repository.CreateAssetTransaction(assetTransaction);

        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }

    public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateAssetTransactionDto request)
    {
        var assetTransaction = await repository.GetAssetTransaction(q => q.Id == id);
        if (assetTransaction == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "AssetTransaction");
        }

        assetTransaction.FixedAssetId = request.FixedAssetId;
        assetTransaction.InventoryItemId = request.InventoryItemId;
        assetTransaction.TransactionType = request.TransactionType;
        assetTransaction.TransactionDate = request.TransactionDate;
        assetTransaction.FromEmployeeId = request.FromEmployeeId;
        assetTransaction.ToEmployeeId = request.ToEmployeeId;
        var result = await repository.UpdateAssetTransaction(assetTransaction);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }

    public async Task<ApiResponse<string>> DeleteAsync(int id)
    {
        var assetTransaction = await repository.GetAssetTransaction(q => q.Id == id);
        if (assetTransaction == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "AssetTransaction");
        }

        var result = await repository.DeleteAssetTransaction(assetTransaction);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }
}