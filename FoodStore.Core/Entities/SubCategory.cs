using FoodStore.Core.Entities.Common;

namespace FoodStore.Core.Entities;

public class SubCategory : BaseEntity
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}