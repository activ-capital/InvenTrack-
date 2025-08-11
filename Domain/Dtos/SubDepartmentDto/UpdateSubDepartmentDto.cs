namespace Domain.Dtos.SubDepartmentDto;

public class UpdateSubDepartmentDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int DepartmentId { get; set; }
    // Начальник подотдела
}