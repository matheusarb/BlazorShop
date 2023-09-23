﻿using BlazorShop.Models.DTOs;
using System.Net.Http.Json;
using System.Net;

namespace BlazorShop.Web.Services
{
    public class ProdutoService : IProdutoService
    {
        public HttpClient _httpClient;
        public ILogger<ProdutoService> _logger;

        public ProdutoService(HttpClient httpClient, ILogger<ProdutoService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<IEnumerable<ProdutoDTO>> GetItens()
        {
            try
            {
                var produtosDto = await _httpClient.
                               GetFromJsonAsync<IEnumerable<ProdutoDTO>>("api/produtos");
            
                return produtosDto;
            }
            catch (Exception)
            {
                _logger.LogError("Erro ao acessar produtos: api/produtos");
                throw; 
            }
        }
    }
}
