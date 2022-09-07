using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juntos_Backend.Migrations
{
    public partial class allowedmembershiprefsupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReferencedMembershipId",
                table: "MembershipRef",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReferencedMembershipId",
                table: "MembershipRef");
        }
    }
}
