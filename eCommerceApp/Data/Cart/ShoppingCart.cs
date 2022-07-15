using eCommerceApp.Interfaces;
using eCommerceApp.Models;
using Microsoft.EntityFrameworkCore;

namespace eCommerceApp.Data.Cart
{
    public class ShoppingCart
    {
        private readonly AppDbContext _dbContext;

        public string ShoppingCartId { get; set; } = string.Empty;
        public List<ShoppingCartItem> ShoppingCartItems { get; set; } = null!;

        public ShoppingCart(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public static ShoppingCart GetShoppingCart(IServiceProvider services)
        {
            ISession session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            AppDbContext dbContext = services.GetService<AppDbContext>();

            string cartId = session.GetString("CartId") ?? Guid.NewGuid().ToString();
            session.SetString("CartId", cartId);

            return new ShoppingCart(dbContext) {  ShoppingCartId = cartId };
        }

        public async Task AddItemToCart(Movie movie)
        {
            var shoppingCartItem = GetShoppingCartItems().Find(c => c.Movie == movie);

            if (shoppingCartItem == null)
            {
                shoppingCartItem = new ShoppingCartItem()
                {
                    Amount = 1,
                    Movie = movie,
                    ShoppingCartId = ShoppingCartId
                };
                _dbContext.ShoppingCartItems.Add(shoppingCartItem);
            }
            else
            {
                shoppingCartItem.Amount++;
            }

            await _dbContext.SaveChangesAsync();
        }

        public async Task RemoveItemFromCart(Movie movie)
        {
            var shoppingCartItem = GetShoppingCartItems().Find(c => c.Movie == movie);

            if (shoppingCartItem != null)
            {
                if (shoppingCartItem.Amount > 1)
                {
                    shoppingCartItem.Amount--;
                }
                else
                {
                    _dbContext.ShoppingCartItems.Remove(shoppingCartItem);
                }
            }

            await _dbContext.SaveChangesAsync();
        }

        public List<ShoppingCartItem> GetShoppingCartItems() =>
            ShoppingCartItems ??= _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Include(c => c.Movie).ToList();

        public double GetShoppingCartTotal() =>
            _dbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId).Select(c => c.Movie.Price * c.Amount).Sum();
    }
}