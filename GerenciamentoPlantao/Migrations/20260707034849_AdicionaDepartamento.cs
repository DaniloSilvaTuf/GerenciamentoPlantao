using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoPlantao.Migrations
{
    /// <inheritdoc />
    public partial class AdicionaDepartamento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Usuarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Solucoes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "CategoriasAcionamento",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DepartamentoId",
                table: "Canais",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_DepartamentoId",
                table: "Usuarios",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Solucoes_DepartamentoId",
                table: "Solucoes",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasAcionamento_DepartamentoId",
                table: "CategoriasAcionamento",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Canais_DepartamentoId",
                table: "Canais",
                column: "DepartamentoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Canais_Departamentos_DepartamentoId",
                table: "Canais",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoriasAcionamento_Departamentos_DepartamentoId",
                table: "CategoriasAcionamento",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Solucoes_Departamentos_DepartamentoId",
                table: "Solucoes",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuarios_Departamentos_DepartamentoId",
                table: "Usuarios",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Canais_Departamentos_DepartamentoId",
                table: "Canais");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoriasAcionamento_Departamentos_DepartamentoId",
                table: "CategoriasAcionamento");

            migrationBuilder.DropForeignKey(
                name: "FK_Solucoes_Departamentos_DepartamentoId",
                table: "Solucoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Usuarios_Departamentos_DepartamentoId",
                table: "Usuarios");

            migrationBuilder.DropTable(
                name: "Departamentos");

            migrationBuilder.DropIndex(
                name: "IX_Usuarios_DepartamentoId",
                table: "Usuarios");

            migrationBuilder.DropIndex(
                name: "IX_Solucoes_DepartamentoId",
                table: "Solucoes");

            migrationBuilder.DropIndex(
                name: "IX_CategoriasAcionamento_DepartamentoId",
                table: "CategoriasAcionamento");

            migrationBuilder.DropIndex(
                name: "IX_Canais_DepartamentoId",
                table: "Canais");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Usuarios");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Solucoes");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "CategoriasAcionamento");

            migrationBuilder.DropColumn(
                name: "DepartamentoId",
                table: "Canais");
        }
    }
}
