using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoPlantao.Migrations
{
    /// <inheritdoc />
    public partial class CriacaoModuloEscalas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EscalasBaseRegrasRotacao_AspNetUsers_UsuarioId",
                table: "EscalasBaseRegrasRotacao");

            migrationBuilder.DropForeignKey(
                name: "FK_RegrasRotacao_BlocosHorarioPostosEscala_BlocoHorarioPostoEscalaId",
                table: "RegrasRotacao");

            migrationBuilder.DropIndex(
                name: "IX_RegrasRotacaoHorarios_RegraRotacaoId",
                table: "RegrasRotacaoHorarios");

            migrationBuilder.DropIndex(
                name: "IX_RegrasRotacao_BlocoHorarioPostoEscalaId",
                table: "RegrasRotacao");

            migrationBuilder.DropIndex(
                name: "IX_EscalasBaseRegrasRotacao_UsuarioId",
                table: "EscalasBaseRegrasRotacao");

            migrationBuilder.DropColumn(
                name: "HoraInicio",
                table: "RegrasRotacaoHorarios");

            migrationBuilder.DropColumn(
                name: "BlocoHorarioPostoEscalaId",
                table: "RegrasRotacao");

            migrationBuilder.DropColumn(
                name: "Obrigatoria",
                table: "RegrasRotacao");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "EscalasBaseRegrasRotacao");

            migrationBuilder.DropColumn(
                name: "Duracao",
                table: "RegrasRotacaoHorarios");

            migrationBuilder.DropColumn(
                name: "DiaSemana",
                table: "RegrasRotacaoHorarios");

            migrationBuilder.AddColumn<int>(
                name: "OrdemRotacao",
                table: "RegrasRotacaoHorarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BlocoHorarioPostoEscalaId",
                table: "RegrasRotacaoHorarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.RenameColumn(
                name: "Ordem",
                table: "RegrasRotacao",
                newName: "OrdemExecucao");

            migrationBuilder.RenameIndex(
                name: "IX_RegrasRotacao_DepartamentoId_Ordem",
                table: "RegrasRotacao",
                newName: "IX_RegrasRotacao_DepartamentoId_OrdemExecucao");

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacaoHorarios_BlocoHorarioPostoEscalaId",
                table: "RegrasRotacaoHorarios",
                column: "BlocoHorarioPostoEscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacaoHorarios_RegraRotacaoId_BlocoHorarioPostoEscalaId",
                table: "RegrasRotacaoHorarios",
                columns: new[] { "RegraRotacaoId", "BlocoHorarioPostoEscalaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacaoHorarios_RegraRotacaoId_OrdemRotacao",
                table: "RegrasRotacaoHorarios",
                columns: new[] { "RegraRotacaoId", "OrdemRotacao" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_RegrasRotacaoHorarios_BlocosHorarioPostosEscala_BlocoHorarioPostoEscalaId",
                table: "RegrasRotacaoHorarios",
                column: "BlocoHorarioPostoEscalaId",
                principalTable: "BlocosHorarioPostosEscala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RegrasRotacaoHorarios_BlocosHorarioPostosEscala_BlocoHorarioPostoEscalaId",
                table: "RegrasRotacaoHorarios");

            migrationBuilder.DropIndex(
                name: "IX_RegrasRotacaoHorarios_BlocoHorarioPostoEscalaId",
                table: "RegrasRotacaoHorarios");

            migrationBuilder.DropIndex(
                name: "IX_RegrasRotacaoHorarios_RegraRotacaoId_BlocoHorarioPostoEscalaId",
                table: "RegrasRotacaoHorarios");

            migrationBuilder.DropIndex(
                name: "IX_RegrasRotacaoHorarios_RegraRotacaoId_OrdemRotacao",
                table: "RegrasRotacaoHorarios");

            migrationBuilder.DropColumn(
                name: "OrdemRotacao",
                table: "RegrasRotacaoHorarios");

            migrationBuilder.DropColumn(
                name: "BlocoHorarioPostoEscalaId",
                table: "RegrasRotacaoHorarios");

            migrationBuilder.RenameColumn(
                name: "OrdemExecucao",
                table: "RegrasRotacao",
                newName: "Ordem");

            migrationBuilder.AddColumn<int>(
                name: "Duracao",
                table: "RegrasRotacaoHorarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "DiaSemana",
                table: "RegrasRotacaoHorarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.RenameIndex(
                name: "IX_RegrasRotacao_DepartamentoId_OrdemExecucao",
                table: "RegrasRotacao",
                newName: "IX_RegrasRotacao_DepartamentoId_Ordem");

            migrationBuilder.AddColumn<TimeOnly>(
                name: "HoraInicio",
                table: "RegrasRotacaoHorarios",
                type: "time",
                nullable: false,
                defaultValue: new TimeOnly(0, 0, 0));

            migrationBuilder.AddColumn<int>(
                name: "BlocoHorarioPostoEscalaId",
                table: "RegrasRotacao",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "Obrigatoria",
                table: "RegrasRotacao",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UsuarioId",
                table: "EscalasBaseRegrasRotacao",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacaoHorarios_RegraRotacaoId",
                table: "RegrasRotacaoHorarios",
                column: "RegraRotacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacao_BlocoHorarioPostoEscalaId",
                table: "RegrasRotacao",
                column: "BlocoHorarioPostoEscalaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseRegrasRotacao_UsuarioId",
                table: "EscalasBaseRegrasRotacao",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_EscalasBaseRegrasRotacao_AspNetUsers_UsuarioId",
                table: "EscalasBaseRegrasRotacao",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RegrasRotacao_BlocosHorarioPostosEscala_BlocoHorarioPostoEscalaId",
                table: "RegrasRotacao",
                column: "BlocoHorarioPostoEscalaId",
                principalTable: "BlocosHorarioPostosEscala",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
