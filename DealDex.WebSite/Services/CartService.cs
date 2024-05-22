using DealDex.Api.Dto;
using DealDex.Api.Dto.Carrito;
using DealDex.Core.Http;
using DealDex.WebSite.Services.Interfaces;
using Newtonsoft.Json;

namespace DealDex.WebSite.Services;

public class CartService : ICartService
{
    
    public readonly string _baseURL = "http://localhost:5209/";
    private readonly string _endpoint = "api/Cart";

    public CartService()
    {
        
    }
    
    public async Task<Response<List<CarritoCategoryDto>>> GetAllAsync()
    {
        var url = $"{_baseURL}{_endpoint}";
        var cliente = new HttpClient();
        var res = await cliente.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<List<CarritoCategoryDto>>>(json);
        
        return response;
    }

    public async Task<Response<CarritoCategoryDto>> GetById(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";
        var cliente = new HttpClient();
        var res = await cliente.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<CarritoCategoryDto>>(json);
        return response;
    }

    public async Task<Response<CarritoCategoryDto>> SaveAsync(CarritoCategoryDto carritoDto)
    {
        var url = $"{_baseURL}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(carritoDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<CarritoCategoryDto>>(json);

        return response;
    }

    public async Task<Response<CarritoCategoryDto>> UpdateAsync(CarritoCategoryDto carritoDto)
    {
        var url = $"{_baseURL}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(carritoDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<CarritoCategoryDto>>(json);
        return response;
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";
        var cliente = new HttpClient();
        var res = await cliente.DeleteAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<bool>>(json);
        return response;
    }
}