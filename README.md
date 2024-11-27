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



---




### Chain of Responsibility Pattern - Padrão Cadeia de Responsabilidade
Definição:
O Chain of Responsibility é um padrão comportamental que permite que um pedido seja processado por uma sequência de handlers (manipuladores). Cada handler decide se processa o pedido ou o encaminha para o próximo na cadeia.



### Quando usar?
Quando você tem um conjunto de objetos que podem processar um pedido, mas não sabe qual deles o fará até o momento da execução.
Quando deseja desacoplar o remetente do pedido de seus receptores.
Para implementar pipelines de processamento flexíveis.



### Exemplo Prático
Cenário
Vamos criar um sistema de autenticação onde diferentes etapas verificam:

1 - Se o usuário existe.
2 - Se a senha está correta.
3 - Se o usuário tem permissão para acessar o recurso.
Cada etapa será um handler na cadeia de responsabilidade.



### Implementação
### 1. Criar a Interface Base do Handler
Define a estrutura comum para todos os manipuladores.





    ```
    public interface IHandler
    {
        void SetNext(IHandler next);
        void Handle(Request request);
    }
    ```

### 2. Criar a Classe Base para os Handlers
Facilita a implementação da lógica para passar o pedido adiante.

    ```
    public abstract class BaseHandler : IHandler
    {
        private IHandler _nextHandler;

        public void SetNext(IHandler next)
        {
            _nextHandler = next;
        }

        public virtual void Handle(Request request)
        {
            if (_nextHandler != null)
            {
                _nextHandler.Handle(request);
            }
        }
    }
    ```


### 3. Criar os Handlers Concretos
Cada classe executa uma verificação específica e, se necessário, passa o pedido para o próximo handler.


    ```
    public class UserExistsHandler : BaseHandler
    {
        public override void Handle(Request request)
        {
            if (request.Username != "admin")
            {
                Console.WriteLine("User does not exist.");
                return;
            }

            Console.WriteLine("User exists.");
            base.Handle(request);
        }
    }

    public class PasswordHandler : BaseHandler
    {
        public override void Handle(Request request)
        {
            if (request.Password != "1234")
            {
                Console.WriteLine("Incorrect password.");
                return;
            }

            Console.WriteLine("Password is correct.");
            base.Handle(request);
        }
    }

    public class PermissionHandler : BaseHandler
    {
        public override void Handle(Request request)
        {
            if (!request.HasPermission)
            {
                Console.WriteLine("Permission denied.");
                return;
            }

            Console.WriteLine("Permission granted.");
            base.Handle(request);
        }
    }

    ```




### 4. Criar o Objeto de Pedido
O pedido que será processado pela cadeia.


    ```
    public class Request
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool HasPermission { get; set; }
    }

    ```

### 5. Configurar e Usar a Cadeia de Responsabilidade

    
    ```
    class Program
    {
        static void Main()
        {
            // Criar os handlers
            var userExistsHandler = new UserExistsHandler();
            var passwordHandler = new PasswordHandler();
            var permissionHandler = new PermissionHandler();

            // Configurar a cadeia
            userExistsHandler.SetNext(passwordHandler);
            passwordHandler.SetNext(permissionHandler);

            // Criar o pedido
            var request = new Request
            {
                Username = "admin",
                Password = "1234",
                HasPermission = true
            };

            // Processar o pedido
            userExistsHandler.Handle(request);

            // Saída esperada:
            // User exists.
            // Password is correct.
            // Permission granted.
        }
    }
    ```



### Vantagens do Chain of Responsibility
Desacoplamento: O remetente do pedido não precisa saber qual handler processará o pedido.
Flexibilidade: Fácil adicionar, remover ou reorganizar handlers na cadeia.
Responsabilidade distribuída: Cada handler é responsável por uma etapa específica.


### Quando evitar?
Se o pedido precisa ser processado por todos os handlers (use um padrão diferente, como Composite).
Se a lógica for simples e um único método puder lidar com o pedido.


### Resumo
O Chain of Responsibility é uma ótima escolha quando você precisa que um pedido passe por várias etapas sequenciais de processamento, permitindo que cada etapa decida se deve manipular o pedido ou encaminhá-lo. 🚀



