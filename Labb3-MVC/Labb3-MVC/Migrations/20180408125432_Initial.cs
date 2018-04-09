using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Labb3MVC.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Salong",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MaxKapacitet = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salong", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Filmer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Biljetter = table.Column<int>(nullable: false),
                    Namn = table.Column<string>(nullable: true),
                    SalongId = table.Column<int>(nullable: true),
                    ShowTime = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Filmer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Filmer_Salong_SalongId",
                        column: x => x.SalongId,
                        principalTable: "Salong",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Filmer_SalongId",
                table: "Filmer",
                column: "SalongId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Filmer");

            migrationBuilder.DropTable(
                name: "Salong");
        }
    }
}
