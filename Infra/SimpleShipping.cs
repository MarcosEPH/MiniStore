using RefactoredMiniStore.Core;
using System;

namespace RefactoredMiniStore.Infra
{
    public class SimpleShippingCalculator : IShippingCalculator
    {
        public decimal Calculate(decimal totalWeightKg, decimal subtotal, string shippingType)
        {
            return shippingType switch
            {
                "drone" => totalWeightKg > 2 ? throw new NotSupportedException("Drones no soportan >2kg") : 15m,
                "standard" => Math.Max(5m, totalWeightKg * 3m),
                _ => 10m
            };
        }

        public void Ship(string address, decimal totalWeightKg, string shippingType)
        {
            Console.WriteLine($"[SHIP]{shippingType} to {address} ({totalWeightKg}kg)");
        }
    }
}
