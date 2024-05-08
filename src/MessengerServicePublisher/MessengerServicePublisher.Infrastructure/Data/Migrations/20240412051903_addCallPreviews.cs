using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MessengerServicePublisher.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class addCallPreviews : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CallPreviews",
                schema: "wsp",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Company = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Definition = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    To = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    From = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    FileUrl = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: false),
                    JsonRequest = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    JsonResponse = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Udpated = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Deleted = table.Column<DateTime>(type: "datetime2", nullable: true),
                    DeletedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CallPreviews", x => x.Id);
                });

            string Definition_MVCE = string.Empty;
            Definition_MVCE += $"(var2)\n";

            migrationBuilder.InsertData(
            table: "GmailSettings", schema: "wsp",
            columns: new[] { "Id", "Company", "Definition", "Description", "Type", "Created", "CreatedBy", "Udpated", "UpdatedBy", "Deleted", "DeletedBy", },
            values: new object[] { Guid.NewGuid(), "PROSEGUR", "MVCE", Definition_MVCE, 3, DateTime.Now, "Created", null, null, null, null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CallPreviews",
                schema: "wsp");
        }
    }
}
