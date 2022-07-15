using eCommerceApp.Data;
using eCommerceApp.Interfaces;
using eCommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Services
{
    public class OrdersService : IOrdersService
    {
        private readonly AppDbContext _dbContext;

        public OrdersService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Order>> GetOrdersByUserIdAsync(string userId)
        {
            return await _dbContext.Orders.Include(o => o.Items).ThenInclude(o => o.Movie).Where(o => o.UserId == userId).ToListAsync();
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> shoppingCartItems, string userId, string userEmailAddress)
        {
            Order order = new()
            {
                UserId = userId,
                Email = userEmailAddress,
            };

            await _dbContext.Orders.AddAsync(order);
            await _dbContext.SaveChangesAsync();

            foreach (var item in shoppingCartItems)
            {
                OrderItem orderItem = new()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    Price = item.Movie.Price,
                    OrderId = order.Id
                };
                await _dbContext.OrderItems.AddAsync(orderItem);
            }

            await _dbContext.SaveChangesAsync();
        }
    }
}