using RefactoredMiniStore.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RefactoredMiniStore.Infra
{
    public class FileOrderRepository : IOrderRepository
    {
        private readonly string _path;
        public FileOrderRepository(string path)
        {
            _path = path;
            var dir = Path.GetDirectoryName(_path) ?? ".";
            Directory.CreateDirectory(dir);
            if (!File.Exists(_path)) File.WriteAllText(_path, string.Empty);
        }

        public void SaveOrder(Order order)
        {
            var line = string.Join(";", order.Id, order.CustomerId, order.Subtotal, order.Shipping, order.PaymentType, order.ShippingType, order.Paid);
            File.AppendAllLines(_path, new[] { line });
        }

        public IEnumerable<Order> GetAllOrders()
        {
            if (!File.Exists(_path)) yield break;
            foreach (var line in File.ReadAllLines(_path).Where(l => !string.IsNullOrWhiteSpace(l)))
            {
                var parts = line.Split(';');
                yield return new Order
                {
                    Id = parts.ElementAtOrDefault(0) ?? string.Empty,
                    CustomerId = parts.ElementAtOrDefault(1) ?? string.Empty,
                    Subtotal = decimal.TryParse(parts.ElementAtOrDefault(2), out var s) ? s : 0,
                    Shipping = decimal.TryParse(parts.ElementAtOrDefault(3), out var sh) ? sh : 0,
                    PaymentType = parts.ElementAtOrDefault(4) ?? string.Empty,
                    ShippingType = parts.ElementAtOrDefault(5) ?? string.Empty,
                    Paid = bool.TryParse(parts.ElementAtOrDefault(6), out var p) ? p : false
                };
            }
        }
    }
}
