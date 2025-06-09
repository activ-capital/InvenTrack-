// Infrastructure/Extensions/RegisterService.cs
using System.Text;
using Infrastructure.Data;
using Infrastructure.Interfaces;
using Infrastructure.Repositories.AssetTransactionRepositories;
using Infrastructure.Repositories.DepartmentRepositories;
using Infrastructure.Repositories.EmployeeRepositories;
using Infrastructure.Repositories.FixedAssetRepositories;
using Infrastructure.Repositories.InventoryItemRepositories;
using Infrastructure.Repositories.PositionRepositories;
using Infrastructure.Repositories.SubDepartmentRepositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using IQrCodeService = Infrastructure.Interfaces.IQrCodeService;

namespace Infrastructure.Extensions;

public static class RegisterService
{
    public static void AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<DataContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IDepartmentRepository, DepartmentRepository>();
        services.AddScoped<IDepartmentService, DepartmentService>();

        services.AddScoped<ISubDepartmentRepository, SubDepartmentRepository>();
        services.AddScoped<ISubDepartmentService, SubDepartmentService>();

        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();

        services.AddScoped<IPositionService, PositionService>();
        services.AddScoped<IPositionRepository, PositionRepository>();

        services.AddScoped<IFixedAssetRepository, FixedAssetRepository>();
        services.AddScoped<IFixedAssetService, FixedAssetService>();

        services.AddScoped<IInventoryItemRepository, InventoryItemRepository>();
        services.AddScoped<IInventoryItemService, InventoryItemService>();

        services.AddScoped<IAssetTransactionRepository, AssetTransactionRepository>();
        services.AddScoped<IAssetTransactionService, AssetTransactionService>();
        services.AddScoped<IQrCodeService, QrCodeService>();
    }
}