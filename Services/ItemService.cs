using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Services;

public class ItemService : IItemService
{
    
    private readonly ILogger<ItemService> _logger;
    private readonly IItemQuery _itemQuery;

    public ItemService(IItemQuery itemQuery,  ILogger<ItemService> logger)
    {
        _logger = logger;
        _itemQuery = itemQuery;
    }

    public async Task<object> GetProcessedItemsAsync(int? n = null)
    {
        _logger.LogInformation("[LOG] {Time}: Processing all items", DateTime.UtcNow);
        
        var items = await _itemQuery.QueryAsync(i => i.Value >= 5 && i.IsActive);
        

        if (n.HasValue && n.Value > 0)
        {
            items = items.Take(n.Value);
        }
        var itemList = items.ToList();
        
        var totalCount = itemList.Count;
        var averageValue = itemList.Any() ? itemList.Average(i => i.Value) : 0;
        
        _logger.LogInformation("[LOG] Processed {Count} items with average value: {Average}", totalCount, averageValue);
        return new
        {
            Data = itemList,
            TotalCount = itemList.Count,
            AverageValue = averageValue,
            RetrievedAt = DateTime.UtcNow
        };
    }

    public async Task<object?> GetItemByIdAsync(int id)
    {
        _logger.LogInformation("[LOG] {Time}: Fetching item with id {Id}", DateTime.UtcNow, id);

        if (id <= 0)
        {
            throw new ArgumentException("Id must be a positive integer.");
        }
        var item = await _itemQuery.GetByIdAsync(id);
        if (item == null)
        {
            _logger.LogWarning("[LOG] Item with id {Id} not found", id);
            return null;
        }

        return new
        {
            Data = item,
            RetrievedAt = DateTime.UtcNow
        };
    }
    
}