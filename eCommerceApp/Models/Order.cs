using System.ComponentModel.DataAnnotations;

namespace eCommerceApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        public List<OrderItem>? Items { get; set; }
    }
}