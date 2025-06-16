using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2Glow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddReviewBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Review",
                columns: table => new
                {
                    R_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    R_Stars = table.Column<int>(type: "integer", nullable: false),
                    R_Comments = table.Column<string>(type: "text", nullable: false),
                    R_Datetime = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    R_BookId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Review", x => x.R_Id);
                    table.ForeignKey(
                        name: "FK_Review_Book_R_BookId",
                        column: x => x.R_BookId,
                        principalTable: "Book",
                        principalColumn: "Book_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Review_R_BookId",
                table: "Review",
                column: "R_BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Review");
        }
    }
}
