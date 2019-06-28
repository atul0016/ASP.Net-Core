using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CoreApp.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DeptRowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    DeptNo = table.Column<string>(nullable: false),
                    DeptName = table.Column<string>(nullable: false),
                    Location = table.Column<string>(nullable: false),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DeptRowId);
                });

            migrationBuilder.CreateTable(
                name: "Emplopyees",
                columns: table => new
                {
                    EmpRowId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EmpNo = table.Column<string>(nullable: false),
                    EmpName = table.Column<string>(nullable: false),
                    Designation = table.Column<string>(nullable: false),
                    Salary = table.Column<int>(nullable: false),
                    DeptRowId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Emplopyees", x => x.EmpRowId);
                    table.ForeignKey(
                        name: "FK_Emplopyees_Departments_DeptRowId",
                        column: x => x.DeptRowId,
                        principalTable: "Departments",
                        principalColumn: "DeptRowId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Emplopyees_DeptRowId",
                table: "Emplopyees",
                column: "DeptRowId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Emplopyees");

            migrationBuilder.DropTable(
                name: "Departments");
        }
    }
}
