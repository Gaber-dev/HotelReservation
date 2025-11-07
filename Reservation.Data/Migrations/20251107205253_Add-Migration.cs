using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Reservation.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table:"AspNetRoles",
                columns: new[]{ "Id", "Description", "Name", "NormalizedName", "ConcurrencyStamp" },
                values: new object[]{Guid.NewGuid().ToString(),"Admin Role that have upper hand","Admin","User".ToUpper(), Guid.NewGuid().ToString() }
                );

            migrationBuilder.InsertData(
               table: "AspNetRoles",
               columns: new[] { "Id", "Description", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { Guid.NewGuid().ToString(), "Authenticated person in my website", "Client", "Client".ToUpper(), Guid.NewGuid().ToString() }
               );

            migrationBuilder.InsertData(
               table: "AspNetRoles",
               columns: new[] { "Id", "Description", "Name", "NormalizedName", "ConcurrencyStamp" },
               values: new object[] { Guid.NewGuid().ToString(), "Person that receive requests from clients  ", "Receptionist", "Receptionist".ToUpper(), Guid.NewGuid().ToString() }
               );

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM [AspNetRoles]");
        }
    }
}
