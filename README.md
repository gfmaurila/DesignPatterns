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

---


### Factory Pattern - Padrão de Fábrica

Definição:
O Factory Pattern é um padrão de criação que fornece uma interface para criar objetos em uma superclasse, mas permite que as subclasses alterem o tipo de objeto criado.

### Quando usar?

Quando a lógica de criação de objetos é complexa ou precisa ser centralizada.
Quando você deseja que a criação do objeto seja flexível e desacoplada do código cliente.



### Exemplo Simples em C#

### Sem Factory (Errado)
O cliente precisa saber detalhes de implementação para criar os objetos.


    ```
    public class Car
    {
        public string Model { get; set; }
    }

    public class Bike
    {
        public string Type { get; set; }
    }

    // Código cliente
    Car car = new Car { Model = "Sedan" };
    Bike bike = new Bike { Type = "Mountain" };

    ```


### Problemas:

O cliente precisa instanciar diretamente as classes.
A lógica de criação de objetos fica espalhada no código.


### Com Factory (Certo)
Centralizamos a lógica de criação em uma fábrica.


    ```
    // Definição de um tipo base
    public interface IVehicle
    {
        void Drive();
    }

    // Implementações concretas
    public class Car : IVehicle
    {
        public void Drive()
        {
            Console.WriteLine("Driving a car!");
        }
    }

    public class Bike : IVehicle
    {
        public void Drive()
        {
            Console.WriteLine("Riding a bike!");
        }
    }

    // Fábrica para criar objetos
    public class VehicleFactory
    {
        public static IVehicle CreateVehicle(string vehicleType)
        {
            return vehicleType.ToLower() switch
            {
                "car" => new Car(),
                "bike" => new Bike(),
                _ => throw new ArgumentException("Invalid vehicle type")
            };
        }
    }

    // Código cliente
    IVehicle vehicle = VehicleFactory.CreateVehicle("car");
    vehicle.Drive(); // Output: "Driving a car!"

    vehicle = VehicleFactory.CreateVehicle("bike");
    vehicle.Drive(); // Output: "Riding a bike!"
    ```


## Vantagens do Factory Pattern
Centralização da lógica de criação: Todo o código de criação de objetos fica em um único lugar.
Flexibilidade: É fácil adicionar novos tipos de objetos sem alterar o código cliente.
Desacoplamento: O cliente não precisa saber detalhes das classes concretas.


### Quando evitar?
Quando a lógica de criação é simples e não há necessidade de abstração.
Em sistemas pequenos, o uso do padrão pode adicionar complexidade desnecessária.



### Exemplo no mundo real

    
#### ApiClientFactory

Descrição: Classe responsável por criar instâncias de clientes HTTP configurados para se comunicar com uma API específica. Ela utiliza o padrão Factory para garantir que todas as instâncias sejam criadas de forma consistente.
    

    ```
    using System.Net.Http;
    using Refit;

    namespace GenericNamespace.ApiFactory;

    public sealed class ApiClientFactory : IApiClientFactory
    {
        private readonly IHttpClientFactory _httpClientProvider;

        public ApiClientFactory(IHttpClientFactory httpClientProvider)
        {
            _httpClientProvider = httpClientProvider;
        }

        public IApiClient Create()
        {
            var httpClient = _httpClientProvider.CreateClient(nameof(IApiClientFactory));

            return RestService.For<IApiClient>(httpClient, RefitSettings.DefaultSettings);
        }
    }
    ```

### ApiClientFactoryBuilder

