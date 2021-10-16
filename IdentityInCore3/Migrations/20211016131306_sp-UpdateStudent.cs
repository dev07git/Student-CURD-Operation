using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityInCore3.DAL.Migrations
{
    public partial class spUpdateStudent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sp = @"CREATE PROCEDURE [dbo].[sp_UpdateStudent]
            @Id bigint, @Name varchar(100),@PhoneNumber varchar(15),@Address varchar(255),@PostalCode varchar(10)
        AS
        BEGIN
            SET NOCOUNT ON;
            Update Students set [Name]=@Name,PhoneNumber=@PhoneNumber,[Address]=@Address,PostalCode=@PostalCode where Id=@Id
        END
GO";

            migrationBuilder.Sql(sp);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
        }

    }
}
