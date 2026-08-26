using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoPlantao.Migrations
{
    /// <inheritdoc />
    public partial class AddDepartamentoIdAcionamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Acionamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Acionamentos_DepartamentoId",
                table: "Acionamentos",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Acionamentos_Departamentos_DepartamentoId",
                table: "Acionamentos",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Acionamentos_Departamentos_DepartamentoId",
                table: "Acionamentos");

            migrationBuilder.DropIndex(
                name: "IX_Acionamentos_DepartamentoId",
                table: "Acionamentos");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Acionamentos");
        }
    }
}
