using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<Department> Departments { get; set; }
    public DbSet<SubDepartment> SubDepartments { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Position> Positions { get; set; }
    public DbSet<FixedAsset> FixedAssets { get; set; }
    public DbSet<InventoryItem> InventoryItems { get; set; }
    public DbSet<AssetTransaction> AssetTransactions { get; set; }
    DbSet<Asset> Assets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Employee>()
            .HasOne(e => e.SubDepartment)
            .WithMany(sd => sd.Employees)
            .HasForeignKey(e => e.SubDepartmentId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<SubDepartment>()
            .HasOne(sd => sd.Employee)        // начальник подотдела
            .WithMany()                      // у начальника нет коллекции подотделов
            .HasForeignKey(sd => sd.EmployeeId)
            .OnDelete(DeleteBehavior.Restrict); 

    }
}