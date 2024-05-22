using DealDex.Api.Dto;
using DealDex.Core.Http;
using DealDex.WebSite.Services.Interfaces;
using Newtonsoft.Json;

namespace DealDex.WebSite.Services;

public class UsersService : IUsersService
{
    public readonly string _baseURL = "http://localhost:5209/";
    private readonly string _endpoint = "api/Users";

    public UsersService()
    {
        
    }
    public async Task<Response<List<UsersCategoryDto>>> GetAllAsync()
    {
        var url = $"{_baseURL}{_endpoint}";
        var cliente = new HttpClient();
        var res = await cliente.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject<Response<List<UsersCategoryDto>>>(json);
        
        return response;
    }

    public async Task<Response<UsersCategoryDto>> GetById(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";
        var cliente = new HttpClient();
        var res = await cliente.GetAsync(url);
        var json = await res.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<Response<UsersCategoryDto>>(json);
        return response;
    }

    public async Task<Response<UsersCategoryDto>> SaveAsync(UsersCategoryDto usersDto)
    {
        var url = $"{_baseURL}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(usersDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PostAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<UsersCategoryDto>>(json);

        return response;
    }

    public  async Task<Response<UsersCategoryDto>> UpdateAsync(UsersCategoryDto usersDto)
    {
        var url = $"{_baseURL}{_endpoint}";
        var jsonRequest = JsonConvert.SerializeObject(usersDto);
        var content = new StringContent(jsonRequest, System.Text.Encoding.UTF8, "application/json");
        var client = new HttpClient();
        var res = await client.PutAsync(url, content);
        var json = await res.Content.ReadAsStringAsync();
        
        var response = JsonConvert.DeserializeObject<Response<UsersCategoryDto>>(json);

        return response;
    }

    public async Task<Response<bool>> DeleteAsync(int id)
    {
        var url = $"{_baseURL}{_endpoint}/{id}";
        var client = new HttpClient();
        var res = await client.DeleteAsync(url);
        var json = await res.Content.ReadAsStringAsync();

        var response = JsonConvert.DeserializeObject <Response<bool>>(json);

        return response;
    }
}