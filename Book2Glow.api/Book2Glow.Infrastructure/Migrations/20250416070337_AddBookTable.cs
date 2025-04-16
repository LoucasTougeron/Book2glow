using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2Glow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddBookTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    Book_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Book_UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Book_BusinessId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Book_BookingId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.Book_Id);
                    table.ForeignKey(
                        name: "FK_Book_AspNetUsers_Book_UserId",
                        column: x => x.Book_UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Book_Bookings_Book_BookingId",
                        column: x => x.Book_BookingId,
                        principalTable: "Bookings",
                        principalColumn: "B_Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Book_Business_Book_BusinessId",
                        column: x => x.Book_BusinessId,
                        principalTable: "Business",
                        principalColumn: "B_Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Book_Book_BookingId",
                table: "Book",
                column: "Book_BookingId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Book_BusinessId",
                table: "Book",
                column: "Book_BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_Book_Book_UserId",
                table: "Book",
                column: "Book_UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
