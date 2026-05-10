using Microsoft.AspNetCore.Mvc;
using Siemens.Internship2026.GradeBook.Interfaces;

namespace Siemens.Internship2026.GradeBook.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ItemController : ControllerBase
{
    private readonly IItemService _itemService;

    public ItemController(IItemService itemService)
    {
        _itemService = itemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll([FromQuery] int? n)
    {
        return Ok(await _itemService.GetProcessedItemsAsync(n));
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        try
        {
            var result = await _itemService.GetItemByIdAsync(id);
            return result != null ? Ok(result) : NotFound($"Item with id {id} not found.");

        }
        catch (ArgumentException exception)
        {
            return BadRequest(exception.Message);
        }
    }
}
