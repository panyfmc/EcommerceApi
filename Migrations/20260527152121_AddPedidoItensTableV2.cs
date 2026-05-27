using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EcommerceApi.Migrations
{
    /// <inheritdoc />
    public partial class AddPedidoItensTableV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItem_Pedidos_PedidoId",
                table: "PedidoItem");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItem_Produtos_ProdutoId",
                table: "PedidoItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoItem",
                table: "PedidoItem");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "PedidoItem");

            migrationBuilder.RenameTable(
                name: "PedidoItem",
                newName: "PedidoItems");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoItem_ProdutoId",
                table: "PedidoItems",
                newName: "IX_PedidoItems_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoItem_PedidoId",
                table: "PedidoItems",
                newName: "IX_PedidoItems_PedidoId");

            migrationBuilder.AddColumn<decimal>(
                name: "ValorUnico",
                table: "PedidoItems",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoItems",
                table: "PedidoItems",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItems_Pedidos_PedidoId",
                table: "PedidoItems",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItems_Produtos_ProdutoId",
                table: "PedidoItems",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItems_Pedidos_PedidoId",
                table: "PedidoItems");

            migrationBuilder.DropForeignKey(
                name: "FK_PedidoItems_Produtos_ProdutoId",
                table: "PedidoItems");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PedidoItems",
                table: "PedidoItems");

            migrationBuilder.DropColumn(
                name: "ValorUnico",
                table: "PedidoItems");

            migrationBuilder.RenameTable(
                name: "PedidoItems",
                newName: "PedidoItem");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoItems_ProdutoId",
                table: "PedidoItem",
                newName: "IX_PedidoItem_ProdutoId");

            migrationBuilder.RenameIndex(
                name: "IX_PedidoItems_PedidoId",
                table: "PedidoItem",
                newName: "IX_PedidoItem_PedidoId");

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "PedidoItem",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddPrimaryKey(
                name: "PK_PedidoItem",
                table: "PedidoItem",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItem_Pedidos_PedidoId",
                table: "PedidoItem",
                column: "PedidoId",
                principalTable: "Pedidos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PedidoItem_Produtos_ProdutoId",
                table: "PedidoItem",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
