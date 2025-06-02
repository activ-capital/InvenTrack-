using System.Linq.Expressions;
using System.Net;
using Domain.Dtos.EmployeeDto;
using Domain.Dtos.PositionDto;
using Domain.Entities;
using Domain.Filter;
using Infrastructure.Interfaces;
using Infrastructure.Repositories.PositionRepositories;
using Infrastructure.Response;

namespace Infrastructure.Services;

public class PositionService(IPositionRepository repository) : IPositionService
{
    public async Task<PaginationResponse<List<GetPositionDto>>> GetAllPositionAsync(PositionFilter filter)
    {
        var positions = await repository.GetAll(filter);
        var totalRecords = positions.Count;
        var data = positions
            .Skip((filter.PageNumber - 1) * filter.PageSize)
            .Take(filter.PageSize)
            .ToList();
        var result = positions.Select(p => new GetPositionDto
        {
            Id = p.Id,
            Name = p.Name,
            Employees = p.Employees.Select(e => new GetEmployeeDto()
            {
                Id = e.Id,
                FullName = e.FullName,
                Role = e.Role,
                PositionId = e.PositionId,
                SubDepartmentId = e.SubDepartmentId,
            }).ToList()
        }).ToList();
        return new PaginationResponse<List<GetPositionDto>>(result, totalRecords, filter.PageNumber,
            filter.PageSize);
    }

    public async Task<ApiResponse<GetPositionDto>> GetByIdAsync(int id)
    {
        var position = await repository.GetPosition(q => q.Id == id);
        if (position == null)
        {
            return new ApiResponse<GetPositionDto>(HttpStatusCode.NotFound, "Position not found");
        }

        var result = new GetPositionDto()
        {
            Id = position.Id,
            Name = position.Name,
            Employees = position.Employees.Select(e => new GetEmployeeDto()
            {
                Id = e.Id,
                FullName = e.FullName,
                Role = e.Role,
                PositionId = e.PositionId,
                SubDepartmentId = e.SubDepartmentId,
            }).ToList()
        };
        return new ApiResponse<GetPositionDto>(result);
    }

    public async Task<ApiResponse<string>> CreateAsync(AddPositionDto request)
    {
        var position = new Position()
        {
            Name = request.Name,
        };
        var result = await repository.CreatePosition(position);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }

    public async Task<ApiResponse<string>> UpdateAsync(int id, UpdatePositionDto request)
    {
        var position = await repository.GetPosition(q => q.Id == id);
        if (position == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Position not found");
        }

        position.Id = request.Id;
        position.Name = request.Name;
        var result = await repository.UpdatePosition(position);
        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }

    public async Task<ApiResponse<string>> DeleteAsync(int id)
    {
        var position = await repository.GetPosition(q => q.Id == id);
        if (position == null)
        {
            return new ApiResponse<string>(HttpStatusCode.NotFound, "Position not found");
        }

        var result = await repository.DeletePosition(position);

        return result == 1
            ? new ApiResponse<string>("Success")
            : new ApiResponse<string>(HttpStatusCode.BadRequest, "Failed");
    }
}