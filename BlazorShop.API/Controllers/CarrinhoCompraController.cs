using BlazorShop.API.Mappings;
using BlazorShop.API.Repositories;
using BlazorShop.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.IIS.Core;

namespace BlazorShop.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarrinhoCompraController : Controller
    {
        private readonly ICarrinhoCompraRepository carrinhoCompraRepo;
        private readonly IProdutoRepository produtoRepo;
        private readonly ILogger<CarrinhoCompraController> logger;

        public CarrinhoCompraController(ICarrinhoCompraRepository carrinhoCompraRepository, IProdutoRepository produtoRepository, ILogger<CarrinhoCompraController> logger)
        {
            carrinhoCompraRepo = carrinhoCompraRepository;
            produtoRepo = produtoRepository;
            this.logger = logger;
        }

        [HttpGet]
        [Route("{usuarioId}/GetItens")]
        public async Task<ActionResult<IEnumerable<CarrinhoItemDTO>>> GetItens(string usuarioId)
        {
            try
            {
                var carrinhoItens = await carrinhoCompraRepo.GetItens(usuarioId);
                if (carrinhoItens == null)
                {
                    return NoContent(); // 204 status code
                }

                var produtos = await produtoRepo.GetItens();
                if (produtos == null)
                {
                    throw new Exception("Não existem produtos...");
                }

                var carrinhoItensDto = carrinhoItens.ConverterCarrinhoItensParaDto(produtos);
                return Ok(carrinhoItensDto);
            }
            catch (Exception ex)
            {
                logger.LogError($"## Erro ao obter itens do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<CarrinhoItemDTO>> GetItem(int id)
        {
            try
            {
                var carrinhoItem = await carrinhoCompraRepo.GetItem(id);
                if (carrinhoItem == null)
                {
                    return NotFound("Item não encontrado");
                }

                var produto = await produtoRepo.GetItem(carrinhoItem.ProdutoId);
                if (produto == null)
                {
                    return NotFound("Não existe produto na fonte de dados");
                }

                var cartItemDto = carrinhoItem.ConverterCarrinhoItemParaDto(produto);
                return Ok(cartItemDto);
            }
            catch (Exception ex)
            {
                logger.LogError($"## Erro ao obter itens do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult<CarrinhoItemDTO>> PostItem([FromBody] CarrinhoItemAdicionaDTO carrinhoItemAdicionaDTO)
        {
            try
            {
                var novoCarrinhoItem = await carrinhoCompraRepo.AdicionaItem(carrinhoItemAdicionaDTO);
                if (novoCarrinhoItem == null)
                {
                    return NoContent(); //Status 204
                }

                var produto = await produtoRepo.GetItem(novoCarrinhoItem.ProdutoId);

                if (produto == null)
                {
                    throw new Exception($"Produto não localizado (Id: {novoCarrinhoItem.ProdutoId})");
                }

                var novoCarrinhoItemDto = novoCarrinhoItem.ConverterCarrinhoItemParaDto(produto);
                return CreatedAtAction(nameof(GetItem), new { id = novoCarrinhoItemDto.Id }, novoCarrinhoItemDto);
            }
            catch (Exception ex)
            {
                logger.LogError($"## Erro ao obter itens do carrinho");
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<CarrinhoItemDTO>> DeleteItem(int id)
        {
            try
            {
                var carrinhoItem = await carrinhoCompraRepo.DeletaItem(id);
                if (carrinhoItem == null)
                {
                    return NotFound();
                }

                var produto = await produtoRepo.GetItem(carrinhoItem.ProdutoId);
                if (produto is null)
                    return NotFound();

                var carrinhoItemDto = carrinhoItem.ConverterCarrinhoItemParaDto(produto);
                return Ok(carrinhoItemDto);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
