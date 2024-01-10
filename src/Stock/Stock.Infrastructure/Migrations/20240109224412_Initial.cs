using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Stock.Infrastructure.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Name = table.Column<string>(nullable: true),
                    StockQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductAssociations",
                columns: table => new
                {
                    ParentProductId = table.Column<int>(nullable: false),
                    ChildProductId = table.Column<int>(nullable: false),
                    RequiredQuantity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductAssociations", x => new { x.ParentProductId, x.ChildProductId });
                    table.ForeignKey(
                        name: "FK_ProductAssociations_Products_ChildProductId",
                        column: x => x.ChildProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductAssociations_Products_ParentProductId",
                        column: x => x.ParentProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductAssociations_ChildProductId",
                table: "ProductAssociations",
                column: "ChildProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductAssociations");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
