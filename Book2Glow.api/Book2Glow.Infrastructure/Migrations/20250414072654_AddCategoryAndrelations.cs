using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2Glow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddCategoryAndrelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    C_Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    C_Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.C_Id);
                });

            migrationBuilder.CreateTable(
                name: "BusinessModelCategoryModel",
                columns: table => new
                {
                    BusinessesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoriesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessModelCategoryModel", x => new { x.BusinessesId, x.CategoriesId });
                    table.ForeignKey(
                        name: "FK_BusinessModelCategoryModel_Business_BusinessesId",
                        column: x => x.BusinessesId,
                        principalTable: "Business",
                        principalColumn: "B_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessModelCategoryModel_Category_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Category",
                        principalColumn: "C_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BusinessModelCategoryModel_CategoriesId",
                table: "BusinessModelCategoryModel",
                column: "CategoriesId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BusinessModelCategoryModel");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