### Decorator Pattern - Padrão Decorador
Definição:
O Decorator Pattern é um padrão estrutural que permite adicionar comportamentos a objetos dinamicamente, sem alterar o código da classe original. Ele utiliza uma composição em vez de herança para estender funcionalidades.


### Quando usar?
Quando deseja adicionar funcionalidades a objetos individuais de maneira dinâmica.
Quando não é viável modificar diretamente a classe base.
Quando precisar combinar diferentes comportamentos em tempo de execução.



### Exemplo Prático
Cenário
Vamos criar um sistema de bebidas em um café. Cada bebida pode ter "complementos" como leite, açúcar ou calda. Usaremos o padrão Decorator para adicionar esses complementos sem modificar a classe base das bebidas.


### Implementação
### 1. Criar a Interface Base
Define a estrutura comum para todas as bebidas.

    ```
    public interface IBeverage
    {
        string GetDescription();
        decimal GetCost();
    }
    ```

#### 2. Criar a Classe Base Concreta
Implementa a interface básica.


    ```
    public class Coffee : IBeverage
    {
        public string GetDescription()
        {
            return "Coffee";
        }

        public decimal GetCost()
        {
            return 5.00m; // Preço base do café
        }
    }
    ```



#### 3. Criar a Classe Base para os Decoradores
Todos os decoradores implementam a mesma interface e decoram uma bebida existente.


    ```
    public abstract class BeverageDecorator : IBeverage
    {
        protected IBeverage _beverage;

        protected BeverageDecorator(IBeverage beverage)
        {
            _beverage = beverage;
        }

        public virtual string GetDescription()
        {
            return _beverage.GetDescription();
        }

        public virtual decimal GetCost()
        {
            return _beverage.GetCost();
        }
    }
    ```



#### 4. Criar Decoradores Concretos
Cada decorador adiciona comportamento específico.

    ```
    public class MilkDecorator : BeverageDecorator
    {
        public MilkDecorator(IBeverage beverage) : base(beverage) { }

        public override string GetDescription()
        {
            return _beverage.GetDescription() + ", Milk";
        }

        public override decimal GetCost()
        {
            return _beverage.GetCost() + 1.50m; // Custo adicional do leite
        }
    }

    public class SugarDecorator : BeverageDecorator
    {
        public SugarDecorator(IBeverage beverage) : base(beverage) { }

        public override string GetDescription()
        {
            return _beverage.GetDescription() + ", Sugar";
        }

        public override decimal GetCost()
        {
            return _beverage.GetCost() + 0.50m; // Custo adicional do açúcar
        }
    }

    public class SyrupDecorator : BeverageDecorator
    {
        public SyrupDecorator(IBeverage beverage) : base(beverage) { }

        public override string GetDescription()
        {
            return _beverage.GetDescription() + ", Syrup";
        }

        public override decimal GetCost()
        {
            return _beverage.GetCost() + 2.00m; // Custo adicional da calda
        }
    }
    ```


### 5. Usar o Decorator

    ```
    class Program
    {
        static void Main(string[] args)
        {
            // Criar um café simples
            IBeverage beverage = new Coffee();
            Console.WriteLine($"{beverage.GetDescription()} - {beverage.GetCost():C}");

            // Adicionar leite ao café
            beverage = new MilkDecorator(beverage);
            Console.WriteLine($"{beverage.GetDescription()} - {beverage.GetCost():C}");

            // Adicionar açúcar ao café com leite
            beverage = new SugarDecorator(beverage);
            Console.WriteLine($"{beverage.GetDescription()} - {beverage.GetCost():C}");

            // Adicionar calda ao café com leite e açúcar
            beverage = new SyrupDecorator(beverage);
            Console.WriteLine($"{beverage.GetDescription()} - {beverage.GetCost():C}");
        }
    }
    ```

### Saída Esperada

    ```
    Coffee - $5.00
    Coffee, Milk - $6.50
    Coffee, Milk, Sugar - $7.00
    Coffee, Milk, Sugar, Syrup - $9.00
    ```    

### Vantagens do Decorator Pattern
Flexibilidade: Permite combinar e empilhar comportamentos de forma dinâmica.
Abstração: O cliente não precisa saber como os objetos são decorados.
Reuso: Comportamentos podem ser reutilizados em diferentes combinações.


### Quando evitar?
Se o número de combinações de decoradores for muito alto, pode se tornar complexo e difícil de gerenciar.
Se a hierarquia de classes for mais simples com herança direta.

