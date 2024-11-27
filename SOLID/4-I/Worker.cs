namespace DesignPatterns.SOLID._4_I;

//I - Interface Segregation Principle(Princípio da Segregação de Interfaces)
//Definição: Uma classe não deve ser forçada a implementar interfaces que não usa.

//Exemplo:

// Errado: Uma interface grande
public interface Errado_IWorker
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

//Explicação: No exemplo correto, cada classe implementa apenas os métodos que realmente utiliza.