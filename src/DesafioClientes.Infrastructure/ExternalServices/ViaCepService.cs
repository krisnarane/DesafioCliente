using System.Text.Json;
using DesafioClientes.Application.DTOs;
using DesafioClientes.Application.Interfaces;

namespace DesafioClientes.Infrastructure.ExternalServices;

public class ViaCepService : IViaCepService
{
    private readonly HttpClient _httpClient;
    private readonly JsonSerializerOptions _jsonOptions;

    public ViaCepService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://viacep.com.br/");
        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }

    public async Task<ViaCepResponseDTO?> ObterEnderecoPorCepAsync(string cep)
    {
        var cepLimpo = cep.Replace("-", "").Trim();

        if (string.IsNullOrEmpty(cepLimpo) || cepLimpo.Length != 8)
            return null;

        try
        {
            var response = await _httpClient.GetAsync($"ws/{cepLimpo}/json/");

            if (!response.IsSuccessStatusCode)
                return null;

            var content = await response.Content.ReadAsStringAsync();
            var viaCepResponse = JsonSerializer.Deserialize<ViaCepResponseDTO>(content, _jsonOptions);

            if (viaCepResponse != null && string.IsNullOrEmpty(viaCepResponse.Cep))
                return null;

            return viaCepResponse;
        }
        catch
        {
            return null;
        }
    }
}
