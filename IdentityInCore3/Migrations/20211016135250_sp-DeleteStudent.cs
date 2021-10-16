using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityInCore3.DAL.Migrations
{
    public partial class spDeleteStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[DeleteStudent]
            @Id bigint
        AS
        BEGIN
            
            delete Students where Id=@Id
        END
GO";

            migrationBuilder.Sql(sp);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }

    }
}
