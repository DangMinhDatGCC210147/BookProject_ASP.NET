using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class Test_Add_Image : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 21, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6559), new DateTime(2023, 10, 7, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6554) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 24, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6561), new DateTime(2023, 10, 11, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6560) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 19, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6562), new DateTime(2023, 10, 13, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6561) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddDate",
                value: new DateTime(2023, 10, 14, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6510));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddDate",
                value: new DateTime(2023, 10, 14, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6522));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddDate",
                value: new DateTime(2023, 10, 14, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6523));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddDate",
                value: new DateTime(2023, 10, 14, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6523));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddDate",
                value: new DateTime(2023, 10, 14, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6524));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddDate",
                value: new DateTime(2023, 10, 14, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6525));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7,
                column: "AddDate",
                value: new DateTime(2023, 10, 14, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6526));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8,
                column: "AddDate",
                value: new DateTime(2023, 10, 14, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6526));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9,
                column: "AddDate",
                value: new DateTime(2023, 10, 14, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6527));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10,
                column: "AddDate",
                value: new DateTime(2023, 10, 14, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6527));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 19, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6604));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 19, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6606));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 22, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6608));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 20, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6609));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 23, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6610));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 21, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6611));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 25, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6612));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 24, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6613));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 28, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6614));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 26, 1, 28, 45, 68, DateTimeKind.Local).AddTicks(6615));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Books",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 20, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8749), new DateTime(2023, 10, 6, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8743) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 23, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8751), new DateTime(2023, 10, 10, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8751) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 18, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8753), new DateTime(2023, 10, 12, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8753) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddDate",
                value: new DateTime(2023, 10, 13, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8696));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddDate",
                value: new DateTime(2023, 10, 13, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8709));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddDate",
                value: new DateTime(2023, 10, 13, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8710));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 4,
                column: "AddDate",
                value: new DateTime(2023, 10, 13, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8710));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 5,
                column: "AddDate",
                value: new DateTime(2023, 10, 13, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8711));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 6,
                column: "AddDate",
                value: new DateTime(2023, 10, 13, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8712));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 7,
                column: "AddDate",
                value: new DateTime(2023, 10, 13, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8712));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 8,
                column: "AddDate",
                value: new DateTime(2023, 10, 13, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8715));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 9,
                column: "AddDate",
                value: new DateTime(2023, 10, 13, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8715));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 10,
                column: "AddDate",
                value: new DateTime(2023, 10, 13, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8716));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 18, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8790));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 18, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8794));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 21, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8796));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 19, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8797));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 22, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8798));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 20, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8802));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 24, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8804));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 23, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8805));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 27, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8806));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 25, 20, 34, 4, 13, DateTimeKind.Local).AddTicks(8807));
        }
    }
}
