using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace webapp.Migrations
{
    public partial class ticketreservationcontext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "benutzer",
                columns: table => new
                {
                    BenutzerId = table.Column<Guid>(nullable: false),
                    Firstname = table.Column<string>(nullable: false),
                    Lastname = table.Column<string>(nullable: false),
                    Email = table.Column<string>(maxLength: 100, nullable: false),
                    Password = table.Column<string>(nullable: false),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_benutzer", x => x.BenutzerId);
                });

            migrationBuilder.CreateTable(
                name: "fahrer",
                columns: table => new
                {
                    FahrerId = table.Column<Guid>(nullable: false),
                    FahrerName = table.Column<string>(nullable: true),
                    FahrerVorName = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_fahrer", x => x.FahrerId);
                });

            migrationBuilder.CreateTable(
                name: "bus",
                columns: table => new
                {
                    BusId = table.Column<Guid>(nullable: false),
                    Image = table.Column<string>(nullable: false),
                    StartZiel = table.Column<string>(nullable: false),
                    EndZiel = table.Column<string>(nullable: false),
                    BusName = table.Column<string>(nullable: false),
                    AbfahrtsZeit = table.Column<DateTime>(nullable: false),
                    KlimaAnlage = table.Column<bool>(nullable: false),
                    Unterhaltung = table.Column<bool>(nullable: false),
                    FahrerId = table.Column<Guid>(nullable: false),
                    BusCategory = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bus", x => x.BusId);
                    table.ForeignKey(
                        name: "FK_bus_fahrer_FahrerId",
                        column: x => x.FahrerId,
                        principalTable: "fahrer",
                        principalColumn: "FahrerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "sitzplatz",
                columns: table => new
                {
                    SitzplatzId = table.Column<Guid>(nullable: false),
                    BusId = table.Column<Guid>(nullable: false),
                    Nummer = table.Column<int>(nullable: false),
                    IsVerfugbar = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_sitzplatz", x => x.SitzplatzId);
                    table.ForeignKey(
                        name: "FK_sitzplatz_bus_BusId",
                        column: x => x.BusId,
                        principalTable: "bus",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reservation",
                columns: table => new
                {
                    ReservationId = table.Column<Guid>(nullable: false),
                    SitzplatzId = table.Column<Guid>(nullable: false),
                    BenutzerId = table.Column<Guid>(nullable: false),
                    BusId = table.Column<Guid>(nullable: false),
                    ReservationDate = table.Column<DateTime>(nullable: false),
                    IsReserved = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reservation", x => x.ReservationId);
                    table.ForeignKey(
                        name: "FK_reservation_benutzer_BenutzerId",
                        column: x => x.BenutzerId,
                        principalTable: "benutzer",
                        principalColumn: "BenutzerId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservation_bus_BusId",
                        column: x => x.BusId,
                        principalTable: "bus",
                        principalColumn: "BusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reservation_sitzplatz_SitzplatzId",
                        column: x => x.SitzplatzId,
                        principalTable: "sitzplatz",
                        principalColumn: "SitzplatzId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bus_FahrerId",
                table: "bus",
                column: "FahrerId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_reservation_BenutzerId",
                table: "reservation",
                column: "BenutzerId");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_BusId",
                table: "reservation",
                column: "BusId");

            migrationBuilder.CreateIndex(
                name: "IX_reservation_SitzplatzId",
                table: "reservation",
                column: "SitzplatzId");

            migrationBuilder.CreateIndex(
                name: "IX_sitzplatz_BusId",
                table: "sitzplatz",
                column: "BusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "reservation");

            migrationBuilder.DropTable(
                name: "benutzer");

            migrationBuilder.DropTable(
                name: "sitzplatz");

            migrationBuilder.DropTable(
                name: "bus");

            migrationBuilder.DropTable(
                name: "fahrer");
        }
    }
}
