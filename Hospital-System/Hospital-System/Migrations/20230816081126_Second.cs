using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hospital_System.Migrations
{
    /// <inheritdoc />
    public partial class Second : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "HospitalID",
                table: "Departments",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Hospitals",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HospitalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hospitals", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_HospitalID",
                table: "Departments",
                column: "HospitalID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Hospitals_HospitalID",
                table: "Departments",
                column: "HospitalID",
                principalTable: "Hospitals",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Hospitals_HospitalID",
                table: "Departments");

            migrationBuilder.DropTable(
                name: "Hospitals");

            migrationBuilder.DropIndex(
                name: "IX_Departments_HospitalID",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "HospitalID",
                table: "Departments");
        }
    }
}
