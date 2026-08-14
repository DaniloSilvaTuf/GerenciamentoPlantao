using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoPlantao.Migrations
{
    /// <inheritdoc />
    public partial class AddTrocarSenhaObrigatoriaUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "TrocaSenhaObrigatoria",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TrocaSenhaObrigatoria",
                table: "AspNetUsers");
        }
    }
}
