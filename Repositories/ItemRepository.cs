using System.Text.Json;
using Siemens.Internship2026.GradeBook.Interfaces;
using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Repositories;

public class ItemRepository(HttpClient httpClient, IConfiguration configuration) : IItemReader, IItemQuery
{
    private async Task<IEnumerable<Item>> FetchAllFromApiAsync()
    {
        string? url = configuration["ExternalServices:ItemApiUrl"];
        var options = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true,
        };
        var response = await httpClient.GetFromJsonAsync<ItemResponse>(url, options);
        return response?.Items ?? new List<Item>();
    }

    public async Task<IEnumerable<Item>> GetAllAsync()
    {
        return await FetchAllFromApiAsync();
    }
    
    public async Task<IEnumerable<Item>> QueryAsync(Func<Item, bool> filter)
    {
        var items = await FetchAllFromApiAsync();
        return items.Where(filter);
    }

    public async Task<Item?> GetByIdAsync(int id)
    {
        var items = await  FetchAllFromApiAsync();
        return items.FirstOrDefault(i => i.Id == id);
    }

}
