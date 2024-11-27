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



-------------------------------------------------------------------------------------------------------------------------------------------------------------------



### DRY

### DRY - Don’t Repeat Yourself (Não se Repita) 
Definição: O princípio DRY recomenda evitar duplicação de código. Se você perceber que está repetindo lógica ou estruturas similares, deve refatorar seu código para reutilizar componentes e métodos, centralizando a lógica.
Por que é importante?
Facilita a manutenção.
Reduz erros, pois uma alteração em um local não precisa ser replicada.
Melhora a legibilidade e organização do código.

    
### Exemplo - Sem DRY (Errado)

    ```
        public class InvoiceService
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
    ```


### Problemas:

A lógica de "Salvar no banco de dados" e "Enviar email" está duplicada.
Se houver uma mudança, será necessário alterar em múltiplos lugares.

### Exemplo - Com DRY (Certo)

    ```
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

    ```

### Vantagens do Exemplo Certo:
Reutilização de Código: As ações repetitivas (salvar no banco e enviar email) foram movidas para uma classe reutilizável.
Facilidade de Alteração: Se o processo de envio de email mudar, será necessário ajustar apenas na classe DocumentService.
Organização: Cada classe tem uma responsabilidade clara.

### Resumo
Evite escrever o mesmo código em múltiplos lugares. Sempre que identificar duplicação, pense em como encapsular a lógica em métodos, classes ou funções reutilizáveis. 🚀

-------------------------------------------------------------------------------------------------------------------------------------------------------------------


### YAGNI

### YAGNI - You Aren't Gonna Need It (Você Não Vai Precisar Disso)
Definição: O princípio YAGNI afirma que você não deve implementar funcionalidades ou criar códigos que não são necessários no momento. Foque apenas nos requisitos reais, em vez de antecipar possíveis necessidades futuras.

### Por que é importante?
Evita complexidade desnecessária: Código que não será usado só aumenta o custo de manutenção.
Economiza tempo: Concentre-se no que realmente agrega valor agora.
Melhora a legibilidade: Código simples e direto é mais fácil de entender e manter.


    
### Exemplo - Sem DRY (Errado)

    ```
    public class OrderService
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
    ```

Problemas:

Os métodos ApplyDiscount e ScheduleOrderDelivery foram implementados sem necessidade atual.
Gasto de tempo e recursos em algo que pode nunca ser usado.


### Exemplo - Seguindo YAGNI (Certo)


    ```
    public class OrderService
    {
        public void CreateOrder(string customerName, decimal orderTotal)
        {
            Console.WriteLine($"Creating order for {customerName} with total {orderTotal}");
        }
    }
    ```

### Por que está correto?

Apenas as funcionalidades realmente solicitadas são implementadas.
Métodos adicionais, como desconto ou agendamento de entrega, só serão adicionados caso sejam necessários no futuro.

### Quando aplicar YAGNI?
Durante o desenvolvimento inicial: Foque apenas nos requisitos especificados.
Ao planejar funcionalidades: Pergunte-se: "Isso será realmente usado agora?"
Ao revisar código: Remova partes do código que foram implementadas antecipadamente e não são usadas.

### Resumo
O YAGNI nos lembra de não gastar tempo com "e se" ou "talvez precisemos disso no futuro". Concentre-se no que é necessário agora. Se surgir a necessidade de algo no futuro, você pode implementar na hora certa, com mais clareza sobre os requisitos reais. 🚀


--------------------------------------------------------------------------


### KISS

### KISS - Keep It Simple, Stupid (Mantenha Isso Simples, Estúpido)
Definição:
O princípio KISS afirma que os sistemas e códigos devem ser mantidos o mais simples possível. Soluções complicadas aumentam a dificuldade de manutenção e introduzem potenciais bugs. Sempre prefira implementações claras e diretas.

### Por que é importante?
Fácil de entender: Um código simples é mais legível para você e outros desenvolvedores.
Reduz erros: Menos complexidade significa menos áreas propensas a falhas.
Facilita a manutenção: Soluções simples são mais fáceis de ajustar e expandir.


### Exemplo - Sem KISS (Errado)


    ```
    public int CalculateSumOfEvenNumbers(int[] numbers)
    {
        int sum = 0;
        for (int i = 0; i < numbers.Length; i++)
        {
            if (numbers[i] % 2 == 0)
            {
                sum += numbers[i];
            }
        }
        return sum;
    }
    ```

Embora o código funcione, ele é mais complexo do que precisa ser. Podemos simplificá-lo.

### Exemplo - Com KISS (Certo)

    ```
    public int CalculateSumOfEvenNumbers(int[] numbers)
    {
        return numbers.Where(n => n % 2 == 0).Sum();
    }
    ```

### Por que está certo?

O uso de LINQ simplifica a lógica.
Mais legível e direto ao ponto.




### Exemplo 2 - Sem KISS (Errado)

    ```
    public string GetStatus(int statusCode)
    {
        if (statusCode == 1)
        {
            return "Success";
        }
        else if (statusCode == 2)
        {
            return "Error";
        }
        else if (statusCode == 3)
        {
            return "Pending";
        }
        else
        {
            return "Unknown";
        }
    }
    ```

