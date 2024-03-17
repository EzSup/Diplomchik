namespace Restaurant.Core.Repositories;

public abstract class RepositoryWithSave
{
    private readonly RestaurantDbContext _dbContext;
    public RepositoryWithSave(RestaurantDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    protected virtual async Task<bool> Save()
    {
        return await _dbContext.SaveChangesAsync() > 0;
    }
}