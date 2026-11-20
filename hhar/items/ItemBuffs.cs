using hhar.characters.stats;

namespace hhar.items;

public record ModifierRange(string StatName, int Min, int Max);
public record BuffDefinition(string Name, List<ModifierRange> Modifiers);
public class ItemBuffs
{
    // Relative weights. With a total of 100, each weight is also a percentage.
    private static readonly (Rarity Tier, int Weight)[] RarityWeights =
    [
        (Rarity.Common,    77),
        (Rarity.Uncommon,  10),
        (Rarity.Rare,      7),
        (Rarity.Epic,       5),
        (Rarity.Legendary,  1)
    ];

    private static readonly int TotalWeight = RarityWeights.Sum(entry => entry.Weight);

    private readonly Dictionary<Rarity, List<BuffDefinition>> _buffsByTier = new()
    {
        [Rarity.Common] =
        [
            new("Reinforced", [new("Defense", 1, 2)]),
            new("Lucky",      [new("Luck", 1, 2)]),
            new("Aggressive", [new("AttackDamage", 2, 3), new("Defense", -2, -1)])
        ],

        [Rarity.Uncommon] =
        [
            new("Reliable",      [new("Defense", 2, 3), new("MaxStamina", 1, 2)]),
            new("Sharp-Tongued", [new("Charisma", 2, 4), new("Hostility", 1, 2)]),
            new("Heavy-Duty",    [new("AttackDamage", 3, 5), new("MaxStamina", -2, -1)])
        ],

        [Rarity.Rare] =
        [
            new("Night-Shift",   [new("MaxStamina", 3, 5), new("Wisdom", 2, 3)]),
            new("Union-Backed",  [new("Defense", 3, 5), new("Charisma", 2, 3)]),
            new("Understaffed",  [new("AttackDamage", 5, 7), new("Defense", -3, -2)])
        ],

        [Rarity.Epic] =
        [
            new("Battle-Tested", [new("AttackDamage", 4, 6), new("Defense", 3, 5)]),
            new("Untouchable",   [new("Defense", 5, 7), new("Luck", 3, 5)]),
            new("Overclocked",   [new("AttackDamage", 7, 9), new("MaxHealth", -4, -2)])
        ],

        [Rarity.Legendary] =
        [
            new("Employee of the Month",
                [new("Charisma", 6, 9), new("Luck", 5, 7), new("Hostility", 2, 4)]),
            new("Last One Standing",
                [new("MaxHealth", 7, 10), new("Defense", 5, 7)]),
            new("Contractually Immortal",
                [new("MaxHealth", 12, 16), new("MaxStamina", -4, -2)])
        ]
    };

    public static Rarity RollRarity(Random random)
    {
        int roll = random.Next(TotalWeight);

        foreach (var (tier, weight) in RarityWeights)
        {
            if (roll < weight)
                return tier;

            roll -= weight;
        }

        throw new InvalidOperationException("Invalid rarity weights.");
    }

    public (Rarity Tier, string BuffName, List<StatModifier> Modifiers) RollBuff(
        string source, Random random)
    {
        var tier = RollRarity(random);
        var pool = _buffsByTier[tier];
        var buff = pool[random.Next(pool.Count)];
        var modifiers = new List<StatModifier>();

        foreach (var range in buff.Modifiers)
        {
            int value = random.Next(range.Min, range.Max + 1);
            modifiers.Add(new StatModifier(buff.Name, range.StatName, value, source));
        }

        return (tier, buff.Name, modifiers);
    }
}