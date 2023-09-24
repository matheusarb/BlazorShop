using BlazorShop.Api.Context;
using BlazorShop.API.Entities;
using BlazorShop.Models.DTOs;
using Microsoft.AspNetCore.Http.Connections;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.API.Repositories
{
    public class CarrinhoCompraRepository : ICarrinhoCompraRepository
    {
        private readonly AppDbContext _context;

        public CarrinhoCompraRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<CarrinhoItem> AdicionaItem(CarrinhoItemAdicionaDTO carrinhoItemAdicionaDTO)
        {
            throw new NotImplementedException();
        }

        public Task<CarrinhoItem> AtualizaQuantidade(int id, CarrinhoItemAtualizaQuantidadeDTO carrinhoItemAtualizaQuantidadeDTO)
        {
            throw new NotImplementedException();
        }

        public Task<CarrinhoItem> DeletaItem(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<CarrinhoItem> GetItem(int id)
        {
            return await (from carrinho in _context.Carrinhos
                          join carrinhoItem in _context.CarrinhoItens
                          on carrinho.Id equals carrinhoItem.Id
                          where carrinhoItem.Id == id
                          select new CarrinhoItem
                          {
                              Id = carrinhoItem.Id,
                              ProdutoId = carrinhoItem.ProdutoId,
                              Quantidade = carrinhoItem.Quantidade,
                              CarrinhoId = carrinhoItem.CarrinhoId,
                          }).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<CarrinhoItem>> GetItens(string usuarioId)
        {
            return await(from carrinho in _context.Carrinhos
                         join carrinhoItem in _context.CarrinhoItens
                         on carrinho.Id equals carrinhoItem.CarrinhoId
                         where carrinho.UsuarioId == usuarioId
                         select new CarrinhoItem
                         {
                             Id = carrinhoItem.Id,
                             ProdutoId = carrinhoItem.ProdutoId,
                             Quantidade = carrinhoItem.Quantidade,
                             CarrinhoId = carrinhoItem.CarrinhoId
                         }).ToListAsync();
        }
    }
}
