using FoodStore.Core.Entities;
using FoodStore.Core.Repositories.Interfaces;
using FoodStore.DAL.Context;

namespace FoodStore.DAL.Repositories.Implements;

public class TagRepository : GenericRepository<Tag>, ITagRepository
{
    public TagRepository(FoodStoreDbContext context) : base(context)
    {
    }
}