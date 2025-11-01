using RefactoredMiniStore.Core;
using System;

namespace RefactoredMiniStore.Core
{
   
    public class ConsoleUI
    {
        private readonly OrderProcessor _processor;
        public ConsoleUI(OrderProcessor processor) => _processor = processor;

        public void Run()
        {
            Console.WriteLine("Refactored MiniStore (SOLID)");
            _processor.SeedSampleData();

            while (true)
            {
                Console.WriteLine("1) List orders\n2) Create order\n3) Exit");
                var opt = Console.ReadLine();
                if (opt == "1")
                {
                    foreach (var o in _processor.ListOrders()) Console.WriteLine($"Order {o.Id} - Total: {o.Total:C}");
                }
                else if (opt == "2")
                {
                    Console.Write("OrderId: "); var id = Console.ReadLine() ?? Guid.NewGuid().ToString();
                    Console.Write("CustomerId: "); var cid = Console.ReadLine() ?? "c1";
                    Console.Write("Subtotal: "); var sub = decimal.TryParse(Console.ReadLine(), out var tmp) ? tmp : 0m;
                    var order = _processor.CreateAndPayOrder(id, cid, sub, "addr", "standard", "card");
                    Console.WriteLine($"Order created {order.Id} Total: {order.Total:C}");
                }
                else if (opt == "3") break;
            }
        }
    }
}
