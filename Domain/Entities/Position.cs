using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class Position
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public List<Employee>? Employees { get; set; } 
}