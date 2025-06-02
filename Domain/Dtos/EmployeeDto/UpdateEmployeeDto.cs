using Domain.Enum;
using Microsoft.AspNetCore.Http;

namespace Domain.Dtos.EmployeeDto;

public class UpdateEmployeeDto
{
    public int Id { get; set; }
    public required string FullName { get; set; }
    public Role Role { get; set; }
    public IFormFile? ProfileImage { get; set; }
    public int PositionId { get; set; }
    public int? SubDepartmentId { get; set; }
}