Descrição: Classe estática que configura o cliente HTTP, define políticas de retry (reexecução) e gerencia outras configurações, como validação de SSL, timeout e autenticação. É usada para registrar as dependências necessárias no contêiner de injeção de dependência (DI).


    ```
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Polly;
    using Refit;
    using System;
    using System.Net.Http;
    using System.Net.Http.Headers;

    namespace GenericNamespace.ApiFactory;

    public static class ApiClientFactoryBuilder
    {
        public static void ConfigureApiClientFactory(this IServiceCollection services)
        {
            var config = ApplicationContext.Instance.ApplicationConfiguration.Configuration;

            var apiBaseUrl = config.GetValue<string>(ApiClientParameters.BaseUrl) ?? string.Empty;
            var requestTimeout = config.GetValue<int>(ApiClientParameters.Timeout);
            var sslValidationEnabled = config.GetValue<bool>(ApiClientParameters.ValidateSslCertificates);
            var retryDelaySeconds = config.GetValue<int>(ApiClientParameters.RetryDelaySeconds);
            var maxRetryAttempts = config.GetValue<int>(ApiClientParameters.MaxRetryAttempts);
            var apiClientId = config.GetValue<string>(ApiClientParameters.ClientId);
            var apiClientSecret = config.GetValue<string>(ApiClientParameters.ClientSecret);

            var retryPolicy = Policy<HttpResponseMessage>
                .Handle<TimeoutException>()
                .Or<HttpRequestException>()
                .Or<ApiException>()
                .WaitAndRetryAsync(
                    retryCount: maxRetryAttempts,
                    sleepDurationProvider: _ => TimeSpan.FromSeconds(retryDelaySeconds),
                    onRetry: (result, timespan, count, context) =>
                    {
                        ApplicationContext.Instance.Resolve<ILogger<ApiClientFactoryBuilder>>()
                            .LogWarning("Retrying API call to {0} due to error: {1}, retry attempt: {2}", apiBaseUrl, result.Exception, count);
                    });

            services.AddHttpClient<IApiClientFactory>(client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
                client.Timeout = TimeSpan.FromSeconds(requestTimeout);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("client_id", apiClientId);
                client.DefaultRequestHeaders.Add("client_secret", apiClientSecret);
            }).ConfigurePrimaryHttpMessageHandler(() =>
            {
                var handler = new HttpClientHandler();
                if (!sslValidationEnabled)
                    handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
                return handler;
            }).AddPolicyHandler(retryPolicy);

            services.AddRefitClient<IApiClient>(RefitSettings.DefaultSettings)
                .ConfigureHttpClient(client =>
                {
                    client.BaseAddress = new Uri(apiBaseUrl);
                    client.Timeout = TimeSpan.FromSeconds(requestTimeout);
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    client.DefaultRequestHeaders.Add("client_id", apiClientId);
                    client.DefaultRequestHeaders.Add("client_secret", apiClientSecret);
                }).ConfigurePrimaryHttpMessageHandler(() =>
                {
                    var handler = new HttpClientHandler();
                    if (!sslValidationEnabled)
                        handler.ServerCertificateCustomValidationCallback = (_, _, _, _) => true;
                    return handler;
                }).AddPolicyHandler(retryPolicy);
        }
    }

    ```

#### ApiClientParameters

Descrição: Classe que define constantes para as chaves de configuração relacionadas ao cliente da API, como URL base, timeout, validação de certificados SSL e credenciais de autenticação.



    ```
    namespace GenericNamespace.ApiFactory;

    public class ApiClientParameters
    {
        protected ApiClientParameters() { }

        public const string BaseUrl = "ApiSettings:BaseUrl";
        public const string Timeout = "ApiSettings:Timeout";
        public const string ValidateSslCertificates = "ApiSettings:ValidateSslCertificates";
        public const string RetryDelaySeconds = "ApiSettings:RetryPolicy:RetryDelaySeconds";
        public const string MaxRetryAttempts = "ApiSettings:RetryPolicy:MaxRetryAttempts";
        public const string ClientId = "ApiSettings:Authentication:ClientId";
        public const string ClientSecret = "ApiSettings:Authentication:ClientSecret";
    }

    ```


### IApiClientFactory

Descrição: Interface que define o contrato para uma fábrica de clientes API. Contém o método Create, que é responsável por criar instâncias de clientes API.


    ```
    namespace GenericNamespace.ApiFactory;

    public interface IApiClientFactory
    {
        IApiClient Create();
    }

    ```



---





### Repository Pattern - Padrão de Repositório

Definição:
O Repository Pattern é um padrão arquitetural que atua como um intermediário entre o código de lógica de negócios e a camada de acesso a dados (como o banco de dados). Ele encapsula as operações de leitura e gravação e fornece uma interface limpa para manipular os dados.

### Quando usar?

Para abstrair e centralizar o acesso a dados.
Para separar a lógica de negócios da lógica de persistência.
Em aplicações que podem mudar o mecanismo de persistência no futuro.

