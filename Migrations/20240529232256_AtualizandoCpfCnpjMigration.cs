using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClientSupplierApi.Migrations
{
    /// <inheritdoc />
    public partial class AtualizandoCpfCnpjMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CpfCnpj",
                table: "Customer_Supplier",
                type: "varchar(14)",
                maxLength: 14,
                nullable: false,
                defaultValue: "")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpfCnpj",
                table: "Customer_Supplier");
        }
    }
}
