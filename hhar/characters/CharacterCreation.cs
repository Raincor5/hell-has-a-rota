using System;
using System.IO;
using hhar.characters.stats;
using hhar.characters.stats.classes;
using hhar.characters.stats.gifts;

namespace hhar.characters
{

    public enum Difficulty
    {
        Pub,
        Restaurant,
        Catering,
        FineDining
    }

    public enum Faction
    {
        Unaffiliated,

    }

    public record CreatedCharacter(
        Player Player,
        ClassList StartingClass,
        GiftList StartingGift,
        Difficulty Difficulty,
        Faction Faction);

    public static class CharacterCreation
    {
        // <summary>
        // This class is responsible for creating a new character and initializing its properties.
        // On the start of the game, the player will be prompted to create a new character, and this class will handle that process.
        // Prompts: 
        // 1. Character Name
        // 2. Character Class (Defines the starting stats)
        // 3. Starting Gift (e.g., a special item or ability)
        // 4. Starting Faction (e.g., a group or organization the character belongs to)
        // 5. Starting Difficulty (e.g., easy, medium, hard)
        // </summary>
        public static CreatedCharacter Create()
        {
            Console.WriteLine("PREPARE TO CLOCK IN");

            string name = ReadName();

            ClassList startingClass =
                ReadChoice<ClassList>("Choose your starting class:");

            GiftList gift =
                ReadChoice<GiftList>("Choose your starting gift:");

            Difficulty difficulty =
                ReadChoice<Difficulty>("Choose your difficulty:");

            Faction faction =
                ReadChoice<Faction>("Choose your faction:");

            Player player = new Player(name);
            new AssignClass(startingClass, player.Stats);

            new AssignGift(gift, player.Stats);

            player.Stats.CalculateModifiers();
            player.Stats.CurrentHealth = player.Stats.MaxHealth;
            player.Stats.CurrentStamina = player.Stats.MaxStamina;

            return new CreatedCharacter(
                player,
                startingClass,
                gift,
                difficulty,
                faction);
        }

        private static string ReadName()
        {
            while (true)
            {
                Console.Write("\nEnter your name: ");
                string name = ReadInput().Trim();

                if (name.Length > 0)
                    return name;

                Console.WriteLine("Your name cannot be empty.");
            }
        }

        private static T ReadChoice<T>(string prompt)
            where T : struct, Enum
        {
            T[] options = Enum.GetValues<T>();

            Console.WriteLine($"\n{prompt}");

            for (int i = 0; i < options.Length; i++)
            {
                Console.WriteLine($"{i + 1}. {options[i]}");
            }

            while (true)
            {
                Console.Write("> ");

                if (int.TryParse(ReadInput(), out int choice) &&
                    choice >= 1 &&
                    choice <= options.Length)
                {
                    return options[choice - 1];
                }

                Console.WriteLine(
                    $"Enter a number from 1 to {options.Length}.");
            }
        }

        private static string ReadInput()
        {
            return Console.ReadLine()
                ?? throw new EndOfStreamException(
                    "Console input was closed.");
        }

    }
}