# DesignPatterns
### SOLID 
<div>
    <h2>SOLID</h2>
    <p>S - Single Responsibility Principle (Princípio da Responsabilidade Única)</p>
    <p>Definição: Uma classe deve ter apenas uma razão para mudar, ou seja, deve ter apenas uma responsabilidade.</p>
    <p>
      Exemplo:
      ```
      // Errado: Uma classe com múltiplas responsabilidades
      public class UserService
      {
          public void RegisterUser(string username, string password)
          {
              // Lógica de registro de usuário
          }
      
          public void SendWelcomeEmail(string email)
          {
              // Lógica para enviar email
          }
      }
      
      // Certo: Separação de responsabilidades
      public class UserService
      {
          public void RegisterUser(string username, string password)
          {
              // Lógica de registro de usuário
          }
      }
      
      public class EmailService
      {
          public void SendWelcomeEmail(string email)
          {
              // Lógica para enviar email
          }
      }
      
      ```
    </p> 
</div>
