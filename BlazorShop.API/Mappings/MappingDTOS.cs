using BlazorShop.API.Entities;
using BlazorShop.Models.DTOs;

namespace BlazorShop.API.Mappings
{
    public static class MappingDTOS
    {
        public static IEnumerable<CategoriaDTO> ConverterCategoriasParaDto(this IEnumerable<Categoria> categorias)
        {
            return (from categoria in categorias
                    select new CategoriaDTO
                    {
                        Id = categoria.Id,
                        Nome = categoria.Nome,
                        IconCSS = categoria.IconCSS,
                    }).ToList();
        }

        public static IEnumerable<ProdutoDTO> ConverterProdutosParaDto(this IEnumerable<Produto> produtos)
        {
            return (from produto in produtos
                    select new ProdutoDTO
                    {
                        Id = produto.Id,
                        Nome = produto.Nome,
                        Descricao = produto.Descricao,
                        ImagemUrl = produto.ImagemUrl,
                        Preco = produto.Preco,
                        Quantidade = produto.Quantidade,
                        CategoriaId = produto.Categoria.Id,
                        CategoriaNome = produto.Categoria.Nome
                    }).ToList();
        }

        public static ProdutoDTO ConverterProdutoParaDto(this Produto produto)
        {
            return new ProdutoDTO
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                ImagemUrl = produto.ImagemUrl,
                Preco = produto.Preco,
                Quantidade = produto.Quantidade,
                CategoriaId = produto.Categoria.Id,
                CategoriaNome = produto.Categoria.Nome
            };
        }

        public static IEnumerable<CarrinhoItemDTO> ConverterCarrinhoItensParaDto(
            this IEnumerable<CarrinhoItem> carrinhoItens, IEnumerable<Produto> produtos)
        {
            return (from carrinhoItem in carrinhoItens
                    join produto in produtos
                    on carrinhoItem.ProdutoId equals produto.Id
                    select new CarrinhoItemDTO
                    {
                        Id = carrinhoItem.Id,
                        ProdutoId = carrinhoItem.ProdutoId,
                        ProdutoNome = produto.Nome,
                        ProdutoDescricao = produto.Descricao,
                        ProdutoImagemURL = produto.ImagemUrl,
                        Preco = produto.Preco,
                        CarrinhoId = carrinhoItem.CarrinhoId,
                        Quantidade = carrinhoItem.Quantidade,
                        PrecoTotal = produto.Preco * carrinhoItem.Quantidade
                    }).ToList();
        }

        public static CarrinhoItemDTO ConverterCarrinhoItemParaDto(
            this CarrinhoItem carrinhoItem, Produto produto)
        {
            return new CarrinhoItemDTO
            {
                Id = carrinhoItem.Id,
                ProdutoId = carrinhoItem.ProdutoId,
                ProdutoNome = produto.Nome,
                ProdutoDescricao = produto.Descricao,
                ProdutoImagemURL = produto.ImagemUrl,
                Preco = produto.Preco,
                CarrinhoId = carrinhoItem.CarrinhoId,
                Quantidade = carrinhoItem.Quantidade,
                PrecoTotal = produto.Preco * carrinhoItem.Quantidade
            };
        }
    }
}