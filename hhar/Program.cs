using System;
using hhar.characters;

CreatedCharacter character = CharacterCreation.Create();
Player player = character.Player;

Console.WriteLine($"\nWelcome, {player.Name}.");
Console.WriteLine($"Class: {character.StartingClass}");
Console.WriteLine($"Gift: {character.StartingGift}");
Console.WriteLine($"Difficulty: {character.Difficulty}");
Console.WriteLine($"Faction: {character.Faction}");
Console.WriteLine(player.Stats.GetStatSummary());