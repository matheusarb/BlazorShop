﻿using BlazorShop.Models.DTOs;
using System.Net;
using System.Net.Http.Json;

namespace BlazorShop.Web.Services
{
    public class CarrinhoCompraService : ICarrinhoCompraService
    {
        private readonly HttpClient _httpClient;

        public CarrinhoCompraService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<CarrinhoItemDTO> AdicionaItem(CarrinhoItemAdicionaDTO carrinhoItemAdicionaDTO)
        {
            try
            {
                var response = await _httpClient.PostAsJsonAsync("api/CarrinhoCompra", carrinhoItemAdicionaDTO);
                if (response.IsSuccessStatusCode) //Status code entre 200 299
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        // retorna o valor padrão ou vazio para um objeto do tipo CarrinhoItemDTO
                        return default(CarrinhoItemDTO);
                    }
                    // lê o conteúdo HTTP e retorna o valor resultante
                    // da serialização do conteúdo JSON para o objeto DTO
                    return await response.Content.ReadFromJsonAsync<CarrinhoItemDTO>();
                }
                else
                {
                    // serializa o conteúdo HTTP como uma string
                    var message = response.Content.ReadAsStringAsync();
                    throw new Exception($"{response.StatusCode} Message - {message}");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<CarrinhoItemDTO>> GetItens(string usuarioId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/CarrinhoCompra/{usuarioId}/GetItens");

                if (response.IsSuccessStatusCode)
                {
                    if (response.StatusCode == System.Net.HttpStatusCode.NoContent)
                    {
                        return Enumerable.Empty<CarrinhoItemDTO>().ToList();
                    }
                    return await response.Content.ReadFromJsonAsync<List<CarrinhoItemDTO>>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    throw new Exception($"Http Status Code: {response.StatusCode} Mensagem: {message}");
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<CarrinhoItemDTO> DeletaItem(int id)
        {
            try
            {
                var response = await _httpClient.DeleteAsync($"api/CarrinhoCompra/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadFromJsonAsync<CarrinhoItemDTO>();
                }
                return default(CarrinhoItemDTO);
            }
            catch (Exception)
            {
                throw;
            }
        }


    }
}
