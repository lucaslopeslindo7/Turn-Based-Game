using System;

Entity newHero = new Entity("John", 20, 400);
Entity newEnemy = new Entity("Evil Guy", 20, 400);

int healingCapability = 10;
int enemyHealingCapability = 10;
bool enemyHealedLastTurn = false;

while (newHero.Health > 0 && newEnemy.Health > 0)
{
    Console.WriteLine("\n== Player's Turn ==");
    Console.WriteLine("Enter 'attack' to attack, enter 'heal' to heal. Your current healing capability is: {0}",healingCapability);
    Console.WriteLine("Current Health: {0}/{1}",newHero.Health,newHero.MaxHealth);
    Console.WriteLine("Enemy Health: {0}/{1}", newEnemy.Health,newEnemy.MaxHealth);

    string input = Console.ReadLine();

    Console.WriteLine("\n");

    if (input == "attack" || (input != "attack" && input != "heal"))
    {
        healingCapability += 2;
        newHero.Attack(newEnemy);

        if (newEnemy.Health == 0)
        {
            Console.WriteLine("You win!");
            break;
        }
    }

    if (input == "heal")
    {
        newHero.Heal(healingCapability);
        healingCapability -= 2;
    }

    Console.WriteLine("\n== Enemy's Turn ==");

    if (newEnemy.Health < newEnemy.MaxHealth / 2 && enemyHealedLastTurn != true)
    {
        enemyHealingCapability -= 2;
        enemyHealedLastTurn = true;
        newEnemy.Heal(enemyHealingCapability);
    } else
    {
        enemyHealingCapability += 2;
        enemyHealedLastTurn = false;
        newEnemy.Attack(newHero);

        if (newHero.Health == 0)
        {
            Console.WriteLine("You lose!");
            break;
        }
    }


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

public class Entity : IEntity
{
    public int Health { get; set; }
    public string Name { get; set; } = string.Empty;

    public int Damage { get; private set; }

    public int MaxHealth { get; private set; }

    public void Attack(IEntity enemy)
    {
        int dealtDamage = Damage;

        Random randomCritChance = new();

        int randomRoll = randomCritChance.Next(1, 6);

        if (randomRoll == 1)
        {
            Console.WriteLine("A critical hit!");
            dealtDamage *= 2;
        }

        enemy.Health = Math.Clamp(enemy.Health - dealtDamage, 0, enemy.MaxHealth);
        Console.WriteLine("{0} Attacks {1} with great precision! {2} damage is dealt!", Name, enemy.Name, dealtDamage);

    }

    public void Heal(int healAmount)
    {
        int healingDone = healAmount;

        Random randomCritChance = new();
        Random randomMultiplier = new();

        int randomRoll = randomCritChance.Next(1, 6);

        if (randomRoll == 1)
        {
            Console.WriteLine("A critical heal!");
            healingDone *= randomMultiplier.Next(3, 7);
        }

        Health = Math.Clamp(Health + healAmount, 0, MaxHealth);
        Console.WriteLine("{0} takes some time to mend their wounds. Healed {1} health!", Name, healAmount);
    }

    public Entity(string name, int damage, int maxHealth)
    {
        Name = name;
        Damage = damage;
        MaxHealth = maxHealth;
        Health = maxHealth;
    }
}


