﻿namespace FoodStore.BL.Helpers.DTOs.Category;

public record CategoryGetDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}