### Resumo
O Decorator Pattern permite adicionar funcionalidades a objetos sem alterar seu código ou criar subclasses. Ele é útil para cenários em que os requisitos mudam frequentemente ou há muitas combinações possíveis de comportamentos. 🚀


---

#### Specification Pattern

Definição:
O Specification Pattern é um padrão comportamental que encapsula a lógica de validação ou regras de negócios em objetos reutilizáveis e combináveis. Ele permite criar condições complexas de maneira modular e fácil de reutilizar.


### Quando usar?
Quando você tem regras de negócios ou critérios de validação que podem ser reutilizados em várias partes do sistema.
Para construir condições complexas combinando especificações simples.
Para melhorar a legibilidade e organização do código que lida com validações ou filtros.



### Exemplo Prático
Cenário
Imagine um sistema que lida com a filtragem de produtos em uma loja. Os produtos precisam ser filtrados com base em diferentes critérios, como preço, categoria ou disponibilidade. Usaremos o Specification Pattern para encapsular essas condições.



### Implementação
### 1. Criar a Interface Base
Define o contrato para todas as especificações.


    ```
    public interface ISpecification<T>
    {
        bool IsSatisfiedBy(T entity);
        ISpecification<T> And(ISpecification<T> other);
        ISpecification<T> Or(ISpecification<T> other);
        ISpecification<T> Not();
    }
    ```


### 2. Implementar a Classe Base
Fornece a implementação básica para combinar especificações.

    ```
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract bool IsSatisfiedBy(T entity);

        public ISpecification<T> And(ISpecification<T> other)
        {
            return new AndSpecification<T>(this, other);
        }

        public ISpecification<T> Or(ISpecification<T> other)
        {
            return new OrSpecification<T>(this, other);
        }

        public ISpecification<T> Not()
        {
            return new NotSpecification<T>(this);
        }
    }
    ```


#### 3. Criar Especificações Compostas
Combina especificações usando lógica booleana.


    ```
    public class AndSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return _left.IsSatisfiedBy(entity) && _right.IsSatisfiedBy(entity);
        }
    }

    public class OrSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return _left.IsSatisfiedBy(entity) || _right.IsSatisfiedBy(entity);
        }
    }

    public class NotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _specification;

        public NotSpecification(ISpecification<T> specification)
        {
            _specification = specification;
        }

        public override bool IsSatisfiedBy(T entity)
        {
            return !_specification.IsSatisfiedBy(entity);
        }
    }
    ```


### 4. Criar Especificações Concretas
Encapsula regras específicas de validação.


    ```
    public class PriceSpecification : Specification<Product>
    {
        private readonly decimal _minPrice;

        public PriceSpecification(decimal minPrice)
        {
            _minPrice = minPrice;
        }

        public override bool IsSatisfiedBy(Product product)
        {
            return product.Price >= _minPrice;
        }
    }

    public class CategorySpecification : Specification<Product>
    {
        private readonly string _category;

        public CategorySpecification(string category)
        {
            _category = category;
        }

        public override bool IsSatisfiedBy(Product product)
        {
            return product.Category == _category;
        }
    }
    ```

### 5. Criar a Entidade
Define o objeto a ser filtrado ou validado.


    ```
    public class Product
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
    }
    ```

### 6. Usar as Especificações

    ```
    class Program
    {
        static void Main(string[] args)
        {
            // Criar lista de produtos
            var products = new List<Product>
            {
                new Product { Name = "Laptop", Price = 1500, Category = "Electronics" },
                new Product { Name = "Mouse", Price = 50, Category = "Electronics" },
                new Product { Name = "Shampoo", Price = 10, Category = "Beauty" },
                new Product { Name = "Keyboard", Price = 100, Category = "Electronics" }
            };

            // Criar especificações
            var priceSpec = new PriceSpecification(100);
            var categorySpec = new CategorySpecification("Electronics");

            // Combinar especificações
            var spec = priceSpec.And(categorySpec);

            // Filtrar produtos
            var filteredProducts = products.Where(p => spec.IsSatisfiedBy(p)).ToList();

            // Exibir produtos filtrados
            foreach (var product in filteredProducts)
            {
                Console.WriteLine($"{product.Name} - {product.Price:C} - {product.Category}");
            }
        }
    }
    ```


