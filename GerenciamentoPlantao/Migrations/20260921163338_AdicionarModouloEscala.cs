using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoPlantao.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarModouloEscala : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StatusAcionamento",
                table: "Acionamentos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ApoiosPlantonistasEscala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ApoioUsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PlantonistaUsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApoiosPlantonistasEscala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApoiosPlantonistasEscala_AspNetUsers_ApoioUsuarioId",
                        column: x => x.ApoioUsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApoiosPlantonistasEscala_AspNetUsers_PlantonistaUsuarioId",
                        column: x => x.PlantonistaUsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApoiosPlantonistasEscala_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApoiosPlantonistasEscala_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApoiosPlantonistasEscala_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ApoiosPlantonistasEscala_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlocosHorarioEscala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    HoraFim = table.Column<TimeOnly>(type: "time", nullable: false),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlocosHorarioEscala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlocosHorarioEscala_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlocosHorarioEscala_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlocosHorarioEscala_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlocosHorarioEscala_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DatasEspeciaisEscala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateOnly>(type: "date", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatasEspeciaisEscala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DatasEspeciaisEscala_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DatasEspeciaisEscala_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DatasEspeciaisEscala_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DatasEspeciaisEscala_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EscalasBase",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalasBase", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalasBase_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasBase_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasBase_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasBase_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PostosEscalas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostosEscalas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostosEscalas_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostosEscalas_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostosEscalas_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PostosEscalas_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EscalasGeradas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EscalaBaseId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    DataInicio = table.Column<DateOnly>(type: "date", nullable: false),
                    DataFim = table.Column<DateOnly>(type: "date", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalasGeradas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalasGeradas_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasGeradas_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasGeradas_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasGeradas_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EscalasGeradas_EscalasBase_EscalaBaseId",
                        column: x => x.EscalaBaseId,
                        principalTable: "EscalasBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BlocosHorarioPostosEscala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BlocoHorarioEscalaId = table.Column<int>(type: "int", nullable: false),
                    PostoEscalaId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BlocosHorarioPostosEscala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BlocosHorarioPostosEscala_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlocosHorarioPostosEscala_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlocosHorarioPostosEscala_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BlocosHorarioPostosEscala_BlocosHorarioEscala_BlocoHorarioEscalaId",
                        column: x => x.BlocoHorarioEscalaId,
                        principalTable: "BlocosHorarioEscala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BlocosHorarioPostosEscala_PostosEscalas_PostoEscalaId",
                        column: x => x.PostoEscalaId,
                        principalTable: "PostosEscalas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EscalasGeradasItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EscalaGeradaId = table.Column<int>(type: "int", nullable: false),
                    PostoEscalaId = table.Column<int>(type: "int", nullable: false),
                    NomePosto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DataHoraInicio = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataHoraFim = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalasGeradasItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalasGeradasItems_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EscalasGeradasItems_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasGeradasItems_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasGeradasItems_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasGeradasItems_EscalasGeradas_EscalaGeradaId",
                        column: x => x.EscalaGeradaId,
                        principalTable: "EscalasGeradas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscalasGeradasItems_PostosEscalas_PostoEscalaId",
                        column: x => x.PostoEscalaId,
                        principalTable: "PostosEscalas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EscalasBaseItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EscalaBaseId = table.Column<int>(type: "int", nullable: false),
                    BlocoHorarioPostoEscalaId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalasBaseItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalasBaseItems_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EscalasBaseItems_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasBaseItems_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasBaseItems_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasBaseItems_BlocosHorarioPostosEscala_BlocoHorarioPostoEscalaId",
                        column: x => x.BlocoHorarioPostoEscalaId,
                        principalTable: "BlocosHorarioPostosEscala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EscalasBaseItems_EscalasBase_EscalaBaseId",
                        column: x => x.EscalaBaseId,
                        principalTable: "EscalasBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RegrasRotacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ordem = table.Column<int>(type: "int", nullable: false),
                    Obrigatoria = table.Column<bool>(type: "bit", nullable: false),
                    BlocoHorarioPostoEscalaId = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegrasRotacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegrasRotacao_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegrasRotacao_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegrasRotacao_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegrasRotacao_BlocosHorarioPostosEscala_BlocoHorarioPostoEscalaId",
                        column: x => x.BlocoHorarioPostoEscalaId,
                        principalTable: "BlocosHorarioPostosEscala",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RegrasRotacao_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "HistoricosAlteracaoEscala",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EscalaGeradaItemId = table.Column<int>(type: "int", nullable: false),
                    UsuarioAnteriorId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    NovoUsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HistoricosAlteracaoEscala", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HistoricosAlteracaoEscala_AspNetUsers_NovoUsuarioId",
                        column: x => x.NovoUsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricosAlteracaoEscala_AspNetUsers_UsuarioAnteriorId",
                        column: x => x.UsuarioAnteriorId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_HistoricosAlteracaoEscala_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoricosAlteracaoEscala_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoricosAlteracaoEscala_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_HistoricosAlteracaoEscala_EscalasGeradasItems_EscalaGeradaItemId",
                        column: x => x.EscalaGeradaItemId,
                        principalTable: "EscalasGeradasItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EscalasBaseRegrasRotacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EscalaBaseId = table.Column<int>(type: "int", nullable: false),
                    RegraRotacaoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EscalasBaseRegrasRotacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EscalasBaseRegrasRotacao_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EscalasBaseRegrasRotacao_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasBaseRegrasRotacao_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasBaseRegrasRotacao_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EscalasBaseRegrasRotacao_EscalasBase_EscalaBaseId",
                        column: x => x.EscalaBaseId,
                        principalTable: "EscalasBase",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EscalasBaseRegrasRotacao_RegrasRotacao_RegraRotacaoId",
                        column: x => x.RegraRotacaoId,
                        principalTable: "RegrasRotacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegrasRotacaoHorarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaSemana = table.Column<int>(type: "int", nullable: false),
                    HoraInicio = table.Column<TimeOnly>(type: "time", nullable: false),
                    Duracao = table.Column<int>(type: "int", nullable: false),
                    RegraRotacaoId = table.Column<int>(type: "int", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegrasRotacaoHorarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegrasRotacaoHorarios_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegrasRotacaoHorarios_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegrasRotacaoHorarios_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_RegrasRotacaoHorarios_RegrasRotacao_RegraRotacaoId",
                        column: x => x.RegraRotacaoId,
                        principalTable: "RegrasRotacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApoiosPlantonistasEscala_ApoioUsuarioId",
                table: "ApoiosPlantonistasEscala",
                column: "ApoioUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ApoiosPlantonistasEscala_DepartamentoId",
                table: "ApoiosPlantonistasEscala",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_ApoiosPlantonistasEscala_PlantonistaUsuarioId",
                table: "ApoiosPlantonistasEscala",
                column: "PlantonistaUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ApoiosPlantonistasEscala_UsuarioInativacaoId",
                table: "ApoiosPlantonistasEscala",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_ApoiosPlantonistasEscala_UsuarioInsertId",
                table: "ApoiosPlantonistasEscala",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_ApoiosPlantonistasEscala_UsuarioUpdateId",
                table: "ApoiosPlantonistasEscala",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorarioEscala_DepartamentoId",
                table: "BlocosHorarioEscala",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorarioEscala_UsuarioInativacaoId",
                table: "BlocosHorarioEscala",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorarioEscala_UsuarioInsertId",
                table: "BlocosHorarioEscala",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorarioEscala_UsuarioUpdateId",
                table: "BlocosHorarioEscala",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorarioPostosEscala_BlocoHorarioEscalaId_PostoEscalaId",
                table: "BlocosHorarioPostosEscala",
                columns: new[] { "BlocoHorarioEscalaId", "PostoEscalaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorarioPostosEscala_PostoEscalaId",
                table: "BlocosHorarioPostosEscala",
                column: "PostoEscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorarioPostosEscala_UsuarioInativacaoId",
                table: "BlocosHorarioPostosEscala",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorarioPostosEscala_UsuarioInsertId",
                table: "BlocosHorarioPostosEscala",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_BlocosHorarioPostosEscala_UsuarioUpdateId",
                table: "BlocosHorarioPostosEscala",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_DatasEspeciaisEscala_DepartamentoId_Data",
                table: "DatasEspeciaisEscala",
                columns: new[] { "DepartamentoId", "Data" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DatasEspeciaisEscala_UsuarioInativacaoId",
                table: "DatasEspeciaisEscala",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_DatasEspeciaisEscala_UsuarioInsertId",
                table: "DatasEspeciaisEscala",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_DatasEspeciaisEscala_UsuarioUpdateId",
                table: "DatasEspeciaisEscala",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBase_DepartamentoId",
                table: "EscalasBase",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBase_UsuarioInativacaoId",
                table: "EscalasBase",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBase_UsuarioInsertId",
                table: "EscalasBase",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBase_UsuarioUpdateId",
                table: "EscalasBase",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseItems_BlocoHorarioPostoEscalaId",
                table: "EscalasBaseItems",
                column: "BlocoHorarioPostoEscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseItems_EscalaBaseId_BlocoHorarioPostoEscalaId",
                table: "EscalasBaseItems",
                columns: new[] { "EscalaBaseId", "BlocoHorarioPostoEscalaId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseItems_UsuarioId",
                table: "EscalasBaseItems",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseItems_UsuarioInativacaoId",
                table: "EscalasBaseItems",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseItems_UsuarioInsertId",
                table: "EscalasBaseItems",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseItems_UsuarioUpdateId",
                table: "EscalasBaseItems",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseRegrasRotacao_EscalaBaseId_RegraRotacaoId",
                table: "EscalasBaseRegrasRotacao",
                columns: new[] { "EscalaBaseId", "RegraRotacaoId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseRegrasRotacao_RegraRotacaoId",
                table: "EscalasBaseRegrasRotacao",
                column: "RegraRotacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseRegrasRotacao_UsuarioId",
                table: "EscalasBaseRegrasRotacao",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseRegrasRotacao_UsuarioInativacaoId",
                table: "EscalasBaseRegrasRotacao",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseRegrasRotacao_UsuarioInsertId",
                table: "EscalasBaseRegrasRotacao",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasBaseRegrasRotacao_UsuarioUpdateId",
                table: "EscalasBaseRegrasRotacao",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasGeradas_DepartamentoId",
                table: "EscalasGeradas",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasGeradas_EscalaBaseId",
                table: "EscalasGeradas",
                column: "EscalaBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasGeradas_UsuarioInativacaoId",
                table: "EscalasGeradas",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasGeradas_UsuarioInsertId",
                table: "EscalasGeradas",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasGeradas_UsuarioUpdateId",
                table: "EscalasGeradas",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasGeradasItems_EscalaGeradaId",
                table: "EscalasGeradasItems",
                column: "EscalaGeradaId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasGeradasItems_PostoEscalaId",
                table: "EscalasGeradasItems",
                column: "PostoEscalaId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasGeradasItems_UsuarioId",
                table: "EscalasGeradasItems",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasGeradasItems_UsuarioInativacaoId",
                table: "EscalasGeradasItems",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasGeradasItems_UsuarioInsertId",
                table: "EscalasGeradasItems",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_EscalasGeradasItems_UsuarioUpdateId",
                table: "EscalasGeradasItems",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAlteracaoEscala_EscalaGeradaItemId",
                table: "HistoricosAlteracaoEscala",
                column: "EscalaGeradaItemId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAlteracaoEscala_NovoUsuarioId",
                table: "HistoricosAlteracaoEscala",
                column: "NovoUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAlteracaoEscala_UsuarioAnteriorId",
                table: "HistoricosAlteracaoEscala",
                column: "UsuarioAnteriorId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAlteracaoEscala_UsuarioInativacaoId",
                table: "HistoricosAlteracaoEscala",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAlteracaoEscala_UsuarioInsertId",
                table: "HistoricosAlteracaoEscala",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_HistoricosAlteracaoEscala_UsuarioUpdateId",
                table: "HistoricosAlteracaoEscala",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_PostosEscalas_DepartamentoId",
                table: "PostosEscalas",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_PostosEscalas_UsuarioInativacaoId",
                table: "PostosEscalas",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_PostosEscalas_UsuarioInsertId",
                table: "PostosEscalas",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_PostosEscalas_UsuarioUpdateId",
                table: "PostosEscalas",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacao_BlocoHorarioPostoEscalaId",
                table: "RegrasRotacao",
                column: "BlocoHorarioPostoEscalaId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacao_DepartamentoId_Ordem",
                table: "RegrasRotacao",
                columns: new[] { "DepartamentoId", "Ordem" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacao_UsuarioInativacaoId",
                table: "RegrasRotacao",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacao_UsuarioInsertId",
                table: "RegrasRotacao",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacao_UsuarioUpdateId",
                table: "RegrasRotacao",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacaoHorarios_RegraRotacaoId",
                table: "RegrasRotacaoHorarios",
                column: "RegraRotacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacaoHorarios_UsuarioInativacaoId",
                table: "RegrasRotacaoHorarios",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacaoHorarios_UsuarioInsertId",
                table: "RegrasRotacaoHorarios",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_RegrasRotacaoHorarios_UsuarioUpdateId",
                table: "RegrasRotacaoHorarios",
                column: "UsuarioUpdateId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApoiosPlantonistasEscala");

            migrationBuilder.DropTable(
                name: "DatasEspeciaisEscala");

            migrationBuilder.DropTable(
                name: "EscalasBaseItems");

            migrationBuilder.DropTable(
                name: "EscalasBaseRegrasRotacao");

            migrationBuilder.DropTable(
                name: "HistoricosAlteracaoEscala");

            migrationBuilder.DropTable(
                name: "RegrasRotacaoHorarios");

            migrationBuilder.DropTable(
                name: "EscalasGeradasItems");

            migrationBuilder.DropTable(
                name: "RegrasRotacao");

            migrationBuilder.DropTable(
                name: "EscalasGeradas");

            migrationBuilder.DropTable(
                name: "BlocosHorarioPostosEscala");

            migrationBuilder.DropTable(
                name: "EscalasBase");

            migrationBuilder.DropTable(
                name: "BlocosHorarioEscala");

            migrationBuilder.DropTable(
                name: "PostosEscalas");

            migrationBuilder.DropColumn(
                name: "StatusAcionamento",
                table: "Acionamentos");
        }
    }
}
