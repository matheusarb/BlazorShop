using BlazorShop.Api.Context;
using BlazorShop.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.API.Repositories
{
    public class ProdutoRepository : IProdutoRepository
    {
        private readonly AppDbContext _context;

        public ProdutoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Produto> GetItem(int id)
        {
            var produto = await _context.Produtos
                          .Include(c => c.Categoria)
                          .SingleOrDefaultAsync(c => c.Id == id);
            return produto;
        }

        public async Task<IEnumerable<Produto>> GetItens()
        {
            var listaProdutos = await _context.Produtos
                                .Include(c => c.Categoria)
                                .ToListAsync();
            return listaProdutos;
        }

        public async Task<IEnumerable<Produto>> GetItensPorCategoria(int id)
        {
            var produtoPorCategoria = await _context.Produtos
                                      .Include(c => c.Categoria)
                                      .Where(x => x.CategoriaId == id).ToListAsync();
            return produtoPorCategoria;
        }
    }
}
