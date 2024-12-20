using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyTaskManager.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class initialtwo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Todos_Id_UserId",
                table: "Todos");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Todos_Id_UserId",
                table: "Todos",
                columns: new[] { "Id", "UserId" },
                unique: true);
        }
    }
}
