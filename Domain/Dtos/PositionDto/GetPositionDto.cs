using Domain.Dtos.EmployeeDto;

namespace Domain.Dtos.PositionDto;

public class  GetPositionDto
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public  List<GetEmployeeDto> Employees { get; set; } = new();
}