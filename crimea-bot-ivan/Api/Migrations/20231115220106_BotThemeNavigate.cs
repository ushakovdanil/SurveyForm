using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class BotThemeNavigate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Languages_languageId",
                table: "Users");


            migrationBuilder.AddColumn<Guid>(
                name: "BotThemeToNavigateId",
                table: "ReplyKeyboardButtons",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("0abe94e1-6953-49a7-bfb6-1ae5cc3a2aec"));

            migrationBuilder.CreateIndex(
                name: "IX_ReplyKeyboardButtons_BotThemeToNavigateId",
                table: "ReplyKeyboardButtons",
                column: "BotThemeToNavigateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReplyKeyboardButtons_BotThemes_BotThemeToNavigateId",
                table: "ReplyKeyboardButtons",
                column: "BotThemeToNavigateId",
                principalTable: "BotThemes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Languages_LanguageId",
                table: "Users",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReplyKeyboardButtons_BotThemes_BotThemeToNavigateId",
                table: "ReplyKeyboardButtons");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Languages_LanguageId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_ReplyKeyboardButtons_BotThemeToNavigateId",
                table: "ReplyKeyboardButtons");

            migrationBuilder.DropColumn(
                name: "BotThemeToNavigateId",
                table: "ReplyKeyboardButtons");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Languages_languageId",
                table: "Users",
                column: "languageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
