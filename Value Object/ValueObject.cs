var address1 = new Address("123 Main St", "Springfield", "12345");
var address2 = new Address("123 Main St", "Springfield", "12345");
var address3 = new Address("456 Elm St", "Shelbyville", "67890");

Console.WriteLine(address1.Equals(address2)); // True (mesmos valores)
Console.WriteLine(address1.Equals(address3)); // False (valores diferentes)

// Usando no pedido
var order = new Order(1, address1);
Console.WriteLine($"Order Address: {order.ShippingAddress.Street}");

// Alterando o endereço
order.ChangeShippingAddress(address3);
Console.WriteLine($"Updated Order Address: {order.ShippingAddress.Street}");
