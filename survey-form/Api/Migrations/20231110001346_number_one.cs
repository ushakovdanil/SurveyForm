using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class number_one : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TextId",
                table: "ReplyKeyboardButtons");

            migrationBuilder.AddColumn<Guid>(
                name: "LanguageId",
                table: "Users",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ReplyKeyboardButtonEntityId",
                table: "LocalizebleResources",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_LanguageId",
                table: "Users",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizebleResources_ReplyKeyboardButtonEntityId",
                table: "LocalizebleResources",
                column: "ReplyKeyboardButtonEntityId");

            migrationBuilder.AddForeignKey(
                name: "FK_LocalizebleResources_ReplyKeyboardButtons_ReplyKeyboardButt~",
                table: "LocalizebleResources",
                column: "ReplyKeyboardButtonEntityId",
                principalTable: "ReplyKeyboardButtons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Languages_languageId",
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
                name: "FK_LocalizebleResources_ReplyKeyboardButtons_ReplyKeyboardButt~",
                table: "LocalizebleResources");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Languages_languageId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_languageId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_LocalizebleResources_ReplyKeyboardButtonEntityId",
                table: "LocalizebleResources");

            migrationBuilder.DropColumn(
                name: "languageId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "ReplyKeyboardButtonEntityId",
                table: "LocalizebleResources");

            migrationBuilder.AddColumn<Guid>(
                name: "TextId",
                table: "ReplyKeyboardButtons",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
