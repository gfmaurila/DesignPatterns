namespace DesignPatterns.DRY;

//DRY - Don’t Repeat Yourself(Não se Repita)
//Definição: O princípio DRY recomenda evitar duplicação de código.Se você perceber que está repetindo lógica ou estruturas similares, deve refatorar seu código para reutilizar componentes e métodos, centralizando a lógica.

//Por que é importante?
//Facilita a manutenção.
//Reduz erros, pois uma alteração em um local não precisa ser replicada.
//Melhora a legibilidade e organização do código.


// Exemplo - Sem DRY (Errado)
public class Errado_InvoiceService
{
    public void GenerateInvoice(string customerName, decimal amount)
    {
        Console.WriteLine($"Generating invoice for {customerName} with amount {amount}");
        Console.WriteLine("Saving invoice to the database...");
        Console.WriteLine("Sending invoice to the customer via email...");
    }

    public void GenerateReceipt(string customerName, decimal amount)
    {
        Console.WriteLine($"Generating receipt for {customerName} with amount {amount}");
        Console.WriteLine("Saving receipt to the database...");
        Console.WriteLine("Sending receipt to the customer via email...");
    }
}

//Problemas:

//A lógica de "Salvar no banco de dados" e "Enviar email" está duplicada.
//Se houver uma mudança, será necessário alterar em múltiplos lugares.


//Exemplo - Com DRY(Certo)

public class DocumentService
{
    public void SaveToDatabase(string documentType, string customerName, decimal amount)
    {
        Console.WriteLine($"Saving {documentType} for {customerName} with amount {amount} to the database...");
    }

    public void SendEmail(string documentType, string customerName)
    {
        Console.WriteLine($"Sending {documentType} to {customerName} via email...");
    }
}

public class InvoiceService
{
    private readonly DocumentService _documentService;

    public InvoiceService()
    {
        _documentService = new DocumentService();
    }

    public void GenerateInvoice(string customerName, decimal amount)
    {
        Console.WriteLine($"Generating invoice for {customerName} with amount {amount}");
        _documentService.SaveToDatabase("Invoice", customerName, amount);
        _documentService.SendEmail("Invoice", customerName);
    }

    public void GenerateReceipt(string customerName, decimal amount)
    {
        Console.WriteLine($"Generating receipt for {customerName} with amount {amount}");
        _documentService.SaveToDatabase("Receipt", customerName, amount);
        _documentService.SendEmail("Receipt", customerName);
    }
}



//Vantagens do Exemplo Certo:
//Reutilização de Código: As ações repetitivas(salvar no banco e enviar email) foram movidas para uma classe reutilizável.
//Facilidade de Alteração: Se o processo de envio de email mudar, será necessário ajustar apenas na classe DocumentService.
//Organização: Cada classe tem uma responsabilidade clara.