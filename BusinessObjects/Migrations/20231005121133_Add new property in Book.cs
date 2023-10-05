using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class AddnewpropertyinBook : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Publisher",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 1,
                column: "Publisher",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Books",
                keyColumn: "Id",
                keyValue: 2,
                column: "Publisher",
                value: 0);

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 12, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3393), new DateTime(2023, 9, 28, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3390) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 15, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3394), new DateTime(2023, 10, 2, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3394) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 10, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3395), new DateTime(2023, 10, 4, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3395) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddDate",
                value: new DateTime(2023, 10, 5, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3368));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddDate",
                value: new DateTime(2023, 10, 5, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3378));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddDate",
                value: new DateTime(2023, 10, 5, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3379));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 10, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3419));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 10, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3422));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 13, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3423));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 11, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3424));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 14, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3425));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 12, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3426));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 16, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3427));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 15, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3428));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 19, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3429));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 17, 19, 11, 33, 594, DateTimeKind.Local).AddTicks(3430));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Publisher",
                table: "Books");

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 12, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4842), new DateTime(2023, 9, 28, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4837) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 15, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4844), new DateTime(2023, 10, 2, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4843) });

            migrationBuilder.UpdateData(
                table: "Discounts",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "EndDate", "StartDate" },
                values: new object[] { new DateTime(2023, 10, 10, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4845), new DateTime(2023, 10, 4, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4844) });

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 1,
                column: "AddDate",
                value: new DateTime(2023, 10, 5, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4816));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 2,
                column: "AddDate",
                value: new DateTime(2023, 10, 5, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4827));

            migrationBuilder.UpdateData(
                table: "Genres",
                keyColumn: "Id",
                keyValue: 3,
                column: "AddDate",
                value: new DateTime(2023, 10, 5, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4828));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 1,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 10, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4870));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 2,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 10, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4872));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 3,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 13, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4874));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 4,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 11, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4875));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 5,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 14, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4876));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 6,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 12, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4877));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 7,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 16, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4879));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 8,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 15, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4880));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 9,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 19, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4881));

            migrationBuilder.UpdateData(
                table: "Orders",
                keyColumn: "Id",
                keyValue: 10,
                column: "DeliveryDate",
                value: new DateTime(2023, 10, 17, 17, 2, 38, 39, DateTimeKind.Local).AddTicks(4905));
        }
    }
}
