﻿using FoodStore.Core.Entities.Common;

namespace FoodStore.Core.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Description { get; set; }
    public string SKU { get; set; }
    public int StockQuantity { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<TagProduct> TagProducts { get; set; } = new List<TagProduct>();
    public ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();
}