### sBenefícios
Desacoplamento: A lógica de negócios não precisa saber como os dados são armazenados ou recuperados.
Testabilidade: Permite o uso de mocks ou stubs nos testes unitários.
Reutilização: Facilita o reuso de consultas e operações de dados.



### Exemplo em C#
### Sem Repository (Errado)
O código de persistência está espalhado pela aplicação.




    ```
    public class ProductService
    {
        private readonly SqlConnection _connection;

        public ProductService(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public List<string> GetAllProductNames()
        {
            var productNames = new List<string>();
            _connection.Open();

            using var command = new SqlCommand("SELECT Name FROM Products", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                productNames.Add(reader.GetString(0));
            }

            _connection.Close();
            return productNames;
        }
    }

    ```


### Problemas:

O ProductService depende diretamente do banco de dados.
Alterações no banco de dados exigem alterações na lógica de negócios.


### Com Repository (Certo)
O acesso a dados é encapsulado no repositório.


    ```
    // Entidade
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    // Interface do Repositório
    public interface IProductRepository
    {
        IEnumerable<Product> GetAllProducts();
        Product GetProductById(int id);
        void AddProduct(Product product);
        void DeleteProduct(int id);
    }

    // Implementação do Repositório
    public class ProductRepository : IProductRepository
    {
        private readonly SqlConnection _connection;

        public ProductRepository(string connectionString)
        {
            _connection = new SqlConnection(connectionString);
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = new List<Product>();
            _connection.Open();

            using var command = new SqlCommand("SELECT Id, Name, Price FROM Products", _connection);
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                products.Add(new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2)
                });
            }

            _connection.Close();
            return products;
        }

        public Product GetProductById(int id)
        {
            _connection.Open();

            using var command = new SqlCommand("SELECT Id, Name, Price FROM Products WHERE Id = @Id", _connection);
            command.Parameters.AddWithValue("@Id", id);

            using var reader = command.ExecuteReader();
            if (reader.Read())
            {
                var product = new Product
                {
                    Id = reader.GetInt32(0),
                    Name = reader.GetString(1),
                    Price = reader.GetDecimal(2)
                };

                _connection.Close();
                return product;
            }

            _connection.Close();
            return null;
        }

        public void AddProduct(Product product)
        {
            _connection.Open();

            using var command = new SqlCommand("INSERT INTO Products (Name, Price) VALUES (@Name, @Price)", _connection);
            command.Parameters.AddWithValue("@Name", product.Name);
            command.Parameters.AddWithValue("@Price", product.Price);
            command.ExecuteNonQuery();

            _connection.Close();
        }

        public void DeleteProduct(int id)
        {
            _connection.Open();

            using var command = new SqlCommand("DELETE FROM Products WHERE Id = @Id", _connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();

            _connection.Close();
        }
    }

    // Uso do Repositório na Lógica de Negócios
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public void PrintAllProducts()
        {
            var products = _repository.GetAllProducts();
            foreach (var product in products)
            {
                Console.WriteLine($"{product.Id}: {product.Name} - {product.Price:C}");
            }
        }
    }

    // Exemplo de uso
    var repository = new ProductRepository("your_connection_string");
    var service = new ProductService(repository);
    service.PrintAllProducts();

    ```



### Vantagens do Exemplo Certo
Desacoplamento: A lógica de negócios (ProductService) não depende de como os dados são persistidos (ProductRepository).
Substituição: Fácil trocar o mecanismo de persistência (ex.: trocar SQL Server por MongoDB).
Testes Unitários: A interface IProductRepository pode ser mockada para testes.



---


### Value Object - Objeto de Valor
Definição:
Um Value Object é um padrão do DDD (Domain-Driven Design) que representa um conceito do domínio. Diferentemente de entidades, Value Objects não possuem identidade própria e são imutáveis. Dois objetos são iguais se seus valores forem iguais.

### Características de um Value Object
Imutabilidade: Um objeto de valor nunca muda após ser criado.
Sem Identidade: Não é identificado por um Id, mas pelos valores que contém.
Comparação por Valor: Dois Value Objects são iguais se seus valores forem iguais.


### Exemplo Prático
Cenário
Vamos modelar um endereço em um sistema de pedidos. O endereço (rua, número, cidade) é um Value Object, porque:

### Não tem identidade única.
A troca de um endereço por outro com os mesmos valores não muda o sistema.



