using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class AddAuthUserIdToUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Add column as nullable first so existing rows don't break
            migrationBuilder.AddColumn<string>(
                name: "AuthUserId",
                table: "Users",
                type: "varchar(36)",
                maxLength: 36,
                nullable: true,
                collation: "utf8mb4_hungarian_ci")
                .Annotation("MySql:CharSet", "utf8mb4");

            // Backfill existing rows with unique placeholder so we can make column NOT NULL and add unique index
            migrationBuilder.Sql("UPDATE Users SET AuthUserId = CONCAT('legacy-', Id) WHERE AuthUserId IS NULL;");

            migrationBuilder.AlterColumn<string>(
                name: "AuthUserId",
                table: "Users",
                type: "varchar(36)",
                maxLength: 36,
                nullable: false,
                collation: "utf8mb4_hungarian_ci",
                oldClrType: typeof(string),
                oldType: "varchar(36)",
                oldMaxLength: 36,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Users_AuthUserId",
                table: "Users",
                column: "AuthUserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Users_AuthUserId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AuthUserId",
                table: "Users");
        }
    }
}
