namespace FoodStore.BL.Helpers.DTOs.Category;

public record SubCategoryUpdateDto
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
}