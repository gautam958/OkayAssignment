using Microsoft.EntityFrameworkCore.Migrations;

namespace OkayAssignment.Migrations.AppDB
{
    public partial class modifytransaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Transaction",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_PropertyId",
                table: "Transaction",
                column: "PropertyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Property_PropertyId",
                table: "Transaction",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Property_PropertyId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_PropertyId",
                table: "Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Transaction",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