### Saída Esperada

    ```
    Laptop - $1,500.00 - Electronics
    Keyboard - $100.00 - Electronics    
    ```


### Vantagens do Specification Pattern
Reuso: Especificações podem ser reutilizadas em diferentes partes do sistema.
Modularidade: Regras de negócios são encapsuladas, tornando o código mais limpo.
Composição: Especificações podem ser combinadas facilmente (And, Or, Not).


### Quando evitar?
Se as regras de validação são simples e não precisam de reutilização.
Se o número de especificações for muito pequeno, tornando o padrão desnecessário.


### Resumo
O Specification Pattern é ideal para encapsular regras de negócios ou validações complexas de maneira reutilizável e modular. Ele é altamente flexível e se destaca em sistemas que precisam de critérios dinâmicos ou frequentemente alterados. 🚀

---

#### Resumo de DDD - Domain-Driven Design
Definição:
O Domain-Driven Design (DDD) é uma abordagem de design de software centrada no domínio e na lógica de negócios. Ele enfatiza a colaboração entre especialistas de domínio e desenvolvedores para criar sistemas que reflitam de maneira fiel as regras, conceitos e processos de um domínio específico.


### Princípios Básicos do DDD
Domínio como Foco Principal:

O domínio é o "mundo" do problema que o software resolve.
O objetivo é modelar o software para representar os conceitos e regras do domínio real.
Linguagem Ubiqua (Ubiquitous Language):

Uma linguagem comum e compartilhada entre desenvolvedores e especialistas de domínio.
Deve ser usada consistentemente no código, documentação e conversas para evitar ambiguidades.
Modelagem Rica:

Modelos de domínio devem capturar de forma explícita os comportamentos e as regras de negócios.


### Camadas do DDD
#### 1. Camada de Domínio (Core):

Contém as Entidades, Objetos de Valor, Serviços de Domínio e Repositórios.
Foca na lógica de negócios.

### 2. Camada de Aplicação:

Orquestra as operações do domínio, mas não contém lógica de negócios.
Serve como uma "ponte" entre o domínio e o mundo exterior.

### 3. Camada de Infraestrutura:

Implementa detalhes técnicos, como persistência (banco de dados) e integrações.
Dá suporte às camadas superiores.

#### 4. Camada de Apresentação:


Exibe as informações e coleta entradas do usuário.
Pode incluir APIs, interfaces gráficas, etc.



#### Camadas do DDD

#### 1. Camada de Domínio (Core):

Contém as Entidades, Objetos de Valor, Serviços de Domínio e Repositórios.
Foca na lógica de negócios.


#### 2. Camada de Aplicação:

Orquestra as operações do domínio, mas não contém lógica de negócios.
Serve como uma "ponte" entre o domínio e o mundo exterior.


#### 3. Camada de Infraestrutura:

Implementa detalhes técnicos, como persistência (banco de dados) e integrações.
Dá suporte às camadas superiores.


#### 4. Camada de Apresentação:

Exibe as informações e coleta entradas do usuário.
Pode incluir APIs, interfaces gráficas, etc.






Principais Conceitos do DDD
#### 1. Entidades
Objetos com identidade única.
Exemplo: Pedido (Order), Cliente (Customer).

#### 2. Objetos de Valor (Value Objects)
Objetos imutáveis que não possuem identidade.
Exemplo: Endereço (Address), CPF.

#### 3. Repositórios
Abstraem o acesso a dados, permitindo interagir com as Entidades.
Exemplo: OrderRepository.

#### 4. Serviços de Domínio
Realizam operações que não pertencem diretamente a uma Entidade ou Objeto de Valor.
Exemplo: Serviço para calcular frete.

#### 5. Agregados
Um conjunto de Entidades e Objetos de Valor que são tratados como uma única unidade de consistência.
Exemplo: Order (Pedido) como um agregado que contém itens (OrderItem).

#### 6. Fábricas
Responsáveis por criar instâncias de objetos complexos.
Exemplo: Fábrica para criar uma nova Entidade Order com vários itens.

#### 7. Eventos de Domínio
Representam algo significativo que aconteceu no domínio.
Exemplo: PedidoCriado, PagamentoRealizado.




#### Quando Usar DDD?
#### 1. Domínios Complexos:

Quando as regras de negócios são complicadas e precisam ser bem representadas no código.


