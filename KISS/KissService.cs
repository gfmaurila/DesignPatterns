namespace DesignPatterns.KISS;

public class KissService
{

    //Exemplo 2 - Com KISS(Certo)

    public string GetStatus(int statusCode)
    {
        return statusCode switch
        {
            1 => "Success",
            2 => "Error",
            3 => "Pending",
            _ => "Unknown"
        };
    }
}
