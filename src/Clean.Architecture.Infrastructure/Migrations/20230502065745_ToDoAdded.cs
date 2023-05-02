using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clean.Architecture.Infrastructure.Migrations;

  /// <inheritdoc />
  public partial class ToDoAdded : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.CreateTable(
              name: "toDos",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "TEXT", nullable: false),
                  UserId = table.Column<Guid>(type: "TEXT", nullable: false),
                  ToDoTitle = table.Column<string>(type: "TEXT", nullable: false),
                  ToDoDescription = table.Column<string>(type: "TEXT", nullable: false),
                  IsCompleted = table.Column<bool>(type: "INTEGER", nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_toDos", x => x.Id);
                  table.ForeignKey(
                      name: "FK_toDos_ClientUser_UserId",
                      column: x => x.UserId,
                      principalTable: "ClientUser",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
              });

          migrationBuilder.CreateIndex(
              name: "IX_toDos_UserId",
              table: "toDos",
              column: "UserId");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropTable(
              name: "toDos");
      }
  }
