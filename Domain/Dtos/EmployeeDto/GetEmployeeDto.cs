using Domain.Dtos.FixedAssetDto;
using Domain.Dtos.InventoryItem;
using Domain.Enum;

namespace Domain.Dtos.EmployeeDto;

public class GetEmployeeDto
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public Role Role { get; set; }
    public string? ProfileImagePath { get; set; }
    public int PositionId { get; set; }
    public int? SubDepartmentId { get; set; }
    public List<GetFixedAssetDto> FixedAssets { get; set; } = new();
    public List<GetInventoryItemDto> InventoryItems { get; set; } = new();
}