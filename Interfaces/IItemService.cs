namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IItemService
{
    Task<object> GetProcessedItemsAsync(int? n = null);
    Task<object?> GetItemByIdAsync(int id);
}