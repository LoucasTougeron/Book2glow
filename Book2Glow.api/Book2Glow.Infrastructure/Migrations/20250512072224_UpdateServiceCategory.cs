using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Book2Glow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdateServiceCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Business_AspNetUsers_B_UserId",
                table: "Business");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_Category_S_CategoryId",
                table: "Service");

            migrationBuilder.DropTable(
                name: "BusinessModelCategoryModel");

            migrationBuilder.RenameColumn(
                name: "S_CategoryId",
                table: "Service",
                newName: "S_BusinessCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_S_CategoryId",
                table: "Service",
                newName: "IX_Service_S_BusinessCategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "B_UserId",
                table: "Business",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "CategoryModelId",
                table: "Business",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BusinessCategories",
                columns: table => new
                {
                    BC_Id = table.Column<Guid>(type: "uuid", nullable: false),
                    BC_BusinessId = table.Column<Guid>(type: "uuid", nullable: false),
                    BC_CategoryId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BusinessCategories", x => x.BC_Id);
                    table.ForeignKey(
                        name: "FK_BusinessCategories_Business_BC_BusinessId",
                        column: x => x.BC_BusinessId,
                        principalTable: "Business",
                        principalColumn: "B_Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BusinessCategories_Category_BC_CategoryId",
                        column: x => x.BC_CategoryId,
                        principalTable: "Category",
                        principalColumn: "C_Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Business_CategoryModelId",
                table: "Business",
                column: "CategoryModelId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCategories_BC_BusinessId",
                table: "BusinessCategories",
                column: "BC_BusinessId");

            migrationBuilder.CreateIndex(
                name: "IX_BusinessCategories_BC_CategoryId",
                table: "BusinessCategories",
                column: "BC_CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Business_AspNetUsers_B_UserId",
                table: "Business",
                column: "B_UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Business_Category_CategoryModelId",
                table: "Business",
                column: "CategoryModelId",
                principalTable: "Category",
                principalColumn: "C_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Service_BusinessCategories_S_BusinessCategoryId",
                table: "Service",
                column: "S_BusinessCategoryId",
                principalTable: "BusinessCategories",
                principalColumn: "BC_Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Business_AspNetUsers_B_UserId",
                table: "Business");

            migrationBuilder.DropForeignKey(
                name: "FK_Business_Category_CategoryModelId",
                table: "Business");

            migrationBuilder.DropForeignKey(
                name: "FK_Service_BusinessCategories_S_BusinessCategoryId",
                table: "Service");

            migrationBuilder.DropTable(
                name: "BusinessCategories");

            migrationBuilder.DropIndex(
                name: "IX_Business_CategoryModelId",
                table: "Business");

            migrationBuilder.DropColumn(
                name: "CategoryModelId",
                table: "Business");

            migrationBuilder.RenameColumn(
                name: "S_BusinessCategoryId",
                table: "Service",
                newName: "S_CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Service_S_BusinessCategoryId",
                table: "Service",
                newName: "IX_Service_S_CategoryId");

            migrationBuilder.AlterColumn<string>(
                name: "B_UserId",
                table: "Business",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "BusinessModelCategoryModel",
                columns: table => new
                {
                    BusinessesId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoriesId = table.Column<Guid>(type: "uuid", nullable: false)
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

            migrationBuilder.AddForeignKey(
                name: "FK_Business_AspNetUsers_B_UserId",
                table: "Business",
                column: "B_UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Service_Category_S_CategoryId",
                table: "Service",
                column: "S_CategoryId",
                principalTable: "Category",
                principalColumn: "C_Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
