using aggregate_root_ddd_common_app.Entities;
using aggregate_root_ddd_common_app.Utilities;
using Xunit;

namespace aggregate_root_ddd_test_app
{
    public class OrderTests
    {
        [Fact]
        public void AddPayment_ValidAmount_UpdatesPaidAmountAndStatus()
        {
            // Arrange
            var order = new Order();
            var initialPaidAmount = order.PaidAmount;
            var amountToAdd = 100;

            // Act
            order.AddPayment(amountToAdd);

            // Assert
            Assert.Equal(initialPaidAmount + amountToAdd, order.PaidAmount);
            Assert.Equal(OrderStatus.ReadyForShipping, order.Status);
        }

        [Fact]
        public void AddPayment_InvalidAmount_ThrowsInvalidOperationException()
        {
            // Arrange
            var order = new Order();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.AddPayment(-50));
        }

        [Fact]
        public void AddPayment_ExceedOrderTotal_ThrowsInvalidOperationException()
        {
            // Arrange
            var order = new Order();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.AddPayment(order.OrderTotal + 50));
        }

        [Fact]
        public void AddItem_ValidData_AddsItemToOrder()
        {
            // Arrange
            var order = new Order();
            var itemName = "Test Item";
            var quantity = 2;
            var unitPrice = 10;

            // Act
            order.AddItem(itemName, (uint)quantity, unitPrice);

            // Assert
            Assert.Contains(order.Items, x => x.ItemName == itemName && x.Quantity == quantity && x.UnitPrice == unitPrice);
        }

        [Fact]
        public void AddItem_AlreadyPaidOrder_ThrowsInvalidOperationException()
        {
            // Arrange
            var order = new Order();
            order.AddPayment(100);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.AddItem("Test Item", 2, 10));
        }

        [Fact]
        public void RemoveItem_ValidItem_RemovesItemFromOrder()
        {
            // Arrange
            var order = new Order();
            var itemName = "Test Item";
            order.AddItem(itemName, 2, 10);

            // Act
            order.RemoveItem(itemName);

            // Assert
            Assert.DoesNotContain(order.Items, x => x.ItemName == itemName);
        }

        [Fact]
        public void RemoveItem_AlreadyPaidOrder_ThrowsInvalidOperationException()
        {
            // Arrange
            var order = new Order();
            var itemName = "Test Item";
            order.AddItem(itemName, 2, 10);
            order.AddPayment(100);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.RemoveItem(itemName));
        }

        [Fact]
        public void AddQuantity_ValidItem_AddsQuantityToItem()
        {
            // Arrange
            var order = new Order();
            var itemName = "Test Item";
            order.AddItem(itemName, 2, 10);

            // Act
            order.AddQuantity(itemName, 3);

            // Assert
            var item = order.Items.FirstOrDefault(x => x.ItemName == itemName);
            Assert.NotNull(item);
            Assert.Equal(5u, item.Quantity);
        }

        [Fact]
        public void WithdrawQuantity_ValidItem_WithdrawsQuantityFromItem()
        {
            // Arrange
            var order = new Order();
            var itemName = "Test Item";
            order.AddItem(itemName, 5, 10);

            // Act
            order.WithdrawQuantity(itemName, 3);

            // Assert
            var item = order.Items.FirstOrDefault(x => x.ItemName == itemName);
            Assert.NotNull(item);
            Assert.Equal(2u, item.Quantity);
        }

        [Fact]
        public void ShipOrder_ValidOrder_SetsShippingDateAndStatus()
        {
            // Arrange
            var order = new Order();
            order.AddItem("Test Item", 2, 10);
            order.AddPayment(20);

            // Act
            order.ShipOrder();

            // Assert
            Assert.NotNull(order.ShippingDate);
            Assert.Equal(OrderStatus.InTransit, order.Status);
        }

        [Fact]
        public void ShipOrder_NoItems_ThrowsInvalidOperationException()
        {
            // Arrange
            var order = new Order();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.ShipOrder());
        }

        [Fact]
        public void ShipOrder_UnpaidOrder_ThrowsInvalidOperationException()
        {
            // Arrange
            var order = new Order();
            order.AddItem("Test Item", 2, 10);

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.ShipOrder());
        }

        [Fact]
        public void ShipOrder_AlreadyShipped_ThrowsInvalidOperationException()
        {
            // Arrange
            var order = new Order();
            order.AddItem("Test Item", 2, 10);
            order.AddPayment(20);
            order.ShipOrder();

            // Act & Assert
            Assert.Throws<InvalidOperationException>(() => order.ShipOrder());
        }
    }
}