### Implementação do Value Object




    ```
    public class Address
    {
        public string Street { get; }
        public string City { get; }
        public string ZipCode { get; }

        public Address(string street, string city, string zipCode)
        {
            if (string.IsNullOrWhiteSpace(street) || string.IsNullOrWhiteSpace(city) || string.IsNullOrWhiteSpace(zipCode))
            {
                throw new ArgumentException("All address fields must be provided.");
            }

            Street = street;
            City = city;
            ZipCode = zipCode;
        }

        // Comparação por valor
        public override bool Equals(object obj)
        {
            if (obj is not Address other)
            {
                return false;
            }

            return Street == other.Street &&
                   City == other.City &&
                   ZipCode == other.ZipCode;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Street, City, ZipCode);
        }
    }

    ```


### Uso do Value Object

    ```
    public class Order
    {
        public int Id { get; set; }
        public Address ShippingAddress { get; private set; }

        public Order(int id, Address shippingAddress)
        {
            Id = id;
            ShippingAddress = shippingAddress ?? throw new ArgumentNullException(nameof(shippingAddress));
        }

        public void ChangeShippingAddress(Address newAddress)
        {
            if (newAddress == null)
            {
                throw new ArgumentNullException(nameof(newAddress));
            }

            ShippingAddress = newAddress;
        }
    }
        
    ```

### Exemplo de Código Cliente

    ```
    var address1 = new Address("123 Main St", "Springfield", "12345");
    var address2 = new Address("123 Main St", "Springfield", "12345");
    var address3 = new Address("456 Elm St", "Shelbyville", "67890");

    Console.WriteLine(address1.Equals(address2)); // True (mesmos valores)
    Console.WriteLine(address1.Equals(address3)); // False (valores diferentes)

    // Usando no pedido
    var order = new Order(1, address1);
    Console.WriteLine($"Order Address: {order.ShippingAddress.Street}");

    // Alterando o endereço
    order.ChangeShippingAddress(address3);
    Console.WriteLine($"Updated Order Address: {order.ShippingAddress.Street}");

    ```



### Vantagens do Value Object
Imutabilidade: Garante que os dados não sejam alterados acidentalmente.
Consistência: Comparações baseadas em valor reduzem erros.
Modelagem rica: Representa conceitos do domínio com mais precisão.


### Quando usar Value Objects?
Para representar pequenos conceitos do domínio como:
Endereço, CPF, Nome completo, etc.
Quando a identidade única não é necessária.





----

### Entity - Entidade



Definição:
No contexto de DDD (Domain-Driven Design), uma Entity (Entidade) é um objeto que possui uma identidade única e persistente ao longo do tempo, independentemente de seus atributos. Isso significa que a entidade é identificada por algo (como um Id), e não pelos valores de seus atributos.

### Características de uma Entidade
Identidade: É identificada de forma única, geralmente por um Id.
Mutabilidade: Seus atributos podem ser alterados, mas sua identidade permanece a mesma.
Persistência: Geralmente representa algo que precisa ser armazenado em um banco de dados.

### Exemplo Prático
Cenário
Vamos modelar um sistema de pedidos. O Pedido (Order) é uma entidade porque:

Ele tem um Id único.
É um conceito do domínio que pode mudar ao longo do tempo (adicionar itens, alterar status).


### Implementação da Entidade




    ```
    public class Order
    {
        public int Id { get; private set; }
        public string CustomerName { get; private set; }
        public List<OrderItem> Items { get; private set; } = new List<OrderItem>();
        public DateTime CreatedAt { get; private set; }

        public Order(int id, string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
            {
                throw new ArgumentException("Customer name cannot be null or empty.");
            }

            Id = id;
            CustomerName = customerName;
            CreatedAt = DateTime.UtcNow;
        }

        public void AddItem(OrderItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Items.Add(item);
        }

        public void RemoveItem(OrderItem item)
        {
            if (item == null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            Items.Remove(item);
        }
    }

    ```

### Exemplo de Item do Pedido (Entidade Relacionada)

    ```
    public class OrderItem
    {
        public int Id { get; private set; }
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal Price { get; private set; }

        public OrderItem(int id, string productName, int quantity, decimal price)
        {
            if (string.IsNullOrWhiteSpace(productName))
            {
                throw new ArgumentException("Product name cannot be null or empty.");
            }

            if (quantity <= 0)
            {
                throw new ArgumentException("Quantity must be greater than zero.");
            }

            if (price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }

            Id = id;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }
    }
    ```


