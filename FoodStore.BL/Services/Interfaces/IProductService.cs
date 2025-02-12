﻿using FoodStore.BL.Helpers.DTOs.Product;

namespace FoodStore.BL.Services.Interfaces;

public interface IProductService
{
    Task<ProductGetDto?> GetByIdAsync(int id);

    Task<IEnumerable<ProductGetDto>> GetAllAsync();

    Task<IEnumerable<MainShopProductDto>> GetAllMainShopAsync();

    Task CreateAsync(ProductCreateDto productCreateDto);

    Task UpdateAsync(int id, ProductUpdateDto productUpdateDto);

    Task DeleteAsync(int id);

    Task SoftDeleteAsync(int id);

    Task<bool> UpdateStockAsync(int productId, int quantity);

    Task<ProductGetDto?> GetProductForUpdateAsync(int id);

    Task<IEnumerable<ProductGetDto>> GetProductsByCategoryAsync(int categoryId);

    Task<IEnumerable<ProductGetDto>> GetProductsByTagIdsAsync(IEnumerable<int> tagIds);
}