
public interface IOrderService
{
    void AddLine(Order order, Order.Line line);
    double GetTotal(Order order);
    void ConfirmOrder(Order order);
}

public class Order
{
    public enum StatusOption
    {
        Pending = 0,
        Confirmed
    }
    public class Line
    {
        public string Description { get; set; } = "";
        public double Amount { get; set; }
    }

    public int Id { get; set; }
    public StatusOption Status { get; set; }
    public List<Line> Lines { get; set; } = [];
}

public class OrderService : IOrderService
{
    public void AddLine(Order order, Order.Line line)
    {
        ArgumentNullException.ThrowIfNull(order, nameof(order));
        // Validaciones que DEBERÍAN ser del modelo
        if (order.Status != Order.StatusOption.Pending)
            throw new InvalidOperationException("...");
        ArgumentNullException.ThrowIfNull(line, nameof(line));
        if (string.IsNullOrWhiteSpace(line.Description))
            throw new InvalidOperationException("...");
        if (line.Amount <= 0)
            throw new InvalidOperationException("...");

        // Mutación directa
        order.Lines.Add(line);
    }

    public double GetTotal(Order order)
    {
        // Se repiten validaciones
        ArgumentNullException.ThrowIfNull(order, nameof(order));

        // no debería haber nulls, pero tampoco podemos estar 100% seguros
        return order.Lines?.Sum(l => l?.Amount ?? 0.0) ?? 0.0;
    }

    public void ConfirmOrder(Order order)
    {
        // Se repiten validaciones
        ArgumentNullException.ThrowIfNull(order, nameof(order));
        if (order.Status != Order.StatusOption.Pending)
            throw new InvalidOperationException("...");
        if (!order.Lines.Any())
            throw new InvalidOperationException("...");

        // Mutación directa
        order.Status = Order.StatusOption.Confirmed;
    }
}


/*
public class Order
{
    public enum StatusOption
    {
        Pending = 0,
        Confirmed
    }
    public class Line
    {
        public string Description { get; }
        public double Amount { get; }

        public Line(string description, double amount)
        {
            Description = string.IsNullOrWhiteSpace(description)
                ? throw new ArgumentException("Invalid description", nameof(description))
                : description.Trim();
            Amount = amount <= 0
                ? throw new ArgumentException("Invalid amount", nameof(amount))
                : amount;
        }
    }

    public int Id { get; }
    public StatusOption Status { get; private set; } = StatusOption.Pending;
    private readonly List<Line> _lines = [];
    public IReadOnlyList<Line> Lines => _lines.AsReadOnly();
    public double Total => _lines.Sum(l => l.Amount);

    public Order(int id)
    {
        Id = id <= 0 
            ? throw new ArgumentException("Invalid id", nameof(id)) 
            : id;
    }

    public Order(int id, List<Line> lines) : this(id)
    {
        if (lines is null || lines.Count == 0)
            throw new ArgumentNullException(nameof(lines));
        // no tengo que validar cada Line porque no es mutable y ya viene bien construido

        _lines = lines;
    }

    public void Add(Line line)
    {
        ArgumentNullException.ThrowIfNull(line, nameof(line));
        _lines.Add(line);
    }

    public void Confirm()
    {
        if (Status != StatusOption.Pending)
            throw new InvalidOperationException("...");
        if (_lines.Count == 0)
            throw new InvalidOperationException("...");

        Status = StatusOption.Confirmed;
    }
}
*/



