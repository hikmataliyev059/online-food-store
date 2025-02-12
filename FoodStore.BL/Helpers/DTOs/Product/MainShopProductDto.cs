namespace FoodStore.BL.Helpers.DTOs.Product;

public record MainShopProductDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string ShortDescription { get; set; }
    public List<string> ImageUrls { get; set; } = new List<string>();
    public string HoverImageUrl { get; set; }
}