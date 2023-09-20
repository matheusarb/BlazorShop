using BlazorShop.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace BlazorShop.API.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Carrinho>? Carrinhos { get; set; }
        public DbSet<CarrinhoItem>? CarrinhoItens { get; set; }
        public DbSet<Produto>? Produtos { get; set; }
        public DbSet<Categoria>? Categorias { get; set; }
        public DbSet<Usuario>? Usuarios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Categoria>().HasData( new Categoria
            {
                Id = 1,
                Nome = "Beleza",
                IconCSS = "fas fa-spa"
            });

            modelBuilder.Entity<Produto>().HasData(new Produto
            {
                Id = 1,
                Nome = "Glossier - Beleza Kit",
                Descricao = "Um kit fornecido pela Natura, contendo produtos para cuidados com a pele",
                ImagemUrl = "/Imagens/Beleza/Beleza1.png",
                Preco = 100,
                Quantidade = 100,
                CategoriaId = 1
            });

            modelBuilder.Entity<Usuario>().HasData(new Usuario
            {
                Id=1,
                NomeUsuario = "Matheus"
            });
        }
    }
}
