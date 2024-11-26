# DesignPatterns
### SOLID 
### 1.1 - POST - Login de Usuário via API
    ```
 Errado: Uma classe com múltiplas responsabilidades
public class UserService
{
    public void RegisterUser(string username, string password)
    {
         Lógica de registro de usuário
    }

    public void SendWelcomeEmail(string email)
    {
         Lógica para enviar email
    }
}

 Certo: Separação de responsabilidades
public class UserService
{
    public void RegisterUser(string username, string password)
    {
         Lógica de registro de usuário
    }
}

public class EmailService
{
    public void SendWelcomeEmail(string email)
    {
         Lógica para enviar email
    }
}

    ```
