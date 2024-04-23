using Microsoft.EntityFrameworkCore;

namespace RiverBooks.OrderProcessing;

internal class EfOrderRepository : IOrderRepository
{
    private readonly OrderProcessingDbContext _dbContext;

    public EfOrderRepository(OrderProcessingDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(Order order)
    {
        await _dbContext.Orders.AddAsync(order);
    }

    public async Task<List<Order>> ListAsync()
    {
        return await _dbContext.Orders
            .Include(o => o.OrderItems)//Normally you wouldn't Include anything with ListAsync() we should have used specification
            .ToListAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
}