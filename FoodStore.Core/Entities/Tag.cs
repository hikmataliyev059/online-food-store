using FoodStore.Core.Entities.Common;

namespace FoodStore.Core.Entities;

public class Tag : BaseEntity
{
    public string Name { get; set; }
    public ICollection<TagProduct> TagProducts { get; set; }
}