using FoodStore.BL.Helpers.DTOs.Category;
using FoodStore.BL.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FoodStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService _categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CategoryCreateDto createDto)
    {
        await _categoryService.CreateAsync(createDto);
        return StatusCode(201, createDto);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        return Ok(await _categoryService.GetByIdAsync(id));
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var categories = await _categoryService.GetAllAsync();
        return Ok(categories.Select(c => new { c.Id, c.Name }));
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromForm] CategoryUpdateDto updateDto)
    {
        await _categoryService.UpdateAsync(id, updateDto);
        return StatusCode(204, updateDto);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _categoryService.DeleteAsync(id);
        return NoContent();
    }

    [HttpDelete("SoftDelete/{id}")]
    public async Task<IActionResult> SoftDelete(int id)
    {
        await _categoryService.SoftDeleteAsync(id);
        return NoContent();
    }

    [HttpGet("WithSubcategories")]
    public async Task<IActionResult> GetCategoriesWithSubcategories()
    {
        var categoriesWithSubcategories = await _categoryService.GetCategoriesWithSubcategoriesAsync();
        return Ok(new { categories = categoriesWithSubcategories });
    }

    [HttpGet("WithSubcategoryById{id}")]
    public async Task<IActionResult> GetCategoryWithSubcategoriesById(int id)
    {
        var categoryWithSubcategories = await _categoryService.GetCategoryByIdAsync(id);
        return Ok(categoryWithSubcategories);
    }
}