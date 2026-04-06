using System;

bool combatWon = false;

IEntity newHero = new Hero("John", 20, 100);
IEntity newEnemy = new Enemy("Evil Guy", 10, 50);

int healingCapability = 10;

while (newHero.Health > 0 && newEnemy.Health > 0)
{
    Console.WriteLine("== Player's Turn ==");
    Console.WriteLine("Enter 'attack' to attack, enter 'heal' to heal. Your current healing capability is: {0}",healingCapability);


}

public interface IEntity
{
    int Health { get; set; }
    string Name { get; set; }

    int Damage { get; }

    int MaxHealth { get; }

    void Attack(IEntity enemy);

    void Heal(int healAmount);


}

public class Hero : IEntity
{
    public int Health { get; set; }
    public string Name { get; set; } = string.Empty;

    public int Damage { get; private set; }

    public int MaxHealth { get; private set; }

    public void Attack(IEntity enemy)
    {
        enemy.Health = Math.Clamp(enemy.Health - Damage, 0, enemy.MaxHealth);
    }

    public void Heal(int healAmount)
    {
        Health = Math.Clamp(Health + healAmount, 0, MaxHealth);
    }

    public Hero(string name, int damage, int maxHealth)
    {
        Name = name;
        Damage = damage;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }
}

public class Enemy : IEntity
{
    public int Health { get; set; } = 0;
    public string Name { get; set; } = string.Empty;

    public int Damage { get; private set; }

    public int MaxHealth { get; private set; }

    public void Attack(IEntity enemy)
    {
        enemy.Health = Math.Clamp(enemy.Health - Damage, 0, enemy.MaxHealth);
    }

    public void Heal(int healAmount)
    {
        Health = Math.Clamp(Health + healAmount, 0, MaxHealth);
    }

    public Enemy(string name, int damage, int maxHealth)
    {
        Name = name;
        Damage = damage;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }
}


