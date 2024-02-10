using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataLayer.Migrations
{
    public partial class rootDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Coordinates = table.Column<byte[]>(type: "varbinary(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TerrainType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NodeAId = table.Column<int>(type: "int", nullable: false),
                    NodeBId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Connections_Locations_NodeAId",
                        column: x => x.NodeAId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Connections_Locations_NodeBId",
                        column: x => x.NodeBId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TrainCompositions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TrainType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainCompositions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainCompositions_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Locomotives",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nickname = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    CarryingCapacity = table.Column<double>(type: "float", nullable: false),
                    LocomotiveType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    TrainCompositionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locomotives", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Locomotives_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Locomotives_TrainCompositions_TrainCompositionId",
                        column: x => x.TrainCompositionId,
                        principalTable: "TrainCompositions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "TrainCars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TrainCarType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<double>(type: "float", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false),
                    TrainCompositionId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainCars", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TrainCars_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TrainCars_TrainCompositions_TrainCompositionId",
                        column: x => x.TrainCompositionId,
                        principalTable: "TrainCompositions",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Connections_NodeAId",
                table: "Connections",
                column: "NodeAId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_NodeBId",
                table: "Connections",
                column: "NodeBId");

            migrationBuilder.CreateIndex(
                name: "IX_Locomotives_LocationId",
                table: "Locomotives",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Locomotives_TrainCompositionId",
                table: "Locomotives",
                column: "TrainCompositionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainCars_LocationId",
                table: "TrainCars",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainCars_TrainCompositionId",
                table: "TrainCars",
                column: "TrainCompositionId");

            migrationBuilder.CreateIndex(
                name: "IX_TrainCompositions_LocationId",
                table: "TrainCompositions",
                column: "LocationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropTable(
                name: "Locomotives");

            migrationBuilder.DropTable(
                name: "TrainCars");

            migrationBuilder.DropTable(
                name: "TrainCompositions");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
