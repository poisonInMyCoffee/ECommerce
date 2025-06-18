using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShoppingApp.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddSessionIdandChangingfewNames : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Paymentdate",
                table: "OrderHeaders",
                newName: "PaymentDate");

            migrationBuilder.AddColumn<string>(
                name: "SessionId",
                table: "OrderHeaders",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SessionId",
                table: "OrderHeaders");

            migrationBuilder.RenameColumn(
                name: "PaymentDate",
                table: "OrderHeaders",
                newName: "Paymentdate");
        }
    }
}
