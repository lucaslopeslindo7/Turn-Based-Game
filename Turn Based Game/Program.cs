bool combatWon = false;

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

    public Hero(int health, string name, int damage, int maxHealth)
    {
        Health = health;
        Name = name;
        Damage = damage;
        MaxHealth = maxHealth;
    }
}


