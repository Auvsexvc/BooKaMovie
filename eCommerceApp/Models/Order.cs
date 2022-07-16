using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace eCommerceApp.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        public string Email { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;

        [ForeignKey("UserId")]
        public AppUser User { get; set; } = null!;

        public List<OrderItem> Items { get; set; } = null!;
    }
}