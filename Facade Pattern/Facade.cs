// Subsistemas
public class OrderService
{
    public void PlaceOrder(string product)
    {
        Console.WriteLine($"Order placed for {product}");
    }
}

public class PaymentService
{
    public void ProcessPayment(string product)
    {
        Console.WriteLine($"Payment processed for {product}");
    }
}

public class NotificationService
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"Notification sent: {message}");
    }
}

// Fachada
public class ECommerceFacade
{
    private readonly OrderService _orderService = new OrderService();
    private readonly PaymentService _paymentService = new PaymentService();
    private readonly NotificationService _notificationService = new NotificationService();

    public void CompleteOrder(string product)
    {
        _orderService.PlaceOrder(product);
        _paymentService.ProcessPayment(product);
        _notificationService.SendNotification($"Order for {product} confirmed.");
    }
}

// Código cliente
var eCommerceFacade = new ECommerceFacade();
eCommerceFacade.CompleteOrder("Laptop");
