using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clean.Architecture.Infrastructure.Migrations;

  /// <inheritdoc />
  public partial class InitialCreate : Migration
  {
      /// <inheritdoc />
      protected override void Up(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.CreateTable(
              name: "ClientUser",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "TEXT", nullable: false),
                  UserVerfied = table.Column<bool>(type: "INTEGER", nullable: false),
                  Name = table.Column<string>(type: "TEXT", maxLength: 120, nullable: false),
                  Password = table.Column<string>(type: "TEXT", nullable: false),
                  Email = table.Column<string>(type: "TEXT", nullable: false),
                  UserName = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                  EmailVerfied = table.Column<bool>(type: "INTEGER", nullable: false),
                  TwoStepLogin = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                  PhoneNumberVerfied = table.Column<bool>(type: "INTEGER", nullable: false, defaultValue: false),
                  PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 12, nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_ClientUser", x => x.Id);
              });

          migrationBuilder.CreateTable(
              name: "PhoneValidations",
              columns: table => new
              {
                  Id = table.Column<Guid>(type: "TEXT", nullable: false),
                  UserID = table.Column<Guid>(type: "TEXT", nullable: false),
                  Code = table.Column<int>(type: "INTEGER", nullable: false),
                  ValidTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                  UserPhoneNumber = table.Column<string>(type: "TEXT", maxLength: 12, nullable: false)
              },
              constraints: table =>
              {
                  table.PrimaryKey("PK_PhoneValidations", x => x.Id);
                  table.ForeignKey(
                      name: "FK_PhoneValidations_ClientUser_UserID",
                      column: x => x.UserID,
                      principalTable: "ClientUser",
                      principalColumn: "Id",
                      onDelete: ReferentialAction.Restrict);
              });

          migrationBuilder.CreateIndex(
              name: "IX_PhoneValidations_UserID",
              table: "PhoneValidations",
              column: "UserID");
      }

      /// <inheritdoc />
      protected override void Down(MigrationBuilder migrationBuilder)
      {
          migrationBuilder.DropTable(
              name: "PhoneValidations");

          migrationBuilder.DropTable(
              name: "ClientUser");
      }
  }
