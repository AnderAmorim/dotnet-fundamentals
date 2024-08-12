using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Repositories;

public class CategoriaRepository : ICategoriaRepository
{
    private readonly AppDbContext? _context;

    public IEnumerable<Categoria> List()
    {
        return _context.Categorias.ToList();  
    }

    public Categoria Get(int id)
    {
        return _context.Categorias.FirstOrDefault(p => p.CategoriaId == id);
    }

    public Categoria Create(Categoria categoria)
    {
        if(categoria is null)
        {
            throw new ArgumentNullException(nameof(categoria));
        }
        _context.Categorias.Add(categoria);
        _context.SaveChanges();
        return categoria;
    }

    public Categoria Update(Categoria categoria)
    {
        _context.Entry(categoria).State = EntityState.Modified;
        _context.SaveChanges();
        return categoria;
    }

    public Categoria Delete(int id)
    {
        var categoria = _context.Categorias.Find(id);
        if(categoria is null)
        {
            throw new ArgumentNullException(nameof(categoria));
        }
        _context.Categorias.Remove(categoria);
        _context.SaveChanges();
        return categoria;
    }

}