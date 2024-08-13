using APICatalogo.Context;
using APICatalogo.DTOs;
using APICatalogo.DTOs.Mappings;
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
    public ActionResult<IEnumerable<CategoriaDTO>> Get()
    {
        var categorias = _uow.CategoriaRepository.List().ToList();
        if(categorias is null)
        {
            var msg = "Categorias não encontradas...";
            _logger.LogWarning(msg);
            return NotFound(msg);
        }
        var categoriasDto = categorias.ToCategoriaDTOList();
        return Ok(categoriasDto);
    }

    [HttpGet("{id:int}", Name = "ObterCategoria")]
    public ActionResult<CategoriaDTO> Get(int id)
    {
        var categoria = _uow.CategoriaRepository.Get(c => c.CategoriaId == id);

        if (categoria == null)
        {   
            _logger.LogWarning($"A categoria id {id} não foi encontrada");
            return NotFound("Categoria não encontrada...");
        }
        var categoriaDTO = categoria.ToCategoriaDTO();
        return Ok(categoriaDTO);
    }

    [HttpPost]
    public ActionResult Post(CategoriaDTO categoriaDto)
    {
        if (categoriaDto is null){
            _logger.LogWarning("Categoria nula...");
            return BadRequest();
        }
        var categoria = categoriaDto.ToCategoria();

        _uow.CategoriaRepository.Create(categoria);
        _uow.Commit();

        return new CreatedAtRouteResult("ObterCategoria",
            new { id = categoria.CategoriaId }, categoria);
    }

    [HttpPut("{id:int}")]
    public ActionResult<CategoriaDTO> Put(int id, CategoriaDTO categoriaDto)
    {
        if (id != categoriaDto.CategoriaId)
        {
            return BadRequest();
        }

        var categoria = categoriaDto.ToCategoria();
        
        _uow.CategoriaRepository.Update(categoria);
        _uow.Commit();
        return Ok(categoria);
    }

    [HttpDelete("{id:int}")]
    public ActionResult<CategoriaDTO> Delete(int id)
    {
        var categoria = _uow.CategoriaRepository.Get(categoria => categoria.CategoriaId == id);
        if (categoria == null)
        {
            return NotFound($"A categoria com id {id} não foi encontrada");
        }
        _uow.CategoriaRepository.Delete(categoria);
        _uow.Commit();
        var categoriaDto = categoria.ToCategoriaDTO();
        return Ok(categoriaDto);
    }

    // [HttpGet("produtos")]
    // public ActionResult<IEnumerable<Categoria>> GetCategoriasProdutos()
    // {
    //   return _context.Categorias.Include(p => p.Produtos).ToList();
    // }
}