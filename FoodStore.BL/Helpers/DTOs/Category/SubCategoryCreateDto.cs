namespace FoodStore.BL.Helpers.DTOs.Category;

public record SubCategoryCreateDto
{
    public string Name { get; set; }
    public int CategoryId { get; set; } 
}