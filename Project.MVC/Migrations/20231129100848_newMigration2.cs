using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.MVC.Migrations
{
    public partial class newMigration2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "VehicleMakeId",
                table: "VehicleModel",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_VehicleModel_VehicleMakeId",
                table: "VehicleModel",
                column: "VehicleMakeId");

            migrationBuilder.AddForeignKey(
                name: "FK_VehicleModel_VehicleMake_VehicleMakeId",
                table: "VehicleModel",
                column: "VehicleMakeId",
                principalTable: "VehicleMake",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VehicleModel_VehicleMake_VehicleMakeId",
                table: "VehicleModel");

            migrationBuilder.DropIndex(
                name: "IX_VehicleModel_VehicleMakeId",
                table: "VehicleModel");

            migrationBuilder.DropColumn(
                name: "VehicleMakeId",
                table: "VehicleModel");
        }
    }
}
