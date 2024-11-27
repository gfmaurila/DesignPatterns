namespace DesignPatterns.YAGNI;


//YAGNI - You Aren't Gonna Need It (Você Não Vai Precisar Disso)



//Definição: O princípio YAGNI afirma que você não deve implementar funcionalidades ou criar códigos que não são necessários no momento.Foque apenas nos requisitos reais, em vez de antecipar possíveis necessidades futuras.



//Por que é importante?

//Evita complexidade desnecessária: Código que não será usado só aumenta o custo de manutenção.

//Economiza tempo: Concentre-se no que realmente agrega valor agora.

//Melhora a legibilidade: Código simples e direto é mais fácil de entender e manter.




// Exemplo - Violando YAGNI (Errado)
public class Errado_OrderService
{
    // Método não utilizado atualmente
    public void ApplyDiscount(string orderId, decimal discountAmount)
    {
        Console.WriteLine($"Applying discount of {discountAmount} to order {orderId}");
    }

    public void CreateOrder(string customerName, decimal orderTotal)
    {
        Console.WriteLine($"Creating order for {customerName} with total {orderTotal}");
    }

    // Método antecipado sem uso real
    public void ScheduleOrderDelivery(string orderId, DateTime deliveryDate)
    {
        Console.WriteLine($"Scheduling delivery for order {orderId} on {deliveryDate}");
    }
}



//Problemas:

//Os métodos ApplyDiscount e ScheduleOrderDelivery foram implementados sem necessidade atual.
//Gasto de tempo e recursos em algo que pode nunca ser usado.


// Exemplo - Seguindo YAGNI (Certo)

public class OrderService
{
    public void CreateOrder(string customerName, decimal orderTotal)
    {
        Console.WriteLine($"Creating order for {customerName} with total {orderTotal}");
    }
}

//Por que está correto?

//Apenas as funcionalidades realmente solicitadas são implementadas.
//Métodos adicionais, como desconto ou agendamento de entrega, só serão adicionados caso sejam necessários no futuro.


//Quando aplicar YAGNI?
//Durante o desenvolvimento inicial: Foque apenas nos requisitos especificados.
//Ao planejar funcionalidades: Pergunte-se: "Isso será realmente usado agora?"
//Ao revisar código: Remova partes do código que foram implementadas antecipadamente e não são usadas.


//Resumo
//O YAGNI nos lembra de não gastar tempo com "e se" ou "talvez precisemos disso no futuro". Concentre-se no que é necessário agora.Se surgir a necessidade de algo no futuro, você pode implementar na hora certa, com mais clareza sobre os requisitos reais. 🚀
