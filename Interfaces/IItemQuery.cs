using Siemens.Internship2026.GradeBook.Models;

namespace Siemens.Internship2026.GradeBook.Interfaces;

public interface IItemQuery
{
    Task<Item?> GetByIdAsync(int id);
    
    Task<IEnumerable<Item>> QueryAsync(Func<Item, bool> filter);
}