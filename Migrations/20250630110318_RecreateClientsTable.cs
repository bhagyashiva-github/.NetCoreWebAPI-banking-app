using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace coreAPI_banking_app.Migrations
{
    /// <inheritdoc />
    public partial class RecreateClientsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    clientid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    fullname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    contactnumber = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    accounttype = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    createdon = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("clients_pkey", x => x.clientid);
                });

            migrationBuilder.CreateTable(
                name: "instruments",
                columns: table => new
                {
                    instrumentid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    type = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    market = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    tickersymbol = table.Column<string>(type: "character varying(15)", maxLength: 15, nullable: true),
                    currency = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("instruments_pkey", x => x.instrumentid);
                });

            migrationBuilder.CreateTable(
                name: "portfolios",
                columns: table => new
                {
                    portfolioid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clientid = table.Column<int>(type: "integer", nullable: true),
                    instrumentid = table.Column<int>(type: "integer", nullable: true),
                    quantityheld = table.Column<int>(type: "integer", nullable: true),
                    averagecost = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true),
                    lastupdated = table.Column<DateTime>(type: "timestamp without time zone", nullable: true, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("portfolios_pkey", x => x.portfolioid);
                    table.ForeignKey(
                        name: "portfolios_clientid_fkey",
                        column: x => x.clientid,
                        principalTable: "clients",
                        principalColumn: "clientid");
                    table.ForeignKey(
                        name: "portfolios_instrumentid_fkey",
                        column: x => x.instrumentid,
                        principalTable: "instruments",
                        principalColumn: "instrumentid");
                });

            migrationBuilder.CreateTable(
                name: "trades",
                columns: table => new
                {
                    tradeid = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    clientid = table.Column<int>(type: "integer", nullable: true),
                    instrumentid = table.Column<int>(type: "integer", nullable: true),
                    tradetype = table.Column<string>(type: "character varying(10)", maxLength: 10, nullable: true),
                    quantity = table.Column<int>(type: "integer", nullable: true),
                    priceperunit = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: true),
                    tradedate = table.Column<DateOnly>(type: "date", nullable: true, defaultValueSql: "CURRENT_DATE"),
                    settlementdate = table.Column<DateOnly>(type: "date", nullable: true),
                    brokername = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    tradestatus = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true, defaultValueSql: "'PENDING'::character varying")
                },
                constraints: table =>
                {
                    table.PrimaryKey("trades_pkey", x => x.tradeid);
                    table.ForeignKey(
                        name: "trades_clientid_fkey",
                        column: x => x.clientid,
                        principalTable: "clients",
                        principalColumn: "clientid");
                    table.ForeignKey(
                        name: "trades_instrumentid_fkey",
                        column: x => x.instrumentid,
                        principalTable: "instruments",
                        principalColumn: "instrumentid");
                });

            migrationBuilder.CreateIndex(
                name: "IX_portfolios_clientid",
                table: "portfolios",
                column: "clientid");

            migrationBuilder.CreateIndex(
                name: "IX_portfolios_instrumentid",
                table: "portfolios",
                column: "instrumentid");

            migrationBuilder.CreateIndex(
                name: "IX_trades_clientid",
                table: "trades",
                column: "clientid");

            migrationBuilder.CreateIndex(
                name: "IX_trades_instrumentid",
                table: "trades",
                column: "instrumentid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "portfolios");

            migrationBuilder.DropTable(
                name: "trades");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "instruments");
        }
    }
}