#### 2. Colaboração Intensa com Especialistas de Domínio:

Quando é essencial que o software reflita de forma precisa os processos do domínio.

#### 3. Evolução Contínua:

Quando o domínio muda frequentemente e precisa de um modelo flexível e claro.






#### Benefícios do DDD
Alinhamento com o Negócio: O software reflete fielmente as regras e processos do domínio.
Reutilização e Clareza: Conceitos bem modelados são reutilizáveis e mais fáceis de entender.
Facilidade de Manutenção: Modificar o modelo é mais simples porque ele é baseado na lógica de negócios, não em detalhes técnicos.


### Desafios do DDD
Curva de Aprendizado: Exige compreensão profunda dos conceitos e práticas.
Custo Inicial: Modelar bem o domínio pode levar tempo.
Nem Sempre Necessário: Em sistemas simples, DDD pode adicionar complexidade desnecessária.



#### Resumo
O DDD é ideal para sistemas complexos e com muitas regras de negócios. Ele ajuda a alinhar o software ao domínio real por meio de uma modelagem rica, conceitos claros e uma linguagem comum entre desenvolvedores e especialistas de domínio.

Se precisar de exemplos práticos ou uma introdução detalhada sobre cada componente, é só pedir!


### Exemplo Prático de DDD
Vamos criar um sistema de Gestão de Pedidos (Order Management) com DDD, incluindo os principais conceitos: Entidades, Objetos de Valor, Agregados, Repositórios, Serviços de Domínio e Eventos de Domínio.





### Cenário
1. Um cliente realiza um pedido.
2. O pedido contém itens e tem regras de negócios, como calcular o total.
3. O sistema deve ser capaz de persistir os pedidos e notificar eventos como "Pedido Criado".





#### Passo a Passo
### 1. Entidades
Vamos criar a entidade Order, que representa um pedido. Ela terá uma identidade única (Id) e itens.



    ```
    public class Order
    {
        public Guid Id { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public decimal Total { get; private set; }

        public Order()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
            Items = new List<OrderItem>();
        }

        public void AddItem(OrderItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            Items.Add(item);
            CalculateTotal();
        }

        private void CalculateTotal()
        {
            Total = Items.Sum(i => i.Total);
        }
    }

    ```

### 2. Objeto de Valor
O item do pedido (OrderItem) é um Value Object porque sua identidade é irrelevante no domínio. O que importa são os valores.

    ```
    public class OrderItem
    {
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Total => Quantity * UnitPrice;

        public OrderItem(string productName, int quantity, decimal unitPrice)
        {
            if (string.IsNullOrWhiteSpace(productName)) throw new ArgumentException("Product name is required.");
            if (quantity <= 0) throw new ArgumentException("Quantity must be greater than zero.");
            if (unitPrice <= 0) throw new ArgumentException("Unit price must be greater than zero.");

            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
    ```

### 3. Repositório
O Repositório encapsula o acesso ao banco de dados. Vamos criar um repositório para pedidos.

    ```
    public interface IOrderRepository
    {
        void Add(Order order);
        Order GetById(Guid id);
        IEnumerable<Order> GetAll();
    }
    ```

Implementação usando uma lista em memória (simulação):

    ```
    public class InMemoryOrderRepository : IOrderRepository
    {
        private readonly List<Order> _orders = new List<Order>();

        public void Add(Order order)
        {
            _orders.Add(order);
        }

        public Order GetById(Guid id)
        {
            return _orders.FirstOrDefault(o => o.Id == id);
        }

        public IEnumerable<Order> GetAll()
        {
            return _orders;
        }
    }
    ```


### 4. Serviço de Domínio
Vamos criar um serviço de domínio para realizar operações específicas, como criar um pedido e calcular o total.

    ```
    public class OrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public Order CreateOrder(List<OrderItem> items)
        {
            var order = new Order();

            foreach (var item in items)
            {
                order.AddItem(item);
            }

            _repository.Add(order);

            // Publicar evento (simulação)
            Console.WriteLine($"Order Created: {order.Id}, Total: {order.Total:C}");

            return order;
        }
    }
    ```



