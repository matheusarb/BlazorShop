using BlazorShop.Models.DTOs;
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

        public async Task<ProdutoDTO> GetItem(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/produtos/{id}");

                if (response.IsSuccessStatusCode) //Status code 200-299
                {
                    if (response.StatusCode == HttpStatusCode.NoContent)
                    {
                        return default(ProdutoDTO); //retorna os valores padrão/empty
                    }
                    return await response.Content.ReadFromJsonAsync<ProdutoDTO>();
                }
                else
                {
                    var message = await response.Content.ReadAsStringAsync();
                    _logger.LogError($"Erro ao obter produto pelo id = {id} - {message}");
                    throw new Exception($"Status Code: {response.StatusCode} - {message}");
                }
            }
            catch (Exception)
            {
                _logger.LogError($"Erro ao obter produto pelo id = {id}");
                throw;
            }
            
        }        
    }
}