### Exemplo de Uso

    ```
    // Criando o pedido
    var order = new Order(1, "John Doe");

    // Adicionando itens ao pedido
    var item1 = new OrderItem(1, "Laptop", 1, 1500.00m);
    var item2 = new OrderItem(2, "Mouse", 2, 50.00m);

    order.AddItem(item1);
    order.AddItem(item2);

    // Exibindo detalhes do pedido
    Console.WriteLine($"Order ID: {order.Id}");
    Console.WriteLine($"Customer: {order.CustomerName}");
    Console.WriteLine("Items:");
    foreach (var item in order.Items)
    {
        Console.WriteLine($" - {item.ProductName}, Quantity: {item.Quantity}, Price: {item.Price:C}");
    }

    // Removendo um item
    order.RemoveItem(item2);

    Console.WriteLine("Updated Items:");
    foreach (var item in order.Items)
    {
        Console.WriteLine($" - {item.ProductName}, Quantity: {item.Quantity}, Price: {item.Price:C}");
    }
    ```



### Diferença entre Entidades e Value Objects

<div>
    <table>
        <tr>
            <th>Aspecto</th>
            <th>Entity</th>
            <th>Value Object</th>
        </tr>
        <tr>
            <td>Identidade</td>
            <td>Identidade única (Id).</td>
            <td>Não possui identidade única.</td>
        </tr>
        <tr>
            <td>Comparação</td>
            <td>Comparada por referência ou Id.</td>
            <td>Comparada por valores.</td>
        </tr>
        <tr>
            <td>Persistência</td>
            <td>Geralmente armazenada no banco.</td>
            <td>Geralmente parte de uma entidade.</td>
        </tr>
        <tr>
            <td>Mutabilidade</td>
            <td>Pode ser mutável.</td>
            <td>É imutável.</td>
        </tr>
    </table>
</div>


### Quando usar Entidades?
Para representar objetos do domínio que precisam de uma identidade única e persistente (ex.: Pedido, Cliente, Produto).
Quando o conceito modelado pode mudar ao longo do tempo.

---

### Facade Pattern - Padrão Fachada
Definição:
O Facade Pattern (ou Fachada) é um padrão de design estrutural que fornece uma interface simplificada para um subsistema complexo. 
Ele ajuda a encapsular a complexidade, expondo apenas os métodos necessários para o cliente, tornando o sistema mais fácil de usar.


### Quando usar?
Quando um subsistema tem muitas classes e você deseja ocultar sua complexidade.
Para criar uma interface de alto nível que facilita o uso de subsistemas complexos.
Para desacoplar clientes da implementação interna de subsistemas.

### Exemplo Prático
Cenário
Imagine um sistema de e-commerce com funcionalidades como:

Processamento de pedidos.
Envio de notificações.
Pagamento.
Essas funcionalidades são implementadas em classes diferentes. O cliente precisa de uma interface simplificada para realizar todas as operações necessárias sem lidar diretamente com cada classe.


### Sem Facade (Errado)
O cliente precisa interagir com várias classes diretamente.


    ```
    public class OrderService
    {
        public void PlaceOrder(string product)
        {
            Console.WriteLine($"Order placed for {product}");
        }
    }

    public class PaymentService
    {
        public void ProcessPayment(string product)
        {
            Console.WriteLine($"Payment processed for {product}");
        }
    }

    public class NotificationService
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Notification sent: {message}");
        }
    }

    // Código cliente
    var orderService = new OrderService();
    var paymentService = new PaymentService();
    var notificationService = new NotificationService();

    string product = "Laptop";
    orderService.PlaceOrder(product);
    paymentService.ProcessPayment(product);
    notificationService.SendNotification($"Order for {product} confirmed.");

    ```


### Problemas:

O cliente precisa conhecer e coordenar todas as classes.
Aumenta a complexidade e dificulta mudanças no subsistema.



