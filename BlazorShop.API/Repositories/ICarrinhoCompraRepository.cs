using BlazorShop.API.Entities;
using BlazorShop.Models.DTOs;

namespace BlazorShop.API.Repositories
{
    public interface ICarrinhoCompraRepository
    {
        Task<CarrinhoItem> AdicionaItem(CarrinhoItemAdicionaDTO carrinhoItemAdicionaDTO);

        Task<CarrinhoItem> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDTO carrinhoItemAtualizaQuantidadeDTO);

        Task<CarrinhoItem> DeletaItem(int id);

        Task<CarrinhoItem> GetItem(int id);

        Task<IEnumerable<CarrinhoItem>> GetItens(string usuarioId);
    }
}
