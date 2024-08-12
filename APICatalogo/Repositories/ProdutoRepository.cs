using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;
public class ProdutoRepository : Repository<Produto>,IProdutoRepository
{
    public ProdutoRepository(AppDbContext context):base(context)
    {
    }
    
    public IEnumerable<Produto> GetProdutosPorCategoria(int idCategoria)
    {
        return List().Where(p => p.CategoriaId == idCategoria).ToList();
    }
}