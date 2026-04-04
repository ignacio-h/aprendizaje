namespace Demos.Structs
{
    // Forma clásica — mucho boilerplate, fácil olvidarse de algo
    /*
    public readonly struct OrderId : IEquatable<OrderId>
    {
        public int Value { get; }

        public OrderId(int value)
        {
            if (value <= 0) 
                throw new ArgumentException("OrderId must be positive.", nameof(value));

            Value = value;
        }

        public bool Equals(OrderId other) 
            => Value == other.Value;
        public override bool Equals(object? obj) 
            => obj is OrderId other && Equals(other);
        public override int GetHashCode() 
            => Value.GetHashCode();
        public override string ToString() 
            => $"Order#{Value}";

        public static implicit operator int(OrderId id) => id.Value;
        public static explicit operator OrderId(int value) => new(value);

        public static bool operator ==(OrderId left, OrderId right) 
            => left.Equals(right);

        public static bool operator !=(OrderId left, OrderId right) 
            => !(left == right);
    }
    */

    // ✅ Forma moderna con readonly record struct (C# 10+)
    // Igualdad estructural, ToString, Deconstruct: gratis.

    public readonly record struct OrderId
    {
        public int Value { get; }

        public OrderId(int value)
        {
            // la validación sólo se hace aquí y no hay que repetirla
            if (value <= 0) 
                throw new ArgumentException("OrderId must be positive.", nameof(value));

            Value = value;
        }

        // conversión implícita para poder usar OrderId como un int directamente
        public static implicit operator int(OrderId id) => id.Value;
        public override string ToString() => $"Order#{Value}";
    }

    public class Order
    {
        public int Value { get; }

        public Order(int value)
        {
            if (value <= 0) 
                throw new ArgumentException("OrderId must be positive.", nameof(value));

            Value = value;
        }
    }

    public readonly record struct CustomerId
    {
        public int Value { get; }

        public CustomerId(int value)
        {
            // la validación sólo se hace aquí y no hay que repetirla
            if (value <= 0)
                throw new ArgumentException("CustomerId must be positive.", nameof(value));

            Value = value;
        }

        // conversión implícita para poder usar CustomerId como un int directamente
        public static implicit operator int(CustomerId id) => id.Value;
        public override string ToString() => $"Customer {Value}";
    }


    public static class StructsCl
    {
        public static void ProcessOrder(OrderId orderId, CustomerId customerId)
        {
            // no hace falta repetir las validaciones
            Console.WriteLine($"Processing {orderId} for {customerId}.");
        }

        public static void ProcessOrderPrimitive(int orderId, int customerId)
        {
            // repetir las mismas validaciones, otra vez...
            if (orderId <= 0) 
                throw new ArgumentException("OrderId must be positive.", nameof(orderId));
            if (customerId <= 0)
                throw new ArgumentException("CustomerId must be positive.", nameof(customerId));

            Console.WriteLine($"Processing order {orderId} for customer {customerId}.");
        }

        public static void Main2()
        {
            int orderId = 1;
            int customerId = 2;
            ProcessOrderPrimitive(orderId, customerId);
            // 👇 bug salvaje apareció 👇
            ProcessOrderPrimitive(customerId, orderId);
        }

        public static void Main()
        {
            var orderId = new OrderId(1);
            var customerId = new CustomerId(2);
            ProcessOrder(orderId, customerId);
            // el bug nunca llega a publicarse porque ni compila
            //ProcessOrder(customerId, orderId);
        }

    }
}
