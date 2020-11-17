using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Mig_OrdersFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date_DateRegistered",
                table: "Orders");

            migrationBuilder.RenameColumn(
                name: "Detail_userId",
                table: "Orders",
                newName: "Detail_UserId");

            migrationBuilder.RenameColumn(
                name: "Detail_totalPrice",
                table: "Orders",
                newName: "Detail_TotalPrice");

            migrationBuilder.RenameColumn(
                name: "Detail_numberOfItems",
                table: "Orders",
                newName: "Detail_NumberOfItems");

            migrationBuilder.RenameColumn(
                name: "Date_dateRegisterd",
                table: "Orders",
                newName: "Date_DateRegisterd");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Detail_UserId",
                table: "Orders",
                newName: "Detail_userId");

            migrationBuilder.RenameColumn(
                name: "Detail_TotalPrice",
                table: "Orders",
                newName: "Detail_totalPrice");

            migrationBuilder.RenameColumn(
                name: "Detail_NumberOfItems",
                table: "Orders",
                newName: "Detail_numberOfItems");

            migrationBuilder.RenameColumn(
                name: "Date_DateRegisterd",
                table: "Orders",
                newName: "Date_dateRegisterd");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date_DateRegistered",
                table: "Orders",
                type: "datetime2",
                nullable: true);
        }
    }
}