### 5. Evento de Domínio
Um evento de domínio pode ser disparado após a criação de um pedido.

    ```
    public class OrderCreatedEvent
    {
        public Guid OrderId { get; }
        public decimal Total { get; }

        public OrderCreatedEvent(Guid orderId, decimal total)
        {
            OrderId = orderId;
            Total = total;
        }
    }

    // Simulação de um "publicador de eventos"
    public static class EventPublisher
    {
        public static void Publish(OrderCreatedEvent orderEvent)
        {
            Console.WriteLine($"Event Published: OrderId = {orderEvent.OrderId}, Total = {orderEvent.Total:C}");
        }
    }
    ```

### 6. Uso do Sistema

    ```
    class Program
    {
        static void Main(string[] args)
        {
            // Repositório (In-Memory)
            IOrderRepository repository = new InMemoryOrderRepository();

            // Serviço de domínio
            var orderService = new OrderService(repository);

            // Criar itens do pedido
            var items = new List<OrderItem>
            {
                new OrderItem("Laptop", 1, 1500.00m),
                new OrderItem("Mouse", 2, 25.00m)
            };

            // Criar pedido
            var order = orderService.CreateOrder(items);

            // Publicar evento de domínio
            var orderEvent = new OrderCreatedEvent(order.Id, order.Total);
            EventPublisher.Publish(orderEvent);

            // Recuperar pedido pelo repositório
            var savedOrder = repository.GetById(order.Id);
            Console.WriteLine($"Saved Order: {savedOrder.Id}, Total: {savedOrder.Total:C}");
        }
    }
    ```

#### Saída Esperada

    ```
    Order Created: 123e4567-e89b-12d3-a456-426614174000, Total: $1,550.00
    Event Published: OrderId = 123e4567-e89b-12d3-a456-426614174000, Total: $1,550.00
    Saved Order: 123e4567-e89b-12d3-a456-426614174000, Total: $1,550.00
    ```


### Conclusão
Esse exemplo prático demonstra como usar os conceitos de DDD para modelar um sistema de Gestão de Pedidos, separando claramente as responsabilidades:

Entidades: Order.
Objetos de Valor: OrderItem.
Repositório: IOrderRepository.
Serviço de Domínio: OrderService.
Eventos de Domínio: OrderCreatedEvent.



---


### CQRS - Command Query Responsibility Segregation
Definição:
CQRS (Separação de Responsabilidade entre Comando e Consulta) é um padrão arquitetural que divide a responsabilidade de leitura (queries) e escrita (commands) em modelos separados. Ele melhora a escalabilidade e simplifica operações complexas, especialmente em sistemas com alta concorrência ou requisitos de performance.


### Princípios do CQRS
Separação de Responsabilidades:
Comandos: Alteram o estado do sistema (escrita).
Consultas: Lêem o estado do sistema (leitura).
Modelos Independentes:
O modelo de leitura pode ser otimizado para consultas rápidas.
O modelo de escrita pode ser mais rigoroso, garantindo consistência.



### Benefícios do CQRS
Escalabilidade: Permite escalonar leitura e escrita de forma independente.
Desempenho: As consultas podem usar um banco otimizado, como cache ou um banco NoSQL.
Flexibilidade: Facilita a implementação de arquiteturas event-driven (baseadas em eventos).
Simplicidade: Mantém comandos e consultas focados em suas responsabilidades.


### Exemplo Prático de CQRS
Cenário
Um sistema de Pedidos precisa:

Criar novos pedidos.
Consultar detalhes e listas de pedidos rapidamente.
Vamos implementar CQRS separando os modelos de leitura e escrita.


### 1. Modelo de Domínio (Escrita)
Criamos uma entidade de domínio Order que representa o estado do pedido.


    ```
    public class Order
    {
        public Guid Id { get; private set; }
        public string CustomerName { get; private set; }
        public List<OrderItem> Items { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public Order(string customerName)
        {
            Id = Guid.NewGuid();
            CustomerName = customerName;
            Items = new List<OrderItem>();
            CreatedAt = DateTime.UtcNow;
        }

        public void AddItem(string productName, int quantity, decimal unitPrice)
        {
            Items.Add(new OrderItem(productName, quantity, unitPrice));
        }
    }

    public class OrderItem
    {
        public string ProductName { get; private set; }
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Total => Quantity * UnitPrice;

        public OrderItem(string productName, int quantity, decimal unitPrice)
        {
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
        }
    }
    ```

