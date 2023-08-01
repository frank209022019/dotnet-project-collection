namespace aggregate_root_ddd_common_app.Entities
{
    public class OrderItem
    {
        public long OrderItemId { get; private set; }
        public string ItemName { get; private set; }
        public uint Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        internal OrderItem(string itemName, uint quantity, decimal unitPrice)
        {
            if (string.IsNullOrEmpty(itemName))
                throw new ArgumentException($"'{nameof(itemName)}' cannot be null or empty.", nameof(itemName));
            if (quantity == 0)
                throw new ArgumentException("Quantity must be at least one.", nameof(quantity));
            if (unitPrice <= 0)
                throw new ArgumentException("Unit price must be above zero.", nameof(unitPrice));
            ItemName = itemName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }

        internal void AddQuantity(uint quantity)
        {
            Quantity += quantity;
        }
        internal void WithdrawQuantity(uint quantity)
        {
            if (Quantity - quantity <= 0)
                throw new InvalidOperationException("Can't remove all units. Remove the entire item instead.");
            Quantity -= quantity;
        }
    }
}
