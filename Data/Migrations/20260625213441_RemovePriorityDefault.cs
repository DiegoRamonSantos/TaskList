using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Tasklist.Data.Migrations
{
    /// <inheritdoc />
    public partial class RemovePriorityDefault : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "TaskItems",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldDefaultValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Priority",
                table: "TaskItems",
                type: "INTEGER",
                nullable: false,
                defaultValue: 2,
                oldClrType: typeof(int),
                oldType: "INTEGER");
        }
    }
}