### 2. Comandos (Escrita)
Criamos comandos para realizar operações no sistema de escrita.

    ```
    public class CreateOrderCommand
    {
        public string CustomerName { get; set; }
        public List<CreateOrderItemCommand> Items { get; set; }
    }

    public class CreateOrderItemCommand
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
    ```

### 3. Command Handler
O Command Handler executa as operações solicitadas pelos comandos.

    ```
    public class CreateOrderCommandHandler
    {
        private readonly IOrderRepository _repository;

        public CreateOrderCommandHandler(IOrderRepository repository)
        {
            _repository = repository;
        }

        public void Handle(CreateOrderCommand command)
        {
            var order = new Order(command.CustomerName);

            foreach (var item in command.Items)
            {
                order.AddItem(item.ProductName, item.Quantity, item.UnitPrice);
            }

            _repository.Save(order);
        }
    }
    ```

### 4. Modelo de Leitura
O modelo de leitura é otimizado para consultas rápidas, sem depender diretamente do modelo de escrita.

    ```
    public class OrderReadModel
    {
        public Guid Id { get; set; }
        public string CustomerName { get; set; }
        public decimal Total { get; set; }
    }
    ```


### 5. Queries
As consultas retornam dados diretamente do modelo de leitura.
    
    ```
    public interface IOrderReadRepository
    {
        IEnumerable<OrderReadModel> GetAllOrders();
        OrderReadModel GetOrderById(Guid id);
    }
    ```

Implementação em memória para simulação:

    ```
    public class InMemoryOrderReadRepository : IOrderReadRepository
    {
        private readonly List<OrderReadModel> _readModels = new List<OrderReadModel>();

        public void Add(Order order)
        {
            _readModels.Add(new OrderReadModel
            {
                Id = order.Id,
                CustomerName = order.CustomerName,
                Total = order.Items.Sum(i => i.Total)
            });
        }

        public IEnumerable<OrderReadModel> GetAllOrders()
        {
            return _readModels;
        }

        public OrderReadModel GetOrderById(Guid id)
        {
            return _readModels.FirstOrDefault(o => o.Id == id);
        }
    }

    ```


### 6. Uso do CQRS

    ```
    class Program
    {
        static void Main(string[] args)
        {
            // Repositórios
            var orderWriteRepository = new InMemoryOrderWriteRepository();
            var orderReadRepository = new InMemoryOrderReadRepository();

            // Command Handler
            var createOrderHandler = new CreateOrderCommandHandler(orderWriteRepository);

            // Criar um pedido
            var command = new CreateOrderCommand
            {
                CustomerName = "John Doe",
                Items = new List<CreateOrderItemCommand>
                {
                    new CreateOrderItemCommand { ProductName = "Laptop", Quantity = 1, UnitPrice = 1500 },
                    new CreateOrderItemCommand { ProductName = "Mouse", Quantity = 2, UnitPrice = 50 }
                }
            };

            createOrderHandler.Handle(command);

            // Atualizar o modelo de leitura (simulação)
            var savedOrder = orderWriteRepository.GetLastOrder();
            orderReadRepository.Add(savedOrder);

            // Consultar pedidos
            var orders = orderReadRepository.GetAllOrders();
            foreach (var order in orders)
            {
                Console.WriteLine($"Order ID: {order.Id}, Customer: {order.CustomerName}, Total: {order.Total:C}");
            }
        }
    }
    ```


### Saída Esperada

    ```
    Order ID: 123e4567-e89b-12d3-a456-426614174000, Customer: John Doe, Total: $1,600.00
    ```

### Vantagens do CQRS no Exemplo
Separação de Responsabilidades: Leitura (OrderReadRepository) e escrita (OrderWriteRepository) são independentes.
Otimização: O modelo de leitura é mais simples e direto.
Flexibilidade: O modelo de leitura pode usar uma abordagem diferente de persistência, como um banco NoSQL.


### Conclusão
O CQRS é uma poderosa abordagem para sistemas que precisam lidar com grandes volumes de leitura e escrita, mantendo código organizado e eficiente. Ele também facilita a introdução de event sourcing para capturar todas as mudanças no estado. 🚀

---



### Mediator Pattern - Padrão Mediador
Definição:
O Mediator Pattern é um padrão comportamental que facilita a comunicação entre objetos sem que eles precisem se referenciar diretamente. Ele promove o desacoplamento ao centralizar a comunicação em uma única classe chamada mediador.