#### Problemas:

Muitas condições tornam o código mais difícil de ler e manter.
É propenso a erros caso sejam adicionados novos códigos de status.




### Exemplo 2 - Com KISS (Certo)

    ```
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
    ```

### Por que está certo?

O uso do switch torna o código mais compacto e fácil de entender.
Simplicidade na adição de novos casos no futuro.



### Como aplicar KISS no dia a dia?
Evite over-engineering: Não adicione complexidade desnecessária.
Use padrões e ferramentas apropriadas: Aproveite recursos da linguagem para simplificar seu código (ex.: LINQ, switch, etc.).
Divida responsabilidades: Evite métodos ou classes que fazem "tudo".
Revisão constante: Sempre pergunte: "Isso poderia ser mais simples?"



### Resumo
Soluções simples são frequentemente as mais eficazes. Aplique o KISS escrevendo código direto, que faça apenas o necessário e seja fácil de entender. Evite complicar! 🚀


----------------


### OOP
### OOP - Programação Orientada a Objetos
Definição:
A Programação Orientada a Objetos (OOP) é um paradigma de programação que organiza o software em objetos. Cada objeto é uma combinação de dados (atributos) e comportamentos (métodos), encapsulados em uma estrutura que facilita o reuso, a organização e a manutenção do código.


### Os 4 Pilares da OOP
1 - Encapsulamento
2 - Herança
3 - Polimorfismo
4 - Abstração
Vou explicar cada pilar com exemplos em C#.






### 1. Encapsulamento

Definição:
Esconde os detalhes internos do objeto, expondo apenas o que é necessário. Isso é feito usando modificadores de acesso (public, private, protected).

Exemplo:


    ```
    public class BankAccount
    {
        private decimal _balance; // Atributo privado

        public void Deposit(decimal amount)
        {
            if (amount > 0)
            {
                _balance += amount; // Alteração controlada
            }
        }

        public decimal GetBalance()
        {
            return _balance; // Acesso controlado
        }
    }    
    ```

Explicação:
O saldo da conta está encapsulado e não pode ser acessado diretamente, garantindo integridade.

--- 

### 2. Herança
Definição:
Permite criar novas classes que herdam os atributos e métodos de uma classe existente (classe base).

Exemplo:

    ```
    public class Animal
    {
        public void Eat()
        {
            Console.WriteLine("This animal eats food.");
        }
    }

    public class Dog : Animal
    {
        public void Bark()
        {
            Console.WriteLine("The dog barks.");
        }
    }

    // Uso
    Dog dog = new Dog();
    dog.Eat(); // Herdado da classe Animal
    dog.Bark(); // Método específico da classe Dog
    ```


Explicação:
A classe Dog herda o comportamento de Animal e adiciona funcionalidades próprias.


---


### 3. Polimorfismo
Definição:
Permite que métodos ou objetos tenham comportamentos diferentes, dependendo do contexto.

Exemplo:


    ```
    public class Animal
    {
        public virtual void Speak()
        {
            Console.WriteLine("The animal makes a sound.");
        }
    }

    public class Dog : Animal
    {
        public override void Speak()
        {
            Console.WriteLine("The dog barks.");
        }
    }

    public class Cat : Animal
    {
        public override void Speak()
        {
            Console.WriteLine("The cat meows.");
        }
    }

    // Uso
    Animal animal = new Dog();
    animal.Speak(); // "The dog barks."

    animal = new Cat();
    animal.Speak(); // "The cat meows."

    ```

Explicação:
O mesmo método (Speak) tem diferentes implementações dependendo do tipo do objeto (Dog ou Cat).


---

## 4. Abstração
Definição:
Destaca os comportamentos essenciais e oculta os detalhes. Feito usando classes abstratas ou interfaces.

Exemplo:

    ```
    public abstract class Shape
    {
        public abstract double CalculateArea(); // Definição abstrata
    }

    public class Circle : Shape
    {
        public double Radius { get; set; }

        public override double CalculateArea()
        {
            return Math.PI * Radius * Radius; // Implementação específica
        }
    }

    public class Rectangle : Shape
    {
        public double Width { get; set; }
        public double Height { get; set; }

        public override double CalculateArea()
        {
            return Width * Height; // Implementação específica
        }
    }

    // Uso
    Shape circle = new Circle { Radius = 5 };
    Console.WriteLine(circle.CalculateArea()); // Área do círculo

    Shape rectangle = new Rectangle { Width = 4, Height = 6 };
    Console.WriteLine(rectangle.CalculateArea()); // Área do retângulo
    ```

Explicação:
A classe Shape abstrai o conceito de forma. Classes concretas (Circle, Rectangle) implementam os detalhes.


### Vantagens da OOP
Reutilização de Código: A herança permite criar novas classes sem duplicação de código.
Modularidade: Objetos encapsulam dados e comportamentos relacionados.
Facilidade de Manutenção: Alterações em uma classe afetam apenas o que está relacionado a ela.
Escalabilidade: Novas funcionalidades podem ser adicionadas facilmente.



