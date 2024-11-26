using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class RequestUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplyKeyboardButtons_BotThemes_BotThemeToNavigateId",
                table: "ReplyKeyboardButtons");

            migrationBuilder.AlterColumn<Guid>(
                name: "BotThemeToNavigateId",
                table: "ReplyKeyboardButtons",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplyKeyboardButtons_BotThemes_BotThemeToNavigateId",
                table: "ReplyKeyboardButtons",
                column: "BotThemeToNavigateId",
                principalTable: "BotThemes",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplyKeyboardButtons_BotThemes_BotThemeToNavigateId",
                table: "ReplyKeyboardButtons");

            migrationBuilder.AlterColumn<Guid>(
                name: "BotThemeToNavigateId",
                table: "ReplyKeyboardButtons",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ReplyKeyboardButtons_BotThemes_BotThemeToNavigateId",
                table: "ReplyKeyboardButtons",
                column: "BotThemeToNavigateId",
                principalTable: "BotThemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