### Quando usar?
Quando há múltiplos objetos que precisam se comunicar entre si.
Quando deseja reduzir dependências diretas entre classes.
Quando a lógica de comunicação entre objetos se torna complexa.


#### Benefícios
Desacoplamento: Reduz dependências diretas entre objetos.
Centralização: Toda a lógica de comunicação é gerenciada pelo mediador.
Facilidade de Manutenção: Alterar a comunicação entre objetos impacta apenas o mediador.


### Exemplo Prático
Cenário
Em um sistema de chat, vários usuários podem enviar e receber mensagens. Em vez de os usuários se comunicarem diretamente, o mediador (um "servidor de chat") gerencia todas as mensagens e distribui para os destinatários corretos.


### Implementação


### 1. Interface do Mediador
Define o contrato para comunicação.



    ```
    public interface IChatMediator
    {
        void RegisterUser(ChatUser user);
        void SendMessage(string message, ChatUser sender);
    }
    ```

### 2. Implementação do Mediador
Gerencia os usuários e distribui mensagens.

    ```
    public class ChatMediator : IChatMediator
    {
        private readonly List<ChatUser> _users = new List<ChatUser>();

        public void RegisterUser(ChatUser user)
        {
            _users.Add(user);
        }

        public void SendMessage(string message, ChatUser sender)
        {
            foreach (var user in _users)
            {
                if (user != sender)
                {
                    user.ReceiveMessage(message, sender);
                }
            }
        }
    }
    ```

### 3. Classe Base para os Usuários
Define o comportamento comum entre os usuários.


    ```
    public abstract class ChatUser
    {
        protected IChatMediator Mediator;
        public string Name { get; }

        protected ChatUser(IChatMediator mediator, string name)
        {
            Mediator = mediator;
            Name = name;
        }

        public abstract void SendMessage(string message);
        public abstract void ReceiveMessage(string message, ChatUser sender);
    }
    ```

### 4. Implementação dos Usuários
Cada usuário usa o mediador para enviar mensagens.

    ```
    public class User : ChatUser
    {
        public User(IChatMediator mediator, string name) : base(mediator, name) { }

        public override void SendMessage(string message)
        {
            Console.WriteLine($"{Name} sends: {message}");
            Mediator.SendMessage(message, this);
        }

        public override void ReceiveMessage(string message, ChatUser sender)
        {
            Console.WriteLine($"{Name} received from {sender.Name}: {message}");
        }
    }
    ```


### 5. Uso do Mediator

    ```
    class Program
    {
        static void Main(string[] args)
        {
            // Criar o mediador
            IChatMediator chatMediator = new ChatMediator();

            // Criar usuários e registrá-los no mediador
            var user1 = new User(chatMediator, "Alice");
            var user2 = new User(chatMediator, "Bob");
            var user3 = new User(chatMediator, "Charlie");

            chatMediator.RegisterUser(user1);
            chatMediator.RegisterUser(user2);
            chatMediator.RegisterUser(user3);

            // Enviar mensagens
            user1.SendMessage("Hello, everyone!");
            user2.SendMessage("Hi, Alice!");
            user3.SendMessage("Good morning!");
        }
    }

    ```

### Saída Esperada

    ```
    Alice sends: Hello, everyone!
    Bob received from Alice: Hello, everyone!
    Charlie received from Alice: Hello, everyone!

    Bob sends: Hi, Alice!
    Alice received from Bob: Hi, Alice!
    Charlie received from Bob: Hi, Alice!

    Charlie sends: Good morning!
    Alice received from Charlie: Good morning!
    Bob received from Charlie: Good morning!
    ```

### Vantagens do Mediator Pattern

1. Redução de Dependências:
Os objetos se comunicam através do mediador, e não diretamente entre si.

2. Centralização da Lógica:
Facilita o controle e a modificação das interações.

3. Flexibilidade:
Novos objetos podem ser adicionados sem alterar os já existentes.

### Quando evitar?
Se o mediador crescer muito em complexidade, ele pode se tornar um "ponto único de falha".
Se o número de interações for simples, o padrão pode ser um exagero.


### Resumo
O Mediator Pattern é ideal para sistemas onde muitos objetos precisam interagir entre si, mas não devem conhecer uns aos outros diretamente. Ele centraliza a comunicação, melhorando a modularidade e a escalabilidade do sistema. 🚀


