using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ScupTel.Infra.Data.EntityFramework.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(type: "bit", maxLength: 1, nullable: false),
                    DataDeRegistro = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coberturas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataDeRegistro = table.Column<DateTime>(nullable: false),
                    DddDestino = table.Column<int>(nullable: false),
                    DddOrigem = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coberturas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProdutosChamada",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DataDeRegistro = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    MinutosFranquia = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProdutosChamada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Telefones",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ClienteId = table.Column<Guid>(nullable: false),
                    DataDeRegistro = table.Column<DateTime>(nullable: false),
                    DddRegistro = table.Column<int>(nullable: false),
                    DdiRegistro = table.Column<int>(nullable: false),
                    Numero = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Telefones", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Telefones_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TarifasChamada",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CoberturaId = table.Column<Guid>(nullable: false),
                    DataDeRegistro = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    FimVigencia = table.Column<DateTime>(nullable: false),
                    InicioVigencia = table.Column<DateTime>(nullable: false),
                    Nome = table.Column<string>(nullable: true),
                    ValorBaseCalculo = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TarifasChamada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TarifasChamada_Coberturas_CoberturaId",
                        column: x => x.CoberturaId,
                        principalTable: "Coberturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ClientesProdutosChamada",
                columns: table => new
                {
                    ClienteId = table.Column<Guid>(nullable: false),
                    ProdutoChamadaId = table.Column<Guid>(nullable: false),
                    Ativo = table.Column<bool>(nullable: false),
                    DataDeRegistro = table.Column<DateTime>(nullable: false),
                    Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientesProdutosChamada", x => new { x.ClienteId, x.ProdutoChamadaId });
                    table.ForeignKey(
                        name: "FK_ClientesProdutosChamada_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ClientesProdutosChamada_ProdutosChamada_ProdutoChamadaId",
                        column: x => x.ProdutoChamadaId,
                        principalTable: "ProdutosChamada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Chamadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CoberturaId = table.Column<Guid>(nullable: false),
                    DataDeRegistro = table.Column<DateTime>(nullable: false),
                    Discriminator = table.Column<string>(nullable: false),
                    Fim = table.Column<DateTime>(nullable: false),
                    Inicio = table.Column<DateTime>(nullable: false),
                    ChamadaBaseId = table.Column<Guid>(nullable: true),
                    ClienteId = table.Column<Guid>(nullable: true),
                    NumeroTelefoneCliente = table.Column<int>(nullable: true),
                    NumeroTelefoneDiscado = table.Column<int>(nullable: true),
                    TelefoneId = table.Column<Guid>(nullable: true),
                    TelefoneId1 = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Chamadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Chamadas_Coberturas_CoberturaId",
                        column: x => x.CoberturaId,
                        principalTable: "Coberturas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chamadas_Chamadas_ChamadaBaseId",
                        column: x => x.ChamadaBaseId,
                        principalTable: "Chamadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chamadas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalTable: "Clientes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chamadas_Telefones_TelefoneId",
                        column: x => x.TelefoneId,
                        principalTable: "Telefones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Chamadas_Telefones_TelefoneId1",
                        column: x => x.TelefoneId1,
                        principalTable: "Telefones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ChamadasTarifadas",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ChamadaRealizadaId = table.Column<Guid>(nullable: false),
                    DataDeRegistro = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChamadasTarifadas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChamadasTarifadas_Chamadas_ChamadaRealizadaId",
                        column: x => x.ChamadaRealizadaId,
                        principalTable: "Chamadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ResumoCalculoTarifasChamada",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Aplicado = table.Column<bool>(nullable: false),
                    ChamadaTarifadaId = table.Column<Guid>(nullable: false),
                    DataDeRegistro = table.Column<DateTime>(nullable: false),
                    TarifaChamadaId = table.Column<Guid>(nullable: false),
                    Valor = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResumoCalculoTarifasChamada", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ResumoCalculoTarifasChamada_ChamadasTarifadas_ChamadaTarifadaId",
                        column: x => x.ChamadaTarifadaId,
                        principalTable: "ChamadasTarifadas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ResumoCalculoTarifasChamada_TarifasChamada_TarifaChamadaId",
                        column: x => x.TarifaChamadaId,
                        principalTable: "TarifasChamada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ClientesProdutosChamada_ProdutoChamadaId",
                table: "ClientesProdutosChamada",
                column: "ProdutoChamadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Telefones_ClienteId",
                table: "Telefones",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamadas_CoberturaId",
                table: "Chamadas",
                column: "CoberturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamadas_ChamadaBaseId",
                table: "Chamadas",
                column: "ChamadaBaseId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamadas_ClienteId",
                table: "Chamadas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamadas_TelefoneId",
                table: "Chamadas",
                column: "TelefoneId");

            migrationBuilder.CreateIndex(
                name: "IX_Chamadas_TelefoneId1",
                table: "Chamadas",
                column: "TelefoneId1");

            migrationBuilder.CreateIndex(
                name: "IX_ChamadasTarifadas_ChamadaRealizadaId",
                table: "ChamadasTarifadas",
                column: "ChamadaRealizadaId");

            migrationBuilder.CreateIndex(
                name: "IX_Coberturas_DddOrigem_DddDestino",
                table: "Coberturas",
                columns: new[] { "DddOrigem", "DddDestino" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ResumoCalculoTarifasChamada_ChamadaTarifadaId",
                table: "ResumoCalculoTarifasChamada",
                column: "ChamadaTarifadaId");

            migrationBuilder.CreateIndex(
                name: "IX_ResumoCalculoTarifasChamada_TarifaChamadaId",
                table: "ResumoCalculoTarifasChamada",
                column: "TarifaChamadaId");

            migrationBuilder.CreateIndex(
                name: "IX_TarifasChamada_CoberturaId",
                table: "TarifasChamada",
                column: "CoberturaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ClientesProdutosChamada");

            migrationBuilder.DropTable(
                name: "ResumoCalculoTarifasChamada");

            migrationBuilder.DropTable(
                name: "ProdutosChamada");

            migrationBuilder.DropTable(
                name: "ChamadasTarifadas");

            migrationBuilder.DropTable(
                name: "TarifasChamada");

            migrationBuilder.DropTable(
                name: "Chamadas");

            migrationBuilder.DropTable(
                name: "Coberturas");

            migrationBuilder.DropTable(
                name: "Telefones");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
