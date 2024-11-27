// Criando o pedido
var order = new Order(1, "John Doe");

// Adicionando itens ao pedido
var item1 = new OrderItem(1, "Laptop", 1, 1500.00m);
var item2 = new OrderItem(2, "Mouse", 2, 50.00m);

order.AddItem(item1);
order.AddItem(item2);

// Exibindo detalhes do pedido
Console.WriteLine($"Order ID: {order.Id}");
Console.WriteLine($"Customer: {order.CustomerName}");
Console.WriteLine("Items:");
foreach (var item in order.Items)
{
    Console.WriteLine($" - {item.ProductName}, Quantity: {item.Quantity}, Price: {item.Price:C}");
}

// Removendo um item
order.RemoveItem(item2);

Console.WriteLine("Updated Items:");
foreach (var item in order.Items)
{
    Console.WriteLine($" - {item.ProductName}, Quantity: {item.Quantity}, Price: {item.Price:C}");
}
