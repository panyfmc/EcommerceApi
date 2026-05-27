using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class AdicionarPedidoIdEmPedidoItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItems_Pedidos_PedidoId",
                table: "PedidoItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "PedidoId",
                table: "PedidoItems",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItems_Pedidos_PedidoId",
                table: "PedidoItems",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItems_Pedidos_PedidoId",
                table: "PedidoItems");

            migrationBuilder.AlterColumn<Guid>(
                name: "PedidoId",
                table: "PedidoItems",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItems_Pedidos_PedidoId",
                table: "PedidoItems",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");
        }
    }
}
