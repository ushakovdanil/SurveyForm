using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    /// <inheritdoc />
    public partial class initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BotThemes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    ParentThemeId = table.Column<Guid>(type: "uuid", nullable: true),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotThemes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BotThemes_BotThemes_ParentThemeId",
                        column: x => x.ParentThemeId,
                        principalTable: "BotThemes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Languages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Culture = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Languages", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BotMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    KeyboardType = table.Column<string>(type: "text", nullable: true),
                    FilesUrls = table.Column<List<string>>(type: "text[]", nullable: true),
                    BotThemeEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BotMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BotMessages_BotThemes_BotThemeEntityId",
                        column: x => x.BotThemeEntityId,
                        principalTable: "BotThemes",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TelegramId = table.Column<long>(type: "bigint", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    LastThemeId = table.Column<Guid>(type: "uuid", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_BotThemes_LastThemeId",
                        column: x => x.LastThemeId,
                        principalTable: "BotThemes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InlineKeyboardButtons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TextId = table.Column<Guid>(type: "uuid", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: true),
                    CallbackData = table.Column<string>(type: "text", nullable: true),
                    Row = table.Column<int>(type: "integer", nullable: false),
                    Column = table.Column<int>(type: "integer", nullable: false),
                    BotMessageEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InlineKeyboardButtons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_InlineKeyboardButtons_BotMessages_BotMessageEntityId",
                        column: x => x.BotMessageEntityId,
                        principalTable: "BotMessages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "LocalizebleResources",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    LanguageId = table.Column<Guid>(type: "uuid", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: false),
                    BotMessageEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizebleResources", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalizebleResources_BotMessages_BotMessageEntityId",
                        column: x => x.BotMessageEntityId,
                        principalTable: "BotMessages",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_LocalizebleResources_Languages_LanguageId",
                        column: x => x.LanguageId,
                        principalTable: "Languages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReplyKeyboardButtons",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    TextId = table.Column<Guid>(type: "uuid", nullable: false),
                    Column = table.Column<int>(type: "integer", nullable: false),
                    Row = table.Column<int>(type: "integer", nullable: false),
                    BotMessageEntityId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyKeyboardButtons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplyKeyboardButtons_BotMessages_BotMessageEntityId",
                        column: x => x.BotMessageEntityId,
                        principalTable: "BotMessages",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BotMessages_BotThemeEntityId",
                table: "BotMessages",
                column: "BotThemeEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_BotThemes_ParentThemeId",
                table: "BotThemes",
                column: "ParentThemeId");

            migrationBuilder.CreateIndex(
                name: "IX_InlineKeyboardButtons_BotMessageEntityId",
                table: "InlineKeyboardButtons",
                column: "BotMessageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizebleResources_BotMessageEntityId",
                table: "LocalizebleResources",
                column: "BotMessageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizebleResources_LanguageId",
                table: "LocalizebleResources",
                column: "LanguageId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyKeyboardButtons_BotMessageEntityId",
                table: "ReplyKeyboardButtons",
                column: "BotMessageEntityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_LastThemeId",
                table: "Users",
                column: "LastThemeId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "InlineKeyboardButtons");

            migrationBuilder.DropTable(
                name: "LocalizebleResources");

            migrationBuilder.DropTable(
                name: "ReplyKeyboardButtons");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Languages");

            migrationBuilder.DropTable(
                name: "BotMessages");

            migrationBuilder.DropTable(
                name: "BotThemes");
        }
    }
}
