using Microsoft.EntityFrameworkCore.Migrations;

namespace UsersRoles.Migrations
{
    public partial class addingRoleFieldUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.AddColumn<int>(
            //    name: "Role",
            //    table: "Users",
            //    type: "int",
            //    nullable: true,
            //    defaultValue: null);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            //migrationBuilder.DropColumn(
            //    name: "Role",
            //    table: "Users");

            migrationBuilder.AlterColumn<int>(
                name: "Role",
                table: "Users",
                type: "int",
                nullable: true,
                defaultValue: null);
        }
    }
}
