using System;
using System.IO;
using hhar.characters.stats;
using hhar.characters.stats.classes;

namespace hhar.characters
{
    public enum StartingGift
    {
        LastRation,
        UnpaidGartitude,
        DyingEmber,
        FalseReference,
        LastBreak,
        InheritedHardship,
        ForbiddenRecipe,
        MemoryOfHome
    }

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
        StartingGift StartingGift,
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

            StartingGift gift =
                ReadChoice<StartingGift>("Choose your starting gift:");

            Difficulty difficulty =
                ReadChoice<Difficulty>("Choose your difficulty:");

            Faction faction =
                ReadChoice<Faction>("Choose your faction:");

            Player player = new Player(name);
            new AssignClass(startingClass, player.Stats);

            ApplyGift(player, gift);

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

private static void ApplyGift(Player player, StartingGift gift)
{
    StatModifier[] modifiers = gift switch
    {
        StartingGift.LastRation =>
        [
            new("Well Fed", "MaxHealth", 5, "Last Ration"),
            new("Reserve Strength", "MaxStamina", 2, "Last Ration")
        ],

        StartingGift.UnpaidGartitude =>
        [
            new("Motivated", "AttackDamage", 3, "Unpaid Gratitude"),
            new("Driven", "MaxStamina", 2, "Unpaid Gratitude")
        ],
        
        StartingGift.DyingEmber =>
        [
            new("Burning Spirit", "AttackDamage", 5, "Dying Ember"),
            new("Flickering Flame", "MaxHealth", -2, "Dying Ember")
        ],


        StartingGift.FalseReference =>
        [
            new("Deceptive", "Charisma", 4, "False Reference"),
            new("Cunning", "Intelligence", 3, "False Reference")
        ],

        StartingGift.LastBreak =>
        [
            new("Resilient", "Defense", 3, "Last Break"),
            new("Unyielding", "MaxHealth", 2, "Last Break")
        ],

        StartingGift.InheritedHardship =>
        [
            new("Hardened", "Defense", 3, "Inherited Hardship"),
            new("Enduring", "MaxHealth", 3, "Inherited Hardship")
        ],

        StartingGift.ForbiddenRecipe =>
        [
            new("Culinary Genius", "Intelligence", 5, "Forbidden Recipe"),
            new("Secret Knowledge", "Wisdom", 3, "Forbidden Recipe")
        ],

        StartingGift.MemoryOfHome =>
        [
            new("Nostalgic", "Charisma", 3, "Memory of Home"),
            new("Comforted", "MaxHealth", 2, "Memory of Home")
        ],

        _ => throw new ArgumentOutOfRangeException(nameof(gift))
    };

    foreach (StatModifier modifier in modifiers)
    {
        player.Stats.AddModifier(modifier);
    }
}
    }
}