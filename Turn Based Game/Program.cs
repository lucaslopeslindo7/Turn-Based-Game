namespace Turn_Based_Game
{
    public interface IEntity
    {
        int Health { get; set; }
        string Name { get; set; }

        int Damage { get; }

        int MaxHealth { get; }

        void Attack(IEntity enemy);

        void Heal(int healAmount);


    }

    public class Utility
    {
        public static void WriteColoredText(string text, ConsoleColor color)
        {
            ConsoleColor originalColor = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine(text);
            Console.ForegroundColor = originalColor;
        }
    }
    public class Entity(string name, int damage, int maxHealth) : IEntity
    {
        public int Health { get; set; } = maxHealth;
        public string Name { get; set; } = name;

        public int Damage { get; set; } = damage;

        public int MaxHealth { get; private set; } = maxHealth;

        public void Attack(IEntity enemy)
        {
            int dealtDamage = Damage;

            Random randomCritChance = new();
            Random randomDamageVariety = new();
            Random randomMultiplier = new();

            ConsoleColor originalColor = Console.ForegroundColor;

            dealtDamage += randomDamageVariety.Next(-10, 11);

            int randomRoll = randomCritChance.Next(1, 6);

            if (randomRoll == 1)
            {

                Utility.WriteColoredText("A Critical Hit!", ConsoleColor.Red);

                dealtDamage *= randomMultiplier.Next(2,5);
            }

            enemy.Health = Math.Clamp(enemy.Health - dealtDamage, 0, enemy.MaxHealth);

            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine("{0} Attacks {1} with great precision! {2} damage is dealt!", Name, enemy.Name, dealtDamage);
            Console.ForegroundColor = originalColor;

        }

        public void Heal(int healAmount)
        {
            int healingDone = healAmount;

            ConsoleColor originalColor = Console.ForegroundColor;

            Random randomCritChance = new();
            Random randomMultiplier = new();

            int randomRoll = randomCritChance.Next(1, 6);

            if (randomRoll == 1)
            {
                Utility.WriteColoredText("A Critical Heal!", ConsoleColor.Green);

                healingDone *= randomMultiplier.Next(3, 7);
            }

            Health = Math.Clamp(Health + healingDone, 0, MaxHealth);

            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("{0} takes some time to mend their wounds. Healed {1} health!", Name, healingDone);
            Console.ForegroundColor = originalColor;
        }
    }

    class Program
    {


        static void Main()
        {
            int healingCapability = 10;
            int enemyHealingCapability = 10;

            bool enemyHealedLastTurn = false;
            bool enemyFocusedLastTurn = false;

            Entity newHero = new("the2", 20, 400);
            Entity newEnemy = new("Twinedhealer89", 5, 10000);

            ConsoleColor originalColor = Console.ForegroundColor;

            while (newHero.Health > 0 && newEnemy.Health > 0)
            {
                Console.WriteLine("\n== Player's Turn ==");

                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("Enter 'attack' to attack, enter 'heal' to heal, enter 'focus' to increase your damage. Your current healing capability is: {0}", healingCapability);
                Console.ForegroundColor = originalColor;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("{0} (You)'s Health: {1}/{2}",newHero.Name, newHero.Health, newHero.MaxHealth);
                Console.WriteLine("{0} (Enemy)'s Health: {1}/{2}", newEnemy.Name, newEnemy.Health, newEnemy.MaxHealth);
                Console.ForegroundColor = originalColor;

                string? input = Console.ReadLine();

                Console.WriteLine("\n");

                if (input == "attack" || (input != "attack" && input != "heal" && input != "focus"))
                {
                    Random randomHealingCapabilityChange = new();
                    int randomHealingAddon = randomHealingCapabilityChange.Next(2, 7);
                    healingCapability += randomHealingAddon;

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("{0}'s Healing Capability has increased by {1}",newHero.Name,randomHealingAddon);
                    Console.ForegroundColor = originalColor;

                    newHero.Attack(newEnemy);

                    if (newEnemy.Health == 0)
                    {
                        Utility.WriteColoredText("You Win!", ConsoleColor.Cyan);
                        break;
                    }
                }

                if (input == "focus")
                {
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("{0} Focuses on their strength. Damage increased by 10!", newHero.Name);
                    Console.ForegroundColor = originalColor;
                    newHero.Damage += 10;
                }

                if (input == "heal")
                {
                    newHero.Heal(healingCapability);

                    Random randomHealingCapabilityChange = new();
                    int randomHealingAddon = randomHealingCapabilityChange.Next(2, 7);
                    healingCapability -= randomHealingAddon;

                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("{0}'s Healing Capability has decreased by {1}", newHero.Name, randomHealingAddon);
                    Console.ForegroundColor = originalColor;
                }

                Console.WriteLine("\n== Enemy's Turn ==");

                if (newHero.Health < newEnemy.Health && enemyFocusedLastTurn != true)
                {
                    enemyFocusedLastTurn = true;
                    Console.ForegroundColor = ConsoleColor.DarkMagenta;
                    Console.WriteLine("{0} Focuses on their strength. Damage increased by 10!", newEnemy.Name);
                    Console.ForegroundColor = originalColor;
                    newEnemy.Damage += 1;
                    continue;
                }

                if (newEnemy.Health < newEnemy.MaxHealth / 2 && enemyHealedLastTurn != true)
                {

                    enemyHealedLastTurn = true;
                    enemyFocusedLastTurn = false;
                    newEnemy.Heal(enemyHealingCapability);

                    Random randomHealingCapabilityChange = new();
                    int randomHealingAddon = randomHealingCapabilityChange.Next(2, 7);
                    enemyHealingCapability -= randomHealingAddon;

                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.WriteLine("{0}'s Healing Capability has decreased by {1}", newEnemy.Name, randomHealingAddon);
                    Console.ForegroundColor = originalColor;
                }
                else
                {
                    Random randomHealingCapabilityChange = new();
                    int randomHealingAddon = randomHealingCapabilityChange.Next(2, 7);
                    enemyHealingCapability += randomHealingAddon;

                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("{0}'s Healing Capability has increased by {1}", newEnemy.Name, randomHealingAddon);
                    Console.ForegroundColor = originalColor;

                    enemyHealedLastTurn = false;
                    enemyFocusedLastTurn = false;
                    newEnemy.Attack(newHero);

                    if (newHero.Health == 0)
                    {
                        Utility.WriteColoredText("You Lose!", ConsoleColor.Red);
                        break;
                    }
                }
            }
        }
    }
}
