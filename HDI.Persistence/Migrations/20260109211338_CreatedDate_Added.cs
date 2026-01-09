using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace HDI.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreatedDate_Added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "WorkItems",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Partners",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Partners",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "Partners",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "Partners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Partners",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "ModifiedBy",
                table: "Partners",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedDate",
                table: "Partners",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Keywords",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Agreements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "Partners",
                columns: new[] { "Id", "ApiKey", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsActive", "IsDeleted", "ModifiedBy", "ModifiedDate", "Name" },
                values: new object[] { 1, "hdi-test-key-123", "System", new DateTime(2026, 1, 10, 0, 13, 38, 615, DateTimeKind.Local).AddTicks(5370), null, null, true, false, null, null, "HDI Sigorta A.Ş." });

            migrationBuilder.InsertData(
                table: "Agreements",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "RiskLimit", "TenantId", "Title" },
                values: new object[] { 1, "System", new DateTime(2026, 1, 10, 0, 13, 38, 620, DateTimeKind.Local).AddTicks(5620), null, null, false, null, null, 150m, 1, "Kasko Risk Analizi" });

            migrationBuilder.InsertData(
                table: "Keywords",
                columns: new[] { "Id", "AgreementId", "CreatedBy", "CreatedDate", "DeletedBy", "DeletedDate", "IsDeleted", "ModifiedBy", "ModifiedDate", "RiskWeight", "TenantId", "Word" },
                values: new object[,]
                {
                    { 1, 1, "System", new DateTime(2026, 1, 10, 0, 13, 38, 620, DateTimeKind.Local).AddTicks(6190), null, null, false, null, null, 80m, 1, "kaza" },
                    { 2, 1, "System", new DateTime(2026, 1, 10, 0, 13, 38, 620, DateTimeKind.Local).AddTicks(6200), null, null, false, null, null, 40m, 1, "sel" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Keywords",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Keywords",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Agreements",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Partners",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "WorkItems");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "ModifiedBy",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "ModifiedDate",
                table: "Partners");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Keywords");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "Agreements");
        }
    }
}
