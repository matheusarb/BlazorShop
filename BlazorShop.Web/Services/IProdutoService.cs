﻿using BlazorShop.Models.DTOs;

namespace BlazorShop.Web.Services
{
    public interface IProdutoService
    {
        Task<IEnumerable<ProdutoDTO>> GetItens();

        Task<ProdutoDTO> GetItem(int id);
    }
}
