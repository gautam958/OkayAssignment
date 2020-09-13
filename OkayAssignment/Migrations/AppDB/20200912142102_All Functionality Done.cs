using Microsoft.EntityFrameworkCore.Migrations;

namespace OkayAssignment.Migrations.AppDB
{
    public partial class AllFunctionalityDone : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Property_PropertyId",
                table: "Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Transaction",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Property_PropertyId",
                table: "Transaction",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Property_PropertyId",
                table: "Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "PropertyId",
                table: "Transaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Property_PropertyId",
                table: "Transaction",
                column: "PropertyId",
                principalTable: "Property",
                principalColumn: "PropertyId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
