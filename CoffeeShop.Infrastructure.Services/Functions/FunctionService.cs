using CoffeeShop.Domain.Model.Interfaces.Services.Infrastructure;
using Newtonsoft.Json;
using System.Text;

namespace CoffeeShop.Infrastructure.Services.Functions;
public sealed class FunctionService : IFunctionService
{
    private readonly string _baseUrl;

    public FunctionService
    (
        string functionBaseUrl
    )
    {
        _baseUrl = functionBaseUrl;
    }

    public async Task InvokeAsync(object objecto)
    {
        HttpClient httpClient = new();

        var json = JsonConvert.SerializeObject(objecto);

        StringContent requestData = new(json, Encoding.UTF8, "application/json");

        await httpClient.PostAsync(_baseUrl, requestData);
    }
}
