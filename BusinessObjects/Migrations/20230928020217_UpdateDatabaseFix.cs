using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BusinessObjects.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabaseFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Books_BookID",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Genres_GenreId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Books_Languages_LanguageId",
                table: "Books");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_Books_BookId",
                table: "CartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Books_BookId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Books_BookId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Books_BookId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Books_BookId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Books",
                table: "Books");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "OrderDetails");

            migrationBuilder.RenameTable(
                name: "Books",
                newName: "B");

            migrationBuilder.RenameIndex(
                name: "IX_Books_LanguageId",
                table: "B",
                newName: "IX_B_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_GenreId",
                table: "B",
                newName: "IX_B_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Books_BookID",
                table: "B",
                newName: "IX_B_BookID");

            migrationBuilder.RenameIndex(
                name: "IX_Books_AuthorId",
                table: "B",
                newName: "IX_B_AuthorId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                columns: new[] { "OrderId", "BookId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_B",
                table: "B",
                column: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_B_Authors_AuthorId",
                table: "B",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_B_B_BookID",
                table: "B",
                column: "BookID",
                principalTable: "B",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_B_Genres_GenreId",
                table: "B",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_B_Languages_LanguageId",
                table: "B",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_B_BookId",
                table: "CartDetails",
                column: "BookId",
                principalTable: "B",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_B_BookId",
                table: "Carts",
                column: "BookId",
                principalTable: "B",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_B_BookId",
                table: "Favourites",
                column: "BookId",
                principalTable: "B",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_B_BookId",
                table: "OrderDetails",
                column: "BookId",
                principalTable: "B",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_B_BookId",
                table: "Orders",
                column: "BookId",
                principalTable: "B",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_B_BookId",
                table: "Reviews",
                column: "BookId",
                principalTable: "B",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_B_Authors_AuthorId",
                table: "B");

            migrationBuilder.DropForeignKey(
                name: "FK_B_B_BookID",
                table: "B");

            migrationBuilder.DropForeignKey(
                name: "FK_B_Genres_GenreId",
                table: "B");

            migrationBuilder.DropForeignKey(
                name: "FK_B_Languages_LanguageId",
                table: "B");

            migrationBuilder.DropForeignKey(
                name: "FK_CartDetails_B_BookId",
                table: "CartDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_B_BookId",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_B_BookId",
                table: "Favourites");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_B_BookId",
                table: "OrderDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_Orders_B_BookId",
                table: "Orders");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_B_BookId",
                table: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails");

            migrationBuilder.DropPrimaryKey(
                name: "PK_B",
                table: "B");

            migrationBuilder.RenameTable(
                name: "B",
                newName: "Books");

            migrationBuilder.RenameIndex(
                name: "IX_B_LanguageId",
                table: "Books",
                newName: "IX_Books_LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_B_GenreId",
                table: "Books",
                newName: "IX_Books_GenreId");

            migrationBuilder.RenameIndex(
                name: "IX_B_BookID",
                table: "Books",
                newName: "IX_Books_BookID");

            migrationBuilder.RenameIndex(
                name: "IX_B_AuthorId",
                table: "Books",
                newName: "IX_Books_AuthorId");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "OrderDetails",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderDetails",
                table: "OrderDetails",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Books",
                table: "Books",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_OrderId",
                table: "OrderDetails",
                column: "OrderId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Books_BookID",
                table: "Books",
                column: "BookID",
                principalTable: "Books",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Genres_GenreId",
                table: "Books",
                column: "GenreId",
                principalTable: "Genres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Languages_LanguageId",
                table: "Books",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartDetails_Books_BookId",
                table: "CartDetails",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Books_BookId",
                table: "Carts",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Books_BookId",
                table: "Favourites",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Books_BookId",
                table: "OrderDetails",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Books_BookId",
                table: "Orders",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Books_BookId",
                table: "Reviews",
                column: "BookId",
                principalTable: "Books",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
