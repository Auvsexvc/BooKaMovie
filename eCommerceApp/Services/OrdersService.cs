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

        public async Task<List<Order>> GetOrdersByUserIdAndRoleAsync(string userId, string userRole)
        {
            var orders = await _dbContext.Orders.Include(o => o.Items).ThenInclude(o => o.Movie).Include(o=>o.User).ToListAsync();
            if (userRole != "Admin")
            {
                orders = orders.Where(o => o.UserId == userId).ToList();
            }

            return orders;
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