using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Models
{
    public class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; }
        public Movie Movie { get; set; } = null!;
        public int Amount { get; set; }
        public string ShoppingCartId { get; set; } = null!;
    }
}
