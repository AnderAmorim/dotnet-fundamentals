using APICatalogo.Context;
using APICatalogo.Models;
using APICatalogo.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdutosController : ControllerBase
    { 
        private readonly IRepository<Produto> _context;

        public ProdutosController(IRepository<Produto> context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
          
          return _context.List().ToList();
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            return _context.Get(p => p.ProdutoId == id);
        }
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
          if (produto is null)
            return BadRequest("Produto é nulo");
          
          _context.Create(produto);
          return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
          if (id != produto.ProdutoId)
            return BadRequest("Produto não encontrado");

          _context.Update(produto);
          return Ok(produto);
        }

        [HttpDelete("{id:int}")] 
        public ActionResult Delete(int id)
        {
          var produto = _context.Get(p => p.ProdutoId == id);
          if(produto is null){
            return NotFound("Produto não encontrado");
          }
          _context.Delete(produto);
          return Ok(produto);
        }
    }
}