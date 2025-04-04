﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Mochilog.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMochiPhotoModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFileName",
                table: "MochiPhoto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageFileName",
                table: "MochiPhoto",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
