namespace DesignPatterns.SOLID._3_L;


//L - Liskov Substitution Principle(Princípio da Substituição de Liskov)
//Definição: Uma classe derivada deve ser substituível por sua classe base sem alterar o comportamento esperado.

//Exemplo:

// Errado: Violação da substituição
public class Errado_Bird
{
    public virtual void Fly() { }
}

public class Errado_Penguin : Errado_Bird
{
    public override void Fly()
    {
        throw new NotSupportedException("Pinguins não voam!");
    }
}

// Certo: Usar uma hierarquia mais precisa
public abstract class Certo_Bird { }

public class Certo_FlyingBird : Certo_Bird
{
    public void Fly() { }
}

public class Certo_Penguin : Certo_Bird
{
    // Pinguins não têm o método Fly
}

//Explicação: No exemplo correto, evitamos cenários onde subclasses não respeitam o comportamento esperado da classe base.