### Com Facade (Certo)
Criamos uma classe ECommerceFacade que simplifica as operações para o cliente.


    ```
    // Subsistemas
    public class OrderService
    {
        public void PlaceOrder(string product)
        {
            Console.WriteLine($"Order placed for {product}");
        }
    }

    public class PaymentService
    {
        public void ProcessPayment(string product)
        {
            Console.WriteLine($"Payment processed for {product}");
        }
    }

    public class NotificationService
    {
        public void SendNotification(string message)
        {
            Console.WriteLine($"Notification sent: {message}");
        }
    }

    // Fachada
    public class ECommerceFacade
    {
        private readonly OrderService _orderService = new OrderService();
        private readonly PaymentService _paymentService = new PaymentService();
        private readonly NotificationService _notificationService = new NotificationService();

        public void CompleteOrder(string product)
        {
            _orderService.PlaceOrder(product);
            _paymentService.ProcessPayment(product);
            _notificationService.SendNotification($"Order for {product} confirmed.");
        }
    }

    // Código cliente
    var eCommerceFacade = new ECommerceFacade();
    eCommerceFacade.CompleteOrder("Laptop");

    ```



### Resultado:

O cliente interage apenas com a Facade.
A lógica de coordenação entre subsistemas está encapsulada.


### Exemplo mundo real


### Como o padrão é aplicado nesta classe?

1 - Ocultação de Complexidade:

A classe EntityDataFacade atua como uma fachada que abstrai as chamadas para:
Um command store local (IEntityCommandStore) para buscar ou registrar os dados.
Uma API externa (IExternalApi) para buscar dados adicionais quando necessário.

2 - Interface Unificada:

O método IEntityDataFacade encapsula toda a lógica de:
Verificar se os dados estão disponíveis localmente.
Buscar os dados na API externa caso não estejam.
Tratar exceções e armazenar os dados no sistema local para uso futuro.


3 - Coordenação de Subsistemas:

A classe coordena diferentes serviços (como o API Factory, command store e logger) para cumprir sua responsabilidade de fornecer os dados da cooperativa.
    


    ```
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.Extensions.Logging;

    namespace GenericNamespace.Facade;

    public class EntityDataFacade : IEntityDataFacade
    {
        private readonly IExternalApi _externalApi;
        private readonly IEntityCommandStore _entityCommandStore;
        private readonly ILogger<EntityDataFacade> _logger;

        public EntityDataFacade(IExternalApiFactory externalApiFactory,
                                IEntityCommandStore entityCommandStore,
                                ILogger<EntityDataFacade> logger)
        {
            _entityCommandStore = entityCommandStore;
            _externalApi = externalApiFactory.Create();
            _logger = logger;
        }

        public async Task<Entity> GetEntityByCode(int entityCode)
        {
            var entity = await _entityCommandStore.GetEntityByCode(entityCode);

            if (entity is null)
            {
                var apiEntityResult = await FetchEntityByCodeFromApi(entityCode);

                if (apiEntityResult is null)
                    return null!;

                entity = new Entity(
                    new LegalEntity(
                        Document.FromString(apiEntityResult.Person?.TaxId),
                        apiEntityResult.Person?.Naming?.LegalName,
                        apiEntityResult.Person?.Naming?.TradeName
                    ),
                    apiEntityResult.Code
                );

                await _entityCommandStore.Register(entity);
            }

            return entity;
        }

        private async Task<ApiEntityModel?> FetchEntityByCodeFromApi(long entityCode)
        {
            try
            {
                var apiEntity = await _externalApi.GetEntityDataByCodeAsync(entityCode);

                if (apiEntity is null || apiEntity.Result?.Any() == false)
                    throw new BusinessException("Entity does not exist.");

                return apiEntity.Result?[0];
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while accessing the external API");
                throw;
            }
        }
    }

    ```


    ```
    namespace GenericNamespace.Facade;s

    public interface IEntityDataFacade
    {
        Task<Entity> GetEntityByCode(int entityCode);
    }
    ```
    



### Vantagens do Facade Pattern
Simplicidade: Reduz a complexidade do cliente ao fornecer uma interface única.
Desacoplamento: O cliente não precisa conhecer detalhes dos subsistemas.
Manutenção facilitada: Alterações nos subsistemas podem ser feitas sem impactar o cliente, desde que a interface da fachada permaneça consistente.

### Resumo
O Facade Pattern ajuda a organizar e simplificar sistemas complexos, centralizando a interação com os subsistemas em uma única classe. É ideal para reduzir a complexidade e facilitar o uso de APIs ou sistemas internos. 🚀



