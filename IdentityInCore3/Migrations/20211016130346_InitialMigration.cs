using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityInCore3.DAL.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true),
                    Gender = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 15, nullable: true),
                    Address = table.Column<string>(maxLength: 255, nullable: true),
                    PostalCode = table.Column<string>(maxLength: 10, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SubjectMaster",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    Name = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubjectMaster", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreatedOn = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedOn = table.Column<DateTimeOffset>(nullable: true),
                    StudentId = table.Column<long>(nullable: false),
                    SubjectMasterId = table.Column<long>(nullable: false),
                    Marks = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Subjects_Students",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Subjects_SubjectMaster",
                        column: x => x.SubjectMasterId,
                        principalTable: "SubjectMaster",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "SubjectMaster",
                columns: new[] { "Id", "CreatedOn", "ModifiedOn", "Name" },
                values: new object[,]
                {
                    { 1L, new DateTimeOffset(new DateTime(2021, 10, 16, 13, 3, 46, 27, DateTimeKind.Unspecified).AddTicks(4693), new TimeSpan(0, 0, 0, 0, 0)), null, "Math" },
                    { 2L, new DateTimeOffset(new DateTime(2021, 10, 16, 13, 3, 46, 27, DateTimeKind.Unspecified).AddTicks(5292), new TimeSpan(0, 0, 0, 0, 0)), null, "Hindi" },
                    { 3L, new DateTimeOffset(new DateTime(2021, 10, 16, 13, 3, 46, 27, DateTimeKind.Unspecified).AddTicks(5301), new TimeSpan(0, 0, 0, 0, 0)), null, "EVS" },
                    { 4L, new DateTimeOffset(new DateTime(2021, 10, 16, 13, 3, 46, 27, DateTimeKind.Unspecified).AddTicks(5304), new TimeSpan(0, 0, 0, 0, 0)), null, "English" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Students_Id",
                table: "Students",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_SubjectMaster_Id",
                table: "SubjectMaster",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_Id",
                table: "Subjects",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_StudentId",
                table: "Subjects",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Subjects_SubjectMasterId",
                table: "Subjects",
                column: "SubjectMasterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "SubjectMaster");
        }
    }
}
