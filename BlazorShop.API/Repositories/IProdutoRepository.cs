using BlazorShop.API.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections;

namespace BlazorShop.API.Repositories
{
    public interface IProdutoRepository
    {
        Task<IEnumerable<Produto>> GetItens();
        Task<Produto> GetItem(int id);
        Task<IEnumerable<Produto>> GetItensPorCategoria(int id);
    }
}
