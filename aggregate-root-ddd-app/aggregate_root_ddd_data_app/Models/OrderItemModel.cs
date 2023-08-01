using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace aggregate_root_ddd_data_app.Models
{
    [Table("OrderItem")]
    public class OrderItemModel
    {
        [Key]
        public long OrderItemId { get; set; }
        public long OrderId { get; set; }
        public OrderModel? Order { get; set; }
        public string? ItemName { get; set; }
        public uint Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
