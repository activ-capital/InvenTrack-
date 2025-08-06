using System.Net;
using Domain.Dtos.FixedAssetDto;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Interfaces;
using Infrastructure.Repositories.FixedAssetRepositories;
using Infrastructure.Response;
namespace Infrastructure.Services;

public class FixedAssetService(IFixedAssetRepository repository) : IFixedAssetService
{
    public async Task<PaginationResponse<List<GetFixedAssetDto>>> GetAllFixedAssetAsync(FixedAssetFilter filter)
    {
        var fixedAssets = await repository.GetAll(filter);
        var totalRecords = fixedAssets.Count;
        var data = fixedAssets
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();
        var result = data.Select(f => new GetFixedAssetDto()
        {
            Id = f.Id,
            Name = f.Name,
            InventoryNumber = f.InventoryNumber,
            AcquisitionDate = f.AcquisitionDate,
            EmployeeId = f.EmployeeId,
            SerialNumber = f.SerialNumber,
            UsefulLifeYears = f.UsefulLifeYears
        }).ToList();
        return new PaginationResponse<List<GetFixedAssetDto>>(result, totalRecords, filter.PageNumber,
            filter.PageSize);
    }

    public async Task<ApiResponse<GetFixedAssetDto>> GetByIdAsync(int id)
    {
        var fixedAsset = await repository.GetFixedAsset(q => q.Id == id);
        if (fixedAsset == null)
        {
            return new ApiResponse<GetFixedAssetDto>(HttpStatusCode.NotFound, "FixedAsset not found");
        }

        var result = new GetFixedAssetDto()
        {
            Id = fixedAsset.Id,
            Name = fixedAsset.Name,
            InventoryNumber = fixedAsset.InventoryNumber,
            AcquisitionDate = fixedAsset.AcquisitionDate,
            EmployeeId = fixedAsset.EmployeeId,
            SerialNumber = fixedAsset.SerialNumber,
            UsefulLifeYears = fixedAsset.UsefulLifeYears
        };
        return new ApiResponse<GetFixedAssetDto>(result);
    }

    public async Task<ApiResponse<string>> CreateAsync(AddFixedAssetDto request)
    {
        var fixedAsset = new FixedAsset()
        {
            Name = request.Name,
            InventoryNumber = request.InventoryNumber,
            AcquisitionDate = DateTime.UtcNow,
            EmployeeId = request.EmployeeId,
            SerialNumber = request.SerialNumber,
            UsefulLifeYears = request.UsefulLifeYears
        };
        var result = await repository.CreateFixedAsset(fixedAsset);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }

    public async Task<ApiResponse<string>> UpdateAsync(int id, UpdateFixedAssetDto request)
    {
        var fixedAsset = await repository.GetFixedAsset(q => q.Id == id);
        if (fixedAsset == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "FixedAsset not found");
        }

        fixedAsset.Name = request.Name;
        fixedAsset.Name = request.Name;
        fixedAsset.InventoryNumber = request.InventoryNumber;
        fixedAsset.AcquisitionDate = request.AcquisitionDate;
        fixedAsset.EmployeeId = request.EmployeeId;
        fixedAsset.SerialNumber = request.SerialNumber;
        fixedAsset.UsefulLifeYears = request.UsefulLifeYears;
        var result = await repository.UpdateFixedAsset(fixedAsset);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }

    public async Task<ApiResponse<string>> DeleteAsync(int id)
    {
        var fixedAsset = await repository.GetFixedAsset(q => q.Id == id);
        if (fixedAsset == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "FixedAsset not found");
        }

        var result = await repository.DeleteFixedAsset(fixedAsset);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }
}