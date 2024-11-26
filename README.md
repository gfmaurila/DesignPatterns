# Design Patterns
### SOLID 
### S - Single Responsibility Principle (Princípio da Responsabilidade Única)
Definição: Uma classe deve ter apenas uma razão para mudar, ou seja, deve ter apenas uma responsabilidade.


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


Explicação: No exemplo correto, cada classe tem uma responsabilidade única: uma para registrar o usuário e outra para enviar o email.


### O - Open/Closed Principle (Princípio Aberto/Fechado)

Definição: As classes devem estar abertas para extensão, mas fechadas para modificação. 
Exemplo:

    ```
        // Errado: Modificar a classe para adicionar novos tipos de pagamento
        public class PaymentProcessor
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
        public abstract class PaymentMethod
        {
            public abstract void ProcessPayment();
        }
        
        public class CreditCardPayment : PaymentMethod
        {
            public override void ProcessPayment()
            {
                // Lógica de cartão de crédito
            }
        }
        
        public class PayPalPayment : PaymentMethod
        {
            public override void ProcessPayment()
            {
                // Lógica de PayPal
            }
        }
        
        public class PaymentProcessor
        {
            public void ProcessPayment(PaymentMethod paymentMethod)
            {
                paymentMethod.ProcessPayment();
            }
        }
    
    ```
Explicação: No exemplo correto, novas formas de pagamento podem ser adicionadas sem modificar a classe principal.

### L - Liskov Substitution Principle (Princípio da Substituição de Liskov)
Definição: Uma classe derivada deve ser substituível por sua classe base sem alterar o comportamento esperado.

Exemplo:

    ```
        // Errado: Violação da substituição
        public class Bird
        {
            public virtual void Fly() { }
        }
        
        public class Penguin : Bird
        {
            public override void Fly()
            {
                throw new NotSupportedException("Pinguins não voam!");
            }
        }
        
        // Certo: Usar uma hierarquia mais precisa
        public abstract class Bird { }
        
        public class FlyingBird : Bird
        {
            public void Fly() { }
        }
        
        public class Penguin : Bird
        {
            // Pinguins não têm o método Fly
        }

    ```

Explicação: No exemplo correto, evitamos cenários onde subclasses não respeitam o comportamento esperado da classe base.



### I - Interface Segregation Principle (Princípio da Segregação de Interfaces)
Definição: Uma classe não deve ser forçada a implementar interfaces que não usa.

Exemplo:

    ```
        // Errado: Uma interface grande
        public interface IWorker
        {
            void Work();
            void Eat();
        }
        
        // Certo: Interfaces menores e específicas
        public interface IWorkable
        {
            void Work();
        }
        
        public interface IEatable
        {
            void Eat();
        }
        
        public class Human : IWorkable, IEatable
        {
            public void Work() { }
            public void Eat() { }
        }
        
        public class Robot : IWorkable
        {
            public void Work() { }
        }

    ```
Explicação: No exemplo correto, cada classe implementa apenas os métodos que realmente utiliza.

### D - Dependency Inversion Principle (Princípio da Inversão de Dependência)
Definição: Dependa de abstrações, não de implementações concretas.

Exemplo:


    ```
        // Errado: Classe depende diretamente de uma implementação concreta
        public class EmailService
        {
            public void SendEmail(string message) { }
        }
        
        public class Notification
        {
            private EmailService _emailService = new EmailService();
        
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

    ```

Explicação: No exemplo correto, o código é mais flexível, permitindo trocar a implementação de envio de mensagens facilmente (por exemplo, Email, SMS).


