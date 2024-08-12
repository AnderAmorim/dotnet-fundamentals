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
        private readonly IUnitOfWork _uow;

        public ProdutosController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Produto>> Get()
        {
          
          return _uow.ProdutoRepository.List().ToList();
        }

        [HttpGet("{id:int:min(1)}", Name = "ObterProduto")]
        public async Task<ActionResult<Produto>> Get(int id)
        {
            return _uow.ProdutoRepository.Get(p => p.ProdutoId == id);
        }
        [HttpPost]
        public ActionResult Post(Produto produto)
        {
          if (produto is null)
            return BadRequest("Produto é nulo");
          
          _uow.ProdutoRepository.Create(produto);
          _uow.Commit();
          return new CreatedAtRouteResult("ObterProduto", new { id = produto.ProdutoId }, produto);
        }

        [HttpPut("{id:int}")]
        public ActionResult Put(int id, Produto produto)
        {
          if (id != produto.ProdutoId)
            return BadRequest("Produto não encontrado");

          _uow.ProdutoRepository.Update(produto);
          _uow.Commit();
          return Ok(produto);
        }

        [HttpDelete("{id:int}")] 
        public ActionResult Delete(int id)
        {
          var produto = _uow.ProdutoRepository.Get(p => p.ProdutoId == id);
          if(produto is null){
            return NotFound("Produto não encontrado");
          }
          _uow.ProdutoRepository.Delete(produto);
          _uow.Commit();
          return Ok(produto);
        }
    }
}