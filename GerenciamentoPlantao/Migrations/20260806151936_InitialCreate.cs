using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GerenciamentoPlantao.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Acionamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataAcionamento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CanalId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Acionador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NrAtendimento = table.Column<int>(type: "int", nullable: true),
                    EstabelecimentoId = table.Column<int>(type: "int", nullable: false),
                    SetorId = table.Column<int>(type: "int", nullable: false),
                    CategoriaAcionamentoId = table.Column<int>(type: "int", nullable: false),
                    Apoio = table.Column<bool>(type: "bit", nullable: false),
                    DescProblema = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SolucaoId = table.Column<int>(type: "int", nullable: false),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
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
                    table.PrimaryKey("PK_Acionamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DescNome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Plantonista = table.Column<bool>(type: "bit", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false),
                    Perfil = table.Column<int>(type: "int", nullable: false),
                    DepartamentoId = table.Column<int>(type: "int", nullable: false),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departamentos_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Departamentos_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Departamentos_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Estabelecimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    DataInsert = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UsuarioInsertId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataUpdate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioUpdateId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    DataInativacao = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UsuarioInativacaoId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estabelecimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Estabelecimentos_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Estabelecimentos_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Estabelecimentos_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Canais",
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
                    table.PrimaryKey("PK_Canais", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Canais_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Canais_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Canais_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Canais_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CategoriasAcionamento",
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
                    table.PrimaryKey("PK_CategoriasAcionamento", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoriasAcionamento_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CategoriasAcionamento_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CategoriasAcionamento_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_CategoriasAcionamento_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Solucoes",
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
                    table.PrimaryKey("PK_Solucoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Solucoes_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Solucoes_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Solucoes_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Solucoes_Departamentos_DepartamentoId",
                        column: x => x.DepartamentoId,
                        principalTable: "Departamentos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Setores",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EstabelecimentoId = table.Column<int>(type: "int", nullable: false),
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
                    table.PrimaryKey("PK_Setores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Setores_AspNetUsers_UsuarioInativacaoId",
                        column: x => x.UsuarioInativacaoId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Setores_AspNetUsers_UsuarioInsertId",
                        column: x => x.UsuarioInsertId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Setores_AspNetUsers_UsuarioUpdateId",
                        column: x => x.UsuarioUpdateId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Setores_Estabelecimentos_EstabelecimentoId",
                        column: x => x.EstabelecimentoId,
                        principalTable: "Estabelecimentos",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Acionamentos_CanalId",
                table: "Acionamentos",
                column: "CanalId");

            migrationBuilder.CreateIndex(
                name: "IX_Acionamentos_CategoriaAcionamentoId",
                table: "Acionamentos",
                column: "CategoriaAcionamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Acionamentos_EstabelecimentoId",
                table: "Acionamentos",
                column: "EstabelecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Acionamentos_SetorId",
                table: "Acionamentos",
                column: "SetorId");

            migrationBuilder.CreateIndex(
                name: "IX_Acionamentos_SolucaoId",
                table: "Acionamentos",
                column: "SolucaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Acionamentos_UsuarioId",
                table: "Acionamentos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Acionamentos_UsuarioInativacaoId",
                table: "Acionamentos",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Acionamentos_UsuarioInsertId",
                table: "Acionamentos",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_Acionamentos_UsuarioUpdateId",
                table: "Acionamentos",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_DepartamentoId",
                table: "AspNetUsers",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UsuarioInativacaoId",
                table: "AspNetUsers",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UsuarioInsertId",
                table: "AspNetUsers",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UsuarioUpdateId",
                table: "AspNetUsers",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Canais_DepartamentoId",
                table: "Canais",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Canais_UsuarioInativacaoId",
                table: "Canais",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Canais_UsuarioInsertId",
                table: "Canais",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_Canais_UsuarioUpdateId",
                table: "Canais",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasAcionamento_DepartamentoId",
                table: "CategoriasAcionamento",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasAcionamento_UsuarioInativacaoId",
                table: "CategoriasAcionamento",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasAcionamento_UsuarioInsertId",
                table: "CategoriasAcionamento",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoriasAcionamento_UsuarioUpdateId",
                table: "CategoriasAcionamento",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_UsuarioInativacaoId",
                table: "Departamentos",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_UsuarioInsertId",
                table: "Departamentos",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_Departamentos_UsuarioUpdateId",
                table: "Departamentos",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_Estabelecimentos_UsuarioInativacaoId",
                table: "Estabelecimentos",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Estabelecimentos_UsuarioInsertId",
                table: "Estabelecimentos",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_Estabelecimentos_UsuarioUpdateId",
                table: "Estabelecimentos",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_Setores_EstabelecimentoId",
                table: "Setores",
                column: "EstabelecimentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Setores_UsuarioInativacaoId",
                table: "Setores",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Setores_UsuarioInsertId",
                table: "Setores",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_Setores_UsuarioUpdateId",
                table: "Setores",
                column: "UsuarioUpdateId");

            migrationBuilder.CreateIndex(
                name: "IX_Solucoes_DepartamentoId",
                table: "Solucoes",
                column: "DepartamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_Solucoes_UsuarioInativacaoId",
                table: "Solucoes",
                column: "UsuarioInativacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Solucoes_UsuarioInsertId",
                table: "Solucoes",
                column: "UsuarioInsertId");

            migrationBuilder.CreateIndex(
                name: "IX_Solucoes_UsuarioUpdateId",
                table: "Solucoes",
                column: "UsuarioUpdateId");

            migrationBuilder.AddForeignKey(
                name: "FK_Acionamentos_AspNetUsers_UsuarioId",
                table: "Acionamentos",
                column: "UsuarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acionamentos_AspNetUsers_UsuarioInativacaoId",
                table: "Acionamentos",
                column: "UsuarioInativacaoId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acionamentos_AspNetUsers_UsuarioInsertId",
                table: "Acionamentos",
                column: "UsuarioInsertId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acionamentos_AspNetUsers_UsuarioUpdateId",
                table: "Acionamentos",
                column: "UsuarioUpdateId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acionamentos_Canais_CanalId",
                table: "Acionamentos",
                column: "CanalId",
                principalTable: "Canais",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acionamentos_CategoriasAcionamento_CategoriaAcionamentoId",
                table: "Acionamentos",
                column: "CategoriaAcionamentoId",
                principalTable: "CategoriasAcionamento",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acionamentos_Estabelecimentos_EstabelecimentoId",
                table: "Acionamentos",
                column: "EstabelecimentoId",
                principalTable: "Estabelecimentos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acionamentos_Setores_SetorId",
                table: "Acionamentos",
                column: "SetorId",
                principalTable: "Setores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Acionamentos_Solucoes_SolucaoId",
                table: "Acionamentos",
                column: "SolucaoId",
                principalTable: "Solucoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                table: "AspNetUserClaims",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                table: "AspNetUserLogins",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                table: "AspNetUserRoles",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Departamentos_DepartamentoId",
                table: "AspNetUsers",
                column: "DepartamentoId",
                principalTable: "Departamentos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departamentos_AspNetUsers_UsuarioInativacaoId",
                table: "Departamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Departamentos_AspNetUsers_UsuarioInsertId",
                table: "Departamentos");

            migrationBuilder.DropForeignKey(
                name: "FK_Departamentos_AspNetUsers_UsuarioUpdateId",
                table: "Departamentos");

            migrationBuilder.DropTable(
                name: "Acionamentos");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "Canais");

            migrationBuilder.DropTable(
                name: "CategoriasAcionamento");

            migrationBuilder.DropTable(
                name: "Setores");

            migrationBuilder.DropTable(
                name: "Solucoes");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Estabelecimentos");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Departamentos");
        }
    }
}
