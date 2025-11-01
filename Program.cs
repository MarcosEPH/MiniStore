using RefactoredMiniStore.Core;
using RefactoredMiniStore.Infra;

internal class Program
{
    private static void Main(string[] args)
    {
        INotifier notifier = new CompositeNotifier(new ConsoleEmailNotifier(), new ConsoleSmsNotifier());
        IOrderRepository repo = new FileOrderRepository("db/orders.csv");
        IShippingCalculator shipping = new SimpleShippingCalculator();

        var processor = new OrderProcessor(repo, notifier, shipping);
        var ui = new ConsoleUI(processor);
        ui.Run();
    }
}
