using APICatalogo.Context;

namespace APICatalogo.Repositories;
public class UnitOfWork: IUnitOfWork
{
    private readonly AppDbContext _context;
    private IProdutoRepository _produtoRepository;
    private ICategoriaRepository _categoriaRepository;

    public UnitOfWork(AppDbContext contexto)
    {
        _context = contexto;
    }

    public IProdutoRepository ProdutoRepository
    {
        get
        {
            return _produtoRepository ??= new ProdutoRepository(_context);
        }
    }

    public ICategoriaRepository CategoriaRepository
    {
        get
        {
            return _categoriaRepository ??= new CategoriaRepository(_context);
        }
    }

    public void Commit()
    {
        _context.SaveChanges();
    }
}