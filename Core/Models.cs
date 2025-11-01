namespace RefactoredMiniStore.Core
{
    public record Product(string Sku, string Name, decimal Price, decimal WeightKg);
    public record Customer(string Id, string Name, string Email, string Phone);
    public class Order
    {
        public string Id { get; init; }
        public string CustomerId { get; init; }
        public decimal Subtotal { get; set; }
        public decimal Shipping { get; set; }
        public decimal Total => Subtotal + Shipping;
        public string PaymentType { get; set; } = string.Empty;
        public string ShippingType { get; set; } = string.Empty;
        public bool Paid { get; set; }
    }
}
