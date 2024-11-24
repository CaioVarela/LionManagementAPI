using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ManageIt.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CollaboratorFieldsUpdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Collaborators",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Collaborators");
        }
    }
}
