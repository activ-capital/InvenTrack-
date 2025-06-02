namespace Domain.Dtos;

public class UpdateDepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? EmployeeId { get; set; }
}