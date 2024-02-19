using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UserAccount.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccDetails",
                columns: table => new
                {
                    Mobile_No = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    First_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Last_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DOB = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bio = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccDetails", x => x.Mobile_No);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccDetails");
        }
    }
}
