using System.Collections.Generic;

namespace RefactoredMiniStore.Core
{

    public class OrderProcessor
    {
        private readonly IOrderRepository _repo;
        private readonly INotifier _notifier;
        private readonly IShippingCalculator _shippingCalc;

        public OrderProcessor(IOrderRepository repo, INotifier notifier, IShippingCalculator shippingCalc)
        {
            _repo = repo;
            _notifier = notifier;
            _shippingCalc = shippingCalc;
        }

        public void SeedSampleData()
        {
        
        }

        public IEnumerable<Order> ListOrders() => _repo.GetAllOrders();

        public Order CreateAndPayOrder(string orderId, string customerId, decimal subtotal, string address, string shippingType, string paymentType)
        {
            var order = new Order { Id = orderId, CustomerId = customerId, Subtotal = subtotal, ShippingType = shippingType, PaymentType = paymentType, Paid = false };
            var weight = EstimateWeight(subtotal);
            order.Shipping = _shippingCalc.Calculate(weight, subtotal, shippingType);
            order.Paid = true;
            _repo.SaveOrder(order);

      
            _notifier.Email.SendEmail("customer@example.com", $"Pedido {order.Id} confirmado", $"Total: {order.Total:C}");

            return order;
        }

        private decimal EstimateWeight(decimal subtotal)
        {
          
            return Math.Max(0.1m, subtotal / 100m);
        }
    }
}
