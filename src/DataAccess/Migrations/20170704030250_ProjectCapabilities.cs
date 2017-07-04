using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ProjectCapabilities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardStatus",
                columns: table => new
                {
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardStatus", x => x.Status);
                });

            migrationBuilder.CreateTable(
                name: "ProjectStatus",
                columns: table => new
                {
                    Status = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectStatus", x => x.Status);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Description = table.Column<string>(nullable: false),
                    MainImageUrl = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Summary = table.Column<string>(nullable: false),
                    TileImageUrl = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    VanityImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_ProjectStatus_Status",
                        column: x => x.Status,
                        principalTable: "ProjectStatus",
                        principalColumn: "Status",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Card",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Assignee = table.Column<string>(nullable: false),
                    CardProperties = table.Column<string>(nullable: true),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Description = table.Column<string>(nullable: true),
                    Estimate = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    ProjectId = table.Column<Guid>(nullable: false),
                    Status = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Card", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Card_AspNetUsers_Assignee",
                        column: x => x.Assignee,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Card_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Card_CardStatus_Status",
                        column: x => x.Status,
                        principalTable: "CardStatus",
                        principalColumn: "Status",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProjectOwner",
                columns: table => new
                {
                    ProjectId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectOwner", x => new { x.ProjectId, x.UserId });
                    table.ForeignKey(
                        name: "FK_ProjectOwner_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProjectOwner_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CardBidder",
                columns: table => new
                {
                    CardId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardBidder", x => new { x.CardId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CardBidder_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CardBidder_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CardDeveloper",
                columns: table => new
                {
                    CardId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardDeveloper", x => new { x.CardId, x.UserId });
                    table.ForeignKey(
                        name: "FK_CardDeveloper_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CardDeveloper_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "CardTask",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    CardId = table.Column<Guid>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false, defaultValueSql: "GETUTCDATE()"),
                    Description = table.Column<string>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: false),
                    TaskProperties = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardTask", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardTask_Card_CardId",
                        column: x => x.CardId,
                        principalTable: "Card",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Card_Assignee",
                table: "Card",
                column: "Assignee");

            migrationBuilder.CreateIndex(
                name: "IX_Card_ProjectId",
                table: "Card",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Card_Status",
                table: "Card",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_CardBidder_UserId",
                table: "CardBidder",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardDeveloper_UserId",
                table: "CardDeveloper",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CardTask_CardId",
                table: "CardTask",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_Status",
                table: "Project",
                column: "Status");

            migrationBuilder.CreateIndex(
                name: "IX_ProjectOwner_UserId",
                table: "ProjectOwner",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardBidder");

            migrationBuilder.DropTable(
                name: "CardDeveloper");

            migrationBuilder.DropTable(
                name: "CardTask");

            migrationBuilder.DropTable(
                name: "ProjectOwner");

            migrationBuilder.DropTable(
                name: "Card");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropTable(
                name: "CardStatus");

            migrationBuilder.DropTable(
                name: "ProjectStatus");
        }
    }
}
