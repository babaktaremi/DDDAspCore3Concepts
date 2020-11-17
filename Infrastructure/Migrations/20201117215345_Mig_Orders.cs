using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Migrations
{
    public partial class Mig_Orders : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date_dateRegisterd = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Date_DateRegistered = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Detail_userId = table.Column<int>(type: "int", nullable: true),
                    Detail_totalPrice = table.Column<int>(type: "int", nullable: true),
                    Detail_numberOfItems = table.Column<int>(type: "int", nullable: true),
                    IsFinally = table.Column<bool>(type: "bit", nullable: false),
                    OrderState = table.Column<int>(type: "int", nullable: true),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
