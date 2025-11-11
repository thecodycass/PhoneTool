using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Phonetool.Migrations
{
    /// <inheritdoc />
    public partial class RemoveTenureFromPerson : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tenure",
                table: "Persons");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tenure",
                table: "Persons",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
