namespace DesignPatterns.SOLID._5_D;

//D - Dependency Inversion Principle(Princípio da Inversão de Dependência)
//Definição: Dependa de abstrações, não de implementações concretas.

//Exemplo:

// Errado: Classe depende diretamente de uma implementação concreta
public class Errado_EmailService
{
    public void SendEmail(string message) { }
}

public class Errado_Notification
{
    private Errado_EmailService _emailService = new Errado_EmailService();

    public void Notify(string message)
    {
        _emailService.SendEmail(message);
    }
}

// Certo: Classe depende de uma abstração
public interface IMessageService
{
    void SendMessage(string message);
}

public class EmailService : IMessageService
{
    public void SendMessage(string message) { }
}

public class Notification
{
    private readonly IMessageService _messageService;

    public Notification(IMessageService messageService)
    {
        _messageService = messageService;
    }

    public void Notify(string message)
    {
        _messageService.SendMessage(message);
    }
}

//Explicação: No exemplo correto, o código é mais flexível, permitindo trocar a implementação de envio de mensagens facilmente(por exemplo, Email, SMS).