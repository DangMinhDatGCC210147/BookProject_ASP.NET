using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class DB : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 18, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7850), new DateTime(2023, 10, 4, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7837) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 21, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7853), new DateTime(2023, 10, 8, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7852) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 16, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7855), new DateTime(2023, 10, 10, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7854) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 16, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7903));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 16, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7906));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 19, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7908));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 17, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7910));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 20, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7911));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 18, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7912));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 22, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7914));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 21, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7915));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 25, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7919));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 23, 0, 35, 57, 617, DateTimeKind.Local).AddTicks(7920));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 17, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6424), new DateTime(2023, 10, 3, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6408) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 20, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6427), new DateTime(2023, 10, 7, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6426) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 15, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6428), new DateTime(2023, 10, 9, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6427) });

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 15, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6470));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 15, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6473));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 18, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6475));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 16, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6476));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 19, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6477));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 17, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6478));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 21, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6479));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 20, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6480));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 24, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6481));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 22, 8, 21, 44, 813, DateTimeKind.Local).AddTicks(6482));
        }
    }
}
