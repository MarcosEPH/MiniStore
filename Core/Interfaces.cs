namespace RefactoredMiniStore.Core
{

    public interface IEmailNotifier { void SendEmail(string to, string subject, string body); }
    public interface ISmsNotifier { void SendSms(string phone, string message); }


    public interface INotifier
    {
        IEmailNotifier Email { get; }
        ISmsNotifier Sms { get; }
    }


    public interface IOrderRepository
    {
        void SaveOrder(Order order);
        IEnumerable<Order> GetAllOrders();
    }


    public interface IShippingCalculator
    {
        decimal Calculate(decimal totalWeightKg, decimal subtotal, string shippingType);
        void Ship(string address, decimal totalWeightKg, string shippingType);
    }
}
