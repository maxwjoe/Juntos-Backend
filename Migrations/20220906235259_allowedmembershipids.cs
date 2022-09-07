using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Juntos_Backend.Migrations
{
    public partial class allowedmembershipids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Memberships_Events_EventId",
                table: "Memberships");

            migrationBuilder.DropIndex(
                name: "IX_Memberships_EventId",
                table: "Memberships");

            migrationBuilder.DropColumn(
                name: "EventId",
                table: "Memberships");

            migrationBuilder.CreateTable(
                name: "MembershipRef",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EventId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MembershipRef", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MembershipRef_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_MembershipRef_EventId",
                table: "MembershipRef",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MembershipRef");

            migrationBuilder.AddColumn<int>(
                name: "EventId",
                table: "Memberships",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Memberships_EventId",
                table: "Memberships",
                column: "EventId");

            migrationBuilder.AddForeignKey(
                name: "FK_Memberships_Events_EventId",
                table: "Memberships",
                column: "EventId",
                principalTable: "Events",
                principalColumn: "Id");
        }
    }
}
