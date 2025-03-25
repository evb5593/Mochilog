using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mochilog.Migrations
{
    /// <inheritdoc />
    public partial class AddImageDataToMochiPhoto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageData",
                table: "MochiPhoto",
                type: "BLOB",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageData",
                table: "MochiPhoto");
        }
    }
}
