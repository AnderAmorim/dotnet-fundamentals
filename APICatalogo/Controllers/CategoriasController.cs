using APICatalogo.Context;
using APICatalogo.Filters;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers;

[Route("[controller]")]
[ApiController]
public class CategoriasController : ControllerBase
{
    private readonly IUnitOfWork _uow;
    private readonly ILogger<CategoriasController> _logger;
    public CategoriasController(IUnitOfWork uow, ILogger<CategoriasController> logger)
    {
        _uow = uow;
        _logger = logger;
    }

    [HttpGet]
    [ServiceFilter(typeof(ApiLoggingFilter))]
    public ActionResult<IEnumerable<Categoria>> Get()
    {
        return _uow.CategoriaRepository.List().ToList();
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<Categoria> Get(int id)
    {
        var categoria = _uow.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria == null)
        {   
            _logger.LogWarning($"A categoria id {id} não foi encontrada");
            return NotFound("Categoria não encontrada...");
        }
        return Ok(categoria);
    }

    [HttpPost]
    public ActionResult Post(Categoria categoria)
    {
        if (categoria is null){
            _logger.LogWarning("Categoria nula...");
            return BadRequest();
        }
        _uow.CategoriaRepository.Create(categoria);
        _uow.Commit();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public ActionResult Put(int id, Categoria categoria)
    {
        if (id != categoria.CategoriaId)
        {
            return BadRequest();
        }
        
        _uow.CategoriaRepository.Update(categoria);
        _uow.Commit();
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult Delete(int id)
    {
        var categoria = _uow.CategoriaRepository.Get(categoria => categoria.CategoriaId == id);
        if (categoria == null)
        {
            return NotFound($"A categoria com id {id} não foi encontrada");
        }
        _uow.CategoriaRepository.Delete(categoria);
        _uow.Commit();
        return Ok(categoria);
    }

    // [HttpGet("produtos")]
    // public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    // {
    //   return _context.Categorias.Include(p => p.Produtos).ToList();
    // }
}