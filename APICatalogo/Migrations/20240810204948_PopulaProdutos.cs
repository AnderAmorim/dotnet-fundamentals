using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APICatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulaProdutos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) VALUES('Coca-Cola', 'Refrigerante de cola', 5.45, 'coca.jpg', 50, '2021-09-01', 1)");
            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) VALUES('Pepsi', 'Refrigerante de cola', 5.45, 'pepsi.jpg', 50, '2021-09-01', 1)");
            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) VALUES('Guaraná Antártica', 'Refrigerante de guaraná', 5.45, 'guarana.jpg', 50, '2021-09-01', 1)");
            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) VALUES('X-Bacon', 'Pão, hambúrguer, queijo, bacon, ovo e salada', 15.90, 'xbacon.jpg', 100, '2021-09-01', 2)");
            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) VALUES('X-Tudo', 'Pão, hambúrguer, queijo, presunto, bacon, ovo, milho, ervilha, tomate e salada', 18.90, 'xtudo.jpg', 100, '2021-09-01', 2)");
            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) VALUES('Misto Quente', 'Pão, queijo e presunto', 7.50, 'misto.jpg', 100, '2021-09-01', 2)");
            migrationBuilder.Sql("INSERT INTO Produtos(Nome, Descricao, Preco, ImagemUrl, Estoque, DataCadastro, CategoriaId) VALUES('Petit Gateau', 'Bolo de chocolate com recheio cremoso de chocolate', 12.90, 'petit_gateau.jpg', 20, '2021-09-01', 3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Produtos");
        }
    }
}
