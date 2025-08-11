using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class update : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Employees_EmployeeId",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_SubDepartments_Employees_EmployeeId",
                table: "SubDepartments");

            migrationBuilder.DropIndex(
                name: "IX_SubDepartments_EmployeeId",
                table: "SubDepartments");

            migrationBuilder.DropIndex(
                name: "IX_Departments_EmployeeId",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "SubDepartments");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Departments");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "SubDepartments",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "Departments",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SubDepartments_EmployeeId",
                table: "SubDepartments",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_EmployeeId",
                table: "Departments",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Employees_EmployeeId",
                table: "Departments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SubDepartments_Employees_EmployeeId",
                table: "SubDepartments",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
