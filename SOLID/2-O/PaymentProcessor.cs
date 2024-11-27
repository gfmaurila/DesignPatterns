namespace DesignPatterns.SOLID._2_O;


//O - Open/Closed Principle(Princípio Aberto/Fechado)
//Definição: As classes devem estar abertas para extensão, mas fechadas para modificação.

//Exemplo:

// Errado: Modificar a classe para adicionar novos tipos de pagamento
public class Errado_PaymentProcessor
{
    public void ProcessPayment(string paymentType)
    {
        if (paymentType == "CreditCard")
        {
            // Lógica de cartão de crédito
        }
        else if (paymentType == "PayPal")
        {
            // Lógica de PayPal
        }
    }
}

// Certo: Usar herança e polimorfismo para estender comportamentos
public abstract class Certo_PaymentMethod
{
    public abstract void ProcessPayment();
}

public class CreditCardPayment : Certo_PaymentMethod
{
    public override void ProcessPayment()
    {
        // Lógica de cartão de crédito
    }
}

public class PayPalPayment : Certo_PaymentMethod
{
    public override void ProcessPayment()
    {
        // Lógica de PayPal
    }
}

public class PaymentProcessor
{
    public void ProcessPayment(Certo_PaymentMethod paymentMethod)
    {
        paymentMethod.ProcessPayment();
    }
}


//Explicação: No exemplo correto, novas formas de pagamento podem ser adicionadas sem modificar a classe principal.