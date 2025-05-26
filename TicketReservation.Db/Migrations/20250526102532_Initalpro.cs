using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketReservation.Db.Migrations
{
    public partial class Initalpro : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservation", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketReservation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReservationId = table.Column<int>(type: "INTEGER", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 80, nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketReservation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketReservation_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Name 1" });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Name 2" });

            migrationBuilder.InsertData(
                table: "Reservation",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Name 3" });

            migrationBuilder.CreateIndex(
                name: "IX_TicketReservation_ReservationId",
                table: "TicketReservation",
                column: "ReservationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketReservation");

            migrationBuilder.DropTable(
                name: "Reservation");
        }
    }
}
