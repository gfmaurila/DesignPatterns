# DesignPatterns
### SOLID 
### S - Single Responsibility Principle (Princípio da Responsabilidade Única)
<p>Definição: Uma classe deve ter apenas uma razão para mudar, ou seja, deve ter apenas uma responsabilidade.</p>
    ```
         //Errado: Uma classe com múltiplas responsabilidades
        public class UserService
        {
            public void RegisterUser(string username, string password)
            {
                 //Lógica de registro de usuário
            }
        
            public void SendWelcomeEmail(string email)
            {
                 //Lógica para enviar email
            }
        }
        
         //Certo: Separação de responsabilidades
        public class UserService
        {
            public void RegisterUser(string username, string password)
            {
                 //Lógica de registro de usuário
            }
        }
        
        public class EmailService
        {
            public void SendWelcomeEmail(string email)
            {
                 //Lógica para enviar email
            }
        }

    ```
